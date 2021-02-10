using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    public static Map _map;

    static void Main(string[] args)
    {
        var mapLenght = int.Parse(Console.ReadLine());
        var mapHeight = int.Parse(Console.ReadLine());

        _map = new Map(mapLenght, mapHeight);

        for (int i = 0; i < mapHeight; i++)
        {
            string row = Console.ReadLine();
            _map.AddRow(i, row);

        }

        var bfsResolver = new BfsResolver(_map);

        var results = new List<int>();
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);

            var point = new Point(X, Y);
            results.Add(bfsResolver.GetResult(point));
        }


        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(results[i]);
        }
    }
}

public class BfsResolver
{
    private readonly Map _map;

    public BfsResolver(Map map)
    {
        _map = map;
    }
    class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point x, Point y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Point obj)
        {
            // Perfect hash for practical bitmaps, their width/height is never >= 65536
            return (obj.Y << 16) ^ obj.X;
        }
    }

    public int GetResult(Point initialPoint)
    {
        int result = 0;

        var searchedPoints = new HashSet<Point>(new PointComparer()) { initialPoint };

        var queue = new Queue<Point>();
        queue.Enqueue(initialPoint);

        while (queue.TryDequeue(out var point))
        {
            if (_map.At(point))
            {
                result++;

                EnqueueIfNotQueuedYet(searchedPoints, queue, new Point(point.X + 1, point.Y));
                EnqueueIfNotQueuedYet(searchedPoints, queue, new Point(point.X - 1, point.Y));
                EnqueueIfNotQueuedYet(searchedPoints, queue, new Point(point.X, point.Y + 1));
                EnqueueIfNotQueuedYet(searchedPoints, queue, new Point(point.X, point.Y - 1));
            }
        }

        return result;
    }

    private void EnqueueIfNotQueuedYet(HashSet<Point> searchedPoints, Queue<Point> queue, Point point)
    {
        if (_map.IsOutsideRange(point))
            return;

        if (!searchedPoints.Contains(point))
        {
            searchedPoints.Add(point);
            queue.Enqueue(point);
        }
    }
}

public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

}

public class Map
{
    public bool[,] _rows;
    public int _mapHeight;
    public int _mapLength;
    public Map(in int mapLength, in int mapHeight)
    {
        _mapLength = mapLength;
        _mapHeight = mapHeight;

        _rows = new bool[mapLength, mapHeight];
    }

    public bool At(Point point)
    {
        if (point.X < 0 || point.X >= _mapLength || point.Y < 0 || point.Y >= _mapHeight)
            return false;

        return _rows[point.X, point.Y];
    }

    public bool IsOutsideRange(Point point)
    {
        return point.X < 0 || point.X >= _mapLength || point.Y < 0 || point.Y >= _mapHeight;
    }

    public void AddRow(in int i, string row)
    {
        for (int x = 0; x < row.Length; x++)
        {
            _rows[x, i] = row[x] == 'O';
        }
    }
}
