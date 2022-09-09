using System.Threading;
using Garden_Library;

namespace M16_Task1
{
    public class BirdThread
    {
        
        string thrdName;
        Bird bird;
        Thread thrd;
        

        public Thread Thrd { get { return thrd; } }

        public BirdThread(Bird bird) 
        {
            
            this.thrdName = bird.Name;
            this.bird = bird;

            thrd = new Thread(this.Run);
            thrd.Start();
        }



        public void Run ()
        {
            lock (Print.ConsoleWriterLock) 
                Print.WriteLine($"Поток_{thrdName} начат.");


            this.bird.BirdIsAlive();

            lock (Print.ConsoleWriterLock) 
            {
                if (this.bird.NeedForFood > 0)
                    Print.Text2 = $"Птичка голодная!";
                else
                    Print.Text2 = $"Птичка сытая!";
                Print.WriteLine($"Поток_{thrdName} завершен.");
            }            

        }
    }
}
