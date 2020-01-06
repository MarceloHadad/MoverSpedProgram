using System;
using MoverSped.Entities;

namespace MoverSped
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = @"C:\Users\marceloc\Desktop\MoverSped\RepositorioSped";
            //string targetPath = @"C:\MoverSped";

            Validador Teste = new Validador(sourcePath);

            Teste.Validacao(sourcePath);
        }
    }
}