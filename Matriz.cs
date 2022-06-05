using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace CadeiaMarkov
{
    public class Matriz
    {
        public double[,] Read_Matriz(string arquivo)
        {
            StreamReader reader = new StreamReader(arquivo);
            int linha = int.Parse(reader.ReadLine());
            int coluna = int.Parse(reader.ReadLine());
            double[,] matriz = new double[linha, coluna];
            for (int i = 0; i < linha; i++)
            {
                for (int j = 0; j < coluna; j++)
                {
                    matriz[i, j] = Convert.ToDouble(reader.ReadLine());
                }
            }
            return MatrizDiagInvertido(matriz, linha, coluna);
        }
        public double[] Read_Vetor(string arquivo)
        {
            StreamReader reader = new StreamReader(arquivo);
            int linha = int.Parse(reader.ReadLine());
            double[] vetor = new double[linha];
            for (int i = 0; i < linha; i++)
            {
                vetor[i] = Convert.ToDouble(reader.ReadLine());
            }
            return vetor;
        }

        public double[,] MatrizDiagInvertido(double[,] matriz, int linhas, int colunas)
        {
            double[,] novaMatriz = new double[linhas, colunas];
            double value = 0;
            int l = 0, c = 0;
            List<double> lista = new List<double>();
            for (int linha = 0; linha < linhas; linha++)
            {
                for (int coluna = 0; coluna < colunas; coluna++)
                {
                    value += matriz[linha, coluna] * -1;
                    if (matriz[linha, coluna] == 0 && linha == coluna)
                    {
                        l = linha;
                        c = coluna;

                    }
                    lista.Add(Math.Abs(value));
                }
                matriz[l, c] = value;
                value = 0;
            }
            return DivisaoMatriz(matriz, lista.Max());

        }
        public double[,] DivisaoMatriz(double[,] mat, double max)
        {
            var len = mat.GetLength(1);
            for (int linha = 0; linha < len; linha++)
            {
                for (int coluna = 0; coluna < len; coluna++)
                {
                    mat[linha, coluna] = mat[linha, coluna] / max;
                }
            }
            return mat;
        }
        public double[,] Soma(double[,] matriz)
        {
            var length = matriz.GetLength(1);
            double[,] matrizC = new double[length, length];
            for (int linha = 0; linha < length; linha++)
            {
                for (int coluna = 0; coluna < length; coluna++)
                {
                    if (linha == coluna)
                    {
                        matrizC[linha, coluna] = Math.Abs(1 + matriz[linha, coluna]);
                    }
                    else
                    {
                        matrizC[linha, coluna] = Math.Abs(matriz[linha, coluna]);
                    }
                }
            }
            return matrizC;
        }

        public void MultiplicacaoMatrizVetor(double[] vetor, double[,] matriz, double tolerancia)
        {
            var matrizResult = Soma(matriz);
            var len = matrizResult.GetLength(1);
            double[] vetorResultante = new double[len];
            for (int linha = 0; linha < len; linha++)
            {
                for (int coluna = 0; coluna < len; coluna++)
                {
                    vetorResultante[linha] += vetor[coluna] * matrizResult[coluna, linha];
                }
            }
            int iteracao = 1;
            var casasDecimais = tolerancia.ToString().Substring(tolerancia.ToString().LastIndexOf(",") + 1).Length;
            PrintVetor(vetorResultante, casasDecimais);

            double[] vetorAntigo = vetorResultante;
            while (true)
            {
                var converg = false;

                var result = Variacao(vetorAntigo, matrizResult, tolerancia);
                PrintVetor(vetorAntigo, casasDecimais);
                Console.WriteLine("Quantidade iterações: " + iteracao);
                for (int j = 0; j < result.Count(); j++)
                {
                    var sub = result[j] - vetorAntigo[j];
                    var pow = Math.Pow(sub, 2);
                    converg = (Math.Sqrt(pow) < tolerancia) ? true : false;
                }
                vetorAntigo = result;
                iteracao++;

                

                if (converg)
                {
                    break;
                }
            }

        }

        public double[] Variacao(double[] vetorResultante, double[,] matriz, double tolerancia)
        {
            double[] vetorComparativo = new double[vetorResultante.Count()];
            for (int linha = 0; linha < vetorResultante.Count(); linha++)
            {
                for (int coluna = 0; coluna < vetorResultante.Count(); coluna++)
                {
                    vetorComparativo[linha] += vetorResultante[coluna] * matriz[coluna, linha];
                }
            }
            return vetorComparativo;
        }
        public void PrintVetor(double[] vetorResultante, int casasDecimais)
        {
            for (int j = 0; j < vetorResultante.Count(); j++)
            {
                Console.Write(Math.Round(vetorResultante[j], casasDecimais) + " ");
            }
            Console.WriteLine();
        }
    }
}