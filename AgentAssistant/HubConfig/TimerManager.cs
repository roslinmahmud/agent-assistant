using System;
using System.Threading;

namespace AgentAssistant.HubConfig
{
    public class TimerManager
    {
        public Action Action { get; set; }
        public Timer Timer { get; private set; }

        public TimerManager()
        {
            Timer = new Timer(Execute);
        }

        public void Execute(object stateInfo)
        {
            Action();
        }
    }
}
