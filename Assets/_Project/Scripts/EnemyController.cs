using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int health;
    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBar healthBar;
    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthUI(health / maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
