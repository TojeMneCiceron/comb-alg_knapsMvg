using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace knapsMvg
{
    class Item
    {
        int n;
        double e, m, c;

        public Item(int n, double m, double c)
        {
            this.n = n;
            this.m = m;
            this.c = c;
            e = c / m;
        }

        public int N { get => n; set => n = value; }
        public double M { get => m; set => m = value; }
        public double C { get => c; set => c = value; }
        public double E { get => e; set => e = value; }
    }
    class mvg
    {
        static void Read(ref int N, ref int M, ref List<Item> items)
        {
            StreamReader streamReader = new StreamReader(@"C:\Users\Пользователь\source\repos\knapsMvg\knapsMvg\input.txt");
            var temp = streamReader.ReadLine().Split(' ');
            N = int.Parse(temp[0]);
            M = int.Parse(temp[1]);

            //m
            temp = streamReader.ReadLine().Split(' ');
            //c
            var temp2 = streamReader.ReadLine().Split(' ');

            for (int i = 0; i < N; i++)
            {
                double m = double.Parse(temp[i]);
                double c = double.Parse(temp2[i]);
                items.Add(new Item(i + 1, m, c));
            }

            streamReader.Close();
        }

        static void Print(List<Item> items)
        {
            items.Sort((x, y) => x.N.CompareTo(y.N));

            string res = items.Count == 0 ? "мдауш" : items.Select(x => x.N.ToString()).Aggregate((x, y) => $"{x} {y}");

            Console.WriteLine(res);
        }

        static double F(List<Item> S, int M, int i, double cost, double mass)
        {
            double e = i >= S.Count ? 0 : S[i].E;

            return mass <= M ? (cost + (M - mass) * e) : -1;
        }

        static void MVG(List<Item> items, int M)
        {
            List<Item> res;
            int n = items.Count;
            Heap<List<Item>> heap = new Heap<List<Item>>();

            while (true)
            {
                var curr = heap.DeleteMax();

                if (curr is null)
                    curr = new HeapNode<List<Item>>(0, new List<Item>(), 1, 0, 0, "");
                //else
                //    Console.WriteLine("Curr: " + curr.Key.ToString() + " Level: " + curr.Level.ToString() + " Mask: " + curr.Mask);

                if (curr.Level == n + 1)
                {
                    res = curr.Value;
                    break;
                }

                //Console.WriteLine(heap.ToString());

                int i = curr.Level;

                double S_plus = F(items, M, curr.Level, curr.Cost + items[i - 1].C, curr.Mass + items[i - 1].M);
                double S_minus = F(items, M, curr.Level, curr.Cost, curr.Mass);

                //Console.WriteLine(S_plus);
                //Console.WriteLine(S_minus);
                
                var S = new List<Item>(curr.Value);
                S.Add(items[i - 1]);

                if (S_plus > -1)
                    heap.Insert(S_plus, S, i + 1, curr.Cost + items[i - 1].C, curr.Mass + items[i - 1].M, curr.Mask + "+");
                //Console.WriteLine(heap.ToString());
                if (S_minus > -1)
                    heap.Insert(S_minus, new List<Item>(curr.Value), i + 1, curr.Cost, curr.Mass, curr.Mask + "-");

                //Console.WriteLine();
                //Console.WriteLine(heap.ToString());
                //Console.WriteLine();
            }

            Print(res);
        }

        static void Main(string[] args)
        {
            int N = 0, M = 0;
            List<Item> items = new List<Item>();

            Read(ref N, ref M, ref items);
            items.Sort((x, y) => y.E.CompareTo(x.E));

            MVG(items, M);
        }
    }
}
