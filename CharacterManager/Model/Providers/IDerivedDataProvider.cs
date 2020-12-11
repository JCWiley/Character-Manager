using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Providers
{
    public interface IDerivedDataProvider
    {
        public List<String> Locations { get;}
        public RTree<IEntity> Tree { get; set; }
        public void SetEqual(object derivedDataProvider);
    }
}
