using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorGraf1
{
    // В задании указано что нужно сделать представление ориентированного графа, но при этом есть
    // задание на список рёбер, т.е. неориентированных. Ниже представлена реализация
    // неориентированного взвешенного графа.
    internal class EdgeListGraph
    {
        public List<Edge> Edges;

        public class Edge
        {
            public int FirstNode { get; set; }
            public int SecondNode { get; set; }
            public int Weight { get; set; }

            public Edge(int first, int second, int weight)
            {
                FirstNode = first;
                SecondNode = second;
                Weight = weight;
            }
        }

        public EdgeListGraph()
        {
            Edges = new List<Edge>();
        }
        
        // Ниже не представлены реализации методов для вершин, т.к. подобные методы

        public Edge FindEdge(int firstNode, int secondNode)
        {
            foreach (Edge edge in Edges)
            {
                if (edge.FirstNode == firstNode || edge.FirstNode == secondNode)
                    if(edge.SecondNode == secondNode || edge.SecondNode == firstNode)
                        return edge;
            }
            return null;
        }

        public Edge AddEdge(int first, int second, int weight)
        {
            if(FindEdge(first, second) != null)
                return null;
            Edge edge = new Edge(first, second, weight);
            Edges.Add(edge);
            return edge;
        }

        public void RemoveEdge(int firstNode, int secondNode)
        {
            Edge toRemove = FindEdge(firstNode, secondNode);
            if (toRemove != null)
                Edges.Remove(toRemove);
        }

        public void Print()
        {
            foreach(Edge edge in Edges)
            {
                Console.WriteLine($"Ребро между вершинами {edge.FirstNode} и {edge.SecondNode} с весом {edge.Weight}");
            }
        }

    }
}
