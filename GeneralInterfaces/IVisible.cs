using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.GeneralInterfaces
{
    public interface IVisible
    {
        public bool Visible { get; set; }
        public bool IsSelected { get; set; }
        public bool IsExpanded { get; set; }
    }
}
