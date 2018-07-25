using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region p_Movement.
    [Header("Movement")]
    [SerializeField] private float m_Speed;

	private Vector3 v_Direction;
    private float v_Angle;
    #endregion

    #region Turning
    [Header("Turning")]
    public float turnDamping;
    #endregion


    #region p_Shooting.
    [Header("Shooting")]
    [SerializeField] private Rigidbody m_ProjectilePrefab;
    private Rigidbody m_Projectile;
    [SerializeField] private float m_ShootForce;
    [SerializeField] private Transform m_ProjectileOrigin;
    #endregion

    #region Inputs.
    private Vector3 inputDirection;

    private Vector3 m_Direction;
    private float m_Angle;
    #endregion

    #region Testing.
    [Header("Test properties")]
    public Transform test;
    #endregion

    #region References.
    [Header("Tank Components")]
    public Transform m_TankBody;
    public Transform m_TankTurret;

    private Rigidbody rb;
	private Animator anim;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateInput();
        TurnBody();
        TurnTurret();

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void FixedUpdate()
    {
		Move();
    }

    private void UpdateInput()
    {
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y, Input.GetAxisRaw("Vertical")).normalized;
    }

	private void Move() 
	{
		rb.velocity = inputDirection * m_Speed;
        v_Direction = rb.velocity.normalized;
	}

    private void TurnBody()
    {
        if (v_Direction != Vector3.zero)
            v_Angle = Vector3.Angle(Vector3.forward, v_Direction);

        if (rb.velocity.x < 0)
            v_Angle = -v_Angle;

        Quaternion rotation = Quaternion.Euler(transform.rotation.x, v_Angle, transform.rotation.z);
        m_TankBody.rotation = Quaternion.Slerp(m_TankBody.rotation, rotation, turnDamping * Time.deltaTime);
    }

    private void TurnTurret()
    {
        m_Direction = (new Vector3(Utility.mousePos.x, transform.position.y, Utility.mousePos.z) - transform.position).normalized;

        m_Angle = Vector3.Angle(Vector3.forward, m_Direction);

        if (Utility.mousePos.x < transform.position.x)
            m_Angle = -m_Angle;

        m_TankTurret.rotation = Quaternion.Euler(transform.rotation.x, m_Angle, transform.rotation.z);
    }

    private void Shoot()
    {
        m_Projectile = Instantiate(m_ProjectilePrefab, m_ProjectileOrigin.position, Quaternion.identity);

        m_Projectile.AddForce(m_Direction * m_ShootForce, ForceMode.Impulse);
    }
}
