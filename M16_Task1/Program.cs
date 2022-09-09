using System;
using System.Collections.Generic;
using Garden_Library;

namespace M16_Task1
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Print.WriteLine("Основной поток начат.");

            List<Feeder> listFeeder = new List<Feeder>(5);
            List<Bird> listBird = new List<Bird>(8);
            List<BirdThread> listBirdThread = new List<BirdThread>(8);
            

            Random rnd = new Random();

            for (int i=0; i< 5; i++)
            {
                listFeeder.Add(new Feeder(i, rnd.Next(1,3)));
            }

            for(int i=0; i< 8; i++) 
            {
                listBird.Add(new Bird($"Птичка {i}", 1, rnd.Next(5, 20)));
                foreach (Feeder item in listFeeder)
                    item.FeederFillNotify += listBird[i].OnFeederFilling;
                listBirdThread.Add(new BirdThread(listBird[i]));
            }

            Human human = new Human("Маша", listFeeder);
            HumanThread humanThread = new HumanThread(human);

            humanThread.Thrd.Join();
            foreach(BirdThread item in listBirdThread) 
            {
                item.Thrd.Join();
            }

                Print.WriteLine("Основной поток завершен.");
            Console.ReadLine();
        }
    }
}
