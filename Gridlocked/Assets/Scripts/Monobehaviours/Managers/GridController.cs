using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridController : MonoBehaviour
{
    public bool isGridMovementActive { get; private set; }

    private Cell currentCell;
    [SerializeField] private Transform cursor;

    public float g_MovementCooldown;
    private float g_MovementTimer;

    private bool g_WaitingForInput = false;

    private void OnEnable()
    {
        Player.DestinationReached += DeactivateGridMovement;
    }

    private void OnDisable()
    {
        Player.DestinationReached -= DeactivateGridMovement;
    }

    private void Update()
    {
        currentCell = Grid.instance.WorldToGrid(Utility.mousePos);
        cursor.position = currentCell.worldPosition;

        if (Input.GetKeyDown(KeyCode.Space))
            ActivateController();

        if (g_WaitingForInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!currentCell.c_Occupied && currentCell.c_Walkable)
                {
                    MoveTank(currentCell);
                    DisableController();
                }
            }
        }
    }

    private void MoveTank(Cell targetCell)
    {
        Player.instance.SetNavDestination(currentCell.worldPosition);
    }

    private void ActivateController()
    {
        if (g_WaitingForInput == false && Time.time >= g_MovementTimer)
        {
            GameManager.instance.Pause();
            isGridMovementActive = true; //temp
            Grid.instance.ActivateIndicator();
            g_WaitingForInput = true;
        }
    }

    private void DisableController()
    {
        if (g_WaitingForInput == true)
        {
            Grid.instance.DisableIndicator();
            g_WaitingForInput = false;          
        }

        g_MovementTimer = Time.time + g_MovementCooldown;
    }

    private void DeactivateGridMovement()
    {
        isGridMovementActive = false; //temp
        GameManager.instance.Play();
    }
}
