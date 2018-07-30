using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoUI;

    private void OnEnable()
    {
        Player.AmmoChanged += UpdateAmmoUI;
    }

    private void OnDisable()
    {
        Player.AmmoChanged -= UpdateAmmoUI;
    }

    private void UpdateAmmoUI(int currentAmmo)
    {
        ammoUI.text = "Ammo: " + currentAmmo.ToString();
    }
}
