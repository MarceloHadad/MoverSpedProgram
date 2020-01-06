using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MoverSped.Entities
{
    class Validador
    {
        public string Extension { get; set; }
        public string Linha { get; set; }
        public string Caminho { get; set; }

        public Validador(string path)
        {
            Caminho = path;
        }

        public void Validacao(string sourcePath)
        {
            Extension = Path.GetExtension(sourcePath);
            var files = Directory.EnumerateFiles(sourcePath, "*.*", SearchOption.AllDirectories);
            foreach (string s in files)
            {
                if (string.IsNullOrEmpty(s) == false)
                {
                    string[] lines = File.ReadAllLines(sourcePath);
                    foreach (string line in lines)
                    {
                        if (Extension == ".txt")
                        {
                            string[] Linha = line.Split("|");

                            string CodFin = Linha[3];
                            string Competencia = Linha[4];
                            string Nome = Linha[6];
                            string CNPJ = Linha[7];
                            string Identificador = Linha[14];
                            foreach (string bloco in Linha)
                            {
                                Console.WriteLine(bloco);
                            }
                            //é um SPED
                            //if(BlocoIdentificador == letra é icms) se for numero é pis
                        }

                        //if (Extension == ".pdf")
                        //{
                        //    e contem "RECIBO DE ENTREGA DE ESCRITURAÇÃO FISCAL DIGITAL" é um recibo, senao sai do if.
                        //    se o texto "RECIBO DE ENTREGA DE ESCRITURAÇÃO FISCAL DIGITAL" terminar com "- CONTRIBUIÇÕES" é pis, senao é cofins
                        //}
                    }
                }
            }
        }
    }
}