using System;
using UnityEngine;

namespace HuntroxGames.Utils
{
    [Serializable]
    public class Grid<T>
    {
        public event Action<int, int, GridOrientation> OnGridChanged;
        private int width;
        private int height;
        private GridOrientation orientation;
        private float cellSize;
        private Vector3 originPosition;
        private T[,] gridArray;
        private TextMesh[,] debugTextArray;


        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        public T[,] GridArray => gridArray;

        public Grid(int width, int height, float cellSize, GridOrientation orientation, Vector3 originPosition,
            Func<Grid<T>, int, int, T> createObjectCallback)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.orientation = orientation;
            this.gridArray = new T[width, height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    gridArray[i, j] = createObjectCallback(this, i, j);
                }
            }
            debugTextArray = new TextMesh[width, height];
            OnGridChanged += (x, y, orientation) =>
            {
                if (debugTextArray[x, y])
                    debugTextArray[x, y].text = gridArray[x, y]?.ToString();
            };

        }

        public Grid<T> GridGizmos(bool text = false ,Color? gridColor = null)
        {
            var color = gridColor??Color.white;
            for (var x = 0; x < gridArray.GetLength(0); x++)
            {
                for (var z = 0; z < gridArray.GetLength(1); z++)
                {
                    var pos = GetWorldPosition(x, z) + (orientation == GridOrientation.XZ? new Vector3(cellSize, 0, cellSize) * .5f 
                        : new Vector3(cellSize, cellSize,0 ) * .5f);
                    if(text)
                        CreateDebugText(gridArray[x, z]?.ToString(), pos, 15, color);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), color, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), color, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height),color, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), color, 100f);
            return this;
        }
        private static TextMesh CreateDebugText(string text,Vector3 position, int size, Color color)
        {
            var textMeshObj = new GameObject($"GridText({position})", typeof(TextMesh))
            {
                transform =
                {
                    position = position
                }
            };
            var textMesh = textMeshObj.GetComponent<TextMesh>();
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.alignment = TextAlignment.Center;
            textMesh.text = text;
            textMesh.fontSize = size;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = 2000;
            return textMesh;
        }
        public Vector3 GetWorldPosition(int x, int y)
        {
            return orientation switch
            {
                GridOrientation.XZ => new Vector3(x, 0, y) * cellSize + originPosition,
                GridOrientation.XY => new Vector3(x, y, 0) * cellSize + originPosition,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public void GetGridPosition(Vector3 worldPosition, out int x, out int y) 
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = orientation == GridOrientation.XZ? Mathf.FloorToInt((worldPosition - originPosition).z / cellSize)
                : Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }
        public void SetGridValue(int x, int y, T value)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return;
            gridArray[x, y] = value;
            OnGridChanged?.Invoke(x, y, orientation);
        }
        public void SetGridValue(Vector3 worldPosition, T value) 
        {
            GetGridPosition(worldPosition, out var x, out var y);
            SetGridValue(x, y, value);
        }
        
        public T GetGridValue(int x, int z)
        {
            if (x >= 0 && z >= 0 && x < width && z < height)
                return gridArray[x, z];
            
            return default;
        }

        public T GetGridValue(Vector3 worldPosition)
        {
            GetGridPosition(worldPosition, out var x, out var y);
            return GetGridValue(x, y);
        }
        public bool TryGetGridValue(int x, int y, out T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                value = gridArray[x, y];
                return true;
            }
            value = default;
            return false;
        }
        public bool TryGetGridValue(Vector3 worldPosition, out T value)
        {
            GetGridPosition(worldPosition, out var x, out var y);
            return TryGetGridValue(x, y, out value);
        }
    }

    public enum GridOrientation
    {
        XY,
        XZ,
    }
}
