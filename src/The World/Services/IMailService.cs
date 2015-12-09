using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_World.Services
{
    public interface IMailService
    {
        bool SendMessage(string to, string from, string subject, string body);
    }
}
