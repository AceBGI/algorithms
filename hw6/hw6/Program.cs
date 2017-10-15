using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6
{
    class Program
    {
        static Dictionary<string, LinkedList<corridor>> adjacencyList = new Dictionary<string, LinkedList<corridor>>();
        static Dictionary<string, corridor> corridors = new Dictionary<string, corridor>();
        static void Main(string[] args)
        {
            List<decimal> output = new List<decimal>();
            bool loop = true;
            while (loop)
            {
                string firstLine = Console.ReadLine();
                int n;
                int m;
                Int32.TryParse(firstLine[0].ToString(), out n);
                Int32.TryParse(firstLine[2].ToString(), out m);

                if(n == 0 && m == 0)
                {
                    loop = false;
                }
                else
                {
                    corridor c1 = new corridor();
                    c1.Name = 0 + "" + 0;
                    c1.X = 0;
                    c1.Y = 0;
                    c1.F = 0.0;
                    corridors.Add(c1.Name, c1);
                    LinkedList<corridor> l = new LinkedList<corridor>();
                    adjacencyList.Add(c1.Name, l);
                    for (int i = 0; i < m; i++)
                    {
                        int x;
                        int y;
                        double f;
                        string line = Console.ReadLine();
                        string[] cityInfo = line.Split(new[] { '\t', ' ' });
                        Int32.TryParse(cityInfo[0], out x);
                        Int32.TryParse(cityInfo[1], out y);
                        double.TryParse(cityInfo[2], out f);

                        corridor c = new corridor();
                        c.Name = x + "" + y;
                        c.X = x;
                        c.Y = y;
                        c.F = f;
                       
                        LinkedList<corridor> newLinkList = new LinkedList<corridor>();
                        adjacencyList.Add(c.Name, newLinkList);

                        foreach (var cor in corridors.Values)
                        {
                            if(c.X == cor.X || c.Y == cor.Y || c.X == cor.Y)
                            {
                                adjacencyList[cor.Name].AddLast(c);
                                adjacencyList[c.Name].AddLast(cor);
                            }

                        }
                        corridors.Add(c.Name, c);
                    }

                    decimal num = Dijkstra(corridors.First().Value.Name, corridors.Count - 1);
                    output.Add(num);
                    // clear intersects Dict
                    corridors.Clear();
                    adjacencyList.Clear();
                }
            }

            foreach (decimal item in output)
            {
                Console.WriteLine(string.Format("{0:f4}", item));
            }
            //Console.ReadLine();
        }

        private static decimal Dijkstra(string start, int end)
        {
            Dictionary<string, double> dist = new Dictionary<string, double>();
            foreach (string item in corridors.Keys)
            {
                dist.Add(item, 0.0);
            }
            dist[start] = 1.0;

            PriorityQueue<double, corridor> PQ = new PriorityQueue<double, corridor>();
            PQ.InsertOrChange(1.0, corridors[start]);

            while(!PQ.IsEmpty)
            {
                KeyValuePair<double,corridor> u = PQ.Dequeue();

                foreach (var edge in adjacencyList[u.Value.Name])
                {
                    if (dist[edge.Name] < dist[u.Value.Name] * edge.F)
                    {
                        dist[edge.Name] = dist[u.Value.Name] * edge.F;
                        PQ.InsertOrChange(dist[edge.Name], edge);
                    }
                }
            }

            decimal d = Convert.ToDecimal(dist.ElementAt(end - 1).Value);
            return d;
        }
    }

    public class corridor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double F { get; set; }
        public corridor Prev { get; set; }
        public double Dist { get; set; }
        public string Name { get; set; }
    }

    public class PriorityQueue<TPriority, TValue>
    {
        private List<KeyValuePair<TPriority, TValue>> _baseHeap;
        private IComparer<TPriority> _comparer;

        public PriorityQueue()
            : this(Comparer<TPriority>.Default)
        {
        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException();

            _baseHeap = new List<KeyValuePair<TPriority, TValue>>();
            _comparer = comparer;
        }

        public void InsertOrChange(TPriority priority, TValue value)
        {
            KeyValuePair<TPriority, TValue> val =
                new KeyValuePair<TPriority, TValue>(priority, value);
            _baseHeap.Add(val);

            // heapify after insert, from end to beginning
            HeapifyFromEndToBeginning(_baseHeap.Count - 1);
        }

        private int HeapifyFromEndToBeginning(int pos)
        {
            if (pos >= _baseHeap.Count) return -1;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (pos > 0)
            {
                int parentPos = (pos - 1) / 2;
                if (_comparer.Compare(_baseHeap[parentPos].Key, _baseHeap[pos].Key) > 0)
                {
                    ExchangeElements(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }

        private void ExchangeElements(int pos1, int pos2)
        {
            KeyValuePair<TPriority, TValue> val = _baseHeap[pos1];
            _baseHeap[pos1] = _baseHeap[pos2];
            _baseHeap[pos2] = val;
        }

        public TValue DequeueValue()
        {
            return Dequeue().Value;
        }

        public KeyValuePair<TPriority, TValue> Dequeue()
        {
            if (!IsEmpty)
            {
                KeyValuePair<TPriority, TValue> result = _baseHeap[0];
                DeleteRoot();
                return result;
            }
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        private void DeleteRoot()
        {
            if (_baseHeap.Count <= 1)
            {
                _baseHeap.Clear();
                return;
            }

            _baseHeap[0] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);

            // heapify
            HeapifyFromBeginningToEnd(0);
        }

        private void HeapifyFromBeginningToEnd(int pos)
        {
            if (pos >= _baseHeap.Count) return;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (true)
            {
                // on each iteration exchange element with its smallest child
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;
                if (left < _baseHeap.Count &&
                    _comparer.Compare(_baseHeap[smallest].Key, _baseHeap[left].Key) > 0)
                    smallest = left;
                if (right < _baseHeap.Count &&
                    _comparer.Compare(_baseHeap[smallest].Key, _baseHeap[right].Key) > 0)
                    smallest = right;

                if (smallest != pos)
                {
                    ExchangeElements(smallest, pos);
                    pos = smallest;
                }
                else break;
            }
        }

        public bool IsEmpty
        {
            get { return _baseHeap.Count == 0; }
        }
    }
}
