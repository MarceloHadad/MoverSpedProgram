using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MoverSped.Entities;
using MoverSped.Interfaces;
using MoverSped.Repositories.Pdf;
using MoverSped.Repositories.Txt;
using System;
using System.Diagnostics;
using System.IO;

namespace MoverSped
{
    class Program
    {

        public static void ExecutarTarefa(ISpedRepository repo, FileInfo arquivo)
        {
            var sped = repo.ObterInfoSped(arquivo.FullName);
        }

        static void Main(string[] args)
        {
            Sped spd;
            var ext = "txt";
            var arquivo = new FileInfo("caminho");

            if(arquivo.Extension == ".txt")
            {
                ExecutarTarefa(new SpedTxtRepository(), arquivo);
            }

            if (ext == "pdf")
            {
                ExecutarTarefa(new SpedPdfRepository(), arquivo);
            }

            //spd = new SpedTxtRepository();


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


                    if (lines[5].Contains(recibo.EhPIS))
                    {
                        recibo.Status = lines[8].Substring(36, 8).ToUpper();
                        recibo.Competencia = lines[10].Substring(21, 10);
                        recibo.CNPJ = lines[8].Substring(6, 18).Replace("-", "").Replace(".", "").Replace("/", "");

                        reader.Close();

                        recibo.CaminhoCriarPasta = recibo.TargetPath + "\\" + recibo.CNPJ + "\\" + recibo.Competencia.Substring(6, 4) + "\\" + recibo.Competencia.Substring(3, 2) + "\\PIS";

                        if (Directory.Exists(recibo.CaminhoCriarPasta))
                        {
                            string NovoNome = recibo.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                            Console.WriteLine(arquivo);
                            Console.WriteLine(NovoNome);
                            File.Move(arquivo, NovoNome);
                        }

                        else
                        {
                            Directory.CreateDirectory(recibo.CaminhoCriarPasta);

                            string NovoNome = recibo.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                            File.Move(arquivo, NovoNome);

                        }
                    }

                    if (lines[4].Contains(recibo.EhICMS))
                    {
                      
                        recibo.Status = lines[8].Substring(42, 8).ToUpper();
                        recibo.Competencia = lines[9].Substring(9, 10);
                        recibo.CNPJ = lines[7].Substring(10, 18).Replace("-", "").Replace(".", "").Replace("/", "");

                        Console.WriteLine(recibo.Status);
                        Console.WriteLine(recibo.Competencia);
                        Console.WriteLine(recibo.CNPJ);
                        reader.Close();

                        recibo.CaminhoCriarPasta = recibo.TargetPath + "\\" + recibo.CNPJ + "\\" + recibo.Competencia.Substring(6, 4) + "\\" + recibo.Competencia.Substring(3, 2) + "\\ICMS";

                        if (Directory.Exists(recibo.CaminhoCriarPasta))
                        {
                            string NovoNome = recibo.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                            Console.WriteLine(arquivo);
                            Console.WriteLine(NovoNome);
                            File.Move(arquivo, NovoNome);
                        }

                        else
                        {
                            Directory.CreateDirectory(recibo.CaminhoCriarPasta);

                            string NovoNome = recibo.CaminhoCriarPasta + "\\" + DateTime.Now.ToString("dd-MM-yyy HHmmss") + " " + nomeArquivo;

                            File.Move(arquivo, NovoNome);

                        }
                    }

                    var files = Directory.EnumerateFiles(sped.SourcePath, "*.txt*", SearchOption.AllDirectories);
                    foreach (string s in files)
                    {
                        if (Path.GetExtension(s) == ".txt")
                        {
                        if (string.IsNullOrEmpty(s))
                        {
                            break;
                        }

                        else
                        {
                            string arquivo = Path.GetFullPath(s);
                            string caminhoOrigem = Path.GetDirectoryName(s);
                            string nomeArquivo = Path.GetFileName(s);

                            foreach (string line in File.ReadLines(arquivo)) //Para cada arquivo
                            {
                                var ehBloco0 = line.StartsWith("|0000"); //Se o bloco iniciar com |0000
                                if (!ehBloco0)
                                    break;

                                string[] Linha = line.Split("|"); // Divide a linha em pedaços

                                sped.Identificador = Linha[14]; // Atribui o valor identificador
                                sped.TesteEcf = Linha[2];

                                if (sped.TesteEcf == "LECF")
                                    break;

                                int num;
                                bool isNum = Int32.TryParse(sped.Identificador, out num);
                                if (isNum)
                                {
                                    for (int i = 0; i < sped.ValoresPis.Length; i++)
                                    {
                                        if (sped.Identificador == sped.ValoresPis[i])
                                        {
                                            //console.writeline("é pis");

                    //                        sped.status = linha[3];
                    //                        sped.competencia = linha[6];
                    //                        sped.nome = linha[8];
                    //                        sped.cnpj = linha[9];
                    //                        sped.caminhocriarpasta = sped.targetpath + "\\" + sped.cnpj + "\\" + sped.competencia.substring(4, 4) + "\\" + sped.competencia.substring(2, 2) + "\\pis";

                    //                        directory.createdirectory(sped.caminhocriarpasta);

                    //                        sped.contadorpis++;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //                for (int j = 0; j < sped.valoresicms.length; j++)
                    //                {
                    //                    if (sped.identificador == sped.valoresicms[j])
                    //                    {
                    //                        //console.writeline("é icms");
                    //                        sped.status = linha[3];
                    //                        sped.competencia = linha[4];
                    //                        sped.nome = linha[6];
                    //                        sped.cnpj = linha[7];
                    //                        sped.caminhocriarpasta = sped.targetpath + "\\" + sped.cnpj + "\\" + sped.competencia.substring(4, 4) + "\\" + sped.competencia.substring(2, 2) + "\\icms";

                    //                        directory.createdirectory(sped.caminhocriarpasta);
                    //                        sped.contadoricms++;
                    //                    }
                    //                }
                    //        }
                    //        if (directory.exists(sped.caminhocriarpasta))
                    //        {
                    //            string novonome = sped.caminhocriarpasta + "\\" + datetime.now.tostring("dd-mm-yyy hhmmss") + " " + nomearquivo;

                    //            file.move(arquivo, novonome);
                    //        }
                    //    }
                    //    //}
                    //}

                    //    console.writeline("icms: " + sped.contadoricms);
                    //console.writeline("pis: " + sped.contadorpis);

                    //stopwatch.stop();
                    //// get the elapsed time as a timespan value.
                    //timespan ts = stopwatch.elapsed;
                    //console.writeline(ts.tostring());
                }

            }
        }
    }
}

