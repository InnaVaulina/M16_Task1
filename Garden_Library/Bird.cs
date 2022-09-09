using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Garden_Library
{
    public class Bird
    {
        string name;
        float speed;
        int needForFood;
        List<Feeder> feeders;
        IEnumerator<Feeder> feederEnumerator;
        Feeder takeFeeder;
        int state;


        /// <summary>
        /// птичка заняла кормушку
        /// значение: кормушка или null
        /// </summary>
        public Feeder TakeFeeder
        {
            get { return takeFeeder; }
            set { takeFeeder = value; }
        }

        public string Name { get { return name; } }

        public int NeedForFood { get { return needForFood; } }

        public List<Feeder> Feeders { get { return feeders; } }

        public Bird(string name, float speed, int need)
        {
            this.name = name;
            this.speed = speed;
            this.needForFood = need;
            feeders = new List<Feeder>(5);
            feederEnumerator = feeders.GetEnumerator();
            TakeFeeder = null;

        }

        /// <summary>
        /// птичка сидит на веточке
        /// </summary>
        public void Rest()
        {
            lock (Print.ConsoleWriterLock)
                Print.WriteLine($"{name}: Сижу на веточке. Чирик-Чирик! ");
        }

        /// <summary>
        /// птичка кушает
        /// </summary>
        public void Eate()
        {

            if (this.needForFood > 0)
            {
                bool a = false;
                lock (TakeFeeder) a = TakeFeeder.Exploit();
                if (a == true)
                {
                    this.needForFood--;
                    lock (Print.ConsoleWriterLock)
                        Print.WriteLine($"{name}: Мое зернышко!");
                }

            }
            if (TakeFeeder.Food == 0 || this.needForFood == 0)
                this.state = 3;
        }



        /// <summary>
        /// птичка ищет место в кормушке
        /// </summary>
        public void SearchFreeFeeder()
        {
            Feeder x = null;
            if (this.Feeders.Count == 0)
            {
                lock (Print.ConsoleWriterLock)
                    Print.WriteLine($"{name}: Корма нигде нет!");
                this.state = 0;
                return;
            }
            if (this.feederEnumerator.MoveNext())
                x = this.feederEnumerator.Current;
            else
            {
                this.feederEnumerator.Reset();
                x = this.feederEnumerator.Current;
            }
            if (x != null)
            {
                bool a = false;
                lock (x) a = x.LetBird();

                if (a == true)
                {
                    this.TakeFeeder = x;
                    lock (Print.ConsoleWriterLock)
                        Print.WriteLine($"{name}: Я в кормушке {TakeFeeder.ID}!");
                    this.state = 2;
                }
                else
                {
                    lock (Print.ConsoleWriterLock)
                        Print.WriteLine($"{name}: В кормушке {x.ID} места нет!");
                }

            }


        }

        /// <summary>
        /// птичка покидает кормушку
        /// </summary>
        public void LeaveFeeder()
        {

            lock (TakeFeeder) TakeFeeder.BirdFlewAway();
            this.feeders.Remove(TakeFeeder);
            feederEnumerator = feeders.GetEnumerator();


            lock (Print.ConsoleWriterLock)
            {
                if (this.needForFood > 0)
                    Print.Text2 = $"Мне нужно еще {this.needForFood} зернышек!";
                else
                    Print.Text2 = "Я сыт!";
                Print.WriteLine($"{name}: Я вылетел из кормушки {TakeFeeder.ID}! ");
            }
            TakeFeeder = null;

            if (this.needForFood == 0)
            {
                this.feeders.Clear();
                feederEnumerator = feeders.GetEnumerator();
                this.state = 0;
            }
            else this.state = 1;

        }

        /// <summary>
        /// птичка узнала, что кормушка наполнена
        /// </summary>
        /// <param name="feeder"></param>
        public void OnFeederFilling(FeederEventArgs e)
        {
            if (this.needForFood > 0)
            {
                this.feeders.Add(e.Feeder);
                feederEnumerator = feeders.GetEnumerator();
                lock (Print.ConsoleWriterLock)
                    Print.WriteLine($"{name}: Я знаю, что в кормушку {e.Feeder.ID} насыпали корм!");
                if (this.state == 0) this.state = 1;
            }
        }

        public void BirdIsAlive()
        {
            state = 0;
            int count = 0;
            do
            {
                switch (state)
                {
                    case 0:                        // птичка сидит на веточке и поет
                        this.Rest();
                        count++;
                        Thread.Sleep(100);
                        break;
                    case 1:                        // птичка ищет пропитание
                        this.SearchFreeFeeder();
                        count = 0;
                        Thread.Sleep(100);
                        break;
                    case 2:                        // птичка кушает
                        this.Eate();
                        Thread.Sleep(10);
                        break;
                    case 3:                        // птичка вылетает из кормушки
                        this.LeaveFeeder();
                        Thread.Sleep(100);
                        break;

                }

            }
            while (count < 10);

        }

    }
}
