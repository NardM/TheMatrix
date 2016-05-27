using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class Matrix
    {
        public int n, m;

        public int[,] matrix;

        public Matrix(int n, int m)
        {
            this.n = n;
            this.m = m;
            this.matrix = new int[n, m];
        }

        public Matrix() { }

        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(' ');

                } Console.WriteLine();
            }
        }

        public static Matrix Print_in_file(string path, Matrix C)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            StreamWriter binOut = new StreamWriter(file);
            binOut.Write(C.n);
            binOut.Write(' ');
            binOut.Write(C.m);
            binOut.WriteLine();
            for (int i = 0; i < C.n; i++)
            {
                for (int j = 0; j < C.m; j++)
                {
                    binOut.Write(C.matrix[i, j]);
                    binOut.Write(' ');
                }
                binOut.WriteLine();
            }
            binOut.Close();
            file.Close();
            return C;
        }

        public static Matrix Generate_Random_Matrix(int n, int m)
        {
            //int n, m;
            //Console.WriteLine("n = ");
            //n = Convert.ToInt32(Console.ReadLine());
           //Console.WriteLine(" m = ");
            //m = Convert.ToInt32(Console.ReadLine());
            Matrix Big_matrix;
            Big_matrix = new Matrix();
            Big_matrix.matrix = new int[n, m];
            Big_matrix.n = n;
            Big_matrix.m = m;
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Big_matrix.matrix[i, j] = rand.Next(0, 10);
                }
            }
            return Big_matrix;
        }

        public static Matrix Create_matrix(string path)
        {
            StreamReader file = new StreamReader(path);

            string line = file.ReadLine();
            line.Trim();
            string[] splitLine = line.Split(' ');
            Matrix new_matrix;
            new_matrix = new Matrix();
            int n = Int32.Parse(splitLine[0]);
            int m = Int32.Parse(splitLine[1]);

            new_matrix.matrix = new int[n, m];
            new_matrix.n = n;
            new_matrix.m = m;
            for (int i = 0; i < n; i++)
            {
                line = file.ReadLine();
                line.Trim();
                splitLine = line.Split(' ');
                for (int j = 0; j < m; j++)
                {
                    new_matrix.matrix[i, j] = Int32.Parse(splitLine[j]);
                }
            }
            return new_matrix;
        }

        static public Matrix Add(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.n, B.m);
            if ((A.n == B.n) && (A.m == B.m))
            {
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = 0; j < A.m; j++)
                    {
                        C.matrix[i, j] = A.matrix[i, j] + B.matrix[i, j];
                    }
                }
                return C;
            }
            else { throw new System.ArgumentException("Ошибка в параметрах"); }
        }

        static public Matrix Multi(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.n, B.m);
            if (A.m == B.n)
            {
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = 0; j < A.m; j++)
                        for (int k = 0; k < B.m; k++)
                        {
                            C.matrix[i, j] += A.matrix[i, k] * B.matrix[k, j];
                        }
                }
            return C;
            }
            else { throw new System.ArgumentException();}
            
        }

        static public Matrix Sub(Matrix A, Matrix B)
        {

            Matrix C = new Matrix(A.n, B.m);
            if ((A.n == B.n) && (A.m == B.m))
            {
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = 0; j < A.m; j++)
                    {
                        C.matrix[i, j] = A.matrix[i, j] - B.matrix[i, j];
                    }
                }
                return C;
            }
            else { throw new System.ArgumentException("Ошибка в параметрах"); }
        }

        static public Matrix Trans(Matrix A)
        {

            if (A.n == A.m)
            {
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = i; j < A.m; j++)
                    {
                        int temp;
                        temp = A.matrix[i, j];
                        A.matrix[i, j] = A.matrix[j, i];
                        A.matrix[j, i] = temp;
                    }
                }

                return A;
            }
            else { throw new System.ArgumentException("ошибка в параметрах"); }

        }

        public static bool operator ==(Matrix A, Matrix B)
        {
            bool flag = true;
            if ((A.n == B.n) && (A.m == B.m))
            {
                for (int i = 0; i < A.n; i++)
                {
                    if (flag == true)
                    {
                        for (int j = 0; j < A.m; j++)
                        {
                            if (A.matrix[i, j] == B.matrix[i, j]) flag = true;
                            else { flag = false; break; }
                        }
                    }

                }
            }
            return flag;
        }

        public static bool operator !=(Matrix A, Matrix B)
        {
            return !(A == B);

        }

        public static Matrix operator +(Matrix A, Matrix B)
        {

            return Add(A, B);
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            return Sub(A, B);
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            return Multi(A, B);
        }

        public static Matrix operator *(Matrix A, int x) {
            return Multi_on_nomber(A, x);
        }

        public static Matrix Multi_on_nomber(Matrix A, int x){
        
            Matrix C = new Matrix(A.n, A.m);
            
                for (int i = 0; i < A.n; i++)
                {
                    for (int j = 0; j < A.m; j++)
                        {
                            C.matrix[i, j] = A.matrix[i, j] * x;
                        }      
                    }
            return C;
        }
        public static Matrix Add_nomber(Matrix A, int x)
        {
            x = 1;
            Matrix C = new Matrix(A.n, A.m);

            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    C.matrix[i, j] = A.matrix[i, j] + x;
                }
            }
            return C;
        }
        public static Matrix Sub_nomber(Matrix A, int x)
        {
            x = 1;
            Matrix C = new Matrix(A.n, A.m);

            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    C.matrix[i, j] = A.matrix[i, j] - x;
                }
            }
            return C;
        }

        public void Write_bynary(String path, Matrix matr)
        {
            FileStream file = new FileStream(path, FileMode.CreateNew);
            BinaryWriter binOut = new BinaryWriter(file);
            binOut.Write(matr.n);
            binOut.Write(matr.m);
            for (int i = 0; i < matr.n; i++)
            {
                for (int j = 0; j < matr.m; j++)
                {
                    binOut.Write(matr.matrix[i, j]);
                }
            }
            binOut.Close();
            file.Close();
        }

        public Matrix Read_bynary(String path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            BinaryReader binIn = new BinaryReader(file);
            int n = binIn.ReadInt32();

            int m = binIn.ReadInt32();
            Matrix matrOut = new Matrix(n, m);
            for (int i = 0; i < matrOut.n; i++)
            {
                for (int j = 0; j < matrOut.m; j++)
                {
                    matrOut.matrix[i, j] = binIn.ReadInt32();
                }
            }
            binIn.Close();

            file.Close();
            return matrOut;
        }
    }
}
