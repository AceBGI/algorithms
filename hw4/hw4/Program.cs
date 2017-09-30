using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    class Program
    {
        public static Dictionary<string, Vertex> Cities = new Dictionary<string, Vertex>();
        public static Dictionary<string, LinkedList<Edge>> adjacencyList = new Dictionary<string, LinkedList<Edge>>();

        static void Main(string[] args)
        {
            // cities and tolls / vertex
            string firstLine = Console.ReadLine();
            int n;
            Int32.TryParse(firstLine, out n);

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                string[] cityInfo = line.Split(new[] { '\t', ' ' });
                int toll;
                Int32.TryParse(cityInfo[1], out toll);
                Vertex newCity = new Vertex();
                newCity.Toll = toll;
                newCity.Name = cityInfo[0];

                Cities.Add(newCity.Name, newCity);
                adjacencyList.Add(newCity.Name, null);
            }

            // hightways / edges
            string nextline = Console.ReadLine();
            int h;
            Int32.TryParse(nextline, out h);

            for (int i = 0; i < h; i++)
            {
                string line = Console.ReadLine();
                string[] path = line.Split(new[] { '\t', ' ' });
                Edge newEdge = new Edge();
                newEdge.Start = Cities[path[0]];
                newEdge.Destination = Cities[path[1]];

                // if null make new linked list
                if (adjacencyList[path[0]] == null)
                {
                    LinkedList<Edge> newList = new LinkedList<Edge>();
                    newList.AddFirst(newEdge);

                    adjacencyList[path[0]] = newList;
                }
                else
                {
                    adjacencyList[path[0]].AddLast(newEdge);
                }
            }

            string nextline2 = Console.ReadLine();
            int t;
            Int32.TryParse(nextline2, out t);
            proposedTrip(t);
            Console.ReadLine();
        }

        public static void proposedTrip(int t)
        {
            List<string> resultList = new List<string>();
            TopologicalSort(Cities.First().Value.Name);
            for (int i = 0; i < t; i++)
            {
                string line = Console.ReadLine();
                string[] trips = line.Split(new[] { '\t', ' ' });
                string start = trips[0];
                string end = trips[1];
                string result = "NO";

                // loops to it's self
                if (start == end)
                    result = "0";
                // starting from a sink
                else if (adjacencyList[start] == null)
                    result = "NO";
                else if (Cities[start].Post > Cities[end].Post)
                    result = MinimumToll(start, end).ToString();

                resultList.Add(result);
            }

            // print out answer
            for (int i = 0; i < t; i++)
            {
                Console.WriteLine(resultList[i]);
            }
        }

        public static void TopologicalSort(string start)
        {
            // visit all vertex with depth first search.
            int count = 1;
            foreach (Vertex city in Cities.Values)
            {
                if (!city.Visited)
                    explore(city.Name, ref count);
            }
        }

        /// <summary>
        /// set pre and post data for each vertex
        /// </summary>
        /// <param name="v"></param>
        /// <param name="count"></param>
        public static void explore(string v, ref int count)
        {
            Cities[v].Visited = true;
            Cities[v].Pre = count++;


            if(adjacencyList[v] != null)
            {
                foreach (var edge in adjacencyList[v])
                {
                    if (!edge.Destination.Visited)
                        explore(edge.Destination.Name, ref count);
                }
            }
            Cities[v].Post = count++;

            
        }

        public static int MinimumToll(string start, string end)
        {
            Queue<Vertex> Q = new Queue<Vertex>();
            Vertex startPoint = Cities[start];
            startPoint.Cost = 0;
            Q.Enqueue(startPoint);
            int smallest = 0;

            while (Q.Count != 0)
            {
                Vertex u = Q.Dequeue();
                if(adjacencyList[u.Name] != null)
                {
                    foreach (Edge edge in adjacencyList[u.Name])
                    {
                        if(edge.Destination.Cost == -1)
                        {
                            Q.Enqueue(edge.Destination);
                            Cities[edge.Destination.Name].Prev = u;
                        }

                        Cities[edge.Destination.Name].Cost = u.Cost + Cities[edge.Destination.Name].Toll;

                        if (Cities[edge.Destination.Name].Name == end && smallest == 0)
                        {
                            smallest = Cities[edge.Destination.Name].Cost;
                        }
                        else if (Cities[edge.Destination.Name].Name == end && Cities[edge.Destination.Name].Cost < smallest)
                        {
                            smallest = Cities[edge.Destination.Name].Cost;
                        }
                    }
                }
            }
            return smallest;
        }
    }

    public class Vertex
    {
        public string Name { get; set; }
        public int Toll { get; set; }

        public int Pre { get; set; }
        public int Post { get; set; }
        public bool Visited { get; set; } = false;

        public Vertex Prev { get; set; } = null;
        public int Cost { get; set; } = -1;
    }

    public class Edge
    {
        public Vertex Destination { get; set; }
        public Vertex Start { get; set; }
    }
}
