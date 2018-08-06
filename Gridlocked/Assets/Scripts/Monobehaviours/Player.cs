using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player instance;

    #region Events & Delegates.

    public delegate void PlayerNavigationEventHandler();
    public static PlayerNavigationEventHandler DestinationReached;

    private void FireEvent(PlayerNavigationEventHandler _event)
    {
        if (_event != null)
            _event();
    }

    private void OnEnable()
    {
        this.health.Healed += Healed;
        this.health.Damaged += Damaged;
        this.health.Dead += Die;
    }

    private void OnDisable()
    {
        this.health.Healed -= Healed;
        this.health.Damaged -= Damaged;
        this.health.Dead -= Die;
    }

    #endregion

    // p: Player, m: Mouse, v:Velocity.

    #region p_Movement.
    [Header("Movement")]
    [SerializeField] private float p_Speed;
    private bool P_PlayerInteractionEnabled = true;

    private Cell currentCell;

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
    [SerializeField] private Projectile p_ProjectilePrefab;
    private Projectile p_Projectile; //Projectile instance.
    [SerializeField] private float p_ShootForce;

    [SerializeField] private Transform p_ProjectileOrigin;
    #endregion

    #region Health.
    [Header("Health")]
    public Health health;
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

    #region Unity callback functions.

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

        if (nav.hasPath)
        {
            v_Direction = nav.velocity.normalized;

            if ((int)(nav.remainingDistance) == 0)
            {
                ArrivedAtDestination();
            }
        }
    }

    private void FixedUpdate()
    {
		Move();
    }

    #endregion

    private void Initialize()
    {
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        nav.enabled = false;
        health.Initialize();
        Reload();
    }

    #region Input

    private void UpdateInput()
    {
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y, Input.GetAxisRaw("Vertical")).normalized;
    }

    #endregion

    #region Movement.

    private void Move() 
	{
        if (P_PlayerInteractionEnabled == true)
        {
            rb.velocity = inputDirection * p_Speed;
            v_Direction = rb.velocity.normalized;

            currentCell = Grid.instance.WorldToGrid(transform.position);
            Cell.OccupyCell(currentCell);
        }
	}

    private void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    #endregion

    #region Tank & Turret Orientation.

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

    #endregion

    #region Shooting.

    private void Shoot()
    {
        if (AmmoHandler.ammo > 0 && P_PlayerInteractionEnabled == true)
        {
            p_Projectile = Instantiate(p_ProjectilePrefab, p_ProjectileOrigin.position, Quaternion.identity);
            p_Projectile.Shoot(m_Direction, p_ShootForce, "Enemy");

            AmmoHandler.DecrementAmmo(1);
        }
    }

    public void Reload()
    {
        AmmoHandler.IncrementAmmo(AmmoHandler.maxAmmo);
    }

    #endregion

    #region Navigation logic.

    public void SetNavDestination(Vector3 destination)
    {
        nav.enabled = true;
        nav.SetDestination(destination);
    }

    private void ArrivedAtDestination()
    {
        nav.ResetPath();

        Reload();

        nav.enabled = false;

        FireEvent(DestinationReached);
    }
    #endregion

    #region Health event listeners.

    private void Healed()
    {

    }

    private void Damaged()
    {
        Debug.Log("I've been hit!");
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameManager.instance.Pause();
    }

    #endregion

    public void TogglePlayerInteraction(bool toggle)
    {
        P_PlayerInteractionEnabled = toggle;

        if (toggle == false)
        {
            ResetVelocity();
        }
    }
}
