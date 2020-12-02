using CharacterManager.Model.Interfaces;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterManager.Model.Other
{
    public class DerivedDataProvider : IDerivedDataProvider
    {
        public DerivedDataProvider(RTree<IEntity> tree)
        {
            Tree = tree;
        }

        private RTree<IEntity> Tree;

        public List<string> Locations
        {
            get
            {
                List<String> temp = new List<string>();
                foreach (IEntity entity in Tree.Get_All_Items())
                {
                    if(!temp.Contains(entity.Location))
                    {
                        temp.Add(entity.Location);
                    }
                }
                return temp;
            }
        }
    }
}
