namespace MoverSped.Entities
{
    public class Recibo
    {
        public string SourcePath { get; set; } = @"C:\MoverSped\RepositorioSped";
        public string TargetPath { get; set; } = @"C:\MoverSped\Organizados";
        public string SourceFileName { get; set; }
        public string DestFileName { get; set; }
        public string EhICMS { get; set; } = "RECIBO DE ENTREGA DE ESCRITURAÇÃO FISCAL DIGITAL";
        public string EhPIS { get; set; } = "RECIBO DE ENTREGA DE ESCRITURAÇÃO FISCAL DIGITAL - CONTRIBUIÇÕES";
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
        public string Linha5 { get; set; }
        public string Linha4 { get; set; }

    }
}
