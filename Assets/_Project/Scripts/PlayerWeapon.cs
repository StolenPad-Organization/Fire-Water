using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private string Tag;
    [SerializeField] private int damage;
    List<EnemyController> enemiesList;
    List<EnemyController> removedList;
    void Start()
    {
        enemiesList = new List<EnemyController>();
        removedList = new List<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            if (!enemiesList.Contains(other.GetComponent<EnemyController>()))
            {
                enemiesList.Add(other.GetComponent<EnemyController>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            if (enemiesList.Contains(other.GetComponent<EnemyController>()))
            {
                enemiesList.Remove(other.GetComponent<EnemyController>());
            }
        }
    }

    private void Update()
    {
        foreach (var enemy in enemiesList)
        {
            if (enemy == null)
                removedList.Add(enemy);
        }

        foreach (var enemy in removedList)
        {
            enemiesList.Remove(enemy);
        }

        foreach (var enemy in enemiesList)
        {
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
    }
}
