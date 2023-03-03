using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Dictionary<Vector2Int, WayPointBlock> grid = new();
    [SerializeField] private WayPointBlock startPoint, endPoint;
    private Queue<WayPointBlock> queue = new();
    private bool isRunningSearch = true;
    private WayPointBlock learnigPoint;
    private List<WayPointBlock> wayPoint = new();

    private readonly Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
        };


    public List<WayPointBlock> GetWay()
    {
        if (wayPoint.Count == 0)
        {
            LoadAllBlocks();
            SetStartEndCubeColor(); //Убрать
            PathFinde();
            CreateWayPoint();
        }

        return wayPoint;
    }

    private void CreateWayPoint()
    {
        AddPointToPath(endPoint);

        var previusPoint = endPoint.exploredFrom;
        previusPoint.SetTopColor(Color.gray);

        while (previusPoint != startPoint)
        {
            AddPointToPath(previusPoint);
            previusPoint.SetTopColor(Color.gray);
            previusPoint = previusPoint.exploredFrom;
        }

        AddPointToPath(startPoint);
        wayPoint.Reverse();
    }

    private void AddPointToPath(WayPointBlock point)
    {
        wayPoint.Add(point);
        point.isPlaceable = false;
    }

    private void PathFinde()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && isRunningSearch)
        {
            learnigPoint = queue.Dequeue();
            learnigPoint.isExlored = true;

            //print($"Исследуем: {learnigPoint}");

            IsEndPoint();
            ExploreNearPoints();
        }
    }

    private void IsEndPoint()
    {
        if (learnigPoint == endPoint)
        {
            //print($"Найден финиш в точке: {learnigPoint}");
            isRunningSearch = false;
        }
    }

    private void SetStartEndCubeColor()
    {
        startPoint.SetTopColor(Color.red);
        endPoint.SetTopColor(Color.green);
    }

    private void ExploreNearPoints()
    {
        if (!isRunningSearch)
        {
            return;
        }

        foreach (var direction in directions)
        {
            var inspectedNearPoint = learnigPoint.GetGridPosition() + direction;

            try
            {
                var nearPoit = grid[inspectedNearPoint];

                if (!nearPoit.isExlored && !queue.Contains(nearPoit))
                {
                    queue.Enqueue(nearPoit);
                    nearPoit.exploredFrom = learnigPoint;
                    //print($"Добавить в очередь: {nearPoit}");
                }
                //else
                //{
                //    print($"{nearPoit} Уже исследован!");
                //}

            }
            catch
            {
                //Debug.LogWarning($"Координата {inspectedNearPoint} отсутствует");
            }


        }
    }

    private void LoadAllBlocks()
    {
        var wayPoints = FindObjectsOfType<WayPointBlock>();

        foreach (var wayPoint in wayPoints)
        {
            var gridPosition = wayPoint.GetGridPosition();
            var isOverlapping = grid.ContainsKey(gridPosition);

            if (isOverlapping)
            {
                Debug.LogWarning($"Repeat block with name \"{wayPoint.name}\"");
                continue;
            }

            grid.Add(gridPosition, wayPoint);
        }
    }
}
