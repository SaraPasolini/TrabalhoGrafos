using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;

namespace AlgoritmosGrafos.Utils
{
    public static class ExportarGrafo
    {
        public static void SalvarGrafoComoJpg(Graph g, string caminho, int width = 1200, int height = 800)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            if (string.IsNullOrWhiteSpace(caminho)) throw new ArgumentNullException(nameof(caminho));

            var renderer = new GraphRenderer(g);
            renderer.CalculateLayout();

            using (var bmp = new Bitmap(width, height))
            {
                using (var g2 = Graphics.FromImage(bmp))
                {
                    g2.Clear(System.Drawing.Color.White);
                    renderer.Render(g2, new System.Drawing.Rectangle(0, 0, width, height));
                }     

                bmp.Save(caminho, ImageFormat.Jpeg);
            }
        }
    }
}
