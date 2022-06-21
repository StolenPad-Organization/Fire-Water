using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateDetector : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            other.transform.DOMoveY(-10f, 0.5f).OnComplete(() => other.gameObject.SetActive(false));
        }
    }
}
