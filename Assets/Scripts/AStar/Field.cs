using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Rect Rect;
    public Vector2Int Cells;
    public bool DrawNet;
    public List<Collider2D> Colliders;
    public Dictionary<Vector2Int, Dictionary<Vector2Int, bool>> Graph = new Dictionary<Vector2Int, Dictionary<Vector2Int, bool>>();
    private float _deltaX, _deltaY;

    private void Start()
    {
        _deltaX = Rect.width / Cells.x;
        _deltaY = Rect.height / Cells.y;
    }
    public void UpdateGraph()
    {
        Graph = new Dictionary<Vector2Int, Dictionary<Vector2Int, bool>>();
        for (int i = 0; i < Cells.x; i++)
        {
            for (int j = 0; j < Cells.y; j++)
            {
                var key = new Vector2Int(i, j);
                if (!Graph.ContainsKey(key))
                {
                    if (CheckCell(i, j))
                    {
                        Graph[key] = new Dictionary<Vector2Int, bool>();
                        if (CheckCell(i, j + 1)) Graph[key][new Vector2Int(i, j + 1)] = true;
                        if (CheckCell(i, j - 1)) Graph[key][new Vector2Int(i, j - 1)] = true;
                        if (CheckCell(i + 1, j)) Graph[key][new Vector2Int(i + 1, j)] = true;
                        if (CheckCell(i - 1, j)) Graph[key][new Vector2Int(i - 1, j)] = true;
                    }
                }
            }
        }

    }

    private bool CheckCell(int x, int y)
    {
        return x >= 0 && x < Cells.x && y >= 0 && y < Cells.y && IsFreeBlock(x, y);
    }

    private bool IsFreeBlock(int x, int y)
    {
        Vector3 center = GetCenterCell(x, y);
        //Bounds bounds = new Bounds(center, new Vector3(_deltaX, _deltaY, 0));
        for (int i = 0; i < Colliders.Count; i++)
        {
            center.z = Colliders[i].bounds.center.z;
            Bounds bounds = new Bounds(center, new Vector3(_deltaX, _deltaY, 0));
            if (Colliders[i].bounds.Intersects(bounds))
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 GetCenterCell(int xId, int yId)
    {
        return new Vector3(GetXCenter(xId), GetYCenter(yId));
    }

    public Vector2Int GetCell(Vector3 position)
    {
        Vector2 delta = new Vector2(position.x - transform.position.x, transform.position.y - position.y);
        int x = (int)(delta.x / _deltaX);
        int y = (int)(delta.y / _deltaY);
        return new Vector2Int(x, y);
    }

    private void OnDrawGizmos()
    {
        if (!DrawNet) return;
        Gizmos.color = Color.red;

        _deltaX = Rect.width / Cells.x;
        _deltaY = Rect.height / Cells.y;
        Vector3 min = Rect.min;

        for (int i = 0; i < Cells.y; i++)
        {
            Vector3 from = new Vector3(GetXMin(0), GetYMin(i), -3);
            Vector3 to = new Vector3(GetXMin(Cells.x), GetYMin(i), -3);
            Gizmos.DrawSphere(to, 0.2f);
            Gizmos.DrawLine(from, to);
        }
        Gizmos.color = Color.green;
        for (int i = 0; i < Cells.x; i++)
        {
            Vector3 from = new Vector3(GetXMin(i), GetYMin(0), -3);
            Vector3 to = new Vector3(GetXMin(i), GetYMin(Cells.y), -3);
            Gizmos.DrawSphere(to, 0.2f);
            Gizmos.DrawLine(from, to);
        }


        if(Graph != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var id in Graph.Keys)
            {
                Gizmos.DrawSphere(GetCenterCell(id.x, id.y), 0.2f);
            }
        }
    }

    private float GetYMin(int yId)
    {
        return Rect.min.y - (yId * _deltaY);
    }

    private float GetYCenter(int yId)
    {
        return Rect.min.y - (yId * _deltaY) - _deltaY / 2;
    }

    private float GetXMin(int xId)
    {
        return Rect.min.x + (xId * _deltaX);
    }

    private float GetXCenter(int xId)
    {
        return Rect.min.x + (xId * _deltaX) + _deltaX / 2;
    }

}

public class Point
{
    public Vector2Int Id { get; set; }
    public Vector2 Center { get; set; }
    public int State { get; set; }

    public Point(Vector2Int id, Vector2 center, int state)
    {
        Id = id;
        Center = center;
        State = state;
    }
}
