using System;
using System.Threading; //многопоточность

namespace ConsoleApplication1
{
    class Token
    {
        public String data;
        public int recipient;

        public Token(string data, int recipient)
        {
            this.data = data;
            this.recipient = recipient;
        }
    }

    class Program
    {
        static object locker = new object();

        static void transfer(int ID_thread, Token token, int count)
        {

            lock (locker)
            {
                if (ID_thread + 1 == token.recipient)
                {
                    Console.WriteLine("Recipient " + ID_thread + " get token. Token`s data: " + token.data);
                }
                else if (ID_thread < token.recipient - 1)
                {
                    Console.WriteLine("Thread " + ID_thread + " send token to next thread");
                }
            }
        }

        static void Main(string[] args)
        {
            Token token = new Token("token", 3);
            int count = 6;
            if (token.recipient > count)
                count = token.recipient;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Create new Thread " + i);
                Thread thread = new Thread(() => transfer(i, token, count));
                thread.Start();
                Thread.Sleep(100);
            }
            Console.ReadLine();
        }
    }
}
