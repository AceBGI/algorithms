using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6
{
    class Program
    {
        static void Main(string[] args)
        {
            List<decimal> output = new List<decimal>();
            bool loop = true;
            while (loop)
            {
                string line = Console.ReadLine();
                string[] flineArray = line.Split(new[] { '\t', ' ' });
                int n;
                int m;
                Int32.TryParse(flineArray[0].ToString(), out n);
                Int32.TryParse(flineArray[1].ToString(), out m);
                
                Dictionary<double, LinkedList<corridor>> adjacencyList = new Dictionary<double, LinkedList<corridor>>(n);
                Dictionary<double, double> size = new Dictionary<double, double>(n);
                if (n == 0 && m == 0)
                {
                    loop = false;
                }
                else
                {

                    for (int i = 0; i < m; i++)
                    {
                        int x;
                        int y;
                        double f;
                        line = Console.ReadLine();
                        string[] lineArray = line.Split(new[] { '\t', ' ' });
                        Int32.TryParse(lineArray[0], out x);
                        Int32.TryParse(lineArray[1], out y);
                        double.TryParse(lineArray[2], out f);

                        if (adjacencyList.ContainsKey(y) == false)
                        {
                            LinkedList<corridor> newLL = new LinkedList<corridor>();
                            adjacencyList.Add(y, newLL);
                            size.Add(y, 0.0);
                        }
                        if (adjacencyList.ContainsKey(x) == false)
                        {
                            LinkedList<corridor> newLL = new LinkedList<corridor>();
                            adjacencyList.Add(x, newLL);
                            size.Add(x, 0.0);
                        }

                        adjacencyList[y].AddLast(new corridor(y, x, f));
                        adjacencyList[x].AddLast(new corridor(x, y, f));

                    }
                    
                    decimal num = dijkstra(ref adjacencyList, 0, n - 1, ref size, m);
                    output.Add(num);
                    adjacencyList.Clear();
                    size.Clear();
                }
            }

            foreach (decimal item in output)
            {
                Console.WriteLine(string.Format("{0:f4}", item));
            }
            Console.ReadLine();
        }

        public static decimal dijkstra(ref Dictionary<double, LinkedList<corridor>> adjacencyList, double start, int end, ref Dictionary<double, double> size, int m)
        {
            int infCount = 0;
            size[start] = 1.0;

            MinHeap pq = new MinHeap(end+1);
            pq.Add(1.0, new corridor(start));

            while (!pq.IsEmpty())
            {
                corridor u = pq.PopMin();
                if (u.seen == true || infCount > (end+1) * m)
                {
                    decimal ddd = Convert.ToDecimal(size[u.name]);
                    return ddd;
                }

                u.seen = true;

                if (u.name == end)
                {
                    decimal dd = Convert.ToDecimal(size[u.name]);
                    return dd;
                }

                LinkedList<corridor> edges = adjacencyList[u.next];
                foreach (corridor v in edges)
                {
                    if (size[v.next] < v.f * size[u.next])
                    {
                        size[v.next] = v.f * size[u.next];
                        pq.Add(size[v.next], new corridor(v.next));
                    }
                }
                infCount++;
            }
            decimal d = Convert.ToDecimal(size[end]);
            return d;
        }
    }

    public class corridor
    {
        public double name;
        public double f;
        public double next;
        public bool seen { get; set; } = false;

        public corridor()
        {
        }

        public corridor(double n)
        {
            this.next = n;
        }

        public corridor(double name, double n, double f)
        {
            this.name = name;
            this.f = f;
            this.next = n;
        }

        public corridor(double n, double f)
        {
            this.name = n;
            this.f = f;
        }
    }

    public class MinHeap
    {
        static int N;
        public MinHeap(int n)
        {
            N = n;
        }

        public Dictionary<double, Queue<corridor>> map = new Dictionary<double, Queue<corridor>>(N);
        public static int count;

        public void Add(double val, corridor node)
        {
            if (!map.ContainsKey(val))
            {
                map.Add(val, new Queue<corridor>(N));
            }

            map[val].Enqueue(node);
            count++;
        }

        public corridor PopMin()
        {
            double minKey = map.First().Key;
            corridor node = map[minKey].Dequeue();

            if (map[minKey].Count == 0)
                map.Remove(minKey);

            count--;
            return node;
        }

        public bool IsEmpty()
        {
            if(count != 0)
            {
                return false;
            }
            return true;
        }
    }
}


