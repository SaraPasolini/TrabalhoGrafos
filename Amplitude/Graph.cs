using System;
using System.Collections.Generic;
using AlgoritmosGrafos.Utils;

public class Graph
{
    private int Vertices;
    private List<(int destino, int peso)>[] adj; // Lista de adjacência representando o grafo

    public Graph(int vertices)
    {
        Vertices = vertices;
        adj = new List<(int destino, int peso)>[vertices]; // Inicializa a lista de adjacência

        for (int i = 0; i < vertices; i++)
            adj[i] = new List<(int destino, int peso)>(); // Inicializa a lista de adjacência para cada vértice
    }

    public void AddEdge(int origem, int destino, int peso) // Adiciona aresta ao grafo
    {
        // Grafo NÃO DIRECIONADO → adiciona nos dois sentidos
        adj[origem].Add((destino, peso));
        adj[destino].Add((origem, peso));
    }

    public void BFS(int inicio)
    {
        bool[] visitado = new bool[Vertices]; // Marca os vértices visitados
        Queue<int> fila = new Queue<int>(); // Fila para a travessia BFS

        visitado[inicio] = true; // Marca o vértice inicial como visitado
        fila.Enqueue(inicio); // Enfileira o vértice inicial

        Console.WriteLine("Travessia BFS:");

        while (fila.Count > 0) // Enquanto houver vértices na fila
        {
            int atual = fila.Dequeue();
            Console.Write(atual + " "); // Processa o vértice atual

            foreach (var vizinho in adj[atual]) // Percorre os vizinhos do vértice atual
            {
                if (!visitado[vizinho.destino]) // Se o vizinho não foi visitado
                {
                    visitado[vizinho.destino] = true; // Marca o vizinho como visitado
                    fila.Enqueue(vizinho.destino); // Enfileira o vizinho
                }
            }
        }

        Console.WriteLine();
    }

    public void PrintGraph() // Imprime a lista de adjacência do grafo
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