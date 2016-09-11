using System;
using System.Net.Mail;
using System.IO;
//using System.Web.Mail;

namespace szcg.com.teamax.util
{
	/// <summary>
	/// �ʼ��� author:shenglianjun
	/// </summary>
	public class MailUtil
	{
		
		/// <summary>
		/// �����ʼ�
		/// </summary>
		/// <param name="from">������</param>
		/// <param name="to">������</param>
		/// <param name="subject">����</param>
		/// <param name="body">����</param>
		/// <param name="files">��������</param>
		public static bool sendMail(string from,string to,string subject,string body,string[] files)
		{
//			MailMessage message=new MailMessage();
//			message.From="slj@163.com";
//			message.To="slj@163.com";
//			message.Subject="sfsf";
//			message.Priority=MailPriority.High;
//			message.Body="sfsdfsdf";
//			//��Ӹ���
//			message.Attachments.Add(new MailAttachment("g:/1.txt"));
			try
            {
                MailAddress frommail = new MailAddress(from);//���˵�ַ
                MailAddress tomail = new MailAddress(to);//�ռ��˵�ַ
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
