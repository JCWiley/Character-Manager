﻿using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using System;
using System.Collections.Generic;

namespace CharacterManager.Model.Providers
{
    public interface IEntityProvider
    {
        public IEntityFactory EF { get; set; }

        public IDataService DS { get; set; }

        public IRTreeMember<IEntity> AddEntity(EntityTypes type, bool ishead = false);

        public void AddChild(IRTreeMember<IEntity> Parent, IRTreeMember<IEntity> Child);

        public List<IRTreeMember<IEntity>> HeadEntities();
        public IRTreeMember<IEntity> GetTreeMemberForEntity(IEntity entity);
        public IRTreeMember<IEntity> GetTreeMemberForGuid(Guid G);
        public IRTreeMember<IEntity> CurrentTargetAsCharacter { get; }
        public IRTreeMember<IEntity> CurrentTargetAsOrganization { get; }
        public List<IRTreeMember<IEntity>> GetAllChildren(IRTreeMember<IEntity> rTreeMember);
        public List<IRTreeMember<IEntity>> GetImmidiateChildren(IRTreeMember<IEntity> rTreeMember);
    }
}
