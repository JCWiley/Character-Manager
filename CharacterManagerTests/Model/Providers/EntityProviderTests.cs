using CharacterManager.Model.Entities;
using CharacterManager.Model.Factories;
using CharacterManager.Model.RedundantTree;
using CharacterManager.Model.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;

namespace CharacterManager.Model.Providers.Tests
{
    [TestClass()]
    public class EntityProviderTests
    {
        [DataTestMethod]
        [DataRow( EntityTypes.Organization, true )]
        [DataRow( EntityTypes.Organization, false )]
        [DataRow( EntityTypes.Character, false )]
        public void AddEntity_Succeed(EntityTypes type, bool IsHead)
        {
            //arrange
            IRTreeMember<IEntity> TargetTreeMember = CreateSampleTreeMember( type, IsHead );

            Mock<IDataService> DSM = new();

            Mock<IRTree<IEntity>> RTM = new();
            RTM.Setup( x => x.AddItem( It.IsAny<IEntity>(), IsHead ) ).Returns( TargetTreeMember );

            DSM.Setup( x => x.EntityTree ).Returns( RTM.Object );

            Mock<IEntityFactory> EFM = new();
            EFM.Setup( x => x.CreateOrganization() ).Returns( CreateSampleOrganization() );
            EFM.Setup( x => x.CreateCharacter() ).Returns( CreateSampleCharacter() );

            IEntityProvider EP = new EntityProvider( new EventAggregator(), DSM.Object, EFM.Object );
            //act
            IRTreeMember<IEntity> result = EP.AddEntity( type, IsHead );

            //assert
            Assert.IsNotNull( result );
            Assert.AreEqual( result, TargetTreeMember );

            RTM.Verify( x => x.AddItem( It.IsAny<IEntity>(), IsHead ), Times.Exactly( 1 ) );
            RTM.VerifyNoOtherCalls();

            DSM.VerifyNoOtherCalls();

            EFM.Verify( x => x.CreateOrganization(), Times.AtMostOnce() );
            EFM.Verify( x => x.CreateCharacter(), Times.AtMostOnce() );

            EFM.VerifyNoOtherCalls();

        }

        [DataTestMethod]
        [DataRow( EntityTypes.Character, true )]
        public void AddEntity_Fail(EntityTypes type, bool IsHead)
        {
            //arrange
            IRTreeMember<IEntity> TargetTreeMember = CreateSampleTreeMember( type, IsHead );

            Mock<IDataService> DSM = new();

            Mock<IRTree<IEntity>> RTM = new();
            RTM.Setup( x => x.AddItem( It.IsAny<IEntity>(), IsHead ) ).Returns( TargetTreeMember );

            DSM.Setup( x => x.EntityTree ).Returns( RTM.Object );

            Mock<IEntityFactory> EFM = new();
            EFM.Setup( x => x.CreateOrganization() ).Returns( CreateSampleOrganization() );
            EFM.Setup( x => x.CreateCharacter() ).Returns( CreateSampleCharacter() );

            IEntityProvider EP = new EntityProvider( new EventAggregator(), DSM.Object, EFM.Object );

            //assert // act
            Assert.ThrowsException<InvalidOperationException>( () => EP.AddEntity( type, IsHead ) );
        }

        #region Helper Functions
        private Organization CreateSampleOrganization()
        {
            return new Organization();
        }
        private Character CreateSampleCharacter()
        {
            return new Character();
        }

        private IEntity CreateEntityFromType(EntityTypes type)
        {
            IEntity NewEntity = null;
            switch (type)
            {
                case EntityTypes.Organization:
                    NewEntity = CreateSampleOrganization();
                    break;
                case EntityTypes.Character:
                    NewEntity = CreateSampleCharacter();
                    break;
                default:
                    break;
            }
            return NewEntity;
        }

        private IRTreeMember<IEntity> CreateSampleTreeMember(EntityTypes type, bool isHead)
        {
            IRTreeMember<IEntity> sample = new RTreeMember<IEntity>( new List<Guid>(), new List<Guid>(), Guid.Empty, new RTree<IEntity>() );
            sample.Item = CreateEntityFromType( type );
            sample.IsHead = isHead;
            return sample;
        }
        #endregion
    }
}