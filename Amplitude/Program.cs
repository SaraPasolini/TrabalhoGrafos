using System;
using AlgoritmosGrafos.Utils;

namespace Amplitude
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lê matriz do arquivo
            int[,] matriz = LeitorDeGrafo.LerGrafoDeArquivo(@"C:\Users\saraa\Grafos\TrabalhoGrafos\Utils\GrafoTeste.txt");


            int n = matriz.GetLength(0);

            // Cria grafo com a quantidade correta de vértices
            Graph g = new Graph(n);

            // Converte matriz → lista de adjacência
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++) // evita duplicar arestas
                {
                    if (matriz[i, j] != 0)
                    {
                        g.AddEdge(i, j, matriz[i, j]);
                    }
                }
            }

            // Mostra grafo
            g.PrintGraph();

            Console.WriteLine();
            Console.Write("Digite o vértice inicial para BFS: ");
            int inicio = int.Parse(Console.ReadLine()!);

            Console.WriteLine();
            g.BFS(inicio);

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
