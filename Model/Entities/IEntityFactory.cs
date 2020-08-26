﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Entities
{
    public interface IEntityFactory
    {
        public IEntity CreateCharacter();
        public IEntity CreateOrganization();
    }
}