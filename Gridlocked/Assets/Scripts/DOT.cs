using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : MonoBehaviour
{
    [SerializeField] private float m_LifeTime;
    private float expiryTime;

    private void Start()
    {
        expiryTime = Time.time + m_LifeTime;
    }

    private void Update()
    {
        if (Time.time >= expiryTime)
            Destroy(this.gameObject);
    }
}
