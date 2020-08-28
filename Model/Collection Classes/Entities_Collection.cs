using Character_Manager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Collection_Classes
{
    public class Entities_Collection : ObservableCollection<Entity>
    {
        public Entities_Collection()
        {
        }

        public Entities_Collection(IEnumerable<Entity> collection) : base(collection)
        {
        }
    }
}
