﻿using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels.Helpers
{
    public interface IDialogServiceHelper
    {
        public void ShowNewEventPopup(Action<IDialogResult> action,DialogParameters Paramaters);
    }
}
