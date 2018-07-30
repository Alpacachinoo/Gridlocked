using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Pause()
    {
        StandardEnemy.ToggleAI(false);
        Player.instance.TogglePlayerInteraction(false);
    }

    public void Play()
    {
        StandardEnemy.ToggleAI(true);
        Player.instance.TogglePlayerInteraction(true);
    }
}