using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace The_World.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMessage(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Message from: {from}, Subject: {subject}, Text: {body}");
            return true;
        }
    }
}
