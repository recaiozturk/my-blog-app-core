﻿using MyBlog.WebUI.Util.Abstract;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;

namespace MyBlog.WebUI.Util
{
    public class SmtpEmailSender : IEmailSender
    {
        private string? _host;
        private int _port;
        private bool _enableSSL;
        private string? _userName;
        private string? _password;

        public SmtpEmailSender(string? host, int port, bool enableSSL, string? userName, string? password)
        {
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _userName = userName;
            _password = password;
        }

        public Task SendEmailAsync(string email,string clientName,string clientEmail, string subject, string message)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL
            };

            string body = $"<p><strong>Gönderen İsim :</strong> {clientName}</p> <p><strong>Gönderen Kişi Mail:</strong>{clientEmail}</p> <p><strong>Mesaj:</strong>{message}</p> ";

            return client.SendMailAsync(new MailMessage(_userName, email, subject, body) { IsBodyHtml = true });
        }
    }
}
