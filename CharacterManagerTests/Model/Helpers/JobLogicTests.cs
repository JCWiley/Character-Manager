using CharacterManager.Events;
using CharacterManager.Model.Jobs;
using CharacterManager.Model.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;
using System;

namespace CharacterManager.Model.Helpers.Tests
{
    [TestClass()]
    public class JobLogicTests
    {
        #region AdvanceJob
        [DataTestMethod]
        [DataRow( 0, 5, 0, 0, 0, 1 )]
        [DataRow( 4, 7, 4, 1, 0, 1 )]
        [DataRow( 0, 7, 3, 0, 3, 6 )]
        public void AdvanceJob_ShouldAdvance(int startdate, int duration, int CurrentDay, int progress, int dayssincecreation, int daysToAdvance)
        {
            //arrange
            Job J_Test = new( CurrentDay )
            {
                StartDate = startdate,
                Duration = duration,
                Progress = progress,
                IsCurrentlyProgressing = true,
                Days_Since_Creation = dayssincecreation,
                Complete = false,
                SuccessChance = 0,
                FailureChance = 0
            };

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );

            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act
            JL_Test.AdvanceJob( J_Test, daysToAdvance );

            //assert
            Assert.AreEqual( J_Test.Complete, false, "Complete not false" );
            Assert.AreEqual( J_Test.StartDate, startdate, "StartDate improperly changed" );
            Assert.AreEqual( J_Test.Duration, duration, "Duration improperly changed" );
            Assert.AreEqual( J_Test.Progress, progress + daysToAdvance, "Progress improperly advanced" );
            Assert.AreEqual( J_Test.IsCurrentlyProgressing, true, "IsCurrentlyProgressing not true" );
            Assert.AreEqual( J_Test.Days_Since_Creation, dayssincecreation + daysToAdvance, "Days_Since_Creation improperly advanced" );

            EAM.Verify( x => x.GetEvent<RequestJobEventEvent>(), Times.Exactly( 0 ) );

        }

        [DataTestMethod]
        [DataRow( 0, 5, 0, false, 0, 0, false, 1 )] //will not advance as iscurrentlyprogressing is false
        [DataRow( 5, 5, 0, false, 0, 0, true, 1 )] //will not advance as start date is in the future
        [DataRow( 0, 5, 0, false, 0, 0, false, 100 )] //will not advance as iscurrentlyprogressing is false
        [DataRow( 500, 5, 0, false, 0, 0, true, 100 )] //will not advance as start date is in the future
        [DataRow( 7, 5, 0, false, 0, 0, true, 6 )] //will not advance as start date is in the future
        [DataRow( 5, 5, 0, false, 0, 0, true, 4 )] //will not advance as start date is in the future
        public void AdvanceJob_ShouldNotAdvance(int startdate, int duration, int CurrentDay, bool IsComplete, int progress, int dayssincecreation, bool iscurrentlyprogressing, int daysToAdvance)
        {
            //arrange
            Job J_Test = new( CurrentDay )
            {
                StartDate = startdate,
                Duration = duration,
                Progress = progress,
                IsCurrentlyProgressing = iscurrentlyprogressing,
                Days_Since_Creation = dayssincecreation,
                Complete = IsComplete
            };

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );

            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act
            JL_Test.AdvanceJob( J_Test, daysToAdvance );

