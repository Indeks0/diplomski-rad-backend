using System;
using WAZOI.Backend.Services.Common.MailService;
using WAZOI.Backend.Infrastructure;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace WAZOI.Backend.Services.MailService
{
    public class EmailSender : IEmailSender
    {
        #region Constructors

        public EmailSender(IOptions<AuthMessageSenderOptions> options, IOptions<FrontendDomainOptions> frontendDomainOptions)
        {
            _options = options;
            _frontendDomainOptions = frontendDomainOptions;
        }

        #endregion Constructors

        #region Properties

        private IOptions<AuthMessageSenderOptions> _options { get; }

        private IOptions<FrontendDomainOptions> _frontendDomainOptions { get; }

        #endregion Properties

        //Set with Secret Manager.

        #region Methods

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("podrška@wazoi.hr"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = message };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate("wazoi.service@gmail.com", _options.Value.MailPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task SendRegistrationEmailAsync(string email, string token)
        {
            var tokenEscapedString = Uri.EscapeDataString(token);
            var confirmMailLink = _frontendDomainOptions.Value.HostAddress + "/login/confirm-email/" + tokenEscapedString + "/" + email;

            await SendEmailAsync(email, "Potvrda računa", "Dobrodošli! Odlaskom na sljedeću poveznicu potvrdite Vaš račun: \n" + confirmMailLink);
        }

        #endregion Methods
    }
}