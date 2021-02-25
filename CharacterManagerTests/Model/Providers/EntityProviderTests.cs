using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharacterManager.Model.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityAutoMoq;
using Prism.Events;
using CharacterManager.Model.Services;
using CharacterManager.Model.Entities;
using CharacterManager.Model.RedundantTree;

namespace CharacterManager.Model.Providers.Tests
{
    [TestClass()]
    public class EntityProviderTests
    {
        private UnityAutoMoqContainer MOQC = new UnityAutoMoqContainer();
        [TestMethod()]
        public void EntityProvider_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NotifySelectedCharacterChanged_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NotifySelectedOrganizationChanged_()
        {
            Assert.Fail();
        }

        [DataTestMethod]
        [DataRow(EntityTypes.Organization,true)]
        [DataRow(EntityTypes.Organization, false)]
        [DataRow(EntityTypes.Character, false)]
        public void AddEntity_Succeed(EntityTypes type,bool IsHead)
        {
            IEntityProvider EP = new EntityProvider(MOQC.Resolve<IEventAggregator>(), MOQC.Resolve<IDataService>());
            EP.AddEntity(type, IsHead);


            //MOQC.Resolve<IRTreeMember<IEntity>())

            Assert.Fail();
        }
        [DataTestMethod]
        [DataRow(EntityTypes.Character, true)]
        public void AddEntity_Fail(EntityTypes type, bool IsHead)
        {
            IEntityProvider EP = new EntityProvider(MOQC.Resolve<IEventAggregator>(), MOQC.Resolve<IDataService>());
            EP.AddEntity(type, IsHead);


            //MOQC.Resolve<IRTreeMember<IEntity>())

            Assert.Fail();
        }

        [TestMethod()]
        public void AddChild_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HeadEntities_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllChildren_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetImmidiateChildren_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTreeMemberForEntity_()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTreeMemberForGuid_()
        {
            Assert.Fail();
        }
    }
}