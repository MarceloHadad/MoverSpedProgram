using OfficeOpenXml;
using System.IO;
using System;
using MoverSped.Entities;
using MoverSped.Repositories;
using System.Text;

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

            for (int i = 1; i <= 10; i++)
            {
                sheet.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                sheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                sheet.Cells[1, i].Style.Font.Bold = true;
                sheet.Cells[1, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                sheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
            }
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
                Encoding utf8 = Encoding.UTF8;

                sped = TxtRepo.ObterInfoSped(arquivoTxt);

                if (sped != null)
                {
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

                    for (int i = 1; i <= 10; i++)
                    {
                        sheet.Cells[row, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }
                    row++;
                }
            }
            package.Save();
        }
    }
}
