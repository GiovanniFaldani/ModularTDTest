using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using Unity.Collections.LowLevel.Unsafe;

public class Module_RapidFire : MonoBehaviour, IStackable, IModule
{
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] private float fireCooldown;
    [SerializeField] private float turnSpeed;
    [SerializeField] GameObject shotPrefab;


    private float fireTime;
    private List<GameObject> targets = new List<GameObject>();
    private GameObject currentTarget;
    [SerializeField] private bool active = false; // activate turret only after being built


    public void AddToBase(Turret_Base turretBase)
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        // always target element 0 of targets list,
        // when killed move out of trigger so it unsubscribes from target list
        throw new System.NotImplementedException();
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
