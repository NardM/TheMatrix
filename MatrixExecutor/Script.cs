using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;
using System.IO;
using MatrixExecutor.Exeptions;


namespace MatrixExecutor
{
    public class Script
    {
        public List<Operator> Operators { get; private set; }
        public Dictionary<string, Matrix> Matrixes { get; set; }

        public Script()
        {
            Operators = new List<Operator>();
            Matrixes = new Dictionary<string, Matrix>();
        }

        bool isFunctionCall(string oper)
        {
            return !oper.Contains('=') && oper.Contains('(');
        }
        bool isUnaryOperator(string oper)
        {
            return oper.Contains('\'') | oper.Contains('^');
        }
        bool isBinaryOperator(string oper)
        {
            return oper.Contains('+') | oper.Contains('*') | oper.Contains('-');
        }

        public void SplitFile(string path)
        {
            StreamReader file = new StreamReader(path);
            Operator currOper = new Operator();
            string currOperString;
            int lineNumber = 1;
            do
            {
                try
                {
                    currOperString = file.ReadLine();
                    currOperString = currOperString.Replace(" ", string.Empty);
                    currOperString = currOperString.Replace("\t", string.Empty);
                    currOperString = currOperString.Replace("\r", string.Empty);
                
                    if (isFunctionCall(currOperString))
                    {
                        currOper = FunctionCall.Parse(currOperString, lineNumber);
                    }
                    else if (isUnaryOperator(currOperString))
                    {
                        currOper = UnaryOperator.Parse(currOperString, lineNumber);
                    }
                    else if (isBinaryOperator(currOperString))
                    {
                        currOper = BinaryOperator.Parse(currOperString, lineNumber);
                    }
                    else
                    {
                        exceptionInDisassemble("Несуществующий оператор", lineNumber);
                    }
                } catch(FormatException e){
                    exceptionInDisassemble(e.Message, lineNumber);
                } catch(Exception e)
                {
                    exceptionInDisassemble(e.Message, 0);
                }
                currOper.packetExeptionsHendler += exceptionInPerform;
                currOper.packetResultHendlers += ResultHendler;
                Operators.Add(currOper);
                ++lineNumber;
                continue;
            } while (!file.EndOfStream);
            file.Close();
        }

        public void Run()
        {
            foreach (Operator oper in Operators)
            {
                oper.DoIt(Matrixes);
            }
        }

        // Событие, для обработки ответов
        public delegate void InvokeResultHendler(string result);
        public event InvokeResultHendler packetResultHendlers;

        // Событие для вызова синтаксических ошибок
        public delegate void InvokSintaxExeption(SintaxExeption e);
        public event InvokSintaxExeption packetSintaxExeptions;

        // Событие для вызова ошибок выполнения
        public delegate void InvokDoitExeption(PerformExeption e);
        public event InvokDoitExeption packetPerformExeptions;

        // Обрабатывает результаты, полученные от методов doit (подпищик)
        private void ResultHendler(string result)
        {
            if (result == "#")
            {
                packetResultHendlers(result + Operators.Count);
            }
            else
            {
                packetResultHendlers(result);
            }
        }

        // Методы для упаковки информации об ошибках в классы-обертки PerformExeption и SintaxExeption
        public void exceptionInDisassemble(string name_of_ex, int number)
        {
            string answer = "";
            answer = "syntaxis exeption in " + number + ": " + name_of_ex;
            SintaxExeption ex = new SintaxExeption(answer, number);
            packetSintaxExeptions(ex);
        }

        public void exceptionInPerform(string description, int number)
        {
            string answer = "";
            answer = "perform exeption in " + number + ":" + description;
            PerformExeption ex = new PerformExeption(answer, number);
            packetPerformExeptions(ex);
        }
    }
}
