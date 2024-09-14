using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private IFluentEmail _fluentEmail;
        public EmailService(IConfiguration configuration, IFluentEmail fluentEmail)
        {
            _config = configuration;
            _fluentEmail = fluentEmail;
        }

        public async Task<bool> SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var email = await _fluentEmail.To(toEmail).Subject(subject).Body(body).SendAsync();
                return email.Successful;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }
    }
}
