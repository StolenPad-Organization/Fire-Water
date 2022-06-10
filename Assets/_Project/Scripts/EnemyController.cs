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
    public Transform player;
    [SerializeField] private ParticleSystem deathVFX;
    public Transform model;
    [SerializeField] private Transform Water;
    [SerializeField] private float minYPos;

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
        if(EnemyElement == Element.Frost)
        {
            Water.localScale = new Vector3(Mathf.Lerp(1.5f, 0.5f, health / maxHealth), 1.5f, Mathf.Lerp(1.5f, 0.5f, health / maxHealth));
            model.localPosition = new Vector3(0, Mathf.Lerp(minYPos, 0f, health / maxHealth), 0);
        }
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
