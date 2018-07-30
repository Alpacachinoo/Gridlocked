using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cell
{
    public static Cell c_OccupiedCell { get { return occupiedCell; } }
    private static Cell occupiedCell;

    public static void OccupyCell(Cell cell)
    {
        if (cell.walkable)
        {
            if (occupiedCell != null)
                UnoccupyCell(occupiedCell);

            cell.occupied = true;
            occupiedCell = cell;
        }
    }

    private static void UnoccupyCell(Cell cell)
    {
        cell.occupied = false;
        occupiedCell = null;
    }

    public int x { get; set; }
    public int y { get; set; }
    public Vector3 worldPosition { get; set; }

    public GameObject tile;

    public bool c_Occupied { get { return occupied; } }
    private bool occupied;

    public bool c_Walkable { get { return walkable; } }
    private bool walkable;

    public Cell(Vector3 worldPosition, int x, int y, bool walkable)
    {
        this.worldPosition = worldPosition;
        this.x = x;
        this.y = y;
        this.walkable = walkable;
    }
}