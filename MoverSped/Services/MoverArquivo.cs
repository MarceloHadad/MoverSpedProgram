using MoverSped.Entities;
using System.IO;
using MoverSped.Repositories;

namespace MoverSped.Services
{
    public class MoverArquivo
    {
        public MoverArquivo(Manipulador org)
        {
            _org = org;
        }

        private readonly Manipulador _org;

        public void ListarPdf()
        {
            Recibo rec = new Recibo();
            PdfRepository PdfRepo = new PdfRepository();

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

                    _org.MoverRecibo(rec);
                }
            }
        }

        public void ListarTxt()
        {
            Sped sped = new Sped();
            TxtRepository TxtRepo = new TxtRepository();

            var spedFiles = Directory.EnumerateFiles(sped.SourcePath, "*.txt*", SearchOption.AllDirectories);
            foreach (string arquivoTxt in spedFiles)
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

                    _org.MoverSped(sped);
                }
            }
        }
    }
}
