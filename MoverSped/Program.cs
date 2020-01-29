using MoverSped.Services;
using System;

namespace MoverSped
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            Console.WriteLine("Escolha uma opção: ");
            Console.WriteLine("1- Para organizar os arquivos.");
            Console.WriteLine("2- Gerar um log.");
            Console.WriteLine("3- Imprimir info do Recibo.");
            var opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                var org = new Manipulador();
                MoverArquivo listar = new MoverArquivo(org);

                listar.ListarPdf();
                listar.ListarTxt();

            }

            else if (opcao == 2)
            {
                Log log = new Log();
                log.GerarLog();
            }

            else if(opcao == 3)
            {

            }

            else
            {
                Console.WriteLine("OPÇÃO INVÁLIDA");
            }

            //stopwatch.stop();
            //// get the elapsed time as a timespan value.
            //timespan ts = stopwatch.elapsed;
            //console.writeline(ts.tostring());
        }
    }
}