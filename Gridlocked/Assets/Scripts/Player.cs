using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public delegate void PlayerEventHandler(int currentAmmo);
    public static PlayerEventHandler AmmoChanged;

    private void FireEvent(PlayerEventHandler _event)
    {
        if (_event != null)
            _event(p_Ammo);
    }

    // p: Player, m: Mouse, v:Velocity.

    #region p_Movement.
    [Header("Movement")]
    [SerializeField] private float p_Speed;
    private bool isAuto = false;

    //Velocity direction & angle.
	private Vector3 v_Direction;
    private float v_Angle;
    #endregion

    #region Turning
    [Header("Turning")]
    public float p_TurnDamping;
    #endregion

    #region p_Shooting.
    [Header("Shooting")]
    [SerializeField] private Rigidbody p_ProjectilePrefab;
    private Rigidbody p_Projectile; //Projectile instance.
    [SerializeField] private float p_ShootForce;

    public int p_MaxAmmo;
    private int p_Ammo;

    [SerializeField] private Transform p_ProjectileOrigin;
    #endregion

    #region Inputs.
    private Vector3 inputDirection;

    //Mouse direction & angle;
    private Vector3 m_Direction;
    private float m_Angle;
    #endregion

    #region References.
    [Header("Tank Components")]
    public Transform m_TankBody;
    public Transform m_TankTurret;

    private Rigidbody rb;
	private Animator anim;
    private NavMeshAgent nav;
    #endregion

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateInput();
        TurnBody();
        TurnTurret();

        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        if (nav.hasPath)
        {
            v_Direction = nav.velocity.normalized;

            if ((int)(nav.remainingDistance) == 0)
            {
                nav.ResetPath();
            }
        }
    }

    private void FixedUpdate()
    {
		Move();
    }

    private void Initialize()
    {
        Reload();
    }

    private void UpdateInput()
    {
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y, Input.GetAxisRaw("Vertical")).normalized;
    }

	private void Move() 
	{
        if (isAuto == false)
        {
            rb.velocity = inputDirection * p_Speed;
            v_Direction = rb.velocity.normalized;
        }
	}

    private void TurnBody()
    {
        if (v_Direction != Vector3.zero)
            v_Angle = Vector3.Angle(Vector3.forward, v_Direction);

        if (v_Direction.x < 0)
            v_Angle = -v_Angle;

        Quaternion rotation = Quaternion.Euler(transform.rotation.x, v_Angle, transform.rotation.z);
        m_TankBody.rotation = Quaternion.Slerp(m_TankBody.rotation, rotation, p_TurnDamping * Time.deltaTime);
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
        if (p_Ammo > 0)
        {
            p_Projectile = Instantiate(p_ProjectilePrefab, p_ProjectileOrigin.position, Quaternion.identity);

            p_Projectile.AddForce(m_Direction * p_ShootForce, ForceMode.Impulse);

            p_Ammo--;

            FireEvent(AmmoChanged);
        }
    }

    public void Reload()
    {
        p_Ammo = p_MaxAmmo;
        FireEvent(AmmoChanged);
    }

    public void SetNavDestination(Vector3 destination)
    {
        nav.SetDestination(destination);
    }
}
