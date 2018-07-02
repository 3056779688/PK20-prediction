using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 拯救小姐姐
{
    public partial class MainWindow : Window
    {
        public void sendmessage(string msg)
        {
#if false
            System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
            
            {
                mail.To = "3056779688@qq.com";
                mail.From = "3056779688@qq.com";
                mail.Subject = "secret";
                mail.BodyFormat = System.Web.Mail.MailFormat.Html;
                mail.Body = message;

                mail.Fields.Add("https://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                mail.Fields.Add("https://schemas.microsoft.com/cdo/configuration/sendusername", mail.From);
                mail.Fields.Add("https://schemas.microsoft.com/cdo/configuration/sendpassword", "bamqurrgcelrdfch");
                mail.Fields.Add("https://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);
                mail.Fields.Add("https://schemas.microsoft.com/cdo/configuration/smtpisessl", "true");

                System.Web.Mail.SmtpMail.SmtpServer = "smtp.qq.com";
                System.Web.Mail.SmtpMail.Send(mail);
            }
#endif
            SmtpClient client = new SmtpClient("smtp.qq.com");

            //SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("3056779688@qq.com", "mqtiywvooiuvddcg");

            MailAddress from = new MailAddress("3056779688@qq.com", "handsome guy", Encoding.UTF8);//初始化发件人  

            MailAddress to = new MailAddress("3056779688@qq.com", "", Encoding.UTF8);//初始化收件人  

            //设置邮件内容  
            MailMessage message = new MailMessage(from, to);
            // message.From = new MailAddress("123456789@qq.com", "", Encoding.UTF8);
            message.Priority = MailPriority.High;
         
            message.Body = msg;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "test";
            message.SubjectEncoding = Encoding.UTF8;

            //string htmlBody = "<html><body><h1>"+msg+"</h1><br><img src=\"cid:Pic1\"></body></html>";
            //AlternateView avHtml = AlternateView.CreateAlternateViewFromString
            //  (htmlBody, null, MediaTypeNames.Text.Html);
            //// Create a LinkedResource object for each embedded image
            //LinkedResource pic1 = new LinkedResource("1.jpg", MediaTypeNames.Image.Jpeg);
            //pic1.ContentId = "Pic1";
            //avHtml.LinkedResources.Add(pic1);
            //// Create an alternate view for unsupported clients
            //string textBody = "You must use an e-mail client that supports HTML messages";
            //AlternateView avText = AlternateView.CreateAlternateViewFromString
            //  (textBody, null, MediaTypeNames.Text.Plain);
            //message.AlternateViews.Add(avHtml);
            //message.AlternateViews.Add(avText);

            //发送邮件  

            {
                client.Send(message);
            }
            
        }
    }
}
