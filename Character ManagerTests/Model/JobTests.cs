using Microsoft.VisualStudio.TestTools.UnitTesting;
using Character_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Tests
{
    [TestClass()]
    public class JobTests
    {
        [DataTestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(0, 0, 0, 1)]
        [DataRow(5, 0, 0, 0)]
        [DataRow(5, 3, 5, 2)]
        [DataRow(1, 5, 5, 2)]
        public void AddJobEvent_JobCompletes_ReturnsTrue(int startdate,int progress,int duration,int progressimpact)
        {
            //Event Types: "Critical Failure","Failure","Anomaly","Success","Critical Success"
            //Arrange
            DataModel DM = new DataModel();
            Job J = new Job(DM)
            {
                StartDate = startdate,
                Progress = progress,
                Duration = duration
            };

            //Act
            bool complete = J.AddJobEvent("Anomaly", "", progressimpact);

            //Assert
            Assert.IsTrue(complete);
        }

        [DataTestMethod]
        [DataRow(0, 0, 0, -1)]
        [DataRow(0, 0, 0, -5)]
        [DataRow(5, 0, 0, -1)]
        [DataRow(5, 5, 10, 2)]
        [DataRow(1, 5, 5, -2)]
        public void AddJobEvent_JobCompletes_ReturnsFalse(int startdate, int progress, int duration, int progressimpact)
        {
            //Event Types: "Critical Failure","Failure","Anomaly","Success","Critical Success"
            //Arrange
            DataModel DM = new DataModel();
            Job J = new Job(DM)
            {
                StartDate = startdate,
                Progress = progress,
                Duration = duration
            };

            //Act
            bool complete = J.AddJobEvent("Anomaly", "", progressimpact);

            //Assert
            Assert.IsFalse(complete);
        }

        [TestMethod()]
        public void MarkJobAsCompleteTest_SetComplete_CompleteIsTrue()
        {
            //Arrange
            DataModel DM = new DataModel();
            Job J = new Job(DM);
            //Act
            J.MarkJobAsComplete();
            //Assert
            Assert.AreEqual(J.Complete, true);
        }
        [DataTestMethod]
        [DataRow(1, 0)]
        [DataRow(2, 1)]
        public void DaysRemaining_durationGTRTHNprogress_PosInt(int duration,int progress)
        {
            //Arrange
            DataModel DM = new DataModel();
            Job J = new Job(DM)
            {
                Progress = progress,
                Duration = duration
            };

            //Act
            int complete = J.DaysRemaining;

            //Assert
            Assert.IsTrue(complete > 0);
        }
        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(0, 1)]
        [DataRow(2, 2)]
        [DataRow(2, 5)]
        public void DaysRemaining_durationLSSTHNEQLprogress_Zero(int duration, int progress)
        {
            //Arrange
            DataModel DM = new DataModel();
            Job J = new Job(DM)
            {
                Progress = progress,
                Duration = duration
            };

            //Act
            int complete = J.DaysRemaining;

            //Assert
            Assert.IsTrue(complete == 0);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(0, 1)]
        [DataRow(2, 2)]
        [DataRow(2, 5)]
        public void EndDate_startdateANDduration_SUM(int startdate, int duration)
        {
            //Arrange
            DataModel DM = new DataModel();
            Job J = new Job(DM)
            {
                Duration = duration,
                StartDate = startdate
            };

            //Act
            int complete = J.EndDate;

            //Assert
            Assert.IsTrue(complete == (startdate + duration));
        }
    }
}