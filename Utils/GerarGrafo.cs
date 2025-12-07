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

            foreach (var v in vertices)
            {
                graph.AddNode(v.ToString()).Attr.FillColor = Color.LightBlue;
            }

            foreach (var v in vertices)
            {
                if (v < 0 || v >= parent.Length) continue;
                int p = parent[v];
                if (p != -1)
                {
                    var edge = graph.AddEdge(p.ToString(), v.ToString());
                    edge.Attr.Color = Color.DarkGreen;
                    edge.Attr.LineWidth = 2;
                }
            }

            return graph;
        }
    }
}
