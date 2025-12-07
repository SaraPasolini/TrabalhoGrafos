using System;
using System.IO;
using AlgoritmosGrafos.Utils;   // LeitorDeGrafo e GrafoLista
using Amplitude;      // BFS
using Profundidade;   // DFS
using Dijkstra;       // Dijkstra
using Prim;           // Prim

namespace TrabalhoGrafos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE ALGORITMOS EM GRAFOS ===");

            // Caminho do grafo
            string caminho = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Utils", "GrafoTeste.txt");
            caminho = Path.GetFullPath(caminho);

            // Lê matriz
            int[,] grafo = LeitorDeGrafo.LerGrafoDeArquivo(caminho);
            int n = grafo.GetLength(0);

            // CONVERTE para lista de adjacência
            GrafoLista lista = new GrafoLista(n);

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (grafo[i, j] != 0)
                        lista.AddEdge(i, j, grafo[i, j]);
                }
            }

            while (true)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Mostrar Grafo");
                Console.WriteLine("2 - BFS (Amplitude)");
                Console.WriteLine("3 - DFS (Profundidade)");
                Console.WriteLine("4 - Dijkstra");
                Console.WriteLine("5 - Prim");
                Console.WriteLine("0 - Sair");

                Console.Write("Opção: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        lista.PrintGraph();
                        break;

                    case "2":
                        Console.Write("Início BFS: ");
                        lista.BFS(int.Parse(Console.ReadLine()));
                        break;

                    case "3":
                        Console.Write("Início DFS: ");
                        DFS.RunDFS(lista, int.Parse(Console.ReadLine()));
                        break;

                    case "4":
                        Console.Write("Origem Dijkstra: ");
                        Dijkstra.RunDijkstra(lista, int.Parse(Console.ReadLine()));
                        break;

                    case "5":
                        Prim.RunPrim(lista);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
