using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostDamagingEffect : MonoBehaviour
{
    [Header("Damaging Effect")]
    [SerializeField] private ParticleSystem hitVFX;
    public void PlayHitEffect()
    {
        if (!hitVFX.isPlaying)
            hitVFX.Play();
    }
}
