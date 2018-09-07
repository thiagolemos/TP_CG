using System;

namespace CGPaint
{
    class Matriz
    {
        public static int[,] Multiplicacao(int[,] a, int[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int p = b.GetLength(0);
            int q = b.GetLength(1);
            int[,] resultado = new int[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    int soma = 0;
                    for (int k = 0; k < p; k++)
                    {
                        soma += a[i, k] * b[k, j];
                    }
                    resultado[i, j] = soma;
                }
            }
            return resultado;
        }

        public static int[,] Multiplicacao(int[,] a, double[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int p = b.GetLength(0);
            int q = b.GetLength(1);
            int[,] resultado = new int[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    double soma = 0;
                    for (int k = 0; k < p; k++)
                    {
                        soma += a[i, k] * b[k, j];
                    }
                    resultado[i, j] = Convert.ToInt32(soma);
                }
            }
            return resultado;
        }

        public static double[,] Multiplicacao(double[,] a, double[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int p = b.GetLength(0);
            int q = b.GetLength(1);
            double[,] resultado = new double[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    double soma = 0;
                    for (int k = 0; k < p; k++)
                    {
                        soma += a[i, k] * b[k, j];
                    }
                    resultado[i, j] = soma;
                }
            }
            return resultado;
        }

        public static int[,] Multiplicacao(double[,] a, int[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int p = b.GetLength(0);
            int q = b.GetLength(1);
            int[,] resultado = new int[m, q];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    double soma = 0;
                    for (int k = 0; k < p; k++)
                    {
                        soma += a[i, k] * b[k, j];
                    }
                    resultado[i, j] = Convert.ToInt32(soma);
                }
            }
            return resultado;
        }

        public static int[,] Reflexao(int[,] a, int[,] b)
        {
            int[,] resultado = new int[3, 1];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        resultado[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return resultado;
        }

        public static int[,] CisalhamentoHorizontal(int[,] a, int[,] b, int f)
        {
            int[,] resultado = new int[3, 1];

            a[1, 0] = f;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        resultado[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return resultado;
        }

        public static int[,] CisalhamentoVertical(int[,] a, int[,] b, int f)
        {
            int[,] resultado = new int[3, 1];

            a[0, 1] = f;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        resultado[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return resultado;
        }
    }
}
