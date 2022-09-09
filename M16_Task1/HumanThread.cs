using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Garden_Library;

namespace M16_Task1
{
    public class HumanThread
    { 
        string thrdName;
        Human human;
        Thread thrd;
        

        public Thread Thrd { get { return thrd; } }

        public HumanThread(Human human) 
        {
            
            this.thrdName = human.Name;
            this.human = human;

            thrd = new Thread(this.Run);
            thrd.Start();
        }



        public void Run ()
        {
            lock (Print.ConsoleWriterLock) 
                Print.WriteLine($"Поток_{thrdName} начат.");


            this.human.HumanRun();

            lock (Print.ConsoleWriterLock) 
                Print.WriteLine($"Поток_{thrdName} завершен.");           
        }
    }
}
