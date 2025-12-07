using System;
using AlgoritmosGrafos.Utils;
using System.IO;

namespace Amplitude
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lê matriz do arquivo
            var caminhoRel = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Utils", "Grafoteste.txt");
            var caminhoGrafo = Path.GetFullPath(caminhoRel);
            Console.WriteLine($"Lendo grafo de: {caminhoGrafo}");
            int[,] matriz = LeitorDeGrafo.LerGrafoDeArquivo(caminhoGrafo);


            int n = matriz.GetLength(0);

            // Cria grafo com a quantidade correta de vértices
            Graph g = new Graph(n);

            // Converte matriz → lista de adjacência
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++) // evita duplicar arestas
                {
                    if (matriz[i, j] != 0)
                    {
                        g.AddEdge(i, j, matriz[i, j]);
                    }
                }
            }

            // Mostra grafo
            g.PrintGraph();

            Console.WriteLine();
            Console.Write("Digite o vértice inicial para BFS: ");
            int inicio = int.Parse(Console.ReadLine()!);

            Console.WriteLine();
            g.BFS(inicio);

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();

            // Gera e exporta visualização do grafo com MSAGL, aplicando cores por ordem de BFS
            try
            {
                var msagl = GerarGrafo.MontarGrafoAPartirDaMatriz(matriz);

                // obtém ordem do BFS e rotula/nomeia nós com ordem
                var bfsOrder = g.GetBFSOrder(inicio);
                int m = bfsOrder.Count;

                // aplica labels e cores decrescentes em tons de verde
                for (int i = 0; i < m; i++)
                {
                    int v = bfsOrder[i];
                    var node = msagl.FindNode(v.ToString());
                    if (node == null) continue;
                    node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                    node.LabelText = v + " (" + (i + 1).ToString() + ")";

                    int maxG = 220;
                    int minG = 80;
                    int greenVal = (m > 1) ? (maxG - (i * (maxG - minG) / (m - 1))) : maxG;
                    int redVal = 40;
                    int blueVal = 40;
                    try
                    {
                        node.Attr.FillColor = new Microsoft.Msagl.Drawing.Color((byte)redVal, (byte)greenVal, (byte)blueVal);
                    }
                    catch
                    {
                        node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;
                    }
                }

                var saidaRel = System.IO.Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "meu_grafo_amplitude.jpg");
                var saida = System.IO.Path.GetFullPath(saidaRel);
                ExportarGrafo.SalvarGrafoComoJpg(msagl, saida, 1200, 800);
                Console.WriteLine($"Grafo (Amplitude) salvo em: {saida}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível gerar/exportar o grafo (Amplitude): " + ex.Message);
            }
        }
    }
}
