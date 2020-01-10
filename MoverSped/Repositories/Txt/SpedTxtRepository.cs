using MoverSped.Entities;
using MoverSped.Interfaces;
using System;

namespace MoverSped.Repositories.Txt
{
    public class SpedTxtRepository : ISpedRepository
    {
        public Sped ObterInfoSped(string caminho)
        {
            var sped = new Sped();

            if (caminho.Length > 20)
            {
                sped.Nome = caminho;
                return sped;
            }
            else
            {
                sped.CaminhoCriarPasta = caminho;
                return sped;
            }
        }

    }
}
