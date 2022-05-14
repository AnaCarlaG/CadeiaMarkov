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
            var path2 = Path.Combine(Environment.CurrentDirectory, "Input", "identidade.txt");
            var path3 = Path.Combine(Environment.CurrentDirectory, "Input", "vetorStart.txt");

            Matriz matriz = new Matriz();
            double[,] outraMatriz = matriz.Soma(matriz.Read_Matriz(path,true),matriz.Read_Matriz(path2,false));
           var vetor= matriz.TransformVector(matriz.Read_Vetor(path3), outraMatriz);
        }
    }
}
