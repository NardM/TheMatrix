using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;
using MatrixExecutor.Exeptions;

namespace MatrixExecutor
{
    public enum BinaryOperationType 
    {
        Add,
        Multi,
        Sub,
        NotSpecified
    }
    class BinaryOperator : Operator
    {
        public BinaryOperationType OperationType { get; private set; }
        public string Assigned { get; private set; }
        public string LeftOperand { get; private set; }
        public string RightOperand { get; private set; }

        static int FindBinaryOpeationSign(string line, out BinaryOperationType type)
        {
            if (line.Contains('+'))
            {
                type = BinaryOperationType.Add;
                return line.LastIndexOf('+');
            }
            else if (line.Contains('-'))
            {
                type = BinaryOperationType.Sub;
                return line.LastIndexOf('-');
            }
            else if (line.Contains('*'))
            {
                type = BinaryOperationType.Multi;
                return line.LastIndexOf('*');
            }
            else
            {
                type = BinaryOperationType.NotSpecified;
                return -1;
            }
        }

        public static Operator Parse(string oper, int num)
        {
            int indexOfEquals = oper.LastIndexOf("=");
            string assigned = "";
            try
            {
                assigned = oper.Substring(0, indexOfEquals);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new FormatException("Отсутствует знак присвоения");
            }
            if (IsUnacceptableIdentifier(assigned))
                throw new FormatException("Ошибка в задании цели присваивания (неверный идентификатор)");

            BinaryOperationType operationType;
            int indexOfSign = FindBinaryOpeationSign(oper, out operationType);
            if (operationType == BinaryOperationType.NotSpecified)
                throw new FormatException("Несуществующая операция");

            string leftOperand = oper.Substring(indexOfEquals + 1, (indexOfSign - indexOfEquals) - 1);
            if (IsUnacceptableIdentifier(leftOperand))
                throw new FormatException("Ошибка в задании левого операнда (неверный идентификатор)");
            string rightOperand = oper.Substring(indexOfSign + 1, (oper.Length - indexOfSign) - 1);
            if (IsUnacceptableIdentifier(rightOperand))
                throw new FormatException("Ошибка в задании правого операнда (неверный идентификатор)");

            BinaryOperator operation = new BinaryOperator(assigned, leftOperand, rightOperand, operationType, num);
            return operation;
        }

        public BinaryOperator()
        {
            base.OperatorType = OperatorType.Binary;
        }
        public BinaryOperator(string assigned, string left, string right, BinaryOperationType operationType, int lineNumber)
            : this()
        {
            Assigned = assigned;
            LeftOperand = left;
            RightOperand = right;
            OperationType = operationType;
            base.LineNumber = lineNumber;
        }


        public override void DoIt(Dictionary<string, Matrix> matrixes)
        {

            Matrix assigned, left, right;
            if(!matrixes.TryGetValue(LeftOperand, out left))
            {
                base.InvokeEventPerformExeptions("Не задана матрица " + LeftOperand, this.LineNumber);
                return;
            }
            
            if(!matrixes.TryGetValue(RightOperand, out right))
            {
                base.InvokeEventPerformExeptions("Не задана матрица " + RightOperand, this.LineNumber);
                return;
            }
            switch (OperationType)
            {
                case (BinaryOperationType.Add):
                    assigned = left + right;
                    if (matrixes.ContainsKey(Assigned))
                    {
                        matrixes.Remove(Assigned);
                    }
                    matrixes.Add(Assigned, assigned);
                    base.InvokeEventResultHendler("#");
                    break;
                case (BinaryOperationType.Sub):
                    assigned = left - right;
                    if (matrixes.ContainsKey(Assigned))
                    {
                        matrixes.Remove(Assigned);
                    }
                    matrixes.Add(Assigned, assigned);
                    base.InvokeEventResultHendler("#");
                    break;
                case (BinaryOperationType.Multi):
                    assigned = left * right;
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
