using CharacterManager.Model.Entities;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.RedundantTree;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CharacterManager.Model.Helpers.JobLogic;

namespace CharacterManager.ViewModels.Helpers
{
    public interface IDialogServiceHelper
    {
        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity);
        public void ShowNewEventPopup(Action<IDialogResult> action, IJob targetjob, IRTreeMember<IEntity> targetentity,int effects);

        public void ShowAdvanceDayPopup(Action<IDialogResult> action);
    }
}
