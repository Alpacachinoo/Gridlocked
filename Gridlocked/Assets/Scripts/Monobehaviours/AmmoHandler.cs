using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// DO THIS AT CLASS. 8/2/18.

public class AmmoHandler : MonoBehaviour
{
    public static AmmoHandler instance;

    public delegate void AmmoEventHandler(int ammo);
    public static event AmmoEventHandler AmmoChanged;

    public static void FireEvent(AmmoEventHandler _event)
    {
        if (_event != null)
            AmmoChanged(ammo);
    }

    public static readonly int maxAmmo = 5;
    public static int ammo { get; private set; }

    public static void IncrementAmmo(int increment)
    {
        if (ammo + increment >= maxAmmo)
            increment = maxAmmo - ammo;

            ammo += increment;

        for (int i = 0; i < increment; i++)
        {
            instance.AddShellUI();
        }

        FireEvent(AmmoChanged);
    }

    public static void DecrementAmmo(int decrement)
    {
        if (ammo - decrement <= 0)
            decrement = ammo;

        ammo -= decrement;

        for (int i = 0; i < decrement; i++)
        {
            instance.RemoveShellUI();
        }

        FireEvent(AmmoChanged);
    }

    public Transform ammoUIContainer;
    public GameObject shellUI;

    private void Awake()
    {
        instance = this;
    }

    private void AddShellUI()
    {
        Instantiate(shellUI, ammoUIContainer);
    }

    private void RemoveShellUI()
    {
        Destroy(ammoUIContainer.GetChild(ammoUIContainer.childCount - 1).gameObject);
    }
}