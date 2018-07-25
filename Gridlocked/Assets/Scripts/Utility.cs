using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
	//Mouse Position.
    public static Vector3 mousePos { get { return new Vector3(m_MousePos.x, 0, m_MousePos.z); } }
    private static Vector3 m_MousePos;
    private static Vector3 tempPos;

	public Transform cursor;

	public Transform ground;
	private float distance;


    private void Update()
    {
		distance = (ground.position - Camera.main.transform.position).magnitude;
		tempPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance));

        m_MousePos = new Vector3(tempPos.x, 0, tempPos.z);

        cursor.position = m_MousePos;
    }
}
