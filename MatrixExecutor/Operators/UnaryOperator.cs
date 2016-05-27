using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;
using System.IO;

namespace MatrixExecutor
{
    public enum UnaryOperationType
    {
        Transpose,
        Orthogonal,
        NotSpecified
    }
    class UnaryOperator : Operator
    {
        //static public List<string> all_types_unary = new List<string>("^,'".Split(','));
        public UnaryOperationType OperationType { get; private set; }
        public string Assigned { get; private set; }
        public string Operand { get; private set; }

        static UnaryOperationType GetUnaryOperationType(char sign)
        {
            switch (sign)
            {
                case '\'':
                    return UnaryOperationType.Transpose;
                case '^':
                    return UnaryOperationType.Orthogonal;
                default:
                    return UnaryOperationType.NotSpecified;
            }
        }
        public static Operator Parse(string oper, int num)
        {
            int indexOfEquals = oper.LastIndexOf("=");

            string assigned = oper.Substring(0, indexOfEquals);
            if (IsUnacceptableIdentifier(assigned))
                throw new FormatException("Ошибка в задании цели присваивания (неверный идентификатор)");

            int indexOfSign = oper.Length - 1;
            UnaryOperationType operationType = GetUnaryOperationType(oper.ElementAt(indexOfSign));
            if (operationType == UnaryOperationType.NotSpecified)
                throw new FormatException("Несуществующая операция");

            string operand = oper.Substring(indexOfEquals + 1, (indexOfSign - indexOfEquals - 1));
            if (IsUnacceptableIdentifier(assigned))
                throw new FormatException("Ошибка в задании операнда (неверный идентификатор)");

            UnaryOperator operation = new UnaryOperator(assigned, operand, operationType, num);
            return operation;
        }

        public UnaryOperator()
        {
            base.OperatorType = OperatorType.Unary;
        }
        public UnaryOperator(string assigned, string operand, UnaryOperationType operationType, int lineNumber)
            : this()
        {
            Assigned = assigned;
            Operand = operand;
            OperationType = operationType;
            base.LineNumber = lineNumber;
        }

        public override void DoIt(Dictionary<string, Matrix> matrixes)
        {
            Matrix assigned, operand;
            if (!matrixes.TryGetValue(Operand, out operand))
            {
                base.InvokeEventPerformExeptions("Нет матрицы с названием " + Operand, LineNumber);
                return;
            }
            switch (OperationType)
            {
                case (UnaryOperationType.Transpose):
                    assigned = Matrix.Trans(operand);
                    if (matrixes.ContainsKey(Assigned))
                    {
                        matrixes.Remove(Assigned);
                    }
                    matrixes.Add(Assigned, assigned);
                    base.InvokeEventResultHendler("#");
                    break;
                case (UnaryOperationType.Orthogonal):
                    assigned = operand;
                    if (matrixes.ContainsKey(Assigned))
                    {
                        matrixes.Remove(Assigned);
                    }
                    matrixes.Add(Assigned, assigned);
                    base.InvokeEventResultHendler("#");
                    break;
            }
        }
    }
}