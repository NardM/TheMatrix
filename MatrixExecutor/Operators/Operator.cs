using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;

namespace MatrixExecutor
{
    public enum OperatorType {
        Binary,
        Unary,
        FunctionCall
    }
    public class Operator
    {

        // Делегат для вызова ошибок выполнения
        public delegate void InvokeDoitExeption(string description, int lineNumber);
        public event InvokeDoitExeption packetExeptionsHendler;
        public void InvokeEventPerformExeptions(string description, int lineNumber)
        {
            packetExeptionsHendler(description, lineNumber);
        }

        // Делегат, вызывающийся при получении корректного результата
        public delegate void InvokeResultHendler(string result);
        public event InvokeResultHendler packetResultHendlers;
        public void InvokeEventResultHendler(string result)
        {
            packetResultHendlers(result);
        }

        public virtual void DoIt(Dictionary<string, Matrix> matrixes) { }

        public OperatorType OperatorType { get; protected set; }
        public int LineNumber { get; protected set; }
        protected static bool IsUnacceptableIdentifier(string id)
        {
            try
            {
                return id == null || Char.IsDigit(id[0]);
            }
            catch (IndexOutOfRangeException e)
            {
                return true;
            }
        }
    }
}
