using MoverSped.Entities;
using System;
using System.Diagnostics;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using System.Text.RegularExpressions;

namespace MoverSped
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Sped sped = new Sped();
            Recibo recibo = new Recibo();

            var filesPdf = Directory.EnumerateFiles(recibo.SourcePath, "*.pdf*", SearchOption.AllDirectories);
            foreach (string s in filesPdf)
            {
                if (string.IsNullOrEmpty(s))
                {
                    break;
                }

                else
                {
                    string arquivo = System.IO.Path.GetFullPath(s);
                    string caminhoOrigem = System.IO.Path.GetDirectoryName(s);
                    string nomeArquivo = System.IO.Path.GetFileName(s);
                    PdfReader reader = new PdfReader(arquivo);

                    string text = PdfTextExtractor.GetTextFromPage(reader, 1);
                    string[] lines = text.Split('\n');

                    //for(int i = 0; i<lines.Length; i++)
                    //{
                    //    Console.WriteLine(i + " " + lines[i]);
                    //}

                    if (lines[5].Contains(recibo.EhPIS))
                    {
                        //Console.WriteLine("É PIS");
                        recibo.Status = lines[8].Substring(36, 8);
                        recibo.Competencia = lines[10].Substring(21, 10);
                        recibo.CNPJ = lines[8].Substring(6, 18).Replace("-", "").Replace(".", "").Replace("/", "");

                        recibo.CaminhoCriarPasta = recibo.TargetPath + "\\" + recibo.CNPJ + "\\" + recibo.Competencia.Substring(6, 4) + "\\" + recibo.Competencia.Substring(3, 2) + "\\PIS";

                        if (Directory.Exists(recibo.CaminhoCriarPasta))
                        {
                            string NovoNome = recibo.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                            Console.WriteLine(arquivo);
                            Console.WriteLine(NovoNome);
                            //File.Move(arquivo, NovoNome);
                        }

                        else
                        {
                            Directory.CreateDirectory(recibo.CaminhoCriarPasta);

                            string NovoNome = recibo.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                            //File.Move(arquivo, NovoNome);

                        }
                    }

                    //if (lines[4].Contains("RECIBO DE ENTREGA DE ESCRITURAÇÃO FISCAL DIGITAL"))
                    //{
                    //    Console.WriteLine("É ICMS");
                    //}

                    //recibo.Status = lines[4];

                    //Console.WriteLine(recibo.Status);

                    //var files = Directory.EnumerateFiles(sped.SourcePath, "*.txt*", SearchOption.AllDirectories);
                    //foreach (string s in files)
                    //{
                    //    //if (Path.GetExtension(s) == ".txt")
                    //    //{
                    //    if (string.IsNullOrEmpty(s))
                    //    {
                    //        break;
                    //    }

                    //    else
                    //    {
                    //        string arquivo = Path.GetFullPath(s);
                    //        string caminhoOrigem = Path.GetDirectoryName(s);
                    //        string nomeArquivo = Path.GetFileName(s);

                    //        foreach (string line in File.ReadLines(arquivo)) //Para cada arquivo
                    //        {
                    //            var ehBloco0 = line.StartsWith("|0000"); //Se o bloco iniciar com |0000
                    //            if (!ehBloco0)
                    //                break;

                    //            string[] Linha = line.Split("|"); // Divide a linha em pedaços

                    //            sped.Identificador = Linha[14]; // Atribui o valor identificador
                    //            sped.TesteEcf = Linha[2];

                    //            if (sped.TesteEcf == "LECF")
                    //                break;

                    //            int num;
                    //            bool isNum = Int32.TryParse(sped.Identificador, out num);
                    //            if (isNum)
                    //            {
                    //                for (int i = 0; i < sped.ValoresPis.Length; i++)
                    //                {
                    //                    if (sped.Identificador == sped.ValoresPis[i])
                    //                    {
                    //                        //Console.WriteLine("É PIS");

                    //                        sped.Status = Linha[3];
                    //                        sped.Competencia = Linha[6];
                    //                        sped.Nome = Linha[8];
                    //                        sped.CNPJ = Linha[9];
                    //                        sped.CaminhoCriarPasta = sped.TargetPath + "\\" + sped.CNPJ + "\\" + sped.Competencia.Substring(4, 4) + "\\" + sped.Competencia.Substring(2, 2) + "\\PIS";

                    //                        Directory.CreateDirectory(sped.CaminhoCriarPasta);

                    //                        sped.ContadorPis++;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //                for (int j = 0; j < sped.ValoresIcms.Length; j++)
                    //                {
                    //                    if (sped.Identificador == sped.ValoresIcms[j])
                    //                    {
                    //                        //Console.WriteLine("É ICMS");
                    //                        sped.Status = Linha[3];
                    //                        sped.Competencia = Linha[4];
                    //                        sped.Nome = Linha[6];
                    //                        sped.CNPJ = Linha[7];
                    //                        sped.CaminhoCriarPasta = sped.TargetPath + "\\" + sped.CNPJ + "\\" + sped.Competencia.Substring(4, 4) + "\\" + sped.Competencia.Substring(2, 2) + "\\ICMS";

                    //                        Directory.CreateDirectory(sped.CaminhoCriarPasta);
                    //                        sped.ContadorIcms++;
                    //                    }
                    //                }
                    //        }
                    //        if (Directory.Exists(sped.CaminhoCriarPasta))
                    //        {
                    //            string NovoNome = sped.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                    //            File.Move(arquivo, NovoNome);
                    //        }
                    //    }
                    //    //}
                    //}

                    //    Console.WriteLine("ICMS: " + sped.ContadorIcms);
                    //Console.WriteLine("PIS: " + sped.ContadorPis);

                    //stopWatch.Stop();
                    //// Get the elapsed time as a TimeSpan value.
                    //TimeSpan ts = stopWatch.Elapsed;
                    //Console.WriteLine(ts.ToString());
                }

            }
        }
    }
}

