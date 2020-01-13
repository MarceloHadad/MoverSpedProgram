using OfficeOpenXml;
using System.IO;
using System;
using MoverSped.Entities;
using MoverSped.Repositories;

namespace MoverSped.Services
{
    public class Log
    {
        public void GerarLog()
        {
            Sped sped = new Sped();
            TxtRepository TxtRepo = new TxtRepository();

            var caminhoNomeArquivo = File.Create(@"C:\MoverSped\Log\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " Auditoria.xlsx");

            ExcelPackage package = new ExcelPackage(caminhoNomeArquivo);

            ExcelWorkbook workbook = package.Workbook;

            ExcelWorksheet sheet = workbook.Worksheets.Add("AuditoriaFiscal");

            var row = 2;
            sheet.Cells[1, 1].Value = "CodCliente";
            sheet.Cells[1, 2].Value = "RazaoSocial";
            sheet.Cells[1, 3].Value = "Grupo";
            sheet.Cells[1, 4].Value = "CNPJ";
            sheet.Cells[1, 5].Value = "InicioVigencia";
            sheet.Cells[1, 6].Value = "MesCompetencia";
            sheet.Cells[1, 7].Value = "AnoCompetencia";
            sheet.Cells[1, 8].Value = "Status";
            sheet.Cells[1, 9].Value = "Movimento";
            sheet.Cells[1, 10].Value = "Tipo";

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

                    sheet.Cells[row, 1].Value = "Inacessível";
                    sheet.Cells[row, 2].Value = sped.RazaoSocial;
                    sheet.Cells[row, 3].Value = "Inacessível";
                    sheet.Cells[row, 4].Value = sped.CNPJ;
                    sheet.Cells[row, 5].Value = "Inacessível";
                    sheet.Cells[row, 6].Value = sped.MesCompetencia;
                    sheet.Cells[row, 7].Value = sped.AnoCompetencia;
                    sheet.Cells[row, 8].Value = sped.Status;
                    sheet.Cells[row, 9].Value = "Validando...";
                    sheet.Cells[row, 10].Value = sped.TipoSped;
                    
                    row += 1;
                }
            }
            package.Save();
        }
    }
}
