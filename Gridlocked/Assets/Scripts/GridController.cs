using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private Cell currentCell;
    [SerializeField] private Transform cursor;

    public bool active;

    private void Update()
    {
        currentCell = Grid.instance.WorldToGrid(Utility.mousePos);
        cursor.position = currentCell.worldPosition;

        if (Input.GetKeyDown(KeyCode.Space))
            ActivateController();

        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveTank(Player.instance, currentCell);
                DisableController();
            }
        }
    }

    private void MoveTank(Player tank, Cell targetCell)
    {
        tank.SetNavDestination(currentCell.worldPosition);
    }

    private void ActivateController()
    {
        if (active == false)
        {
            Grid.instance.ActivateIndicator();
            active = true;
        }
    }

    private void DisableController()
    {
        if (active == true)
        {
            Grid.instance.DisableIndicator();
            active = false;          
        }
    }
}
