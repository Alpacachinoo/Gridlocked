using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private string target; //Target's tag.
    [SerializeField] private float damage;

    #region References.
    private Rigidbody rb;
    #endregion

    public int hits;

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

    private void Hit(Health other)
    {
        other.Damage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == target)
        {
            if (target == "Enemy")
            {
                Hit(other.GetComponent<Enemy>().health);
            }
            else if (target == "Player")
            {
                Hit(other.GetComponent<Player>().health);
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "Obstacle")
            Destroy(this.gameObject);
    }
}