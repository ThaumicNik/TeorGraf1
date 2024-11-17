using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorGraf1
{
    internal class ArcListGraph
    {
        public List<Arc> Arcs;

        public class Arc
        {
            public int FirstNode { get; set; }
            public int SecondNode { get; set; }
            public int Weight { get; set; }
            public int ID { get; set; }

            public Arc(int first, int second, int weight)
            {
                FirstNode = first;
                SecondNode = second;
                Weight = weight;
            }
        }

        public ArcListGraph()
        {
            Arcs = new List<Arc>();
        }

        // Ниже не представлены реализации методов для вершин, т.к. подобные методы
        // не имеют смысла при представлении в виде списка дуг

        public Arc FindArc(int firstNode, int secondNode)
        {
            foreach (Arc arc in Arcs)
            {
                if (arc.FirstNode == firstNode && arc.SecondNode == secondNode)
                return arc;
            }
            return null;
        }

        public Arc AddArc(int first, int second, int weight)
        {
            if (FindArc(first, second) != null)
                return null;
            Arc arc = new Arc(first, second, weight);
            Arcs.Add(arc);
            return arc;
        }

        public void RemoveEdge(int firstNode, int secondNode)
        {
            Arc toRemove = FindArc(firstNode, secondNode);
            if (toRemove != null)
                Arcs.Remove(toRemove);
        }

        public void Print()
        {
            foreach (Arc arc in Arcs)
            {
                Console.WriteLine($"Дуга от вершины {arc.FirstNode} к вершине {arc.SecondNode} с весом {arc.Weight}");
            }
        }

    }
}
