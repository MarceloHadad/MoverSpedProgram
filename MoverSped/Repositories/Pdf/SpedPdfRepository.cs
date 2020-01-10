using MoverSped.Entities;
using MoverSped.Interfaces;

namespace MoverSped.Repositories.Pdf
{
    public class SpedPdfRepository : ISpedRepository
    {


        public Sped ObterInfoSped(string caminho)
        {
            var sped = new Sped();
            sped.CNPJ = caminho;

            return sped;
        }
    }
}
