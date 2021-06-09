using CharacterManager.Model.Entities;
using CharacterManager.Model.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.Helpers
{
    public interface IDialogServiceHelper
    {
        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity);
        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity,int effects,int Date);

        public void ShowAdvanceDayPopup(Action<IDialogResult> action);
        public void ShowWarning(string WarningText);

        public void ShowEventReportPopup(List<IEvent> allevents);

        public void ShowJobReportPopup(IJobDirectoryProvider JDP);

        public void ShowMessage(string Message, string Header);

        public System.Windows.MessageBoxResult ShowYesNoCancelMessage(string Message, string Header);
    }
}
