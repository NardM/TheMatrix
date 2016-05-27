using MatrixExecutor.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            Script scr = new Script();
            try
            {
                scr.SplitFile(args[0]);
            }
            catch(SintaxExeption e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (Operator oper in scr.Operators)
            {
                try
                {
                    oper.DoIt(scr.Matrixes);
                    //if (result != String.Empty)
                    //{
                    //    Console.WriteLine(result);
                    //}
                }
                catch (PerformExeption e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
