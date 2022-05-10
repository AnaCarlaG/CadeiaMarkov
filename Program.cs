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
            //Read_CSV();
             double[,] OutraMatriz = carregaMatriz(path,5,5);

             static double[,] carregaMatriz(string arquivo, int linhas, int colunas)
            {
                StreamReader reader = new StreamReader(arquivo);
                int linha = linhas;
                int coluna = colunas;
                double[,] Matriz = new double[linha, coluna];
                for (int i = 0; i < linha; i++)
                {
                    for (int j = 0; j < coluna; j++)
                    {
                        Matriz[i, j] = Convert.ToDouble(reader.ReadLine());
                    }
                }
                return Matriz;
                
            }
            
            static void Read_CSV()
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Input", "entrada.csv");

                var file = new FileInfo(path);
                if (!file.Exists)
                {
                    throw new FileNotFoundException($"File {path} doens't exist");
                }
                using var sr = new StreamReader(file.FullName);
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
                using var csvReader = new CsvReader(sr, csvConfig);

                List<string> matrizString = new List<string>();
                double[,] mat = new double[5,5];

                var registros = csvReader.GetRecords<dynamic>().ToArray();

                foreach (var item in registros)
                {
                    
                    matrizString.Add(item.A);
                    matrizString.Add(item.B);
                    matrizString.Add(item.C);
                    matrizString.Add(item.D);
                    matrizString.Add(item.E);
                    matrizString.Add("#");

                    AddMatriz(mat, matrizString,5);

                }
                Console.WriteLine("----------------");
            }
            static void AddMatriz(double[,] mat, List<String> m, int tamanhoVetor)
            {
                int count = -1;
                count ++;
                for(int i = 0; i < tamanhoVetor; i++)
                {
                    mat[count,i] = Convert.ToDouble(m[i]);
                }
                
            }
        }
    }
}
