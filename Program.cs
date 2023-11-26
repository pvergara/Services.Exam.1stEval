using System.Diagnostics;

namespace Services.Exam._1stEval
{
    internal abstract class Program
    {
        public static Process[] threads = Process.GetProcesses();
        public static bool mustWait = false;

        private static void Main()
        {
            var mustExit = false;

            var threadDrone1 = new Thread(() => { Drones.FlyDrone(1, 5); });
            threadDrone1.Start();
            var threadDrone2 = new Thread(() => { Drones.FlyDrone(2, 7); });
            threadDrone2.Start();
            while (!mustExit)
            {
                if (!threadDrone1.IsAlive && !threadDrone2.IsAlive)
                {
                    return;
                }
                if (Console.KeyAvailable) //if there’s a key in keyboard’s buffer
                {
                    var key = Console.ReadKey(true);
                    switch (key.KeyChar)
                    {
                        case 'p':
                        
                            PauseThread();
                            break;

                        case 'c':
                            ResumeThread();
                            break;
                        
                        case '1':
                            Drones.FinalizeDrone1 = true;
                            break;
                        case '2': //finalize drone 2
                            Drones.FinalizeDrone2 = true;
                            break;
                        case 'o':
                            mustExit = true;
                            break;
                        case 'i':
                            Drones.ExceptionControl(Drones.RandomInfo);
                            break;
                    }
                }
            }
        }

        public static void PauseThread()
        {
            
            lock (Drones.l)
            {
                mustWait = true;
                
            }
        }

        public static void ResumeThread()
        {
            
            lock (Drones.l)
            {
                mustWait = false;
                Monitor.PulseAll(Drones.l);
                
            }
        }
    }
}