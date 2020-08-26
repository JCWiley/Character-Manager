using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.GeneralInterfaces
{
    public interface IMessageSender
    {
        public void SendErrorMessage(string content, string title);
        public void SendWarningMessage(string content, string title);
        public void SendNotificationMessage(string content, string title);
    }
}
