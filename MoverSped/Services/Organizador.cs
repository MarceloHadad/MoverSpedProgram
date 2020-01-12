using System;
using System.IO;
using MoverSped.Entities;

namespace MoverSped.Services
{
    public class Organizador
    {
        public void MoverRecibo(Recibo recibo)
        {
            recibo.DestFileName = recibo.CaminhoCriarPasta 
                + "\\" + DateTime.Now.ToString("dd-mm-yyy hhmmss") 
                + " " + recibo.NomeDoArquivo;
            
            if (Directory.Exists(recibo.CaminhoCriarPasta))
            {

                File.Move(recibo.SourceFileName, recibo.DestFileName);
            }

            else
            {
                Directory.CreateDirectory(recibo.CaminhoCriarPasta);

                File.Move(recibo.SourceFileName, recibo.DestFileName);
            }
        }

        public void MoverSped(Sped sped)
        {
            sped.DestFileName = sped.CaminhoCriarPasta
                + "\\" + DateTime.Now.ToString("dd-mm-yyy hhmmss")
                + " " + sped.NomeDoArquivo;

            if (Directory.Exists(sped.CaminhoCriarPasta))
            {

                File.Move(sped.SourceFileName, sped.DestFileName);
            }

            else
            {
                Directory.CreateDirectory(sped.CaminhoCriarPasta);

                File.Move(sped.SourceFileName, sped.DestFileName);
            }
        }
    }
}
