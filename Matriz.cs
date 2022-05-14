using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            double[] vetor= new double[linha];
            for (int i = 0; i < linha; i++)
            {
                    vetor[i] = Convert.ToDouble(reader.ReadLine());
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

        public double[] TransformVector(double[] vetor, double[,] matriz){
            var len = matriz.GetLength(1);
            double[] vetorResultante = new double[len];
            for (int linha = 0; linha < len; linha++)
            {
                for (int coluna = 0; coluna < len; coluna++)
                {
                    if(vetor[coluna] != 0){
                        vetorResultante[linha] += 1*matriz[coluna,linha];
                    }else{
                        vetorResultante[linha] +=vetorResultante != null ? matriz[coluna,linha]*0:vetorResultante[linha];
                    }
                }
            }
            return vetorResultante;
        }
    }
}