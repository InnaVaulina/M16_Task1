using System;
using System.Collections.Generic;
using System.Threading;


namespace Garden_Library
{
    public class Human
    {
        string name;
        List<Feeder> listFeeder;
        public string Name { get { return name; } }

        public List<Feeder> ListFeeder { get { return listFeeder; } }

        public Human(string name, List<Feeder> feeders) 
        {
            this.name = name;
            listFeeder = feeders;
        }

        public void HumanRun() 
        {
            Random rnd = new Random();
            int count;

            foreach(Feeder feeder in listFeeder) 
            {
                count = 0;
                do
                {

                    Thread.Sleep(100);
                    count++;
                } while (count < rnd.Next(1, 6));
                lock (feeder) 
                {
                    feeder.FillFood(rnd.Next(5, 20));
                    lock (Print.ConsoleWriterLock)
                        Print.WriteLine($"{this.Name}: Кормушка {feeder.ID} наполнена, внутри {feeder.Food} зернышк!");
                } 
            }
                       
        }
    }
}
