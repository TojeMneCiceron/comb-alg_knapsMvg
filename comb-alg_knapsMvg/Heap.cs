using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace knapsMvg
{
    class HeapNode<T>
    {
        public HeapNode(double key, T value, int level, double cost, double mass, string mask)
        {
            Key = key;
            Value = value;
            Level = level;
            Cost = cost;
            Mass = mass;
            Mask = mask;
        }

        public double Key { get; set; }
        public T Value { get; set; }
        public int Level { get; set; }
        public double Cost { get; set; }
        public double Mass { get; set; }
        public string Mask { get; set; }

        //public override string ToString()
        //{
        //    return $"{Key}({Value})";
        //}
    }
    class Heap<T>
    {
        int n;
        List<HeapNode<T>> nodes;

        public Heap()
        {
            n = 0;
            nodes = new List<HeapNode<T>>();
            nodes.Add(null);
        }
        public static void Swap(ref HeapNode<T> a, ref HeapNode<T> b)
        {
            HeapNode<T> temp = a;
            a = b;
            b = temp;
        }
        public HeapNode<T> Min()
        {
            return nodes[1];
        }
        public bool Insert(double new_key, T new_value, int new_level, double new_cost, double new_mass, string new_mask)
        {
            n++;
            nodes.Insert(n, new HeapNode<T>(new_key, new_value, new_level, new_cost, new_mass, new_mask));
            //nodes[n] = new HeapNode<T>(new_key, new_value, new_level, new_cost, new_mass, new_mask);

            for (int i = n; i > 1 && nodes[i].Key > nodes[i / 2].Key; i /= 2)
            //Swap(ref nodes[i], ref nodes[i / 2]);
            {
                HeapNode<T> temp = nodes[i];
                nodes[i] = nodes[i / 2];
                nodes[i / 2] = temp;
            }

            return true;
        }
        public HeapNode<T> DeleteMax()
        {
            if (n == 0)
                return null;

            HeapNode<T> max = nodes[1];

            nodes[1] = nodes[n];
            //nodes.RemoveAt(n);
            n--;

            Heapify(1);

            return max;
        }
        void Heapify(int i)
        {
            for (; ; )
            {
                int left = 2 * i;
                int right = 2 * i + 1;

                int max = i;

                if (left <= n && nodes[left].Key >= nodes[right].Key && nodes[left].Key >= nodes[i].Key)
                { 
                    max = left; 
                }

                if (right <= n && nodes[right].Key >= nodes[left].Key && nodes[right].Key >= nodes[i].Key)
                { 
                    max = right;
                }

                if (max == i)
                    break;

                //Swap(ref nodes[i], ref nodes[max]);

                HeapNode<T> temp = nodes[i];
                nodes[i] = nodes[max];
                nodes[max] = temp;

                i = max;
            }
        }
        public override string ToString()
        {
            //if (n == 0)
            //    return "";

            //string res = $"{nodes[1].Key}\n";

            //int a = 4;

            //for (int i = 2; i <= n; i++)
            //{
            //    res += $"{nodes[i].Key} ";

            //    if (i + 1 == a)
            //    {
            //        res += "\n";
            //        a *= 2;
            //    }
            //}

            //return res;

            var nodes1 = nodes.GetRange(0, n + 1);

            return n == 0 ? "" : nodes1.Where(x => x != null).Select(x => x.Key.ToString("0.0")).Aggregate((x, y) => $"{x}\t{y}");
        }
    }
}