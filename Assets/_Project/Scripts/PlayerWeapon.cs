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
    [SerializeField] private Renderer liquidTankBG;
    [SerializeField] private Vector2 liquidCapacity;
    public float liquidValue;
    public float liquidValueBG;
    [SerializeField] private float fillRate;
    private bool canFire = false;
    public bool matchLiquid = true;
    [SerializeField] private GameObject lavaPool;
    private void OnEnable()
    {
        switch (weaponElement)
        {
            case Element.Fire:
                EventManager.GetFireWeapon += GetPlayerWeapon;
                EventManager.OpenFireWeapon += ActivateWeapon;
                break;
            case Element.Frost:
                EventManager.GetFrostWeapon += GetPlayerWeapon;
                EventManager.OpenFrostWeapon += ActivateWeapon;
                break;
            default:
                break;
        }
        
    }

    private void OnDisable()
    {
        switch (weaponElement)
        {
            case Element.Fire:
                EventManager.GetFireWeapon -= GetPlayerWeapon;
                EventManager.OpenFireWeapon -= ActivateWeapon;
                break;
            case Element.Frost:
                EventManager.GetFrostWeapon -= GetPlayerWeapon;
                EventManager.OpenFrostWeapon -= ActivateWeapon;
                break;
            default:
                break;
        }
    }

    void Start()
    {
        enemiesList = new List<EnemyController>();
        removedList = new List<EnemyController>();
        weaponVFX.Stop(true);
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
        if (canFire)
        {
            liquidValue -= fillRate * Time.deltaTime;
            liquidValue = Mathf.Clamp(liquidValue, liquidCapacity.x, liquidCapacity.y);
            if (matchLiquid)
                liquidValueBG = liquidValue;
            if (liquidValue == liquidCapacity.x)
            {
                canFire = false;
                weaponVFX.Stop(true);
            }
        }
        liquidTank.material.SetFloat("Liquid_Fill", liquidValue);
        liquidTankBG.material.SetFloat("Liquid_Fill", liquidValueBG);
    }

    private PlayerWeapon GetPlayerWeapon()
    {
        return this;
    }

    private void ActivateWeapon(bool Activate)
    {
        if (Activate)
        {
            if (liquidValue > liquidCapacity.x)
            {
                canFire = true;
                if (weaponVFX.isStopped)
                {
                    weaponVFX.Play(true);
                    if (lavaPool != null)
                        lavaPool.SetActive(true);
                }
                    
            }   
        }
        else
        {
            canFire = false;
            weaponVFX.Stop(true);
            if (lavaPool != null)
                lavaPool.SetActive(false);
        }

    }
}
