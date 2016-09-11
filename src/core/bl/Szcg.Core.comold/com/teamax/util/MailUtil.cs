using System;
using System.Net.Mail;
using System.IO;
//using System.Web.Mail;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// 邮件类 author:shenglianjun
	/// </summary>
	public class MailUtil
	{
		
		/// <summary>
		/// 发送邮件
		/// </summary>
		/// <param name="from">发送人</param>
		/// <param name="to">接收人</param>
		/// <param name="subject">主题</param>
		/// <param name="body">内容</param>
		/// <param name="files">附件数组</param>
		public static bool sendMail(string from,string to,string subject,string body,string[] files)
		{
//			MailMessage message=new MailMessage();
//			message.From="slj@163.com";
//			message.To="slj@163.com";
//			message.Subject="sfsf";
//			message.Priority=MailPriority.High;
//			message.Body="sfsdfsdf";
//			//添加附件
//			message.Attachments.Add(new MailAttachment("g:/1.txt"));
			try
            {
                MailAddress frommail = new MailAddress(from);//发人地址
                MailAddress tomail = new MailAddress(to);//收件人地址
                //SmtpClient.Send(message);
                string mailServerName = "smtphost";
                using(MailMessage message = new MailMessage(frommail, tomail))
                {
                    message.Subject = subject;
                    message.Body = body;
                    if (files != null && files.Length > 0)
                    {
                        for (int i = 0; i < files.Length; i++)
                        {
                            FileInfo fileInfo = new FileInfo(files[i]);
                            if (fileInfo.Exists)
                            {
                                message.Attachments.Add(new Attachment(files[i]));
                                //message.Attachments.Add(new MailAttachment(files[i]));
                            }
                        }
                    }

                    // SmtpClient is used to send the e-mail
                    SmtpClient mailClient = new SmtpClient(mailServerName);

                    // UseDefaultCredentials tells the mail client to use the 
                    // Windows credentials of the account (i.e. user account) 
                    // being used to run the application
                    mailClient.UseDefaultCredentials = true;

                    // Send delivers the message to the mail server
                    mailClient.Send(message);
                }
                return true;
            }
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return false;
			}
			
		}
	}
}
