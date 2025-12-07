using System;
using System.Collections.Generic;

class Grafo
{
    // Lista de adjacência: vértice (vizinho, peso)
    private Dictionary<char, List<(char, int)>> adj;

    public Grafo()
    {
        adj = new Dictionary<char, List<(char, int)>>();
    }

    public void AdicionarAresta(char u, char v, int peso)
    {
        if (!adj.ContainsKey(u))
            adj[u] = new List<(char, int)>();

        if (!adj.ContainsKey(v))
            adj[v] = new List<(char, int)>();

        adj[u].Add((v, peso));
        adj[v].Add((u, peso)); // Não direcionado
    }

    public Dictionary<char, List<(char, int)>> ObterAdj()
    {
        return adj;
    }
}

class DFS
{
    public static void ExecutarDFS(Grafo grafo, char inicio)
    {
        HashSet<char> visitados = new HashSet<char>();
        Console.WriteLine("DFS a partir do vertice " + inicio + ":");
        DFSRecursivo(grafo, inicio, visitados);
        Console.WriteLine();
    }

    private static void DFSRecursivo(Grafo grafo, char v, HashSet<char> visitados)
    {
        Console.Write(v + " ");
        visitados.Add(v);

        foreach (var (vizinho, peso) in grafo.ObterAdj()[v])
        {
            if (!visitados.Contains(vizinho))
            {
                DFSRecursivo(grafo, vizinho, visitados);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Grafo g = new Grafo();

        // Arestas do grafo da imagem
        g.AdicionarAresta('A', 'B', 7);
        g.AdicionarAresta('A', 'D', 5);
        g.AdicionarAresta('D', 'B', 9);
        g.AdicionarAresta('B', 'C', 8);
        g.AdicionarAresta('B', 'E', 7);
        g.AdicionarAresta('C', 'E', 5);
        g.AdicionarAresta('D', 'E', 15);
        g.AdicionarAresta('D', 'F', 6);
        g.AdicionarAresta('F', 'E', 8);

        // Execução da DFS a partir de A
        DFS.ExecutarDFS(g, 'A');

        Console.WriteLine("\nPressione ENTER para sair...");
        Console.ReadLine();
    }
}