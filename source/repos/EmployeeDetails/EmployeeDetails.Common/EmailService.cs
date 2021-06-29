using MimeKit;
using MailKit.Security;
using MimeKit.Text;
using System;
using Microsoft.Extensions.Configuration;

namespace EmployeeDetails.Common
{
    public class EmailService : IEmailService
    {
        private string _host;
        private string _from;
        private int _port;
        private string _userName;
        private string _passowrd;
        public EmailService(IConfiguration iConfiguration)
        {
            var emailConfig = iConfiguration.GetSection("EmailConfiguration");
            if (emailConfig != null)
            {
                _host = emailConfig.GetSection("Host").Value;
                _from = emailConfig.GetSection("From").Value;
                _port = Convert.ToInt32(emailConfig.GetSection("Port").Value);
                _userName = emailConfig.GetSection("Username").Value;
                _passowrd = emailConfig.GetSection("Password").Value;
            }
        }
        public void Send(string to, string subject, string message)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_from));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Text) { Text = message };

                // send email commented - Fake
                //MailKit.Net.Smtp.SmtpClient smtp = SendEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private MailKit.Net.Smtp.SmtpClient SendEmail(MimeMessage email)
        {
            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_host, _port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_userName, _passowrd);
            smtp.Send(email);
            smtp.Disconnect(true);
            return smtp;
        }
    }
}