using System.Text;

namespace MoverSped.Entities
{
    public class Sped
    {
        //public string SourcePath { get; set; } = @"C:\MoverSped\RepositorioSped";
        public string SourcePath { get; set; } = @"J:\Importação\ARQUIVOS - SPED DE ICMS\2019\12\5 - TRANSMITIDO";
        public string TargetPath { get; set; } = @"C:\MoverSped\Organizados";
        public string SourceFileName { get; set; }
        public string DestFileName { get; set; }
        public string[] EhICMS { get; set; } = { "A", "B", "C" };
        public string[] EhPIS { get; set; } = { "0", "1", "2", "3", "4", "9" };
        public int ContadorIcms { get; set; }
        public int ContadorPis { get; set; }
        public string Identificador { get; set; }
        public string CaminhoCriarPasta { get; set; }
        public string RazaoSocial { get; set; }
        public string Grupo { get; set; }
        public string CNPJ { get; set; }
        public string InicioVigencia { get; set; }
        public string MesCompetencia { get; set; }
        public string AnoCompetencia { get; set; }
        public string Status { get; set; }
        public string TipoSped { get; set; }
        public string TesteEcf { get; set; }
        public string NomeDoArquivo { get; set; }

        public string ValidaStatus(string status)
        {
            try
            {
                if (int.Parse(status) == 0)
                    status = "Original";
                else if (int.Parse(status) == 1)
                {
                    status = "Retificado";
                }
            }
            catch
            {
                return null;
            }

            return status;
        }
        public string CriarCaminhoICMS(string targetPath, string cnpj, string anoCompetencia, string mesCompetencia)
        {
            CaminhoCriarPasta = targetPath
                + "\\" + cnpj
                + "\\" + anoCompetencia
                + "\\" + MesCompetencia
                + "\\ICMS";

            return CaminhoCriarPasta;
        }
        public string CriarCaminhoPISCOFINS(string targetPath, string cnpj, string anoCompetencia, string mesCompetencia)
        {
            CaminhoCriarPasta = targetPath
                + "\\" + cnpj
                + "\\" + anoCompetencia
                + "\\" + MesCompetencia
                + "\\PISCOFINS";

            return CaminhoCriarPasta;
        }

        //public string ConverteNome(string razãoSocial)
        //{
        //    var razaoConvertida = Encoding.GetEncoding(razãoSocial);
        //    //byte[] utf8Bytes = Encoding.UTF8.GetBytes(razãoSocial);
        //    //return Encoding.UTF8.GetString(utf8Bytes);
        //    return razaoConvertida.ToString();
        //}
    }
}