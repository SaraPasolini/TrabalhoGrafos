using System;
using System.IO;
using LeitorDeGrafos.Utils;
using Amplitude;       // BFS
using Profundidade;   // DFS
using Dijksta;    // Dijkstra
using PRIM;        // Prim

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
                    int inicioBfs = int.Parse(Console.ReadLine());
                    lista.BFS(inicioBfs);
                    break;

                case "3":
                    Console.Write("Início DFS: ");
                    int inicioDfs = int.Parse(Console.ReadLine());
                    DFS.RunDFS(lista, inicioDfs);   // SUA FUNÇÃO DFS
                    break;

                case "4":
                    Console.Write("Origem Dijkstra: ");
                    int inicioDij = int.Parse(Console.ReadLine());
                    Dijkstra.RunDijkstra(lista, inicioDij);   // SUA FUNÇÃO
                    break;

                case "5":
                    Prim.RunPrim(lista);   // SUA FUNÇÃO
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
