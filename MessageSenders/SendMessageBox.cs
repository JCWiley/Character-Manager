using Character_Manager.GeneralInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.MessageSenders
{
    class SendMessageBox : IMessageSender
    {
        public void SendErrorMessage(string content, string title)
        {
            throw new NotImplementedException();
        }

        public void SendNotificationMessage(string content, string title)
        {
            throw new NotImplementedException();
        }

        public void SendWarningMessage(string content, string title)
        {
            throw new NotImplementedException();
        }
    }
}
