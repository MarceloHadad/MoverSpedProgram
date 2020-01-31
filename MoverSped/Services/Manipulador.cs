using System;
using System.IO;
using MoverSped.Entities;

namespace MoverSped.Services
{
    public class Manipulador
    {
        public void MoverRecibo(Recibo recibo)
        {
            if (string.IsNullOrWhiteSpace(recibo.CaminhoCriarPasta))
                return;

            recibo.DestFileName = recibo.CaminhoCriarPasta
                + "\\" + DateTime.Now.ToString("dd-MM-yyy hhmmss")
                + " " + recibo.NomeDoArquivo;

            if (Directory.Exists(recibo.CaminhoCriarPasta))
            {
                File.Move(recibo.SourceFileName, recibo.DestFileName);
            }

            else
            {
                Directory.CreateDirectory(recibo.CaminhoCriarPasta);
                File.Move(recibo.SourceFileName, recibo.DestFileName);

                //Console.WriteLine("Dados do arquivo:");
                //Console.WriteLine("Caminho Criar Pasta: " + recibo.CaminhoCriarPasta);
                //Console.WriteLine("Status: " + recibo.Status);
                //Console.WriteLine("Competencia: " + recibo.Competencia);
                //Console.WriteLine("CNPJ: " + recibo.CNPJ);
                //Console.WriteLine("Linha 4: " + recibo.Linha4);
                //Console.WriteLine("Linha 5: " + recibo.Linha5);
            }
        }

        public void MoverSped(Sped sped)
        {
            sped.DestFileName = sped.CaminhoCriarPasta
                + "\\" + DateTime.Now.ToString("dd-MM-yyy hhmmss")
                + " " + sped.NomeDoArquivo;

            if (Directory.Exists(sped.CaminhoCriarPasta))
            {

                File.Move(sped.SourceFileName, sped.DestFileName);
                Console.WriteLine("Movido");
            }

            else
            {
                Directory.CreateDirectory(sped.CaminhoCriarPasta);

                File.Move(sped.SourceFileName, sped.DestFileName);
            }
        }
    }
}
