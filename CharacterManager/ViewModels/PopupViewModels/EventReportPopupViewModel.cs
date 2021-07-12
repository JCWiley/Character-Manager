using CharacterManager.Model.Events;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.PopupViewModels
{
    public class EventReportPopupViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "Event Report";
            Events = parameters.GetValue<List<IEvent>>( "Events" );
        }
        public void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke( dialogResult );
        }

        #region Bindings
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty( ref title, value );
            }
        }

        private List<IEvent> events;
        public List<IEvent> Events
        {
            get
            {
                return events;
            }
            set
            {
                SetProperty( ref events, value );
            }
        }
        #endregion

        #region Commands
        private DelegateCommand _commandclose;
        public DelegateCommand CommandClose
        {
            get
            {
                return _commandclose ??= new DelegateCommand( CommandCloseExecute );
            }
        }
        #endregion
        #region Command Handlers
        private void CommandCloseExecute()
        {
            RaiseRequestClose( new DialogResult( ButtonResult.None ) );
        }
        #endregion
        #region Utilities
        #endregion
    }
}
