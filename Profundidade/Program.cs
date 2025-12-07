using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using AlgoritmosGrafos.Utils;

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

    // Retorna a ordem de visita (lista de vértices na sequência em que foram visitados)
    public static List<char> GetVisitOrder(Grafo grafo, char inicio)
    {
        var visitados = new HashSet<char>();
        var order = new List<char>();
        DFSRecursivoOrder(grafo, inicio, visitados, order);
        return order;
    }

    private static void DFSRecursivoOrder(Grafo grafo, char v, HashSet<char> visitados, List<char> order)
    {
        order.Add(v);
        visitados.Add(v);

        foreach (var (vizinho, peso) in grafo.ObterAdj()[v])
        {
            if (!visitados.Contains(vizinho))
            {
                DFSRecursivoOrder(grafo, vizinho, visitados, order);
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

        Console.WriteLine();

        // Gera e exporta o grafo usando MSAGL + ExportarGrafo
        try
        {
            var adj = g.ObterAdj();
            var msagl = new Graph("Profundidade");

            // calcula ordem de visita e adiciona nós com rótulos indicando a ordem
            var visitOrder = DFS.GetVisitOrder(g, 'A');
            int m = visitOrder.Count;
            for (int i = 0; i < m; i++)
            {
                var v = visitOrder[i];
                var node = msagl.AddNode(v.ToString());
                // label with visit order
                node.LabelText = v + " (" + (i + 1).ToString() + ")";

                // make nodes circular and fill with decreasing green tones
                node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                // compute green intensity from strong (maxG) to lighter (minG)
                int maxG = 220;
                int minG = 80;
                int greenVal = (m > 1) ? (maxG - (i * (maxG - minG) / (m - 1))) : maxG;
                int redVal = 40;
                int blueVal = 40;
                try
                {
                    // try constructing color directly (constructor may accept bytes)
                    node.Attr.FillColor = new Microsoft.Msagl.Drawing.Color((byte)redVal, (byte)greenVal, (byte)blueVal);
                }
                catch
                {
                    // fallback to a named color if constructor not available
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;
                }
            }

            // adiciona arestas evitando duplicatas (usar ordem dos chars)
            var added = new HashSet<string>();
            foreach (var u in adj.Keys)
            {
                foreach (var (v, peso) in adj[u])
                {
                    string key = u < v ? u.ToString() + ":" + v.ToString() : v.ToString() + ":" + u.ToString();
                    if (added.Contains(key)) continue;
                    added.Add(key);

                    var edge = msagl.AddEdge(u.ToString(), v.ToString());
                    edge.LabelText = peso.ToString();
                    edge.Attr.LineWidth = 2;
                    edge.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkGreen;
                    edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }
            }

            var saidaRel = System.IO.Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "meu_grafo_profundidade.jpg");
            var saida = System.IO.Path.GetFullPath(saidaRel);
            ExportarGrafo.SalvarGrafoComoJpg(msagl, saida, 1200, 800);
            Console.WriteLine("Grafo Profundidade salvo em: " + saida);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível gerar/exportar o grafo (Profundidade): " + ex.Message);
        }

        Console.WriteLine("\nPressione ENTER para sair...");
        Console.ReadLine();
    }
}