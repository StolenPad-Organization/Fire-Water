using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] private EnemyController[] EnemiesList;
    private Transform Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.transform;
            ActivateEnemies();
        }
    }

    private void ActivateEnemies()
    {
        foreach (EnemyController enemy in EnemiesList)
        {
            enemy.transform.parent.gameObject.SetActive(true);
            enemy.player = Player;
        }
    }
}
