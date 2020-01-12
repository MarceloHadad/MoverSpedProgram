using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MoverSped.Entities;

namespace MoverSped.Repositories
{
    public class PdfRepository
    {
        public Recibo ObterInfoPDF(string caminho)
        {
            Recibo recibo = new Recibo();
            PdfReader reader = new PdfReader(caminho);

            string text = PdfTextExtractor.GetTextFromPage(reader, 1);
            string[] lines = text.Split('\n');

            if (lines[5].Contains(recibo.EhPIS))
            {
                recibo.Status = lines[8].Substring(36, 8).ToUpper();
                recibo.Competencia = lines[10].Substring(21, 10);
                recibo.CNPJ = lines[8].Substring(6, 18).Replace("-", "").Replace(".", "").Replace("/", "");

                reader.Close();

                recibo.CaminhoCriarPasta = recibo.TargetPath
                + "\\" + recibo.CNPJ
                + "\\" + recibo.Competencia.Substring(6, 4)
                + "\\" + recibo.Competencia.Substring(3, 2)
                + "\\PISCOFINS";
            }

            else if (lines[4].Contains(recibo.EhICMS))
            {

                recibo.Status = lines[8].Substring(42, 8).ToUpper();
                recibo.Competencia = lines[9].Substring(9, 10);
                recibo.CNPJ = lines[7].Substring(10, 18).Replace("-", "").Replace(".", "").Replace("/", "");
                             
                reader.Close();
                
                recibo.CaminhoCriarPasta = recibo.TargetPath
                    + "\\" + recibo.CNPJ
                    + "\\" + recibo.Competencia.Substring(6, 4)
                    + "\\" + recibo.Competencia.Substring(3, 2)
                    + "\\ICMS";
            }


            return recibo;
        }
    }
}
