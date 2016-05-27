using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixExecutor.Exeptions
{
    public class SintaxExeption : FormatException
    {
        public SintaxExeption(string message, int numberLine)
            : base(message)
        {
            lineNumber = numberLine;
        }

        private int lineNumber;
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }
    }
}
