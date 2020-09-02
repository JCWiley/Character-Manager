using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Character_Manager.RedundantTree.Tests
{
    //Arrange
    //Act
    //Assert
    [TestClass()]
    public class RTreeTests
    {
        [TestMethod()]
        public void AddItem_SingleItem()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            //Act
            IRTreeMember<int> rtm = RT.AddItem(3,false);

            //Assert
            Assert.AreEqual(RT.Count,1);
            Assert.AreEqual(rtm.Item, 3);
        }

        [TestMethod()]
        public void AddItem_MultipleItem()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();
            //Act
            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(-1, false);
            //Assert
            Assert.AreEqual(RT.Count, 3);
            Assert.AreEqual(rtm1.Item, 3);
            Assert.AreEqual(rtm2.Item, 5);
            Assert.AreEqual(rtm3.Item, -1);
        }

        [TestMethod()]
        public void RemoveItem_SingleItemNoRelations()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();
            //Act
            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            RT.RemoveItem(rtm1);
            //Assert
            Assert.AreEqual(RT.Count, 0);
        }
        [TestMethod()]
        public void RemoveItem_SingleItemWithParentAndChild()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(-1, false);

            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm3, rtm1);

            //Act
            RT.RemoveItem(rtm1);

            //Assert
            Assert.AreEqual(RT.Count, 2);
        }

        [TestMethod()]
        public void AddChild_SingleChild()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);

            //Act
            RT.AddChild(rtm1, rtm2);
            //Assert

            Assert.IsTrue(rtm1.Children.Contains(rtm2.Gid));
            Assert.IsTrue(rtm1.Children.Count == 1);
            Assert.IsTrue(rtm2.Parents.Contains(rtm1.Gid));
        }

        [TestMethod()]
        public void AddChild_MultipleChildren()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);
            IRTreeMember<int> rtm4 = RT.AddItem(5, false);
            IRTreeMember<int> rtm5 = RT.AddItem(5, false);

            //Act
            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm1, rtm3);
            RT.AddChild(rtm1, rtm4);
            RT.AddChild(rtm1, rtm5);
            //Assert

            Assert.IsTrue(rtm1.Children.Contains(rtm2.Gid));
            Assert.IsTrue(rtm1.Children.Contains(rtm3.Gid));
            Assert.IsTrue(rtm1.Children.Contains(rtm4.Gid));
            Assert.IsTrue(rtm1.Children.Contains(rtm5.Gid));

            Assert.IsTrue(rtm1.Children.Count == 4);

            Assert.IsTrue(rtm2.Parents.Contains(rtm1.Gid));
            Assert.IsTrue(rtm3.Parents.Contains(rtm1.Gid));
            Assert.IsTrue(rtm4.Parents.Contains(rtm1.Gid));
            Assert.IsTrue(rtm5.Parents.Contains(rtm1.Gid));
        }

        [TestMethod()]
        public void AddChild_AddSelfAsChild_NoChange()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);

            //Act
            RT.AddChild(rtm1, rtm1);
            //Assert

            Assert.IsFalse(rtm1.Children.Contains(rtm1.Gid));
            Assert.IsTrue(rtm1.Children.Count == 0);
            Assert.IsFalse(rtm1.Parents.Contains(rtm1.Gid));
        }

        [TestMethod()]
        public void RemoveChild_SingleChild()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);

            //Act
            RT.RemoveChild(rtm1, rtm2);
            //Assert

            Assert.IsFalse(rtm1.Children.Contains(rtm2.Gid));
            Assert.IsTrue(rtm1.Children.Count == 0);
            Assert.IsFalse(rtm2.Parents.Contains(rtm1.Gid));
        }

        [TestMethod()]
        public void RemoveChild_MultipleChildren()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm1, rtm3);

            //Act
            RT.RemoveChild(rtm1, rtm2);
            //Assert

            Assert.IsTrue(rtm1.Children.Count == 1);

            Assert.IsFalse(rtm1.Children.Contains(rtm2.Gid));
            Assert.IsFalse(rtm2.Parents.Contains(rtm1.Gid));

            Assert.IsTrue(rtm1.Children.Contains(rtm3.Gid));
            Assert.IsTrue(rtm3.Parents.Contains(rtm1.Gid));
        }

        //[TestMethod()]
        //public void Get_Item_()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void Get_Immidiate_Parents_OneParent()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);

            //Act
            List<IRTreeMember<int>> parents = RT.Get_Immidiate_Parents(rtm2);

            //Assert
            Assert.IsTrue(parents.Contains(rtm1));
        }

        [TestMethod()]
        public void Get_Immidiate_Parents_MultipleParents()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm3, rtm2);

            //Act
            List<IRTreeMember<int>> parents = RT.Get_Immidiate_Parents(rtm2);

            //Assert
            Assert.IsTrue(parents.Contains(rtm1));
            Assert.IsTrue(parents.Contains(rtm3));
        }

        [TestMethod()]
        public void Get_Immidiate_Children_SingleChild()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);

            //Act
            List<IRTreeMember<int>> children = RT.Get_Immidiate_Children(rtm1);

            //Assert
            Assert.IsTrue(children.Contains(rtm2));
        }

        [TestMethod()]
        public void Get_Immidiate_Children_MultipleChildren()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm1, rtm3);

            //Act
            List<IRTreeMember<int>> children = RT.Get_Immidiate_Children(rtm1);

            //Assert
            Assert.IsTrue(children.Contains(rtm2));
            Assert.IsTrue(children.Contains(rtm3));
        }

        [TestMethod()]
        public void Get_All_Parents_SimpleTree()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm2, rtm3);

            //Act
            List<IRTreeMember<int>> parents = RT.Get_All_Parents(rtm3);

            //Assert
            Assert.IsTrue(parents.Contains(rtm1));
            Assert.IsTrue(parents.Contains(rtm2));
        }

        [TestMethod()]
        public void Get_All_Parents_ComplexTree()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);
            IRTreeMember<int> rtm4 = RT.AddItem(5, false);
            IRTreeMember<int> rtm5 = RT.AddItem(5, false);
            IRTreeMember<int> rtm6 = RT.AddItem(5, false);
            IRTreeMember<int> rtm7 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);    //   2 - 1 - 7
            RT.AddChild(rtm2, rtm3);    //  /       / 
            RT.AddChild(rtm4, rtm3);    // 3 - 4 - 5
            RT.AddChild(rtm5, rtm4);    //  \
            RT.AddChild(rtm6, rtm3);    //   6
            RT.AddChild(rtm7, rtm1);
            RT.AddChild(rtm7, rtm5);

            //Act
            List<IRTreeMember<int>> parents = RT.Get_All_Parents(rtm3);

            //Assert
            Assert.IsTrue(parents.Contains(rtm1));
            Assert.IsTrue(parents.Contains(rtm2));
            Assert.IsTrue(parents.Contains(rtm4));
            Assert.IsTrue(parents.Contains(rtm5));
            Assert.IsTrue(parents.Contains(rtm6));
            Assert.IsTrue(parents.Contains(rtm7));
        }

        [TestMethod()]
        public void Get_All_Children_SimpleTree()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);
            RT.AddChild(rtm2, rtm3);

            //Act
            List<IRTreeMember<int>> children = RT.Get_All_Children(rtm1);

            //Assert
            Assert.IsTrue(children.Contains(rtm2));
            Assert.IsTrue(children.Contains(rtm3));
        }

        [TestMethod()]
        public void Get_All_Children_ComplexTree()
        {
            //Arrange
            IRTreeFactory<int> rtf = new RTreeFactory<int>();
            RTree<int> RT = rtf.CreateRTree();

            IRTreeMember<int> rtm1 = RT.AddItem(3, false);
            IRTreeMember<int> rtm2 = RT.AddItem(5, false);
            IRTreeMember<int> rtm3 = RT.AddItem(5, false);
            IRTreeMember<int> rtm4 = RT.AddItem(5, false);
            IRTreeMember<int> rtm5 = RT.AddItem(5, false);
            IRTreeMember<int> rtm6 = RT.AddItem(5, false);
            IRTreeMember<int> rtm7 = RT.AddItem(5, false);

            RT.AddChild(rtm1, rtm2);    //   2 - 1 - 7
            RT.AddChild(rtm2, rtm3);    //  /       / 
            RT.AddChild(rtm4, rtm3);    // 3 - 4 - 5
            RT.AddChild(rtm5, rtm4);    //  \
            RT.AddChild(rtm6, rtm3);    //   6
            RT.AddChild(rtm7, rtm1);
            RT.AddChild(rtm7, rtm5);

            //Act
            List<IRTreeMember<int>> children = RT.Get_All_Children(rtm7);

            //Assert
            Assert.IsTrue(children.Contains(rtm1));
            Assert.IsTrue(children.Contains(rtm2));
            Assert.IsTrue(children.Contains(rtm3));
            Assert.IsTrue(children.Contains(rtm5));
            Assert.IsTrue(children.Contains(rtm4));

            Assert.IsFalse(children.Contains(rtm6));
        }
    }
}