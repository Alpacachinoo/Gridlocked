using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int x { get; set; }
    public int y { get; set; }
    public Vector3 worldPosition { get; set; }

    public GameObject tile;

    private bool occupied;
    public bool m_occupied { get { return occupied; } }

    public Cell(Vector3 worldPosition)
    {
        this.worldPosition = worldPosition;
    }
}
