using System;

namespace Garden_Library
{
    /// <summary>
    /// эта вся муть, чтобы понять как синхронизировать консоль
    /// </summary>
    public static class Print
    {
        static string text2 = "";
        public static readonly object ConsoleWriterLock = new object();

        public static string Text2
        {
            get { return text2; }
            set
            {
                if (value == "" && text2 != "")
                {
                    text2 = value;
                }

                if (value != "" && text2 == "")
                {
                    text2 = value;
                }
            }
        }


        public static void WriteLine(string text)
        {
            Console.Write(text);
            for (int i = 0; i < 10; i++) Console.Write(".");
            Console.WriteLine(Text2);
            Text2 = "";
        }
    }
}
