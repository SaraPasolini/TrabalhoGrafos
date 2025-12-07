using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

namespace AlgoritmosGrafos.Utils
{
    public static class GerarGrafo
    {
        public static Graph MontarGrafo(List<int> vertices, int[] parent)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            var graph = new Graph("Grafo");
            // garantir grafo não-direcionado por padrão
            graph.Directed = false;

            // tentar aplicar configurações de layout quando disponível (sem definir propriedades específicas)
            try
            {
                graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.Layered.SugiyamaLayoutSettings();
            }
            catch
            {
                // ignora se a API não estiver disponível na versão do MSAGL
            }

            foreach (var v in vertices)
            {
                graph.AddNode(v.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
            }

            foreach (var v in vertices)
            {
                if (v < 0 || v >= parent.Length) continue;
                int p = parent[v];
                if (p != -1)
                {
                    var edge = graph.AddEdge(p.ToString(), v.ToString());
                    edge.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkGreen;
                    edge.Attr.LineWidth = 2;
                    // garantir sem setas (apenas arestas)
                    edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }
            }

            return graph;
        }

        public static Graph MontarGrafoAPartirDaMatriz(int[,] matrix, bool directed = false)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            int n = matrix.GetLength(0);
            if (matrix.GetLength(1) != n) throw new ArgumentException("Matriz deve ser quadrada", nameof(matrix));

            var graph = new Graph("Grafo");
            graph.Directed = directed;

            // adiciona nós
            for (int i = 0; i < n; i++)
            {
                var node = graph.AddNode(i.ToString());
                node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;
            }

            // adiciona arestas (para matriz não-direcionada, assume i<j já evita duplicatas)
            for (int i = 0; i < n; i++)
            {
                int jStart = directed ? 0 : i + 1; // se não-direcionado, evita duplicatas usando j>i
                for (int j = jStart; j < n; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        var edge = graph.AddEdge(i.ToString(), j.ToString());
                        edge.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkGreen;
                        edge.Attr.LineWidth = 2;
                        // garantir sem setas (apenas arestas)
                        edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        // se houver peso, coloca rótulo
                        if (matrix[i, j] != 1)
                            edge.LabelText = matrix[i, j].ToString();
                    }
                }
            }

            return graph;
        }

        public static Graph MontarGrafoAPartirDaLista(List<(int origem, int destino, int peso)> agm, int numVertices)
        {
            if (agm == null) throw new ArgumentNullException(nameof(agm));

            var graph = new Graph("Grafo");
            graph.Directed = false;

            // adiciona nós
            for (int i = 0; i < numVertices; i++)
            {
                var node = graph.AddNode(i.ToString());
                node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;
            }

            // adiciona arestas da AGM
            foreach (var (origem, destino, peso) in agm)
            {
                var edge = graph.AddEdge(origem.ToString(), destino.ToString());
                edge.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkGreen;
                edge.Attr.LineWidth = 2;
                edge.LabelText = peso.ToString();
                // garantir sem setas
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }

            return graph;
        }
    }
}
