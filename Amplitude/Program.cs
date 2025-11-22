using System;

class Program
{
    static void Main(string[] args)
    {
        // Exemplo: grafo com 6 vértices (0 a 5)
        Graph g = new Graph(6);

        // Arestas (origem, destino, peso)
        g.AddEdge(0, 1, 2);
        g.AddEdge(0, 2, 4);
        g.AddEdge(1, 3, 1);
        g.AddEdge(2, 4, 3);
        g.AddEdge(3, 5, 5);
        g.AddEdge(4, 5, 2);

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
