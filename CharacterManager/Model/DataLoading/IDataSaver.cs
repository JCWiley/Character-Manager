using CharacterManager.Model.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.DataLoading
{
    public interface IDataSaver
    {
        public void Save(object Target);
        public void SaveWithDialog(object Target);
    }
}
