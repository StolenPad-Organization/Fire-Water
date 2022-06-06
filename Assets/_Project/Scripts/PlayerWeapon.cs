using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Element weaponElement;
    [SerializeField] private string Tag;
    [SerializeField] private int damage;
    List<EnemyController> enemiesList;
    List<EnemyController> removedList;
    [SerializeField] private ParticleSystem weaponVFX;
    [Header ("Liquid")]
    [SerializeField] private Renderer liquidTank;
    [SerializeField] private Vector2 liquidCapacity;
    [SerializeField] private float liquidValue;
    [SerializeField] private float fillRate;
    private bool canFire = true;
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
        UpdateLiquid();
        if (canFire)
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

    void UpdateLiquid()
    {
        if(liquidValue > liquidCapacity.x)
        {
            liquidValue -= fillRate * Time.deltaTime;
            liquidValue = Mathf.Clamp(liquidValue, liquidCapacity.x, liquidCapacity.y);
            liquidTank.material.SetFloat("Liquid_Fill", liquidValue);
            if(liquidValue == liquidCapacity.x)
            {
                canFire = false;
                weaponVFX.Stop(true);
            }
        }
    }
}
