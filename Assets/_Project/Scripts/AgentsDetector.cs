using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AgentsDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrostEnemy") || other.CompareTag("FireEnemy"))
        {
            EnemyController EC = other.GetComponent<EnemyController>();
            Transform t1 = EC.transform.parent;
            Transform t2 = EC.transform;

            t2.SetParent(null);
            t1.transform.position = t2.transform.position;
            if (other.CompareTag("FireEnemy"))
            {
                t1.LookAt(transform);
            }
                
            t2.SetParent(t1);
            
            EC.player = transform;
        }
    }
}
