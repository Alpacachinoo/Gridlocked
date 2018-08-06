using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthbar;

    private void OnEnable()
    {
        Player.instance.health.HealthStatusChanged += UpdatePlayerHealthbar;
    }

    private void OnDisable()
    {
        Player.instance.health.HealthStatusChanged -= UpdatePlayerHealthbar;
    }

    private void UpdatePlayerHealthbar()
    {
        playerHealthbar.value = Player.instance.health.healthPoints / Player.instance.health.maxHealth;
    }
}