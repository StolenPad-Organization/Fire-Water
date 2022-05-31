using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    void Start()
    {
        
    }

    public void UpdateHealthUI(float amount)
    {
        healthImage.fillAmount = amount;
    }
}
