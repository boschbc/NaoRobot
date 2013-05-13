using System;
using Naovigate.Communication;

namespace Naovigate.Test.Communication
{
    public class GoalComsStub : GoalCommunicator
    {
        private CommunicationStream coms;
        public GoalComsStub(CommunicationStream coms) : base("127.0.0.1", 0)
        {
            this.coms = coms;
        }

        public override CommunicationStream Coms
        {
            get { return coms; }
        }

        public void SetStream(CommunicationStream coms)
        {
            this.coms = coms;
        }
    }
}
