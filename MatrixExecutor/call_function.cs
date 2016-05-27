using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;
using System.IO;
namespace MatrixExecutor
{
    class call_function : Operator
    {
        static public List<string> all_types_function = new List<string>("save,load,print".Split(','));
        public string name_of_function;
        public string assign;
        public List <string> list_arg;
        public call_function(string assign, string name_fun, List<string> list_argum, string type_operation, int numberOfLine) {
            this.assign = assign;
            name_of_function = name_fun;
            list_arg = list_argum;
            base.type_operator = type_operation;
            base.nomber_of_string_in_file = numberOfLine;
        }
        public override bool do_it(Dictionary<string, int[,]> dictionary)
        {
            bool flag = true;
            FileStream file = new FileStream(list_arg.ElementAt(0), FileMode.Open, FileAccess.ReadWrite);
           
            int[,] C;            
            dictionary.TryGetValue(assign, out C);
            Matrix C1 = new Matrix(C);
            if (type_operator.Equals("save"))
            {
                Matrix.Print_in_file(file, C1);
                return flag;
            }
            if (type_operator.Equals("load"))
            {
                Matrix matrix = Matrix.Create_matrix(ref file);
                dictionary.Remove(assign);
                dictionary.Add(assign, matrix.matrix);
                return flag; 
            }
            if (type_operator.Equals("print"))
            {
                Matrix.Print_in_file(file, C1);
                return flag;
            }
            return flag = false;
        }
        
    }
}
