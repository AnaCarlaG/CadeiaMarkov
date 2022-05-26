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
            var path3 = Path.Combine(Environment.CurrentDirectory, "Input", "vetorStart.txt");

            Matriz matriz = new Matriz();
            matriz.MultiplicacaoMatrizVetor(matriz.Read_Vetor(path3), matriz.Read_Matriz(path), 0.0000000001);
        }
    }
}
