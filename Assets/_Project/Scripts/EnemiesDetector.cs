using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDetector : MonoBehaviour
{
    [SerializeField] private List<EnemyController> fireEnemiesList;
    [SerializeField] private List<EnemyController> frostEnemiesList;
    List<EnemyController> removedEnemies = new List<EnemyController>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrostEnemy") || other.CompareTag("FireEnemy"))
        {
            EnemyController EC = other.GetComponent<EnemyController>();

            switch (EC.EnemyElement)
            {
                case Element.Fire:
                    if (!fireEnemiesList.Contains(EC))
                    {
                        fireEnemiesList.Add(EC);
                    }
                    break;
                case Element.Frost:
                    if (!frostEnemiesList.Contains(EC))
                    {
                        frostEnemiesList.Add(EC);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FrostEnemy") || other.CompareTag("FireEnemy"))
        {
            EnemyController EC = other.GetComponent<EnemyController>();

            switch (EC.EnemyElement)
            {
                case Element.Fire:
                    if (fireEnemiesList.Contains(EC))
                    {
                        fireEnemiesList.Remove(EC);
                    }
                    break;
                case Element.Frost:
                    if (frostEnemiesList.Contains(EC))
                    {
                        frostEnemiesList.Remove(EC);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        removedEnemies.Clear();
        foreach (var item in frostEnemiesList)
        {
            if(!item)
                removedEnemies.Add(item);
        }
        foreach (var item in removedEnemies)
        {
            frostEnemiesList.Remove(item);
        }

        removedEnemies.Clear();
        foreach (var item in fireEnemiesList)
        {
            if (!item)
                removedEnemies.Add(item);
        }
        foreach (var item in removedEnemies)
        {
            fireEnemiesList.Remove(item);
        }

        if(fireEnemiesList.Count > 0)
        {
            EventManager.OpenFrostWeapon?.Invoke(true);
        }
        else
        {
            EventManager.OpenFrostWeapon?.Invoke(false);
        }

        if (frostEnemiesList.Count > 0)
        {
            EventManager.OpenFireWeapon?.Invoke(true);
        }
        else
        {
            EventManager.OpenFireWeapon?.Invoke(false);
        }
    }
}
