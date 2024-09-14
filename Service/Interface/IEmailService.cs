using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string body);
    }
}
