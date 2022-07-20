using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlatform : MonoBehaviour
{
    [SerializeField] int value;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.tag = "Untagged";
            EventManager.UpdateScore?.Invoke(value);
            Debug.Log("Score :" + value);
        }
    }
}
