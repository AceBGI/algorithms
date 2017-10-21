using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6
{
    class Program
    {
        static Dictionary<int, LinkedList<corridor>> adjacencyList = new Dictionary<int, LinkedList<corridor>>();
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
                    
                    for (int i = 0; i < m; i++)
                    {
                        int x;
                        int y;
                        double f;
                        string line = Console.ReadLine();
                        string[] lineArray = line.Split(new[] { '\t', ' ' });
                        Int32.TryParse(lineArray[0], out x);
                        Int32.TryParse(lineArray[1], out y);
                        double.TryParse(lineArray[2], out f);

                        corridor c1 = new corridor();
                        c1.Name = x + "" + y;
                        c1.X = x;
                        c1.Y = y;
                        c1.F = f;
                        c1.Next = y;
                        if(corridors.ContainsKey(c1.Name) == false)
                            corridors.Add(c1.Name, c1);

                        corridor c2 = new corridor();
                        c2.Name = y + "" + x;
                        c2.X = x;
                        c2.Y = y;
                        c2.F = f;
                        c2.Next = x;
                        if (corridors.ContainsKey(c2.Name) == false)
                            corridors.Add(c2.Name, c2);

                        if (adjacencyList.ContainsKey(x) == false)
                            adjacencyList.Add(x, new LinkedList<corridor>());
                        if (adjacencyList.ContainsKey(y) == false)
                            adjacencyList.Add(y, new LinkedList<corridor>());

                        adjacencyList[x].AddLast(c1);
                        adjacencyList[y].AddLast(c2);

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
            Console.ReadLine();
        }

        private static decimal Dijkstra(string start, int end)
        {
            Dictionary<int, double> dist = new Dictionary<int, double>();
            foreach (KeyValuePair<int, LinkedList<corridor>> entry in adjacencyList)
            {
                dist.Add(entry.Key, 0.0);
            }
            dist[0] = 1.0;
            corridors[start].F = 1;

            PriorityQueue<double, corridor> PQ = new PriorityQueue<double, corridor>();
            PQ.InsertOrChange(1.0, corridors[start]);

            while(!PQ.IsEmpty)
            {
                KeyValuePair<double,corridor> u = PQ.Dequeue();

                if (Math.Abs(dist[u.Value.Next] - dist[u.Value.Next]) > 0.00000000000000001)
                {
                    continue;
                }

                foreach (var edge in adjacencyList[u.Value.Next])
                {


                    if (dist[edge.Next] < u.Value.F * edge.F)
                    {
                        dist[edge.Next] = u.Value.F * edge.F;
                        PQ.InsertOrChange(dist[edge.Next], edge);
                    }
                }
            }

            decimal d = Convert.ToDecimal(dist.ElementAt(end).Value);
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
        public int Next { get; set; }
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
