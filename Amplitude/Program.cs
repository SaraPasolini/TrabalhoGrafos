using System;
using System.Collections.Generic;
using LeitorDeGrafos.Utils; // Importa o leitor de grafo

public class GrafoLista
{
    private int Vertices;
    private List<(int destino, int peso)>[] adj;

    public GrafoLista(int vertices)
    {
        Vertices = vertices;
        adj = new List<(int destino, int peso)>[vertices];

        for (int i = 0; i < vertices; i++)
            adj[i] = new List<(int destino, int peso)>();
    }

    public void AddEdge(int origem, int destino, int peso)
    {
        // Como o grafo é não direcionado, adiciona nas duas direções
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
        Console.WriteLine("LISTA DE ADJACÊNCIA:\n");

        for (int i = 0; i < Vertices; i++)
        {
            Console.Write($"Vértice {i}: ");
            foreach (var (dest, peso) in adj[i])
                Console.Write($"-> ({dest}, peso: {peso}) ");

            Console.WriteLine();
        }
    }
}
