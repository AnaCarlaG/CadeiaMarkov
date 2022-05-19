using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace CadeiaMarkov
{
    public class Matriz
    {
        public double[,] Read_Matriz(string arquivo, bool invert)
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
            if (invert)
            {
                return InvertMatriz(matriz, linha, coluna); ;
            }
            return matriz;
        }
        public double[] Read_Vetor(string arquivo)
        {
            StreamReader reader = new StreamReader(arquivo);
            int linha = int.Parse(reader.ReadLine());
            double[] vetor = new double[linha];
            for (int i = 0; i < linha; i++)
            {
                vetor[i] =  Convert.ToDouble(reader.ReadLine());
            }
            return vetor;
        }

        public double[,] InvertMatriz(double[,] matriz, int linhas, int colunas)
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
            return DividiMatriz(novaMatriz, max);
        }

        public double InverterValores(double value)
        {
            return value * -1;
        }
        public double[,] DividiMatriz(double[,] mat, double max)
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
        public double[,] Soma(double[,] matrizA, double[,] matrizB)
        {
            var len = matrizB.GetLength(1);
            double[,] matrizC = new double[len, len];
            for (int linha = 0; linha < len; linha++)
            {
                for (int coluna = 0; coluna < len; coluna++)
                {
                    var aux = matrizA[linha, coluna] + matrizB[linha, coluna];
                    matrizC[linha, coluna] = Math.Abs(aux);
                }
            }
            return matrizC;
        }

        public void TransformMatrizVector(double[] vetor, double[,] matriz, double tolerancia, bool equiprovavel)
        {
            var len = matriz.GetLength(1);
            double[] vetorResultante = new double[len];
            for (int linha = 0; linha < len; linha++)
            {
                for (int coluna = 0; coluna < len; coluna++)
                {
                    if (vetor[coluna] != 0)
                    {
                        vetorResultante[linha] += 1 * matriz[coluna, linha];
                    }
                    else
                    {
                        vetorResultante[linha] += vetorResultante != null ? matriz[coluna, linha] * 0 : vetorResultante[linha];
                    }
                }
            }
            Variacao(vetor, vetorResultante, matriz, tolerancia, equiprovavel);
        }

        public void Variacao(double[] vetor, double[] vetorResultante, double[,] matriz, double tolerancia, bool equiprovavel)
        {
            var len = matriz.GetLength(1);
            double[] vetorComparativo = new double[vetorResultante.Count()];
            int iteracao = 1;
            bool converg = false;
            while (!converg)
            {
                if (!equiprovavel)
                {
                    for (int i = 0; i < vetor.Count(); i++)
                    {
                        if (vetor[i] != 0)
                        {
                            vetor[i] = 0;
                            if (i == vetor.Count())
                            {
                                vetor[0] = 1;
                                break;
                            }
                            else
                            {
                                vetor[i + 1] = 1;
                                break;
                            }
                        }
                    }
                }
                for (int linha = 0; linha < len; linha++)
                {
                    for (int coluna = 0; coluna < len; coluna++)
                    {
                        if (vetor[coluna] != 0)
                        {
                            vetorResultante[linha] += 1 * matriz[coluna, linha];
                        }
                        else
                        {
                            vetorResultante[linha] += vetorResultante != null ? matriz[coluna, linha] * 0 : vetorResultante[linha];
                        }
                    }
                }

                PrintVetor(vetorResultante, tolerancia.ToString().Substring(tolerancia.ToString().LastIndexOf(",") + 1).Length);
                converg = Teste(vetorComparativo, vetorResultante, tolerancia);
                if (converg)
                {
                    PrintVetor(vetorResultante, tolerancia.ToString().Substring(tolerancia.ToString().LastIndexOf(",") + 1).Length);
                    Console.WriteLine("Quantidade iterações: " + iteracao);
                } else{
                    vetorComparativo = vetorResultante;
                }
                iteracao++;
            }
        }
        public bool Teste(double[] vetorComparativo, double[] vetorResultante, double tolerancia)
        {
            for (int j = 0; j < vetorComparativo.Count(); j++)
            {
                var result = Math.Sqrt(Math.Pow(Math.Round(vetorComparativo[j], 4) - Math.Round(vetorResultante[j], 4), 2));
                if (result < tolerancia)
                {
                    return true;
                }
            }
            return false;
        }
        public void PrintVetor(double[] vetorResultante ,int casasDecimais)
        {
            for (int j = 0; j < vetorResultante.Count(); j++)
            {
                Console.Write(Math.Round(vetorResultante[j],casasDecimais) + " ");
            }
            Console.WriteLine();
        }
    }
}