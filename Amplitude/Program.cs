using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


public class LeitorDeGrafo
{
    private int Vertices;
    private List<(int destino, int peso)>[] adj;

    public LeitorDeGrafo(int vertices)
    {
        Vertices = vertices;
        adj = new List<(int destino, int peso)>[vertices];

        for (int i = 0; i < vertices; i++)
            adj[i] = new List<(int destino, int peso)>();
    }

    public void AddEdge(int origem, int destino, int peso)
    {
        // Grafo NÃO DIRECIONADO → adiciona nos dois sentidos
        adj[origem].Add((destino, peso));
        adj[destino].Add((origem, peso));
    }

    public void BFS(int inicio)
    {
        bool[] visitado = new bool[Vertices];
        Queue<int> fila = new Queue<int>();

        visitado[inicio] = true;
        fila.Enqueue(inicio);

        Console.WriteLine("Travessia BFS:");

        while (fila.Count > 0)
        {
            int atual = fila.Dequeue();
            Console.Write(atual + " ");

            foreach (var vizinho in adj[atual])
            {
                if (!visitado[vizinho.destino])
                {
                    visitado[vizinho.destino] = true;
                    fila.Enqueue(vizinho.destino);
                }
            }
        }

        Console.WriteLine();
    }

    public void PrintGraph()
    {
        Console.WriteLine("LISTA DE ADJACÊNCIA:");
        Console.WriteLine("");
        for (int i = 0; i < Vertices; i++)
        {
            Console.Write($"Vértice {i}: ");
            foreach (var (dest, peso) in adj[i])
            {
                Console.Write($"-> ({dest}, peso: {peso}) ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        LeitorDeGrafo g = new LeitorDeGrafo(6);

        // Caminho para o arquivo de grafo (relativo à pasta bin)
        string caminhoGrafo = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Utils", "Grafoteste.txt");
        caminhoGrafo = Path.GetFullPath(caminhoGrafo);

        // Lê o grafo do arquivo
        // int[,] grafo = LeitorDeGrafo.LerGrafoDeArquivo(caminhoGrafo);

        // Exibe a matriz de adjacência
        Console.WriteLine("Matriz de Adjacência lida do arquivo:");
        //LeitorDeGrafo.MostraGrafo(grafo);
        Console.WriteLine();

        // Converte a matriz em um grafo com lista de adjacência
        for (int i = 0; i < 6; i++)
        {
            for (int j = i + 1; j < 6; j++) // Começa de i+1 para evitar duplicatas em grafo não-direcionado
            {
                if (grafo[i, j] != 0)
                {
                    g.AddEdge(i, j, grafo[i, j]);
                }
            }
        }

        // Exibe a lista de adjacência
        Console.WriteLine();
        g.PrintGraph();
        Console.WriteLine();

        // Executa BFS começando do vértice 0
        g.BFS(0);
    }
}