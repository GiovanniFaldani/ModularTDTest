using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;
    private Vector3 _direction;


    void Start()
    {

    }

    // Move in a straight line for a period of time
    void Update()
    {
        Lifetime();
    }

    private void Lifetime()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        this._direction = newDirection;
    }

    public void SetDamage(int newDamage)
    {
        this.damage = newDamage;
    }

    // hit detect an enemy
    private void OnTriggerEnter(Collider other)
    {
        // source and tag check for enemy, walls or player to explode
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

}

