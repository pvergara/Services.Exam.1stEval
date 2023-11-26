using System.Text.RegularExpressions;

namespace Services.Exam._1stEval
{
    internal class Drones
    {
        public static readonly object l = new();
        static readonly object gate = new();
        public static bool FinalizeDrone2 { get; set; }
        public static bool FinalizeDrone1 { get; set; }

        public static void RandomInfo()
        {
            Console.SetCursorPosition(1, 10);
            Console.WriteLine(new string(' ', 1000));

            var item = Program.threads[new Random().Next(Program.threads.Length)];


            Console.SetCursorPosition(1, 10);
            Console.WriteLine(string.Format("{0} {1} {2}", Regex.Replace(item.ProcessName.ToString(), "(?<=^.{15}).*", "..."),
                item.Id.ToString(),
                item.Threads.Count.ToString()) + "\t");

            for (var index = 0; index < ((item.Modules.Count >= 10) ? 10 : item.Modules.Count); index++)
            {
                var processModule = item.Modules[index];
                Console.WriteLine(processModule.ModuleName);
            }
        }

        public delegate void MyDelegate();

        public static void ExceptionControl(MyDelegate myDelegate)
        {
            try
            {
                myDelegate();
            }
            catch (Exception e)
            {
                Console.Write("Panic Error!!" + e.Message);
            }
        }

        public static void FlyDrone(int droneNumber, int row)
        {
            var speed = new Random().Next(100, 200);
            var format = droneNumber==1?"*":"+";
            for (var i = 1; i <= 20; i++)
            {
                Thread.Sleep(speed);

                if (droneNumber == 1 && FinalizeDrone1)
                    return;
                if (droneNumber == 2 && FinalizeDrone2)
                    return;
                        lock (l)
                        {
                            while (Program.mustWait)
                            {
                                Thread.Sleep(100);
                                Monitor.Wait(l);
                            }

                            Console.SetCursorPosition(i, row);
                            Console.Write(format);        
                        }
                        
                    
                    
                
            }
            lock (gate)
            {
                for (var i = 21; i <= 30; i++)
                {
                    if (droneNumber == 1 && FinalizeDrone1)
                        return;
                    if (droneNumber == 2 && FinalizeDrone2)
                        return;
                    Thread.Sleep(speed);
                    Console.SetCursorPosition(i, 6);
                    Console.Write(format);
                }
            }
        }
    }
}
