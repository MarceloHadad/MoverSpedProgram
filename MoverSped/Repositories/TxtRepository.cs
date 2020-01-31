using System;
using MoverSped.Entities;
using System.IO;

namespace MoverSped.Repositories
{
    class TxtRepository
    {
        public Sped ObterInfoSped(string caminho)
        {
            Sped sped = new Sped();

            foreach (string line in File.ReadLines(caminho)) //Para cada arquivo
            {
                var ehBloco0 = line.StartsWith("|0000"); //Se o bloco iniciar com |0000
                if (ehBloco0)
                {
                    string[] Linha = line.Split("|"); // Divide a linha em pedaços

                    sped.Identificador = Linha[14]; // Atribui o valor identificador
                    sped.TesteEcf = Linha[2];

                    if (sped.TesteEcf == "LECF")
                        break;

                    int num;
                    bool isNum = Int32.TryParse(sped.Identificador, out num);
                    if (isNum)
                    {
                        for (int i = 0; i < sped.EhPIS.Length; i++)
                        {
                            if (sped.Identificador == sped.EhPIS[i])
                            {
                                sped.RazaoSocial = (Linha[8]);
                                sped.CNPJ = Linha[9];
                                sped.MesCompetencia = Linha[6].Substring(2, 2);
                                sped.AnoCompetencia = Linha[6].Substring(4, 4);
                                sped.Status = sped.ValidaStatus(Linha[3]);
                                sped.TipoSped = "PISCOFINS";
                                sped.CriarCaminhoICMS(sped.TargetPath, sped.CNPJ, sped.AnoCompetencia, sped.MesCompetencia);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < sped.EhICMS.Length; j++)
                        {
                            if (sped.Identificador == sped.EhICMS[j])
                            {
                                sped.RazaoSocial = (Linha[6]);
                                sped.CNPJ = Linha[7];
                                sped.MesCompetencia = Linha[4].Substring(2, 2);
                                sped.AnoCompetencia = Linha[4].Substring(4, 4);
                                sped.Status = sped.ValidaStatus(Linha[3]);
                                sped.TipoSped = "ICMS";
                                sped.CriarCaminhoICMS(sped.TargetPath, sped.CNPJ, sped.AnoCompetencia, sped.MesCompetencia);
                            }
                        }
                    }
                }
                else
                    return null;
            }
            return sped;
        }
    }
}
