using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthbar;
    public Text ammoUI;

    private void OnEnable()
    {
        Player.AmmoChanged += UpdateAmmoUI;
        Player.instance.health.HealthStatusChanged += UpdatePlayerHealthbar;
    }

    private void OnDisable()
    {
        Player.AmmoChanged -= UpdateAmmoUI;
        Player.instance.health.HealthStatusChanged -= UpdatePlayerHealthbar;
    }

    private void UpdateAmmoUI(int currentAmmo)
    {
        ammoUI.text = "Ammo: " + currentAmmo.ToString();
    }

    private void UpdatePlayerHealthbar()
    {
        playerHealthbar.value = Player.instance.health.healthPoints / Player.instance.health.maxHealth;
    }
}