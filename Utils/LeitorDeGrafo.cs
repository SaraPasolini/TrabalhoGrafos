namespace AlgoritmosGrafos.Utils
{
    public static class LeitorDeGrafo
    {
        public static int[,] LerGrafoDeArquivo(string caminho)
        {
            var linhas = File.ReadAllLines(caminho);

            int n = int.Parse(linhas[0]);  
            int[,] grafo = new int[n, n];

            for (int i = 1; i <= n; i++)
            {
                var valores = linhas[i].Split(' ');

                for (int j = 0; j < n; j++)
                {
                    grafo[i - 1, j] = int.Parse(valores[j]);
                }
            }

            return grafo;
        }
         public static void MostraGrafo(int[,] grafo)
        {
            int n = grafo.GetLength(0);
           for(int i=0; i<n;i++)
            {
                for(int j=0; j<n;j++)
                {
                    Console.Write(grafo[i,j]+ " ");
                }
                Console.WriteLine();
            } 
        }

    }

}
