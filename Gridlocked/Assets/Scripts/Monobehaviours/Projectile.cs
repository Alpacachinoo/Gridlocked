using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private string target; //Target's tag.

    #region References.
    private Rigidbody rb;
    #endregion

    public void Shoot(Vector3 direction, float force, string target)
    {
        rb = GetComponent<Rigidbody>();

        this.target = target;

        float angle = Vector3.Angle(Vector3.forward, direction);

        if (direction.x < 0)
            angle = -angle;

        transform.rotation = Quaternion.Euler(0, angle, 0);

        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == target)
            Debug.Log("Hit: " + target);
    }
}
