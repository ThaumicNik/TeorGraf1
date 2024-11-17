using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorGraf1
{
    internal class AdjacencyListGraph
    {
        List<AdjacencyLine> AdjacencyList;

        // Эти классы необходимы для хранения данных о весе дуг и вершинах
        public class AdjacencedNode
        {
            public int Weight;
            public int NodeID;

            public AdjacencedNode(int nodeID, int weight)
            {
                this.NodeID = nodeID;
                this.Weight = weight;
            }
        }

        public class AdjacencyLine
        {
            public List<AdjacencedNode> Line;
            public int NodeID;

            public AdjacencyLine(int nodeID)
            {
                Line = new List<AdjacencedNode>();
                NodeID = nodeID;
            }
        }

        public AdjacencyListGraph()
        {
            AdjacencyList = new();
        }

        public AdjacencyLine FindNode(int nodeID)
        {
            foreach(AdjacencyLine line in AdjacencyList)
            {
                if(line.NodeID == nodeID)
                {
                    return line;
                }
            }
            return null;
        }

        public AdjacencyLine AddNode(int nodeID)
        {
            foreach (AdjacencyLine line in AdjacencyList)
            {
                if (line.NodeID == nodeID)
                {
                    return null;
                }
            }
            AdjacencyLine output = new AdjacencyLine(nodeID);
            AdjacencyList.Add(output);
            return output;
        }

        public void RemoveNode(int nodeID)
        {
            foreach (AdjacencyLine line in AdjacencyList)
            {
                if (line.NodeID == nodeID)
                {
                    AdjacencyList.Remove(line);
                    break;
                }
            }
            foreach (AdjacencyLine line in AdjacencyList)
            {
                foreach(AdjacencedNode node in line.Line)
                {
                    if(node.NodeID == nodeID)
                    {
                        line.Line.Remove(node);
                    }
                }
            }

        }

        public AdjacencedNode FindArc(int firstNode, int secondNode)
        {
            foreach (AdjacencyLine line in AdjacencyList)
            {
                if(line.NodeID == firstNode)
                {
                    foreach(AdjacencedNode node in line.Line)
                    {
                        if(node.NodeID == secondNode)
                        {
                            return node;
                        }
                    }
                    return null;
                }
            }
            // Два return, т.к. если мы нашли первый узел, но не нашли второй,
            // то мы гарантированно уже не найдём его
            return null;
        }

        public AdjacencedNode AddArc(int firstNode, int secondNode, int weight = 1)
        {
            // Проверяем на то, есть ли уже данная дуга
            AdjacencedNode tempNode = FindArc(firstNode, secondNode);
            if (tempNode != null)
                return tempNode;

            // Если узлов, к которому мы хотим привязаться не существует, мы его создаём.
            // Сначала рассматриваем второй узел, т.к. в конце outputLine должен содержать ссылку на первый
            AdjacencyLine tempLine = FindNode(secondNode);
            if(tempLine == null)
            {
                AddNode(secondNode);
            }

            // Теперь первый
            tempLine = FindNode(firstNode);
            if(tempLine == null)
            {
                tempLine = AddNode(firstNode);
            }

            tempNode = new AdjacencedNode(secondNode, weight);
            tempLine.Line.Add(tempNode);
            return tempNode;
        }

        // Тут всё похоже на предыдущий метод. Я не буду подробно расписывать.
        public void RemoveArc(int firstNode, int secondNode)
        {
            AdjacencedNode arc = FindArc(firstNode, secondNode);
            if (arc == null)
            {
                return;
            }

            AdjacencyLine line = FindNode(firstNode);
            line.Line.Remove(arc);
        }

        public void Print()
        {
            foreach(AdjacencyLine line in AdjacencyList)
            {
                Console.Write($"{line.NodeID}:");
                foreach(AdjacencedNode node in line.Line)
                {
                    Console.Write($" {node.NodeID}");
                }
                Console.Write("\n");
            }
        }
    }
}
