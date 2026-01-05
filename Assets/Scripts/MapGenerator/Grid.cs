//Adapted from Code Monkey on Youtube (2019) https://www.youtube.com/watch?v=8jrAWtI8RXg
using UnityEngine;
using System.Collections.Generic;
using System;

public class CustomGrid<TGridObject>
{
    private int _width = 1;
    private int _height = 1;
    private float _cellSize = 1f;
    private Vector3 _originPosition = Vector3.zero;

    private TGridObject[,] _cells;

    public CustomGrid(int gridWidth, int gridHeight, float cellSize, Vector3 originPosition, Func<CustomGrid<TGridObject>,int,int,TGridObject> createGridObject)
    {
        _width = gridWidth;
        _height = gridHeight;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _cells = new TGridObject[gridWidth, gridHeight];


        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                _cells[x, y] = createGridObject(this, x, y);
            }
        }

        DebugDrawGrid();
    }

    //Helper Functions
    public void DebugDrawGrid()
    {
        Debug.Log("Grid: " + this + " drawn");
        float maxWorldspaceGridHeight = _originPosition.y + (_height * _cellSize);
        float worldSpaceGridWidth = _originPosition.x + (_width * _cellSize);
        for (int x = 0; x < _width + 1; x++)
        {
            Debug.DrawLine(new Vector3(_originPosition.x + x, _originPosition.y, _originPosition.z),
                            new Vector3(_originPosition.x + x, maxWorldspaceGridHeight, _originPosition.z),
                            Color.white, 100f);
        }

        for (int y = 0; y < _height + 1; y++)
        {
            Debug.DrawLine(new Vector3(_originPosition.x, _originPosition.y + y, _originPosition.z),
                            new Vector3(worldSpaceGridWidth, _originPosition.y + y, _originPosition.z),
                            Color.white, 100f);
        }
    }

    public bool IsInGrid(int x, int y)
    {
        if (x >= 0 && y >= 0 && x <= _width && y <= _height) return true;
        return false;
    }


    //Setters
    public void SetCellValue(TGridObject newValue, int x, int y)
    {
        if (IsInGrid(x, y))
        {
            _cells[x, y] = newValue;
        }
    }


    //Getters
    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public TGridObject GetCellValue(int x, int y)
    {
        if (IsInGrid(x, y))
        {
            return _cells[x, y];
        }
        return default(TGridObject);
    }

    public TGridObject GetCellValue(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        int y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
        return GetCellValue(x, y);
    }

    public Vector3 WorldPosFromIndex(int x, int y)
    {
        //returns world position center of the indexed cell
        float xPos = _originPosition.x + x * _cellSize + (_cellSize / 2);
        float yPos = _originPosition.y + y * _cellSize + (_cellSize / 2);
        return new Vector3(xPos, yPos); //z component is zero with this constructor usage
    }

    public Vector2 GetCellIndex(TGridObject cell)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_cells[i, j].Equals(cell))
                {
                    return new Vector2(i, j);
                }
            }
        }
        return new Vector2(-1, -1);
    }
}
