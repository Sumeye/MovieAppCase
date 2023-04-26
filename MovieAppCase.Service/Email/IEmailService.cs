using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Service.Email
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
