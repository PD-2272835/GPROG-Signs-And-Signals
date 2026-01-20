//Adapted from Code Monkey on Youtube (2019) https://www.youtube.com/watch?v=8jrAWtI8RXg
using UnityEngine;
using System;

public class CustomGrid<TGridObject>
{
    private int _Width = 1;
    private int _Height = 1;
    private float _CellSize = 1f;
    private Vector3 _OriginPosition = Vector3.zero;

    private TGridObject[,] _Cells;

    public CustomGrid(int gridWidth, int gridHeight, float cellSize, Vector3 originPosition, Func<CustomGrid<TGridObject>,int,int,TGridObject> createGridObject)
    {
        _Width = gridWidth;
        _Height = gridHeight;
        _CellSize = cellSize;
        _OriginPosition = originPosition;
        _Cells = new TGridObject[gridWidth, gridHeight];


        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                _Cells[x, y] = createGridObject(this, x, y);
            }
        }

        DebugDrawGrid();
    }

    public CustomGrid(int gridWidth, int gridHeight, float cellSize, Vector3 originPosition)
    {
        _Width = gridWidth;
        _Height = gridHeight;
        _CellSize = cellSize;
        _OriginPosition = originPosition;
        _Cells = new TGridObject[gridWidth, gridHeight];

        DebugDrawGrid();
    }



    //Helper Functions
    public void DebugDrawGrid()
    {
        //Debug.Log("Grid: " + this + " drawn");
        float maxWorldspaceGridHeight = _OriginPosition.y + (_Height * _CellSize);
        float worldSpaceGridWidth = _OriginPosition.x + (_Width * _CellSize);
        for (int x = 0; x < _Width + 1; x++)
        {
            Debug.DrawLine(new Vector3(_OriginPosition.x + x, _OriginPosition.y, _OriginPosition.z),
                            new Vector3(_OriginPosition.x + x, maxWorldspaceGridHeight, _OriginPosition.z),
                            Color.white, 100f);
        }

        for (int y = 0; y < _Height + 1; y++)
        {
            Debug.DrawLine(new Vector3(_OriginPosition.x, _OriginPosition.y + y, _OriginPosition.z),
                            new Vector3(worldSpaceGridWidth, _OriginPosition.y + y, _OriginPosition.z),
                            Color.white, 100f);
        }
    }

    public bool IsInGrid(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _Width && y < _Height) return true;
        return false;
    }

    public bool IsInGrid(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _OriginPosition).x / _CellSize);
        int y = Mathf.FloorToInt((worldPosition - _OriginPosition).y / _CellSize);
        return IsInGrid(x, y);
    }

    //Setters
    public void SetCellValue(TGridObject newValue, int x, int y)
    {
        if (IsInGrid(x, y))
        {
            _Cells[x, y] = newValue;
        }
    }


    //Getters
    public int GetWidth()
    {
        return _Width;
    }

    public int GetHeight()
    {
        return _Height;
    }

    public TGridObject GetCellValue(int x, int y)
    {
        if (IsInGrid(x, y))
        {
            return _Cells[x, y];
        }
        return default;
    }

    public TGridObject GetCellValue(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _OriginPosition).x / _CellSize);
        int y = Mathf.FloorToInt((worldPosition - _OriginPosition).y / _CellSize);
        return GetCellValue(x, y);
    }

    public Vector3 WorldPosFromIndex(int x, int y)
    {
        //returns world position center of the indexed cell
        float xPos = _OriginPosition.x + x * _CellSize + (_CellSize / 2);
        float yPos = _OriginPosition.y + y * _CellSize + (_CellSize / 2);
        return new Vector3(xPos, yPos); //z component is zero with this constructor usage
    }

    public Vector2 GetCellIndex(TGridObject cell)
    {
        for (int i = 0; i < _Width; i++)
        {
            for (int j = 0; j < _Height; j++)
            {
                if (_Cells[i, j].Equals(cell))
                {
                    return new Vector2(i, j);
                }
            }
        }
        return new Vector2(-1, -1);
    }
}
