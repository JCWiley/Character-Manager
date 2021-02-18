using CharacterManager.Views.PopupViews;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.Helpers
{
    public class DialogServiceHelper : IDialogServiceHelper
    {
        public DialogServiceHelper(IDialogService dialogService)
        {
            DS = dialogService;
        }
        private IDialogService DS;

        public void ShowNewEventPopup(Action<Prism.Services.Dialogs.IDialogResult> action, DialogParameters Paramaters)
        {
            DS.ShowDialog(nameof(NewEventPopupView),Paramaters, action);
        }

        public void ShowAdvanceDayPopup(Action<Prism.Services.Dialogs.IDialogResult> action, DialogParameters Paramaters)
        {
            DS.ShowDialog(nameof(AdvanceDayPopupView), Paramaters, action);
        }
    }
}
