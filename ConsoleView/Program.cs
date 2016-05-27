using MatrixExecutor;
using MatrixExecutor.Exeptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleView
{
    public class Program
    {
        const string temp = "temp.txt";
        public static void Main(string[] args)
        {
            Script script = new Script();
            // Подписка на три вида событий
            script.packetPerformExeptions += performExeptionHendler;
            script.packetSintaxExeptions += sintaxExeptionHendler;
            script.packetResultHendlers += resultHendler;

            //Console.WriteLine("Введите название файла: ");
            //string path = Console.ReadLine();
            if (!File.Exists(args[0]))
            {
                //Console.WriteLine("Указанного файла не существует");
                return;
            }
            try
            {
                script.SplitFile(args[0]);
                script.Run();;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Не известная ошибка " + e.Message);
            }
            //Console.ReadKey();
        }

        private static StreamWriter streamWriter;
        private static int countResult = 0;
        static void resultHendler(string result)
        {
            if (countResult == 0)
            {
                streamWriter = new StreamWriter("tempResult.txt", false);
            }
            else
            {
                streamWriter = new StreamWriter("tempResult.txt", true);
            }
            if (!result.Contains("#"))
            {
                streamWriter.WriteLine(result);
               // Console.WriteLine(result);
            }
            countResult++;
            streamWriter.Close();
        }
        static int countExeption = 0;
        static void performExeptionHendler(PerformExeption e)
        {
            if (countExeption == 0)
            {
                //Console.WriteLine(e.Message);
                countExeption++;
            }
        }

        static void sintaxExeptionHendler(SintaxExeption e)
        {
            //Console.WriteLine(e.Message);
        }
    }
}
