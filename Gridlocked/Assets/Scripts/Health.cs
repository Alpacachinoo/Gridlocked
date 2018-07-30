using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Health
{
    public float maxHealth;
    public float healthPoints { get; private set; }

    public delegate void HealthEventHandler();
    public event HealthEventHandler Healed;
    public event HealthEventHandler Damaged;
    public event HealthEventHandler Dead;

    private void FireEvent(HealthEventHandler _event)
    {
        if (_event != null)
            _event();
    }

    public void Initialize()
    {
        healthPoints = maxHealth;
    }

    public void Heal(float health)
    {
        healthPoints += health;

        if (healthPoints > maxHealth)
            healthPoints = maxHealth;

        FireEvent(Healed);
    }

    public void Damage(float damage)
    {
        healthPoints = healthPoints - damage;

        if (healthPoints <= 0)
        {
            Die();
        }
        else
        {
            FireEvent(Damaged);
        }
    }

    private void Die()
    {
        healthPoints = 0;
        FireEvent(Dead);
    }
}