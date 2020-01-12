namespace MoverSped.Entities
{
    public class Sped
    {
        public string SourcePath { get; set; } = @"C:\MoverSped\RepositorioSped";
        public string TargetPath { get; set; } = @"C:\MoverSped\Organizados";
        public string SourceFileName { get; set; }
        public string DestFileName { get; set; }
        public string[] EhICMS { get; set; } = { "A", "B", "C" };
        public string[] EhPIS { get; set; } = { "0", "1", "2", "3", "4", "9" };
        public int ContadorIcms { get; set; }
        public int ContadorPis { get; set; }
        public string Identificador { get; set; }
        public string CaminhoCriarPasta { get; set; }
        public string Status { get; set; }
        public string Competencia { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string TesteEcf { get; set; }
        public string NomeDoArquivo { get; set; }

    }
}
