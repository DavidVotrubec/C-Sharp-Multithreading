using System;
using System.Threading;

namespace Multithreading
{    
    public class Example
    {
        public int NumberA;
        public int NumberB;

        Random rnd = new Random();

        static readonly object Object = new object();

        public void DivideRandomNumbers()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock(Object) { 
                    // initialize variables
                    NumberA = rnd.Next(1, 100);
                    NumberB = rnd.Next(1, 100);

                    // This is a problematic line, because we can get DivisionByZero excpetion
                    // when this code is accessed by multiple threads at the same time
                    int result = NumberA / NumberB;

                    // reset variables
                    NumberA = 0;
                    NumberB = 0;
                }
            }
        }
    }

    public class Program
    {
        // This object is shared between threads
        static Example exampleInstance = new Example();

        public static void Main()
        {
            // invoke the same method from another thread 
            Thread thread1 = new Thread(exampleInstance.DivideRandomNumbers);
            thread1.Start();
            // and from the main thread
            exampleInstance.DivideRandomNumbers();


            // Choose random action
            for (int i = 0; i < 10; i++)
            {
                ChooseAction();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Choose Different Action on the fly
        /// and pass it to newly created Thread
        /// </summary>
        private static void ChooseAction()
        {
            Action action1 = () => { Console.WriteLine("huh, this is action 1 ");};
            Action action2 = () => { Console.WriteLine("huh, this is action 2, something else "); };
            Action actionToCall = null;
            Random rnd = new Random();

            actionToCall = rnd.Next(0, 20) > 10 ? action1 : action2;

            ThreadStart start = new ThreadStart(actionToCall);
            Thread thread = new Thread(start);
            thread.Start();
        }
    }
}
