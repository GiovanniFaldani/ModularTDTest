using UnityEngine;

public class Goal: MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        MessagePrinter.Instance.PrintMessage("Took " + damage + " damage", 2);
        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Update()
    {
        GameManager.Instance.baseHP = health;
    }

}
