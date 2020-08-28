using Character_Manager.Model.Jobs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Collection_Classes
{
    public class Job_Collection : ObservableCollection<Job>
    {
        public Job_Collection()
        {

        }
        public Job_Collection(IEnumerable<Job> original) : base(original)
        {

        }
    }

}
