using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CharacterManager.Model.Providers
{
    [DataContract(Name = "DerivedDataProvider", Namespace = "CharacterManager.Model.Providers")]
    [KnownType(typeof(RTree<IEntity>))]
    [KnownType(typeof(Character))]
    [KnownType(typeof(Organization))]
    public class DerivedDataProvider : IDerivedDataProvider
    {
        public DerivedDataProvider()
        {

        }
        public DerivedDataProvider(RTree<IEntity> tree)
        {
            Tree = tree;
        }

        [DataMember(Name = "Tree")]
        private RTree<IEntity> tree;
        public RTree<IEntity> Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        [JsonIgnore]
        [IgnoreDataMember]
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

        public void SetEqual(object derivedDataProvider)
        {
            Tree = ((IDerivedDataProvider)derivedDataProvider).Tree;
        }
    }
}
