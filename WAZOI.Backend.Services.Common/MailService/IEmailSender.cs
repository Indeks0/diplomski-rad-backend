using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAZOI.Backend.Services.Common.MailService
{
    public interface IEmailSender
    {
        #region Methods

        Task SendEmailAsync(string toEmail, string subject, string message);

        Task SendRegistrationEmailAsync(string toEmail, string subject);

        #endregion Methods
    }
}