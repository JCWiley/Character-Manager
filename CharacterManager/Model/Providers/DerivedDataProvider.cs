using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CharacterManager.Model.Providers
{
    public class DerivedDataProvider : IDerivedDataProvider
    {
        IDataService DS;

        public DerivedDataProvider(IDataService dataService)
        {
            DS = dataService;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<string> Locations
        {
            get
            {
                List<String> temp = new List<string>();
                foreach (IEntity entity in DS.EntityTree.Get_All_Items())
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
