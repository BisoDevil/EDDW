using EDDW.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.MailHub
{
    public class MailSendingHub
    {
        public void SendToSpeaker(Speaker speaker)
        {
            string body = GetMailBody(speaker.Fullname, speaker.Username, "Without");

            SendEmail(speaker.Email, body);
        }

        public void SendToEmployee(Employee employee)
        {
            string body = GetMailBody(employee.Fullname, employee.Username, "Without");
            SendEmail(employee.Email, body);
        }

        public void SendToGuest(Guest guest)
        {
            string body = GetMailBody(guest.Fullname, guest.Username, guest.Password);
            SendEmail(guest.Email, body);
        }




        private async void SendEmail(string email, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EDDW", "no-reply@innovationcodes.com"));
            message.To.Add(new MailboxAddress(email));
            message.Subject = "Account Credentials";
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Connect("mail.innovationcodes.com", 2525);

                ////Note: only needed if the SMTP server requires authentication
                client.Authenticate("no-reply@innovationcodes.com", "P@ssw0rd");

                await client.SendAsync(message);

                client.Disconnect(true);
            }
        }


        private string GetMailBody(string Name, string UserName, string Password)
        {
            return

                "<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">" +
                "<tbody>" +
                "<tr>" +
                "<td align=\"center\" bgcolor=\"#e9ecef\">&nbsp;</td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"center\" bgcolor=\"#e9ecef\">" +
                "<table style=\"max-width: 600px;\" border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">" +
                "<tbody>" +
                "<tr>" +
                "<td style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\" align=\"left\" bgcolor=\"#ffffff\">" +
                $"<h1 style=\"margin: 0; font-size: 32px; font-weight: bold; letter-spacing: -1px; line-height: 48px;\">Dear&nbsp;{Name},</h1>" +
                "</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td align=\"center\" bgcolor=\"#e9ecef\">" +
                "<table style=\"max-width: 600px;\" border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">" +
                "<tbody>" +
                "<tr style=\"height: 96.4px;\">" +
                "<td style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; height: 96.4px;\" align=\"left\" bgcolor=\"#ffffff\">" +
                "<p>A new account has been created with the following&nbsp;informations:</p>" +
                "</td>" +
                "</tr>" +
                "<tr style=\"height: 82px;\">" +
                "<td style=\"height: 82px;\" align=\"left\" bgcolor=\"#ffffff\">" +
                "<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">" +
                "<tbody>" +
                "<tr>" +
                "<td style=\"padding: 12px;\" align=\"center\" bgcolor=\"#ffffff\">Badge number</td>" +
                $"<td style=\"padding: 12px;\" align=\"center\" bgcolor=\"#ffffff\">{UserName}</td>" +
                "</tr>" +
                "<tr>" +
                $"<td style=\"padding: 12px;\" align=\"center\" bgcolor=\"#ffffff\">Password</td>" +
                $"<td style=\"padding: 12px;\" align=\"center\" bgcolor=\"#ffffff\">{Password}</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "</tr>" +
                "<tr style=\"height: 96px;\">" +
                "<td style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; height: 96px;\" align=\"left\" bgcolor=\"#ffffff\">" +
                "<p style=\"margin: 0;\">You can download our app from</p>" +
                "<p style=\"margin: 0;\">AndroidLink,iPhoneLink</p>" +
                "<p style=\"margin: 0;\">If that doesn't work, Send us an email :</p>" +
                "<p style=\"margin: 0;\">mail@innovationcodes.com</p>" +
                "</td>" +
                "</tr>" +
                "<tr style=\"height: 97px;\">" +
                "<td style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf; height: 97px;\" align=\"left\" bgcolor=\"#ffffff\">" +
                "<p style=\"margin: 0;\">Cheers,<br />EDDW</p>" +
                "</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>" +
                "</td>" +
                "</tr>" +
                "<tr>" +
                "<td style=\"padding: 24px;\" align=\"center\" bgcolor=\"#e9ecef\">&nbsp;</td>" +
                "</tr>" +
                "</tbody>" +
                "</table>";
        }
    }
}
