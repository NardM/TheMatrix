using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;
using System.IO;

namespace MatrixExecutor
{
    public enum FunctionName
    {
        Save,
        Print,
        Load,
        NotSpecified
    }
    class FunctionCall : Operator
    {
        public FunctionName FunctionName { get; private set; }
        public string Assigned { get; private set; }
        public List<string> ArgsList { get; private set; }

        public FunctionCall()
        {
            base.OperatorType = OperatorType.FunctionCall;
        }
        public FunctionCall(string assigned, List<string> argsList, FunctionName operationType, int lineNumber)
            : this ()
        {
            Assigned = assigned;
            ArgsList = new List<string>(argsList);
            FunctionName = operationType;
            base.LineNumber = lineNumber;
        }

        static FunctionName GetFunctionName(string name)
        {
            switch (name)
            {
                case "save":
                    return FunctionName.Save;
                case "print":
                    return FunctionName.Print;
                case "load":
                    return FunctionName.Load;
                default:
                    return FunctionName.NotSpecified;
            }
        }
        public static Operator Parse(string oper, int num)
        {
            int indexOfBracket = oper.LastIndexOf('(');
            FunctionName functionName = GetFunctionName(oper.Substring(0, indexOfBracket));
            if (functionName == FunctionName.NotSpecified)
                throw new FormatException("Несуществующая функция");
            string assigned;
            
            List<string> argsList = new List<string>();
            FunctionCall operation;
            int indexOfBracketClose = oper.LastIndexOf(')');
            if (indexOfBracketClose == -1)
                throw new FormatException("Отсутствует закрывающаяся скобка");
            if (oper.Contains(','))
            {
                int indexOfComma = oper.LastIndexOf(',');

                assigned = oper.Substring(indexOfBracket + 1, indexOfComma - indexOfBracket - 1);
                if (IsUnacceptableIdentifier(assigned))
                    throw new FormatException("Ошибка в задании цели присваивания (неверный идентификатор)");

                int indexOfQuotesR = oper.LastIndexOf('\"');
                if (indexOfQuotesR == -1)
                    throw new FormatException("Ошибка в задании пути файла (отсутствует правые кавычки)");
                int indexOfQuotesL = oper.LastIndexOf('\"', indexOfQuotesR - 1);
                if (indexOfQuotesL == -1)
                    throw new FormatException("Ошибка в задании пути файла (отсутствует левые кавычки)");

                string arg = oper.Substring(indexOfQuotesL + 1, indexOfQuotesR - indexOfQuotesL - 1);
                if (arg == null)
                    throw new FormatException("Пустая строка пути файла");
                argsList.Add(arg);
            }
            else
            {
              

                assigned = oper.Substring(indexOfBracket + 1, indexOfBracketClose - indexOfBracket - 1);
                if (IsUnacceptableIdentifier(assigned))
                    throw new FormatException("Ошибка в задании цели присваивания (неверный идентификатор)");
            }

            operation = new FunctionCall(assigned, argsList, functionName, num);
            argsList.Clear();
            return operation;
        }

        

        public override void DoIt(Dictionary<string, Matrix> matrixes)
        {
            Matrix assigned = null;
            string path;
            switch (FunctionName)
            {
                case (FunctionName.Load):
                    path = ArgsList.ElementAt(0);
                    try
                    {
                        assigned = Matrix.ReadFromTextFile(path);
                    }
                    catch (FormatException e)
                    {
                        base.InvokeEventPerformExeptions(e.Message, LineNumber);
                        return;
                    }
                    catch (NullReferenceException)
                    {
                        base.InvokeEventPerformExeptions("Файла " + path + " не существует", LineNumber);
                        return;
                    }
                    if (matrixes.ContainsKey(Assigned))
                    {
                        matrixes.Remove(Assigned);
                    }
                    matrixes.Add(Assigned, assigned);
                    base.InvokeEventResultHendler("#");
                    break;
                case (FunctionName.Print):
                    if (!matrixes.TryGetValue(Assigned, out assigned))
                    {
                        base.InvokeEventPerformExeptions("Нет матрицы с названием " + Assigned, LineNumber);
                        return;
                    }
                    Matrix.Print(assigned);
                    base.InvokeEventResultHendler("#");
                    base.InvokeEventResultHendler(assigned.ToString());
                    break;
                case (FunctionName.Save):
                    path = ArgsList.ElementAt(0);
                    if (!matrixes.TryGetValue(Assigned, out assigned))
                    {
                        base.InvokeEventPerformExeptions("Нет матрицы с названием " + Assigned, LineNumber);
                        return;
                    }
                    Matrix.WriteInTextFile(path, assigned);
                    base.InvokeEventResultHendler("#");
                    base.InvokeEventResultHendler("Матрица " + Assigned + " сохранена в файл");
                    break;
            }
        }
    }
}
