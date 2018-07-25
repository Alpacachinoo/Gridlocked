using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Cell[,] grid;

    public Transform originPoint;
    public Transform test;
    private Cell testCell;

    public float cellSize;
    public Vector2Int gridDimensions;

    public GameObject tilePrefab;
    public Material highlightMaterial;
    public Material defaultMaterial;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        testCell = WorldToGrid(test.position);

        foreach (Cell cell in grid)
        {
            if (testCell == cell)
                cell.tile.GetComponent<MeshRenderer>().material = highlightMaterial;
            else
                cell.tile.GetComponent<MeshRenderer>().material = defaultMaterial;

        }
    }

    private void Initialize()
    {
        grid = new Cell[gridDimensions.x, gridDimensions.y];

        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                grid[x, y] = new Cell(new Vector3(originPoint.position.x + cellSize * x, 0, originPoint.position.z + cellSize * y));
                grid[x, y].tile = Instantiate(tilePrefab, grid[x, y].worldPosition, Quaternion.identity);
            }
        }
    }

    private Cell WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.Clamp(Mathf.RoundToInt(worldPos.x / cellSize - originPoint.position.x), 0, gridDimensions.x - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(worldPos.z / cellSize - originPoint.position.z), 0, gridDimensions.y - 1);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (Cell cell in grid)
            {
                Gizmos.color = new Color(145, 99, 50, 0.25f);

                if (cell == testCell)
                    Gizmos.color = new Color(145, 99, 50, 0.5f);
                    

                Gizmos.DrawCube(cell.worldPosition, new Vector3(cellSize - 0.05f, 0.1f, cellSize - 0.05f));
            }
        }
    }
}
