using UnityEngine;
using System.Collections.Generic;

public class Shooter_RapidFire : Module, IShooter
{
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] private float fireCooldown;
    [SerializeField] private float turnSpeed;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform shotSocket;


    private float fireTime;
    private List<GameObject> targets = new List<GameObject>();
    private GameObject currentTarget;

    public void Shoot()
    {
        // when killed move target out of trigger so it unsubscribes from target list
        Instantiate(shotPrefab, shotSocket).GetComponent<Bullet>().SetDirection(currentTarget.transform.position - this.transform.position);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireTime = fireCooldown;
    }

    // Update is called once per frame
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
        fireTime -= Time.deltaTime;
        if (fireTime < 0 && currentTarget != null)
        {
            Shoot();
            fireTime = fireCooldown;
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
    
}
