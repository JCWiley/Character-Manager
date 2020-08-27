using Character_Manager.GeneralInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Character_Manager.MessageSenders
{
    class SendMessageBox : IMessageSender
    {
        public void SendErrorMessage(string content, string header)
        {
            MessageBox.Show(content, header,MessageBoxButton.OK,MessageBoxImage.Error);
        }

        public void SendNotificationMessage(string content, string header)
        {
            MessageBox.Show(content, header, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SendWarningMessage(string content, string header)
        {
            MessageBox.Show(content, header, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public MessageBoxResult SendYesNoQuestion(string question, string header)
        {
            return MessageBox.Show(question, header, MessageBoxButton.YesNo, MessageBoxImage.Information);
        }
        public MessageBoxResult SendYesNoCancelQuestion(string question, string header)
        {
            return MessageBox.Show(question, header, MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
        }
    }
}
