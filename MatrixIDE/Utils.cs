using MatrixExecutor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixIDE
{
    class Utils
    {
        public const string TEMP_FILE = @"temp.txt";

        public void ScrollToLine(int lineNumber, RichTextBox rtb)
        {
            rtb.SelectionStart = lineNumber;
            rtb.ScrollToCaret();
        }

        public void selectLineWithError(int numberLine, RichTextBox rtb)
        {
            int count = 1;
            int begin = 0;
            int end = 0;
            int letterCount = 0;
            bool beginIsFind = false;
            foreach (char letter in rtb.Text)
            {
                letterCount++;
                if (letter == '\n')
                {
                    if (beginIsFind)
                    {
                        break;
                    }
                    count++;
                }
                if (count == numberLine && !beginIsFind)
                {
                    begin = letterCount - 1;
                    beginIsFind = true;
                }
            }
            end = letterCount - 1;
            rtb.Select(begin, (end - begin));
            rtb.SelectionColor = Color.Red;
            ScrollToLine(begin, rtb);
        }
        
        //Запись в буферный файл
        public void writeTempFile(string source)
        {
            string temp = TEMP_FILE;
            StreamWriter streamWriter = new StreamWriter(temp, false);
            streamWriter.Write(source);
            streamWriter.Close();
            streamWriter.Dispose();
        }

        public string getFileName(string fileName)
        {
            string outTitle = String.Empty;
            foreach (char c in fileName)
            {
                if (c != '\\')
                {
                    outTitle += c;
                }
                else
                {
                    outTitle = String.Empty;
                }
            }
            return outTitle;
        }

        public int extractCountOperators(string str)
        {
            string numStr = str.Substring(1);
            return Int32.Parse(numStr);
        }
    }
}
