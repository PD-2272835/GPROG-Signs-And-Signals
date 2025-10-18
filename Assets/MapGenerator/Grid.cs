using UnityEngine;

public class Grid<TGridObject>
{
    private int _width = 1;
    private int _height = 1;
    private float _cellSize = 1f;
    private Vector3 _originPosition = Vector3.zero;

    private TGridObject[,] _cells;

    public Grid(int gridWidth, int gridHeight, float cellSize, Vector3 originPosition)
    {
        _width = gridWidth;
        _height = gridHeight;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _cells = new TGridObject[gridWidth, gridHeight];
    }

    private bool IsInGrid(int x, int y)
    {
        if (x >= 0 && y >= 0 && x <= _width && y <= _height)
        {
            return true;
        }
        return false;
    }

    public void SetCellValue(TGridObject newValue, int x, int y)
    {
        if (IsInGrid(x, y))
        {
            _cells[x, y] = newValue;
        }
    }

    public TGridObject GetCellValue(int x, int y)
    {
        if (IsInGrid(x, y))
        {
            return _cells[x, y];
        }
        return default;
    }

    public TGridObject GetCellValue(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / _cellSize); 
        return GetCellValue(x, y);
    }
}
