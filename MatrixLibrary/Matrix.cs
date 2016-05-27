using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class Matrix
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        int[,] matrix;
        public int this[int row, int column]
        {
            get { return matrix[row, column]; }
            protected set { matrix[row, column] = value; }
        }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            matrix = new int[rows, columns];
        }
        public Matrix(int[,] a)
        {
            matrix = a;
            Rows = a.GetLength(0);
            Columns = a.GetLength(1);
        }
        //public Matrix();
        public static Matrix GenerateRandomMatrix(int rows, int columns, int minValue, int maxValue)
        {
            Matrix a = new Matrix(rows, columns);
            a.Rows = rows;
            a.Columns = columns;
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    a[i, j] = rand.Next(minValue, maxValue);
            return a;
        }

        static bool SameSize(Matrix a, Matrix b)
        {
            return (a.Rows == b.Rows) && (a.Columns == b.Columns);
        }
        static public Matrix Add(Matrix a, Matrix b)
        {
            if (SameSize(a, b))
            {
                Matrix c = new Matrix(a.Rows, a.Columns);
                for (int i = 0; i < a.Rows; i++)
                    for (int j = 0; j < a.Columns; j++)
                        c[i, j] = a[i, j] + b[i, j];
                return c;
            }
            else { throw new System.ArgumentException("Матрицы разной размерности"); }
        }
        static public Matrix Multi(Matrix a, Matrix b)
        {
            if (a.Columns == b.Rows)
            {
                Matrix c = new Matrix(a.Rows, b.Columns);
                for (int i = 0; i < a.Rows; i++)
                    for (int j = 0; j < b.Columns; j++)
                        for (int k = 0; k < b.Rows; k++)
                            c[i, j] += a[i, k] * b[k, j];
                return c;
            }
            else { throw new System.ArgumentException(); }
        }
        public static Matrix Multi(Matrix a, int x)
        {
            Matrix b = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    b[i, j] = a[i, j] * x;
            return b;
        }
        public static Matrix Multi(int x, Matrix a)
        {
            return Multi(a, x);
        }
        static public Matrix Sub(Matrix a, Matrix b)
        {
            if (SameSize(a, b))
            {
                Matrix c = new Matrix(a.Rows, a.Columns);
                for (int i = 0; i < a.Rows; i++)
                    for (int j = 0; j < a.Columns; j++)
                        c[i, j] = a[i, j] - b[i, j];
                return c;
            }
            else { throw new System.ArgumentException("Матрицы разной размерности"); }
        }
        static public Matrix Trans(Matrix a)
        {
            Matrix b = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < b.Rows; i++)
            {
                for (int j = i; j < b.Columns; j++)
                {
                    b[i, j] = a[j, i];
                    b[j, i] = a[i, j];
                }
            }
            return b;
        }

        public override string ToString()
        {
            string str = String.Empty;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    str += matrix[i, j] + " ";
                }
                str += Environment.NewLine;
            }
            return str;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            bool equal = SameSize(a, b);
            for (int i = 0; equal && (i < a.Rows); i++)
                for (int j = 0; equal && (j < a.Columns); j++)
                    equal = a[i, j] == b[i, j];
            return equal;
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);

        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            return Add(a, b);
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            return Sub(a, b);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            return Multi(a, b);
        }
        public static Matrix operator *(Matrix a, int x)
        {
            return Multi(a, x);
        }
        public static Matrix operator *(int x, Matrix a)
        {
            return Multi(a, x);
        }

        static int[] SplitNextLine(string line)
        {
            line.Trim();
            return Array.ConvertAll(line.Split(' '), Int32.Parse);
        }
        public static Matrix ReadFromTextFile(StreamReader file)
        {
            int[] splittedLine = SplitNextLine(file.ReadLine());
            Matrix newMatrix = new Matrix(splittedLine[0], splittedLine[1]);
            for (int i = 0; i < newMatrix.Rows; i++)
            {
                splittedLine = SplitNextLine(file.ReadLine());
                for (int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i, j] = splittedLine[j];
            }
            file.Close();
            return newMatrix;
        }
        public static Matrix ReadFromTextFile(String path)
        {
            if (!File.Exists(path))
            {
                throw new FormatException("Файла с названием " + path + " не существует");
            }
            else
            {
                return ReadFromTextFile(new StreamReader(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read)));
            }
        }
        public static Matrix ReadFromBinaryFile(BinaryReader file)
        {
            int n = file.ReadInt32();
            int m = file.ReadInt32();
            Matrix newMatrix = new Matrix(n, m);
            for (int i = 0; i < newMatrix.Rows; i++)
                for (int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i, j] = file.ReadInt32();
            file.Close();
            return newMatrix;
        }
        public static Matrix ReadFromBinaryFile(String path)
        {
            return ReadFromBinaryFile(new BinaryReader(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read)));
        }
        public static void WriteInTextFile(StreamWriter file, Matrix a)
        {
            file.Write(a.Rows);
            file.Write(' ');
            file.Write(a.Columns);
            file.WriteLine();
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    file.Write(a[i, j] + ' ');
                }
                file.WriteLine();
            }
            file.Close();
        }
        public static void WriteInTextFile(String path, Matrix a)
        {
            WriteInTextFile(new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)), a);
        }
        public static void WriteInBinaryFile(BinaryWriter file, Matrix a)
        {
            file.Write(a.Rows);
            file.Write(a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    file.Write(a[i, j]);
            file.Close();
        }
        public static void WriteInBinaryFile(String path, Matrix a)
        {
            WriteInBinaryFile(new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)), a);
        }
        public static void Print(Matrix a)
        {
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}