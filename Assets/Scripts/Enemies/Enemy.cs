using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int reward;
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float proximity;
    [SerializeField] private Transform[] pathPoints;

    private float height;
    [SerializeField] private float deadtimer = -15.0f;

    private int targetIndex;

    void Start()
    {
        targetIndex = 0;
        height = this.transform.position.y;
    }

    void Update()
    {
        UpdateTarget();
        MoveToTarget();
        CheckDestroy();
        CheckLivelihood();
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    private void CheckLivelihood()
    {
        if(health <= 0)
        {
            // move target out of game world to unsubscribe it from turret target list
            this.transform.position = new Vector3(-100, -100, -100);
            deadtimer = 1.0f;
            health = 10;
            GameManager.Instance.AddMoney(reward);
        }
    }

    private void CheckDestroy()
    {
        deadtimer -= Time.deltaTime;
        if (deadtimer <= 0 && deadtimer > -10)
        {
            // Destroy object after it has been moved out of turret range
            Destroy(this.gameObject);
        }
    }

    private void MoveToTarget()
    {
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, pathPoints[targetIndex].position, moveSpeed);
        // Maintain height
        this.transform.position = new Vector3(this.transform.position.x, height, this.transform.position.z);
    }

    private void UpdateTarget()
    {
        if((this.transform.position - pathPoints[targetIndex].position).magnitude < proximity)
        {
            targetIndex++; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            other.GetComponent<Goal>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
