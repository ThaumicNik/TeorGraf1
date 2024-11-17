using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

// Если честно, данный способ я не до конца понял.
// Я нашёл реализацию в учебнике, но это, как по мне, слишком сложно и неоднородно.
// Для частной реализации - пойдёт, но для постоянного использования недостаточно гибко.
namespace TeorGraf1
{
    internal class ArcBunchGraph
    {
        // Словари мне показались удобнее чем просто массивы, буду использовать их.
        public Dictionary<int, int> Headers;
        public Dictionary<int, int> Continues;
        public Dictionary<int, int> Destinations;
        public Dictionary<int, int> Weights;

        public ArcBunchGraph()
        {
            Headers = new();
            Continues = new();
            Destinations = new();
            Weights = new();
        }

        // Создание уникальных идентификаторов
        // Вспомогательные поля
        private static int nodeUID = 0;
        private static int arcUID = 0;

        public static int GetNodeUID()
        {
            nodeUID++;
            return nodeUID;
        }

        public static int GetArcID()
        {
            arcUID++;
            return arcUID;
        }

        public void AddNode(int id)
        {
            if (!Headers.ContainsKey(id))
            {
                // Дуга -1 обозначает отсутствие дуги. Нам надо хранить вершины без дуг
                Headers.Add(id, -1);
                nodeUID = Math.Max(nodeUID, id);
            }
        }

        public void AddArc(int firstNode, int secondNode, int weight)
        {
            if (!Headers.ContainsKey(firstNode))
            {
                return;
            }
            if (!Headers.ContainsKey(secondNode))
            {
                return;
            }
            int newArc = GetArcID();
            if (Headers[firstNode] == -1)
            {
                Headers[firstNode] = newArc;
            }
            else
            {
                int tempArc = Headers[firstNode];
                while (tempArc > 0)
                {
                    if (Continues[tempArc] == -1)
                    {
                        Continues[tempArc] = newArc;
                    }
                    tempArc = Continues[tempArc];
                }
            }
            Continues.Add(newArc, -1);
            Destinations.Add(newArc, secondNode);
            Weights.Add(newArc, weight);
        }

        public void RemoveNode(int id)
        {
            if (!Headers.ContainsKey(id))
            {
                return;
            }

            Stack<int> ArcsToDelete = new();

            // Заполняем стэк
            int tempArc = Headers[id];
            while(tempArc > 0)
            {
                ArcsToDelete.Push(tempArc);
                tempArc = Continues[tempArc];
            }

            // Удаляем дуги
            while(ArcsToDelete.Count > 0)
            {
                tempArc = ArcsToDelete.Pop();
                Continues.Remove(tempArc);
                Weights.Remove(tempArc);
            }

            // Удаляем вершину
            Headers.Remove(id);
        }

        private int GetPrevArc(int id)
        {
            foreach(int header in Headers.Values)
            {
                int arc = header;
                while (Continues[arc] > 0)
                {
                    if (Continues[arc] == id)
                    {
                        return arc;
                    }
                    else
                    {
                        arc = Continues[arc];
                    }
                }
            }
            return -1;
        }

        public void RemoveArc(int id)
        {
            if (!Continues.ContainsKey(id))
            {
                return;
            }
            int tempArc = GetPrevArc(id);
            // Если не первая в последовательности
            if (tempArc > 0)
            {
                Continues[tempArc] = Continues[id];
            }
            // Если первая в последовательности
            else
            {
                if (Headers.ContainsValue(id))
                {
                    // Ищем подходящий узел
                    foreach (int node in Headers.Keys)
                    {
                        if (Headers[node] == id)
                        {
                            Headers[node] = -1;
                            break;
                        }
                    }
                }
            }
            Continues.Remove(id);
            Weights.Remove(id);
        }

        // Возвращает все исходящие дуги
        public List<int> FindNode(int id)
        {
            List<int> output = new();
            int tempArc = Headers[id];
            while(tempArc > 0)
            {
                output.Add(tempArc);
                tempArc = Continues[tempArc];
            }
            return output;
        }

        // Возвращает номер дуги по вершинам
        // -1 если не существует
        public int FindArc(int firstNode, int secondNode)
        {
            int tempArc = Headers[firstNode];
            while (tempArc > 0)
            {
                if (Destinations[tempArc] == secondNode)
                {
                    return tempArc;
                }
                tempArc = Continues[firstNode];
            }
            return -1;
        }

        public void Print()
        {
            foreach(int node in Headers.Keys)
            {
                Console.Write($"Из вершины {node} исходят дуги:\n");
                int tempArc = Headers[node];
                while(tempArc > 0)
                {
                    Console.Write($"\tВ вершину {Destinations[tempArc]}\n");
                    tempArc = Continues[tempArc];
                }
            }
        }
    }
}
