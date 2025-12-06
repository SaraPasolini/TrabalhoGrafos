using System;                         
using AlgoritmosGrafos.Utils;          // Importa a classe LeitorDeGrafo
class GrafoDijkstra                   // Declaração da classe principal do programa.
{
    public const double INF = double.PositiveInfinity;
    // Constante que representa infinito e é usada quando não houver conexão entre dois vértices.
    // Método que implementa o algoritmo de Dijkstra.
    // Retorna duas coisas: o vetor de distâncias mínimas e o vetor de predecessores.
    public static (double[] dist, int[] prev) Dijkstra(double[,] W, int source)
    {
        int n = W.GetLength(0);          // Obtém o número de vértices do grafo (é o tamanho da matriz).

        double[] dist = new double[n];   // Vetor que armazenará as menores distâncias encontradas.
        bool[] visited = new bool[n];    // Marca se um vértice já foi visitado.
        int[] prev = new int[n];         // Guarda o vértice anterior no caminho mínimo.

        // Inicialização dos vetores
        for (int i = 0; i < n; i++)
        {
            dist[i] = INF;              // Todas as distâncias começam como infinito.
            visited[i] = false;         // Nenhum vértice foi visitado.
            prev[i] = -1;               // Nenhum predecessor definido ainda.
        }

        dist[source] = 0;               // A distância da origem para ela mesma é zero.

        // Loop principal: processa todos os vértices
        for (int i = 0; i < n; i++)
        {
            int u = -1;                 // Armazena o vértice com menor distância ainda não visitado.
            double best = INF;          // Armazena a melhor distância encontrada.

            // Procura o vértice ainda não visitado com menor dist[u]
            for (int v = 0; v < n; v++)
            {
                if (!visited[v] && dist[v] < best)
                {
                    best = dist[v];
                    u = v;              // u agora é o vértice mais próximo.
                }
            }

            if (u == -1)                // Caso não haja vértices alcançáveis restantes.
                break;

            visited[u] = true;          // Marca o vértice como visitado.

            // Relaxamento das arestas: tenta melhorar distâncias
            for (int v = 0; v < n; v++)
            {
                double w = W[u, v];     // Peso da aresta u->v.

                // Só tenta atualizar se:
                // v não foi visitado
                // existe aresta (w > 0)
                // w não é infinito
                if (!visited[v] && w != INF && w > 0)
                {
                    double alt = dist[u] + w;   // Testa novo caminho até v.

                    // Se encontrou caminho melhor:
                    if (alt < dist[v])
                    {
                        dist[v] = alt;          // Atualiza distância.
                        prev[v] = u;            // Guarda o que veio antes.
                    }
                }
            }
        }

        return (dist, prev);            // Retorna os resultados.
    }

    // Imprime o caminho mínimo reconstruindo a rota a partir dos predecessores.
    public static void PrintPath(int[] prev, double[] dist, int source, int target)
    {
        if (dist[target] == INF)                    // Se a distância é infinita, não existe caminho.
        {
            Console.WriteLine($"Não existe caminho de {source} até {target}.");
            return;
        }

        var path = new System.Collections.Generic.List<int>();  // Lista para guardar o caminho.
        int u = target;                                         // Começa do destino.

        // Caminha de trás pra frente usando o vetor prev[]
        while (u != -1)
        {
            path.Add(u);               // Adiciona o vértice na rota.
            if (u == source) break;    // Se chegou à origem, para.
            u = prev[u];               // Vai para o predecessor.
        }

        path.Reverse();                // Inverte a lista para ficar do início ao final.

        Console.WriteLine($"Caminho {source} até {target}: " + string.Join(" -> ", path));
    }

    // Método principal do programa (ponto de entrada).
    static void Main()
    {
        Console.WriteLine("Lendo grafo do arquivo Grafo.txt...\n");

        // Lê a matriz de adjacência do arquivo usando a classe utilitária LeitorDeGrafo.
        int[,] grafoInt = LeitorDeGrafo.LerGrafoDeArquivo("../Grafo.txt");

        // Mostra a matriz lida na tela.
        Console.WriteLine("Matriz lida:");
        LeitorDeGrafo.MostraGrafo(grafoInt);
        Console.WriteLine();

        // Converte a matriz int[,] para double[,], substituindo 0 por infinito.
        int n = grafoInt.GetLength(0);
        double[,] W = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    W[i, j] = 0;                     // Distância de um vértice para ele mesmo = 0
                else if (grafoInt[i, j] == 0)
                    W[i, j] = INF;                  // Sem aresta, vira infinito.
                else
                    W[i, j] = grafoInt[i, j];       // Caso contrário, peso real.
            }
        }

        int source = 0;                // Define o vértice de origem para o algoritmo.

        // Executa Dijkstra.
        var (dist, prev) = Dijkstra(W, source);

        // Imprime as distâncias calculadas.
        Console.WriteLine($"Distâncias a partir do vértice {source}:");
        for (int i = 0; i < dist.Length; i++)
        {
            Console.WriteLine($"{source} -> {i} = {dist[i]}");
        }

        Console.WriteLine();
        Console.WriteLine("Caminho mínimo:");

        // Imprime o caminho mínimo para cada vértice do grafo.
        for (int t = 0; t < dist.Length; t++)
            PrintPath(prev, dist, source, t);
    }
}
