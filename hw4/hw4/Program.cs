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
        }

        public static void proposedTrip(int t)
        {
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
                    result = "0";
                else if (Cities[start].Post > Cities[end].Post)
                    

                Console.WriteLine(result);
            }
        }

        public static void TopologicalSort(string start)
        {
            // visit all vertex with depth first search.
            int count = 1;
            foreach (Vertex city in Cities.Values)
            {
                if (!city.Visited)
                    explore(start, count);
            }
        }

        /// <summary>
        /// set pre and post data for each vertex
        /// </summary>
        /// <param name="v"></param>
        /// <param name="count"></param>
        public static void explore(string v, int count)
        {
            Cities[v].Visited = true;
            Cities[v].Pre = count;
            count++;

            foreach (var edge in adjacencyList[v])
            {
                if (!edge.Destination.Visited)
                    explore(edge.Destination.Name, count);
            }
            count++;
            Cities[v].Post = count;
        }

        public static int MinimumToll(string start, string end)
        {
            return 0;
        }
    }

    public class Vertex
    {
        public string Name { get; set; }
        public int Toll { get; set; }

        public int Pre { get; set; }
        public int Post { get; set; }
        public bool Visited { get; set; } = false;
    }

    public class Edge
    {
        public Vertex Destination { get; set; }
        public Vertex Start { get; set; }
    }
}
