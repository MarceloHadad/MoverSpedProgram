using MoverSped.Entities;
using System.IO;
using MoverSped.Repositories;
using MoverSped.Services;

namespace MoverSped
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            Sped sped = new Sped();
            Recibo rec = new Recibo();
            PdfRepository PdfRepo = new PdfRepository();
            TxtRepository TxtRepo = new TxtRepository();
            Organizador org = new Organizador();

            var recFiles = Directory.EnumerateFiles(rec.SourcePath, "*.pdf*", SearchOption.AllDirectories);
            foreach (string arquivoPdf in recFiles)
            {
                if (string.IsNullOrEmpty(arquivoPdf))
                {
                    break;
                }

                else
                {
                    rec = PdfRepo.ObterInfoPDF(arquivoPdf);
                    rec.SourceFileName = Path.GetFullPath(arquivoPdf);
                    rec.NomeDoArquivo = Path.GetFileName(arquivoPdf);

                    org.MoverRecibo(rec);
                }
            }

            var files = Directory.EnumerateFiles(sped.SourcePath, "*.txt*", SearchOption.AllDirectories);
            foreach (string arquivoTxt in files)
            {
                if (string.IsNullOrEmpty(arquivoTxt))
                {
                    break;
                }

                else
                {
                    sped = TxtRepo.ObterInfoSped(arquivoTxt);
                    sped.SourceFileName = Path.GetFullPath(arquivoTxt);
                    sped.NomeDoArquivo = Path.GetFileName(arquivoTxt);

                    org.MoverSped(sped);
                }
            }
            //stopwatch.stop();
            //// get the elapsed time as a timespan value.
            //timespan ts = stopwatch.elapsed;
            //console.writeline(ts.tostring());
        }
    }
}
