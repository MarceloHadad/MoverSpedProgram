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
                rec = PdfRepo.ObterInfoPDF(arquivoPdf);

                if (rec != null)
                {
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
                sped = TxtRepo.ObterInfoSped(arquivoTxt);
                sped.SourceFileName = Path.GetFullPath(arquivoTxt);
                sped.NomeDoArquivo = Path.GetFileName(arquivoTxt);

                //System.Console.WriteLine(sped.CNPJ);
                //System.Console.WriteLine(sped.Status);

                _org.MoverSped(sped);
            }
        }
    }
}
