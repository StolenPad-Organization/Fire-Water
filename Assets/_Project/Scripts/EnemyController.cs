using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Dreamteck.Splines;

public class EnemyController : MonoBehaviour
{
    public Element EnemyElement;
    [SerializeField] private float speed;
    [SerializeField] private int health;
    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject[] Models;
    [SerializeField] private NavMeshAgent agent;
    public SplineFollower splineFollower;
    public Transform model;
    public Transform player;
    [SerializeField] private ParticleSystem deathVFX;
    
    void Start()
    {
        maxHealth = health;
        //int x = Random.Range(0, Models.Length);
        //for (int i = 0; i < Models.Length; i++)
        //{
        //    if(i == x)
        //    {
        //        Models[i].SetActive(true);
        //    }
        //    else
        //    {
        //        Models[i].SetActive(false);
        //    }
        //}
    }

    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(player != null)
            agent.SetDestination(player.position);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthUI(health / maxHealth);
        if (health <= 0)
        {
            deathVFX.transform.SetParent(null);
            deathVFX.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
