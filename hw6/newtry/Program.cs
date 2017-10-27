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
            Dictionary<double, LinkedList<corridor>> adjacencyList = new Dictionary<double, LinkedList<corridor>>();
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
                        }
                        if (adjacencyList.ContainsKey(x) == false)
                        {
                            LinkedList<corridor> newLL = new LinkedList<corridor>();
                            adjacencyList.Add(x, newLL);
                        }

                        adjacencyList[y].AddLast(new corridor(y, x, f));
                        adjacencyList[x].AddLast(new corridor(x, y, f));


                    }
                    
                    decimal num = dijkstra(adjacencyList, 0, n - 1);
                    output.Add(num);
                    // clear adjacencyList Dict
                    adjacencyList.Clear();
                }
            }

            foreach (decimal item in output)
            {
                Console.WriteLine(string.Format("{0:f4}", item));
            }
            Console.ReadLine();
        }

        public static decimal dijkstra(Dictionary<double, LinkedList<corridor>> adjacencyList, double start, int end)
        {
            Dictionary<double, double> size = new Dictionary<double, double>();

            foreach (KeyValuePair<double, LinkedList<corridor>> entry in adjacencyList)
            {
                size.Add(entry.Key, 0.0);
            }
            size[start] = 1.0;

            PriorityQueue pq = new PriorityQueue();
            pq.insert(new corridor(start), 1.0);
            pq.firstTime();

            while (!pq.isEmpty())
            {
                corridor u = pq.deleteMax();

                LinkedList<corridor> edges = adjacencyList[u.next];
                foreach (corridor v in edges)
                {
                    if (size[v.next] < v.f * size[u.next])
                    {
                        size[v.next] = v.f * size[u.next];
                        pq.insert(new corridor(v.next), size[v.next]);
                    }
                }

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
    
    public class PriorityQueue
    {
        corridor max = new corridor();
        int first = 0;
        int count = 0;
        List<corridor> list = new List<corridor>();
        HashSet<corridor> top = new HashSet<corridor>();

        public PriorityQueue()
        {

        }

        public bool isEmpty()
        {
            return count == 0;
        }

        public corridor deleteMax()
        {
            top.Remove(max);
            count--;
            return max;
        }

        public void insert(corridor v, double w)
        {
            first++;
            if (w > max.f || !top.Contains(max))
            {
                max = v;
                max.f = w;
                top.Add(max);
            }
            count++;
            v.f = w;
            list.Add(v);
        }

        public void dec()
        {
            count--;
        }

        public void firstTime()
        {
            first = 1;
        }
    }
}

