using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    public class SMS : IMessage
    {
        public string Number { get; set; }
        public string Content { get; set; }

        public void SendMessage()
        {
            SendSMS();
        }

        public void SendSMS()
        {
            Console.WriteLine("Sending SMS...");
        }
    }

    public class MMS : IMessage
    {
        public string Number { get; set; }
        public byte[] Content { get; set; }

        public void SendMessage()
        {
            SendMMS();
        }

        public void SendMMS()
        {
            Console.WriteLine("Sending MMS...");
        }
    }

    public class Email : IMessage
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public void SendEmail()
        {
            Console.WriteLine("Sending Email...");
        }

        public void SendMessage()
        {
            SendEmail();
        }
    }

    public interface IMessage
    {
        void SendMessage();
    }

    class Messanger
    {
        public IEnumerable<IMessage> Messages { get; set; }

        public void SenMessages()
        {
            Messages?.ToList().ForEach(x => x.SendMessage());
        }
    }
}
