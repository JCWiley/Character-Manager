using CharacterManager.Model.Providers;
using CharacterManager.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.DataLoading
{
    public interface IDataLoader
    {
        public IDataService LoadLastFile();
        public IDataService LoadWithDialog();
    }
}
