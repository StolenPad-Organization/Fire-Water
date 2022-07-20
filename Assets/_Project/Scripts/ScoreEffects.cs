using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreEffects : MonoBehaviour
{
    [SerializeField] private Renderer Renderer;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer.material.EnableKeyword("_EMISSION");
            Renderer.transform.DOScale(new Vector3(14, 10, 14), 0.1f).OnComplete(() => Renderer.transform.DOScale(new Vector3(12, 10, 12), 0.1f));
        }
    }
}
