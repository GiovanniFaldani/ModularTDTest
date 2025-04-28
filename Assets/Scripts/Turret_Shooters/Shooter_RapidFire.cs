using UnityEngine;
using System.Collections.Generic;

public class Shooter_RapidFire : Module, IShooter
{
    [SerializeField] private int baseDamage;
    [SerializeField] private int cost;
    [SerializeField] private float baseFireCooldown;
    [SerializeField] private float turnSpeed;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform shotSocket;

    [Header("Debug Variables")]
    [SerializeField] private int damage;
    [SerializeField] private float fireCooldown;
    [SerializeField] private float fireTimer;
    private List<GameObject> targets = new List<GameObject>();
    private GameObject currentTarget;

    public void Shoot()
    {
        // when killed move target out of trigger so it unsubscribes from target list
        Bullet shot = Instantiate(shotPrefab, shotSocket).GetComponent<Bullet>();
        shot.SetDirection(currentTarget.transform.position - this.transform.position);
        shot.SetDamage(damage);
    }

    void Awake()
    {
        damage = baseDamage;
        fireCooldown = baseFireCooldown;
    }

    void Start()
    {
        fireTimer = fireCooldown;
    }

    void Update()
    {
        if (active)
        {
            FindTargetFIFO();
            FollowTarget();
            ShotCooldown();
        }
    }

    void ShotCooldown()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer < 0 && currentTarget != null)
        {
            Shoot();
            fireTimer = fireCooldown;
        }
        else if (fireTimer < 0)
        {
            fireTimer = -1; // avoid underflow
        }
    }

    // Always returns the first target that enters the radius
    void FindTargetFIFO() 
    {
        if (targets.Count > 0)
        {
            currentTarget = targets[0];
        }
        else
        {
            currentTarget = null;
        }
    }

    // Rotate the turret towards the enemy
    void FollowTarget()
    {
        if (currentTarget != null) {
            Vector3 direction = currentTarget.transform.position - this.transform.position;
            Quaternion destination = Quaternion.LookRotation(direction);
            destination = new Quaternion(0, destination.y, 0, destination.w); // only rotate on y axis
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, destination, turnSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // subscribe enemy to target list
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // unsubscribe enemy to target list
        if (other.CompareTag("Enemy"))
        {
            targets.Remove(other.gameObject);
        }
    }

    public void ModifyDamage(int multiplier)
    {
        damage = damage * multiplier;
    }

    public void ModifyFireRate(float multiplier)
    {
        fireCooldown = fireCooldown * multiplier;
    }

    public void ApplyDOT()
    {
        throw new System.NotImplementedException();
    }
}
