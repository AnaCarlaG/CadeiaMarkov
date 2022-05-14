using System;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace CadeiaMarkov
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Input", "entrada.txt");
            double[,] OutraMatriz = Read_CSV(path);


            static double[,] Read_CSV(string arquivo)
            {
                StreamReader reader = new StreamReader(arquivo);
                int linha = int.Parse(reader.ReadLine());
                int coluna = int.Parse(reader.ReadLine());
                double[,] Matriz = new double[linha, coluna];
                for (int i = 0; i < linha; i++)
                {
                    for (int j = 0; j < coluna; j++)
                    {
                        Matriz[i, j] = Convert.ToDouble(reader.ReadLine());
                    }
                }
                return InvertMatriz(Matriz, linha, coluna);
            }

            static double[,] InvertMatriz(double[,] matriz, int linhas, int colunas)
            {
                double[,] novaMatriz = new double[linhas, colunas];
                double value = 0, max = 0;
                int l = 0, c = 0;
                List<double> lista = new List<double>();
                for (int linha = 0; linha < linhas; linha++)
                {
                    for (int coluna = 0; coluna < colunas; coluna++)
                    {
                        novaMatriz[linha, coluna] = matriz[linha, coluna];
                        value += InverterValores(novaMatriz[linha, coluna]);
                        if (matriz[linha, coluna] == 0 && linha == coluna)
                        {
                            l = linha;
                            c = coluna;
                        }
                        lista.Add(Math.Abs(value));
                    }
                    max = lista.Max();
                    novaMatriz[l, c] = value;
                    value = 0;
                }
                var i = DividiMatriz(novaMatriz,max);
                return i;
            }

            static double InverterValores(double value)
            {
                return value * -1;
            }
            static double[,] DividiMatriz(double[,] mat, double max)
            {
                var len = mat.GetLength(1);
                for (int linha = 0; linha < len; linha++)
                {
                    for (int coluna = 0; coluna < len; coluna++)
                    {
                        mat[linha, coluna] = max > 0 ? mat[linha, coluna] / max : mat[linha, coluna];
                    }
                }
                return mat;
            }
        }
    }
}
