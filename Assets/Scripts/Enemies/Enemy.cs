using Unity.VisualScripting;
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
            this.transform.position = new Vector3(this.transform.position.x, -200, this.transform.position.y);
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
