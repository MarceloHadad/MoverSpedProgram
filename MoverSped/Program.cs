using MoverSped.Entities;
using System.IO;
using MoverSped.Repositories;
using MoverSped.Services;
using OfficeOpenXml;
using System;

namespace MoverSped
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            var caminhoNomeArquivo = File.Create(@"C:\MoverSped\Log\" + DateTime.Now.ToString("dd -mm-yyy hhmmss") + " Auditoria.xlsx");

            ExcelPackage package = new ExcelPackage(caminhoNomeArquivo);

            ExcelWorkbook workbook = package.Workbook;

            ExcelWorksheet sheet = workbook.Worksheets.Add("AuditoriaFiscal");

            Sped sped = new Sped();
            Recibo rec = new Recibo();
            PdfRepository PdfRepo = new PdfRepository();
            TxtRepository TxtRepo = new TxtRepository();
            Organizador org = new Organizador();
            var row = 1;

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

                    org.MoverSped(sped);
                    sheet.Cells[row, 1].Value = sped.Nome;
                    sheet.Cells[row, 2].Value = sped.CNPJ;
                    sheet.Cells[row, 3].Value = sped.Status;
                    sheet.Cells[row, 4].Value = sped.Competencia;

                    row += 1;
                }
                package.SaveAs((caminhoNomeArquivo));
            }

            //stopwatch.stop();
            //// get the elapsed time as a timespan value.
            //timespan ts = stopwatch.elapsed;
            //console.writeline(ts.tostring());
        }
    }
}
