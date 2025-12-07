using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static int[,] LerMatrizAdjacencia(string caminho)
    {
        string[] linhas = File.ReadAllLines(caminho);

        int quantidadeVertices = int.Parse(linhas[0]);
        int[,] matriz = new int[quantidadeVertices, quantidadeVertices];

        for (int i = 1; i <= quantidadeVertices; i++)
        {
            string[] valores = linhas[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < quantidadeVertices; j++)
            {
                int peso = int.Parse(valores[j]);
                matriz[i - 1, j] = (peso == 0 ? int.MaxValue : peso); 
            }
        }

        return matriz;
    }

    
    static (List<(int origem, int destino, int peso)> agm, int pesoTotal) Prim(int[,] matriz)
    {
        int quantidadeVertices = matriz.GetLength(0);

        bool[] inclusoNaArvore = new bool[quantidadeVertices];
        int[] menorPeso = new int[quantidadeVertices];     
        int[] pai = new int[quantidadeVertices];           

        for (int i = 0; i < quantidadeVertices; i++)
        {
            menorPeso[i] = int.MaxValue;
            pai[i] = -1;
        }

        menorPeso[0] = 0; 

        for (int contador = 0; contador < quantidadeVertices - 1; contador++)
        {
            int verticeEscolhido = -1;
            int menorValor = int.MaxValue;

            
            for (int v = 0; v < quantidadeVertices; v++)
            {
                if (!inclusoNaArvore[v] && menorPeso[v] < menorValor)
                {
                    menorValor = menorPeso[v];
                    verticeEscolhido = v;
                }
            }

            if (verticeEscolhido == -1)
                break;

       
            inclusoNaArvore[verticeEscolhido] = true;

            
            for (int vizinho = 0; vizinho < quantidadeVertices; vizinho++)
            {
                int pesoAresta = matriz[verticeEscolhido, vizinho];

                if (pesoAresta != int.MaxValue &&
                    !inclusoNaArvore[vizinho] &&
                    pesoAresta < menorPeso[vizinho])
                {
                    menorPeso[vizinho] = pesoAresta;
                    pai[vizinho] = verticeEscolhido;
                }
            }
        }

        var agm = new List<(int origem, int destino, int peso)>();
        int pesoTotal = 0;

      
        for (int v = 1; v < quantidadeVertices; v++)
        {
            if (pai[v] != -1)
            {
                int peso = matriz[pai[v], v];
                agm.Add((pai[v], v, peso));
                pesoTotal += peso;
            }
        }

        return (agm, pesoTotal);
    }
    
    static void Main()
    {
        try
        {
            // resolve caminho do arquivo de entrada (procura Utils/Grafoteste.txt na raiz do repo)
            var caminhoRel = System.IO.Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Utils", "Grafoteste.txt");
            var caminho = System.IO.Path.GetFullPath(caminhoRel);
            Console.WriteLine("Lendo arquivo de grafo em: " + caminho);

            int[,] matriz = LerMatrizAdjacencia(caminho);

            var (agm, pesoTotal) = Prim(matriz);

            Console.WriteLine($"Peso total da AGM: {pesoTotal}");
            Console.WriteLine("Arestas da AGM:");
            foreach (var (o, d, p) in agm)
            {
                Console.WriteLine($"{o} - {d} (peso: {p})");
            }

            // gerar grafo MSAGL a partir da lista da AGM e salvar como jpg
            var graph = AlgoritmosGrafos.Utils.GerarGrafo.MontarGrafoAPartirDaLista(agm, matriz.GetLength(0));

            var saidaRel = System.IO.Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "meu_grafo_prim.jpg");
            var saida = System.IO.Path.GetFullPath(saidaRel);

            AlgoritmosGrafos.Utils.ExportarGrafo.SalvarGrafoComoJpg(graph, saida, 1200, 800);
            Console.WriteLine("Grafo PRIM salvo em: " + saida);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
    }
}
