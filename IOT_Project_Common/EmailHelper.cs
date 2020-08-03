using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace IOT_Project_Common
{
   public class EmailHelper
    {
        #region  有授权码
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="FromMial">发件人邮箱</param>
        /// <param name="ToMial">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="AuthorizationCode">发件人授权码</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">//邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="File_Path">附件的地址</param>
        //public void SendMail(string FromMial, string ToMial, string AuthorizationCode, string ReplyTo, string CCMial, string File_Path)
        //{
        //    try
        //    {
        //        //实例化一个发送邮件类。
        //        MailMessage mailMessage = new MailMessage();

        //        //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
        //        mailMessage.Priority = MailPriority.Normal;

        //        //发件人邮箱地址。
        //        mailMessage.From = new MailAddress(FromMial);

        //        //收件人邮箱地址。需要群发就写多个
        //        //拆分邮箱地址
        //        List<string> ToMiallist = ToMial.Split(';').ToList();
        //        for (int i = 0; i < ToMiallist.Count; i++)
        //        {
        //            mailMessage.To.Add(new MailAddress(ToMiallist[i]));  //收件人邮箱地址。
        //        }

        //        if (ReplyTo == "" || ReplyTo == null)
        //        {
        //            ReplyTo = FromMial;
        //        }

        //        //对方回复邮件时默认的接收地址(不设置也是可以的哟)
        //        mailMessage.ReplyTo = new MailAddress(ReplyTo);

        //        if (CCMial != "" && CCMial != null)
        //        {
        //            List<string> CCMiallist = ToMial.Split(';').ToList();
        //            for (int i = 0; i < CCMiallist.Count; i++)
        //            {
        //                //邮件的抄送者，支持群发
        //                mailMessage.CC.Add(new MailAddress(CCMial));
        //            }
        //        }
        //        //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
        //        mailMessage.SubjectEncoding = Encoding.GetEncoding(936);

        //        //邮件正文是否是HTML格式
        //        mailMessage.IsBodyHtml = false;

        //        //邮件标题。
        //        mailMessage.Subject = "发送邮件测试";
        //        //邮件内容。
        //        mailMessage.Body = "测试群发邮件,以及附件邮件！.....";

        //        //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
        //        if (File_Path != "" && File_Path != null)
        //        {
        //            //将附件添加到邮件
        //            mailMessage.Attachments.Add(new Attachment(File_Path));
        //            //获取或设置此电子邮件的发送通知。
        //            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        //        }

        //        //实例化一个SmtpClient类。
        //        SmtpClient client = new SmtpClient();

        //        #region 设置邮件服务器地址

        //        //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
        //        // client.Host = "smtp.163.com";
        //        if (FromMial.Length != 0)
        //        {
        //            //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
        //            string[] addressor = FromMial.Trim().Split(new Char[] { '@', '.' });
        //            switch (addressor[1])
        //            {
        //                case "163":
        //                    client.Host = "smtp.163.com";
        //                    break;
        //                case "126":
        //                    client.Host = "smtp.126.com";
        //                    break;
        //                case "qq":
        //                    client.Host = "smtp.qq.com";
        //                    break;
        //                case "gmail":
        //                    client.Host = "smtp.gmail.com";
        //                    break;
        //                case "hotmail":
        //                    client.Host = "smtp.live.com";//outlook邮箱
        //                    //client.Port = 587;
        //                    break;
        //                case "foxmail":
        //                    client.Host = "smtp.foxmail.com";
        //                    break;
        //                case "sina":
        //                    client.Host = "smtp.sina.com.cn";
        //                    break;
        //                default:
        //                    client.Host = "smtp.exmail.qq.com";//qq企业邮箱
        //                    break;
        //            }
        //        }


        //        //使用安全加密连接。
        //        client.EnableSsl = true;
        //        //不和请求一块发送。
        //        client.UseDefaultCredentials = false;

        //        //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
        //        client.Credentials = new NetworkCredential(FromMial, AuthorizationCode);

        //        //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
        //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //        //发送
        //        client.Send(mailMessage);
        //        Console.WriteLine("发送成功");
        //    }
        //    catch (Exception)
        //    {

        //        Console.WriteLine("发送失败");
        //    }
        //}
        #endregion


        //public class EmailHelper
        //{
            private MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
            private SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
            private int mSenderPort = 25;   //发送邮件所用的端口号（htmp协议默认为25）
            private string mSenderServerHost = "smtp.exmail.qq.com";    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
            private string mSenderPassword = "2424940988ly";    //发件箱的密码
            private string mSenderUsername = "柒 う";   //发件箱的用户名
            private bool mEnableSsl = false;    //是否对邮件内容进行socket层加密传输
            private bool mEnablePwdAuthentication = true;  //是否对发件人邮箱进行密码验证
            private string fromMail = "柒 う";  //发件箱的地址(与用户名一致)


            /// <summary>
            /// 发送邮件
            /// </summary>
            /// <param name="Receive">接收人邮箱</param>
            /// <param name="subject">标题</param>
            /// <param name="emailBody">邮件内容</param>
            /// <param name="cc">抄送人邮箱</param>
            public EmailHelper(List<string> Receive, string subject, string emailBody, List<string> cc)
            {

                mMailMessage = new MailMessage();
                foreach (string item in Receive)
                {
                    mMailMessage.To.Add(item);
                }
                mMailMessage.From = new MailAddress(fromMail);
                mMailMessage.Subject = subject;
                mMailMessage.Body = emailBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;
                foreach (string item in cc)
                {
                    mMailMessage.To.Add(item);
                }

            }

            ///<summary>
            /// 添加附件
            ///</summary>
            ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
            public void AddAttachments(string attachmentsPath)
            {

                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(data);
                }

            }

            ///<summary>
            /// 邮件的发送
            ///</summary>
            public void Send()
            {

                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = this.mEnableSsl;
                    if (this.mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);
                    Console.WriteLine("发送成功");
                }

            }
        //}



        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="To">收件地址</param>
        /// <param name="Name">收件人</param>
        /// <param name="Subject">标题</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        //public static bool SendEmail(string To, string Name, string Subject, string Content)
        //    {
        //        SmtpClient client = new SmtpClient();

        //        //获取或设置用于验证发件人身份的凭据。
        //        client.Credentials = new System.Net.NetworkCredential("邮箱地址", "密码");
        //        client.Port = 25; //端口号
        //                          //获取或设置用于 SMTP 事务的主机的名称或 IP 地址（这里是qq的,163邮箱的是smtp.163.com）
        //        client.Host = "smtp.qq.com";
        //        try
        //        {
        //            client.Send(InitMailMessage(To, Name, Subject, Content));
        //            return true;
        //        }
        //        catch (System.Net.Mail.SmtpException ex)
        //        {
        //            return false;
        //        }
        //    }

        //    /// <summary>
        //    /// 初始化信件相关信息
        //    /// </summary>
        //    /// <param name="To">收件地址</param>
        //    /// <param name="name">收件人</param>
        //    /// <param name="Subject">标题</param>
        //    /// <param name="sendEmailContent">内容</param>
        //    /// <returns></returns>
        //    private static MailMessage InitMailMessage(string To, string name, string Subject, string sendEmailContent)
        //    {
        //        MailMessage mail = new MailMessage();
        //        //发件人
        //        mail.From = new MailAddress("邮箱地址", "设置个名称");

        //        //收件人
        //        if (name != "")
        //        {
        //            MailAddress mailAdd = new MailAddress(To, name);
        //            mail.To.Add(mailAdd);
        //        }
        //        else
        //        {
        //            mail.To.Add(To);
        //        }
        //        //主题
        //        mail.Subject = Subject;

        //        //内容
        //        mail.Body = sendEmailContent;

        //        //邮件主题和正文的编码格式
        //        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;

        //        //邮件正文允许html编码
        //        mail.IsBodyHtml = true;
        //        //优先级
        //        mail.Priority = MailPriority.Normal;

        //        //密送——就是将信密秘抄送给收件人以外的人，所有收件人看不到密件抄送的地址
        //        //mail.Bcc.Add("");


        //        //抄送——就是将信抄送给收件人以外的人,所有的收件人可以在抄送地址处看到此信还抄送给谁
        //        mail.CC.Add("抄送人地址");

        //        mail.Attachments.Add(new Attachment(@"G:\2018\vue-devtools.zip"));     //添加附件

        //        return mail;

        //    }
        
    }
}
