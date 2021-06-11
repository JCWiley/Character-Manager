using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using CharacterManager.ViewModels.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace CharacterManager.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel (IEventAggregator eventAggregator, IRegionManager regionManager, ISettingsService settingsService, IDialogServiceHelper dialogServiceHelper)
        {
            RM = regionManager;
            EA = eventAggregator;
            SS = settingsService;
            DSH = dialogServiceHelper;

            EA.GetEvent<SelectedEntityChangedEvent>().Subscribe(SelectedEntityChangedExecute);
            EA.GetEvent<DataLoadSuccessEvent>().Subscribe(DataLoadSuccessEventExecute);
            RM.RequestNavigate("DETAIL_REGION", "OrganizationDetailView");
            EA.GetEvent<DataLoadRequestEvent>().Publish(LoadRequestTypes.LastFile);
        }

        #region Variables
        private IEventAggregator EA;
        private readonly IRegionManager RM;
        private ISettingsService SS;
        private IDialogServiceHelper DSH;
        #endregion

        #region Binding Targets

        public string Filename
        {
            get
            {
                return SS.Filename;
            }
        }
        #endregion

        #region Commands
        private DelegateCommand<System.ComponentModel.CancelEventArgs> _commandwindowclosing;

        public DelegateCommand<System.ComponentModel.CancelEventArgs> CommandWindowClosing => _commandwindowclosing ??= new DelegateCommand<System.ComponentModel.CancelEventArgs>(CommandWindowClosingExecute);

        #endregion
        #region Command handlers
        private void CommandWindowClosingExecute(System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.MessageBoxResult result = DSH.ShowYesNoCancelMessage("Would you like to save your changes?", "Save Changes");
            switch (result)
            {
                case System.Windows.MessageBoxResult.None:
                    e.Cancel = false;
                    break;
                case System.Windows.MessageBoxResult.OK:
                    throw new Exception("YesNoCancel Messagebox returned OK");
                case System.Windows.MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
                case System.Windows.MessageBoxResult.Yes:
                    e.Cancel = false;
                    EA.GetEvent<DataSaveRequestEvent>().Publish(SaveRequestTypes.Save);
                    break;
                case System.Windows.MessageBoxResult.No:
                    e.Cancel = false;
                    break;
                default:
                    break;
            }



        }
        #endregion

        #region Event Handlers
        void SelectedEntityChangedExecute(IRTreeMember<IEntity> Selected_Item)
        {
            if (Selected_Item.Item is Character)
            {
                RM.RequestNavigate("DETAIL_REGION", "CharacterDetailView");
            }
            else if (Selected_Item.Item is Organization)
            {
                RM.RequestNavigate("DETAIL_REGION", "OrganizationDetailView");
            }
            else
            {
                throw new Exception("Selected_Item is not Character or Organization");
            }
        }

        void DataLoadSuccessEventExecute(IRTreeMember<IEntity> paramater)
        {
            RaisePropertyChanged("Filename");
        }

        #endregion
    }
}
