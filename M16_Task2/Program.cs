using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Garden_Library;

namespace M16_Task2
{
    internal class Program
    {
        static async Task HumanRun(Human human)
        {
            lock (Print.ConsoleWriterLock)
                Print.WriteLine($"Задача_{Task.CurrentId} запущена. {human.Name} двигается.");

            await Task.Run(() => human.HumanRun());


            lock (Print.ConsoleWriterLock)
                Print.WriteLine($"Задача_{Task.CurrentId} выполнена. {human.Name} остановилась.");
        }

        static async Task BirdRun(Bird bird)
        {
            lock (Print.ConsoleWriterLock)
                Print.WriteLine($"Задача_{Task.CurrentId} запущена. {bird.Name} двигается.");

            await Task.Run(() => bird.BirdIsAlive());

            lock (Print.ConsoleWriterLock)
            {
                if (bird.NeedForFood > 0)
                    Print.Text2 = $"Птичка голодная!";
                else
                    Print.Text2 = $"Птичка сытая!";
                Print.WriteLine($"Задача_{Task.CurrentId} выполнена. {bird.Name} остановилась.");
            }


        }

        async static Task Main(string[] args)
        {
            Print.WriteLine("Основная задача запущена.");

            List<Feeder> listFeeder = new List<Feeder>(5);
            List<Bird> listBird = new List<Bird>(8);
            var tasks = new Task[9];
            

            Random rnd = new Random();

            for (int i = 0; i < 5; i++)
            {
                listFeeder.Add(new Feeder(i, rnd.Next(1, 3)));
            }

            for (int i = 0; i < 8; i++)
            {
                listBird.Add(new Bird($"Птичка {i}", 1, rnd.Next(5, 20)));
                foreach (Feeder item in listFeeder)
                    item.FeederFillNotify += listBird[i].OnFeederFilling;
            }
            Human human = new Human("Маша", listFeeder);

            tasks[8] = HumanRun(human);
            for (int i = 0; i < 8; i++) 
            {
                tasks[i] = BirdRun(listBird[i]);
            }


            for (int i = 0; i < 9; i++)
            {
                await tasks[i];
            }

            Print.WriteLine("Основная задача выполнена.");
            Console.ReadLine();
        }
    }
}
