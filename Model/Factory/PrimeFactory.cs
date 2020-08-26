using Character_Manager.GeneralInterfaces;
using Character_Manager.MessageSenders;
using Character_Manager.Model.Entities;
using Character_Manager.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Model.Factory
{
    class PrimeFactory : IPrimeFactory
    {
        public IEntityFactory CreateIEntityFactory()
        {
            return new EntityFactory();
        }

        public IJobFactory CreateIJobFactory()
        {
            return new JobFactory();
        }

        public IRTreeFactory<IEntity> CreateIRTreeFactory_Entity()
        {
            return new RTreeFactory<IEntity>();
        }
        public IRTreeFactory<IJob> CreateIRTreeFactory_Job()
        {
            return new RTreeFactory<IJob>();
        }

        public IMessageSender CreateMessageSender()
        {
            return new SendMessageBox();
        }
    }
}
