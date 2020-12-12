using System.Collections.Generic;
using UnityEngine;

public class AStart : MonoBehaviour
{
    public Field Field;
    private Vector2Int _target;
    private Vector2Int _start;
    private Dictionary<Vector2Int, Dictionary<Vector2Int, bool>> Graph;

    public Dictionary<Vector2Int, Vector2Int> SetStartAndTargetPosition(Vector3 start, Vector3 target)
    {
        _start = SetStart(start);
        _target = SetTarget(target);
        return UpdatePath();
    }

    public Dictionary<Vector2Int, Vector2Int> SetStartPosition(Vector3 start)
    {
        _start = SetStart(start);
        return UpdatePath();
    }

    public Dictionary<Vector2Int, Vector2Int> SetTargetPosition(Vector3 target)
    {
        _target = SetTarget(target);
        return UpdatePath();
    }

    private Vector2Int SetStart(Vector3 start)
    {
        return Convert(start);
    }

    private Vector2Int SetTarget(Vector3 target)
    {
        return Convert(target);
    }

    public Vector3 Convert(Vector2Int idCell)
    {
        return Field.GetCenterCell(idCell.x, idCell.y);
    }

    public Vector2Int Convert(Vector3 position)
    {
        return Field.GetCell(position);
    }

    private Dictionary<Vector2Int, Vector2Int> UpdatePath()
    {
        if (Graph == null)
        {
            Field.UpdateGraph();
            Graph = Field.Graph;
        }
        return MakeAStar();
    }

    private int GetHeuristic(Vector2Int current)
    {
        return Mathf.Abs(current.x - _target.x) + Mathf.Abs(current.y - _target.y);
    }

    private Dictionary<Vector2Int, Vector2Int> MakeAStar()
    {
        BinaryHeap<int, Vector2Int> heap = new BinaryHeap<int, Vector2Int>();
        heap.Insert(0, _start);
        Dictionary<Vector2Int, Vector2Int> visited = new Dictionary<Vector2Int, Vector2Int>();
        visited.Add(_start, default(Vector2Int));
        Dictionary<Vector2Int, int> costVisited = new Dictionary<Vector2Int, int>();
        costVisited.Add(_start, 0);
        int k = 0;
        while (heap.Count > 0 && k < 500)
        {
            var current = heap.ExtractMax();
            var curCost = current.Key;
            var curNode = current.Value;

            if (curNode.x == _target.x && curNode.y == _target.y)
            {
                break;
            }

            if (!Graph.ContainsKey(curNode))
            {
                Debug.Log("Not contains " + curNode);
            }
            var nextNodes = Graph[curNode];
            
            foreach (var node in nextNodes)
            {
                var itemCost = node.Value;
                var itemNode = node.Key;
                var newCost = 1 + costVisited[curNode];

                if (!costVisited.ContainsKey(itemNode) || newCost < costVisited[itemNode])
                {
                    int priority = GetHeuristic(itemNode) + newCost;
                    heap.Insert(priority, itemNode);
                    costVisited[itemNode] = newCost;
                    visited[itemNode] = curNode;
                }
            }
            k++;
        }
        if (k >= 500) Debug.Log("endless cycle v1");
        return visited;
    }
}
