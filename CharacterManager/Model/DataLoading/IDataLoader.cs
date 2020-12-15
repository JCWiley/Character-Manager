using CharacterManager.Model.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.DataLoading
{
    public interface IDataLoader
    {
        public object LoadLastFile();
        public object LoadWithDialog();
    }
}
