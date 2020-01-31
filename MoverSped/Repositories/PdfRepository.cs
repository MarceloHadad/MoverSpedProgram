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
            PdfReader reader = null;

            if (!string.IsNullOrEmpty(caminho))
            {
                try
                {
                    reader = new PdfReader(caminho);
                }

                catch
                {
                    return null;
                }

                if (reader != null)
                {
                    var text = PdfTextExtractor.GetTextFromPage(reader, 1);
                    string[] lines = text.Split('\n');

                    if (lines.Length > 0 && lines[5].Contains(recibo.EhPIS))
                    {
                        recibo.Status = lines[8]?.Substring(36, 8).ToUpper();
                        recibo.Competencia = lines[10]?.Substring(21, 10);
                        recibo.CNPJ = lines[8]?.Substring(6, 18).Replace("-", "").Replace(".", "").Replace("/", "");
                        recibo.Linha5 = lines[5];

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
                        recibo.Linha4 = lines[4];

                        reader.Close();

                        recibo.CaminhoCriarPasta = recibo.TargetPath
                            + "\\" + recibo.CNPJ
                            + "\\" + recibo.Competencia.Substring(6, 4)
                            + "\\" + recibo.Competencia.Substring(3, 2)
                            + "\\ICMS";
                    }
                    else
                        return null;
                }
            }
            return recibo;
        }
    }
}
