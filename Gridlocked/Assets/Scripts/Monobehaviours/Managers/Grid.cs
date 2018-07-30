using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    private Cell[,] grid;

    [SerializeField] private Transform originPoint;

    [SerializeField] private float cellSize;
    [SerializeField] private Vector2Int gridDimensions;

    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private Material occupiedTileMaterial;
    [SerializeField] private Material defaultTileMaterial;

    [SerializeField] private Transform tilesParent;

    [SerializeField] private LayerMask obstacleLayer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Vector3 cellPosition;

        grid = new Cell[gridDimensions.x, gridDimensions.y];

        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                cellPosition = new Vector3(originPoint.position.x + cellSize * x, 0, originPoint.position.z + cellSize * y);

                grid[x, y] = new Cell(cellPosition, x, y, Physics.OverlapSphere(cellPosition, cellSize, obstacleLayer).Length == 0);

                grid[x, y].tile = Instantiate(tilePrefab, grid[x, y].worldPosition, Quaternion.identity, tilesParent);

                if (grid[x, y].c_Walkable == false)
                    grid[x, y].tile.GetComponent<MeshRenderer>().material = occupiedTileMaterial;
            }
        }

        tilesParent.gameObject.SetActive(false);
    }

    public Cell WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.Clamp(Mathf.RoundToInt(worldPos.x / cellSize - originPoint.position.x), 0, gridDimensions.x - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(worldPos.z / cellSize - originPoint.position.z), 0, gridDimensions.y - 1);

        return grid[x, y];
    }

    public void ActivateIndicator()
    {
        if (Cell.c_OccupiedCell != null)
            Cell.c_OccupiedCell.tile.GetComponent<MeshRenderer>().material = occupiedTileMaterial;
        tilesParent.gameObject.SetActive(true);
    }

    public void DisableIndicator()
    {
        if (Cell.c_OccupiedCell != null)
            Cell.c_OccupiedCell.tile.GetComponent<MeshRenderer>().material = defaultTileMaterial;
        tilesParent.gameObject.SetActive(false);
    }
}
