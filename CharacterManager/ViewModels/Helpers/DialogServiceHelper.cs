using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Helpers;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Views.PopupViews;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterManager.ViewModels.Helpers
{
    public class DialogServiceHelper : IDialogServiceHelper
    {
        public DialogServiceHelper(IDialogService dialogService)
        {
            DS = dialogService;
        }
        private IDialogService DS;

        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity)
        {
            DialogParameters Paramaters = new DialogParameters { { "Job", targetjob }, { "Entity", targetentity } };
            DS.ShowDialog(nameof(NewEventPopupView), Paramaters, action);
        }

        public void ShowAdvanceDayPopup(Action<IDialogResult> action)
        {
            DialogParameters Paramaters = new DialogParameters {};
            DS.ShowDialog(nameof(AdvanceDayPopupView), Paramaters, action);
        }

        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity, int effects)
        {
            DialogParameters Paramaters = new DialogParameters { { "Job", targetjob }, { "Entity", targetentity },{"Effects", effects } };
            DS.ShowDialog(nameof(NewEventPopupView), Paramaters, action);
        }

        public void ShowEventReportPopup(List<IEvent> allevents)
        {
            Action<IDialogResult> action = null;

            DialogParameters Paramaters = new DialogParameters { {"Events",allevents } };
            DS.ShowDialog(nameof(EventReportPopupView), Paramaters,action);
        }

        public void ShowWarning(string WarningText)
        {
            MessageBox.Show(WarningText, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
