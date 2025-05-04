using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

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
        }
    }

}