            //assert
            Assert.AreEqual( J_Test.Complete, IsComplete, "ISComplete improperly changed" );
            Assert.AreEqual( J_Test.StartDate, startdate, "StartDate improperly changed" );
            Assert.AreEqual( J_Test.Duration, duration, "Duration improperly changed" );
            Assert.AreEqual( J_Test.Progress, progress, "Progress improperly changed" );
            Assert.AreEqual( J_Test.IsCurrentlyProgressing, iscurrentlyprogressing, "IsCurrentlyProgressing improperly changed" );
            Assert.AreEqual( J_Test.Days_Since_Creation, dayssincecreation + daysToAdvance, "Days_Since_Creation improperly advanced" );
        }

        [DataTestMethod]
        [DataRow( 0, 5, 4, 4, 4, 1, 1 )]
        [DataRow( 0, 5, 4, 4, 4, 2, 1 )]
        [DataRow( 4, 5, 5, 1, 1, 4, 1 )]
        [DataRow( 4, 5, 5, 1, 1, 15, 3 )]
        public void AdvanceJob_RecurringCompleted(int startdate, int duration, int CurrentDay, int progress, int dayssincecreation, int daysToAdvance, int timeswillcomplete)
        {
            //arrange
            Job J_Test = new( CurrentDay )
            {
                StartDate = startdate,
                Duration = duration,
                Progress = progress,
                IsCurrentlyProgressing = true,
                Days_Since_Creation = dayssincecreation,
                Complete = false,
                Recurring = true,
                SuccessChance = 0,
                FailureChance = 0
            };

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );

            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act
            JL_Test.AdvanceJob( J_Test, daysToAdvance );

            //assert
            Assert.AreEqual( J_Test.Complete, false );
            Assert.AreEqual( J_Test.StartDate, startdate );
            Assert.AreEqual( J_Test.Duration, duration );
            Assert.AreEqual( J_Test.Progress, (daysToAdvance - (duration - progress)) % duration );
            Assert.AreEqual( J_Test.IsCurrentlyProgressing, true );
            Assert.AreEqual( J_Test.Days_Since_Creation, dayssincecreation + daysToAdvance );

            EAM.Verify( x => x.GetEvent<RequestJobEventEvent>(), Times.Exactly( timeswillcomplete ) );
        }

        [DataTestMethod]
        [DataRow( 0, 5, 4, 4, 4, 1 )] //has progressed 4/5, will advance 1, will complete with 0 extra days
        [DataRow( 0, 5, 4, 4, 4, 2 )] //has progressed 4/5, will advance 2, will complete with 1 extra day
        [DataRow( 4, 5, 5, 1, 1, 4 )] //has progressed 1/5, will advance 4, will complete with 0 extra days
        [DataRow( 4, 5, 5, 1, 1, 15 )]//has progressed 1/5, will advance 15, will complete with 11 extra days, would complete 3 times if labled as recurring
        public void AdvanceJob_NotRecurringCompleted(int startdate, int duration, int CurrentDay, int progress, int dayssincecreation, int daysToAdvance)
        {
            //arrange
            Job J_Test = new( CurrentDay )
            {
                StartDate = startdate,
                Duration = duration,
                Progress = progress,
                IsCurrentlyProgressing = true,
                Days_Since_Creation = dayssincecreation,
                Complete = false,
                Recurring = false,
                SuccessChance = 0,
                FailureChance = 0
            };

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );

            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act
            JL_Test.AdvanceJob( J_Test, daysToAdvance );

            //assert
            Assert.AreEqual( J_Test.Complete, true, "Complete not false" );
            Assert.AreEqual( J_Test.StartDate, startdate, "StartDate improperly changed" );
            Assert.AreEqual( J_Test.Duration, duration, "Duration improperly changed" );
            Assert.AreEqual( J_Test.Progress, duration, "Progress improperly advanced" );
            Assert.AreEqual( J_Test.IsCurrentlyProgressing, false, "IsCurrentlyProgressing not false" );
            Assert.AreEqual( J_Test.Days_Since_Creation, dayssincecreation + daysToAdvance, "Days_Since_Creation improperly advanced" );

            EAM.Verify( x => x.GetEvent<RequestJobEventEvent>(), Times.Exactly( 1 ) );
        }

        [TestMethod]
        public void AdvanceJob_NegativeAdvanceThrowsException()
        {
            //arrange
            Job J_Test = new( 0 )
            {
                StartDate = 0,
                Duration = 0,
                Progress = 0,
                IsCurrentlyProgressing = false,
                Days_Since_Creation = 0,
                Complete = false,
                Recurring = false
            };
            int daysToAdvance = -1;

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );


            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act // assert
            Assert.ThrowsException<InvalidOperationException>( () => JL_Test.AdvanceJob( J_Test, daysToAdvance ) );
        }

        [DataTestMethod]
        //        startdate, duration, CurrentDay, IsComplete, progress, dayssincecreation, iscurrentlyprogressing, daysToAdvance, isrecurring, targetprogress, shouldcomplete
        [DataRow( 3, 5, 0, false, 0, 0, true, 6, false, 4, false )]//wait 3 days, then advance 4, do not complete, not recurring
        [DataRow( 60, 50, 10, false, 0, 0, true, 100, false, 50, true )]
        //[DataRow(         3,        5,          0,      false,        0,                 0,                   true,             6,       false,              4,          false)]
        //[DataRow(         3,        5,          0,      false,        0,                 0,                   true,             6,       false,              4,          false)]
        //[DataRow(         3,        5,          0,      false,        0,                 0,                   true,             6,       false,              4,          false)]
        public void AdvanceJob_ShouldBeginToAdvance(int startdate, int duration, int CurrentDay, bool IsComplete, int progress, int dayssincecreation, bool iscurrentlyprogressing, int daysToAdvance, bool isrecurring, int targetprogress, bool shouldcomplete)
        {
            //arrange
            Job J_Test = new( CurrentDay )
            {
                StartDate = startdate,
                Duration = duration,
                Progress = progress,
                IsCurrentlyProgressing = iscurrentlyprogressing,
                Days_Since_Creation = dayssincecreation,
                Complete = IsComplete,
                Recurring = isrecurring
            };

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );

            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act
            JL_Test.AdvanceJob( J_Test, daysToAdvance );

            //assert
            Assert.AreEqual( J_Test.Complete, shouldcomplete, "ISComplete improperly changed" );
            Assert.AreEqual( J_Test.StartDate, startdate, "StartDate improperly changed" );
            Assert.AreEqual( J_Test.Duration, duration, "Duration improperly changed" );
            Assert.AreEqual( J_Test.Progress, targetprogress, "Progress improperly advanced" );
            Assert.AreEqual( J_Test.Days_Since_Creation, dayssincecreation + daysToAdvance, "Days_Since_Creation improperly advanced" );
        }

        #endregion

        #region ProgressJob
        [DataTestMethod]
        [DataRow( 0, 5, 0, 0, 0, 1 )]
        [DataRow( 4, 7, 4, 1, 0, 1 )]
        [DataRow( 0, 7, 3, 0, 3, 6 )]
        public void ProgressJob_ShouldAdvance(int startdate, int duration, int CurrentDay, int progress, int dayssincecreation, int daysToAdvance)
        {
            //arrange
            Job J_Test = new( CurrentDay )
            {
                StartDate = startdate,
                Duration = duration,
                Progress = progress,
                IsCurrentlyProgressing = true,
                Days_Since_Creation = dayssincecreation,
                Complete = false
            };

            Mock<IEventAggregator> EAM = new();
            Mock<RequestJobEventEvent> RJEM = new();
            EAM.Setup( x => x.GetEvent<RequestJobEventEvent>() ).Returns( RJEM.Object );

            JobLogic JL_Test = new( new RandomProvider(), EAM.Object );

            //act
            JL_Test.ProgressJob( J_Test, daysToAdvance );

            //assert
            Assert.AreEqual( J_Test.Complete, false, "Complete not false" );
            Assert.AreEqual( J_Test.StartDate, startdate, "StartDate improperly changed" );
            Assert.AreEqual( J_Test.Duration, duration, "Duration improperly changed" );
            Assert.AreEqual( J_Test.Progress, progress + daysToAdvance, "Progress improperly advanced" );
            Assert.AreEqual( J_Test.IsCurrentlyProgressing, true, "IsCurrentlyProgressing not true" );
            Assert.AreEqual( J_Test.Days_Since_Creation, dayssincecreation, "Days_Since_Creation improperly advanced" );

            EAM.VerifyNoOtherCalls();

        }


        #endregion
    }
}