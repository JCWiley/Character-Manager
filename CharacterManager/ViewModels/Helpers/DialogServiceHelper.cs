using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Views.PopupViews;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CharacterManager.ViewModels.Helpers
{
    public class DialogServiceHelper : IDialogServiceHelper
    {
        public DialogServiceHelper(IDialogService dialogService)
        {
            DS = dialogService;
        }
        private readonly IDialogService DS;

        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity)
        {
            DialogParameters Paramaters = new() { { "Job", targetjob }, { "Entity", targetentity } };
            DS.ShowDialog( nameof( NewEventPopupView ), Paramaters, action );
        }

        public void ShowAdvanceDayPopup(Action<IDialogResult> action)
        {
            DialogParameters Paramaters = new() { };
            DS.ShowDialog( nameof( AdvanceDayPopupView ), Paramaters, action );
        }

        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity, int effects, int Date)
        {
            DialogParameters Paramaters = new() { { "Job", targetjob }, { "Entity", targetentity }, { "Effects", effects }, { "Date", Date } };
            DS.ShowDialog( nameof( NewEventPopupView ), Paramaters, action );
        }

        public void ShowEventReportPopup(List<IEvent> allevents)
        {
            Action<IDialogResult> action = null;

            DialogParameters Paramaters = new() { { "Events", allevents } };
            DS.ShowDialog( nameof( EventReportPopupView ), Paramaters, action );
        }

        public void ShowJobReportPopup(IJobDirectoryProvider JDP)
        {
            Action<IDialogResult> action = null;

            DialogParameters Paramaters = new() { { "JDP", JDP } };
            DS.ShowDialog( nameof( JobReportPopupView ), Paramaters, action );
        }

        public void ShowWarning(string WarningText)
        {
            MessageBox.Show( WarningText, "Error", MessageBoxButton.OK, MessageBoxImage.Warning );
        }

        public void ShowMessage(string Message, string Header)
        {
            MessageBox.Show( Message, Header, MessageBoxButton.OK, MessageBoxImage.Information );
        }

        public MessageBoxResult ShowYesNoCancelMessage(string Message, string Header)
        {
            return MessageBox.Show( Message, Header, MessageBoxButton.YesNoCancel, MessageBoxImage.Information );

        }
    }
}
