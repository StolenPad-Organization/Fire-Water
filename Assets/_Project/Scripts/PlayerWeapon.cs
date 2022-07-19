using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Element weaponElement;
    [SerializeField] private string Tag;
    [SerializeField] private int damage;
    List<EnemyController> enemiesList;
    List<EnemyController> removedList;
    [SerializeField] private ParticleSystem weaponVFX;
    [SerializeField] private ParticleSystem weaponMuzzleVFX;
    [Header("Liquid")]
    [SerializeField] private Renderer liquidTank;
    [SerializeField] private Renderer liquidTankBG;
    public Vector2 liquidCapacity;
    public Vector2 realLiquidCapacity;
    public float liquidValue;
    public float realLiquidValue;
    public float liquidValueBG;
    public float realLiquidValueBG;
    [SerializeField] private float fillRate;
    private bool canFire = false;
    public bool matchLiquid = true;
    [SerializeField] private GameObject lavaPool;
    [SerializeField] private EnemiesDetector enemiesDetector;
    private Vector3 originalRotation;
    private bool canAim = true;
    private Vector3 originalLiquidRotation;
    [SerializeField] private Transform weaponBone;

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

    public void LockAim()
    {
        fillRate = 12;
        transform.localEulerAngles = originalRotation;
        canAim = false;
        ActivateWeapon(true);
        weaponVFX.transform.localEulerAngles = Vector3.zero;
    }

    public void FreeAim()
    {
        canAim = true;
        ActivateWeapon(false);
        weaponVFX.transform.localEulerAngles = originalLiquidRotation;
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
        if(Database.Instance.GetLevelData().LevelTextValue == 1)
        {
            damage += 7;
            fillRate -= 5;
        }
        else if (Database.Instance.GetLevelData().LevelTextValue == 2)
        {
            damage += 5;
            fillRate -= 3;
        }
        originalLiquidRotation = weaponVFX.transform.localEulerAngles;
        originalRotation = transform.localEulerAngles;
        enemiesList = new List<EnemyController>();
        removedList = new List<EnemyController>();
        weaponVFX.Stop(true);
        if (weaponMuzzleVFX != null)
            weaponMuzzleVFX.Stop(true);
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
        //if (canAim)
        //{
        //    switch (weaponElement)
        //    {
        //        case Element.Fire:
        //            if (enemiesDetector.frostEnemiesList.Count > 0)
        //            {
        //                Transform closestTarget = enemiesDetector.frostEnemiesList[0].transform;
        //                transform.LookAt(closestTarget);
        //            }
        //            break;
        //        case Element.Frost:
        //            if (enemiesDetector.fireEnemiesList.Count > 0)
        //            {
        //                Transform closestTarget = enemiesDetector.fireEnemiesList[0].transform;
        //                transform.LookAt(closestTarget);
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

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
        }

        liquidValue = Mathf.Clamp(liquidValue, liquidCapacity.x, liquidCapacity.y);
        float t = Mathf.InverseLerp(liquidCapacity.x, liquidCapacity.y, liquidValue);
        realLiquidValue = Mathf.Lerp(realLiquidCapacity.x, realLiquidCapacity.y, t);
        if (matchLiquid)
        {
            liquidValueBG = liquidValue;
            realLiquidValueBG = realLiquidValue;
        }

        if (canFire)
        {
            if (liquidValue == liquidCapacity.x)
            {
                canFire = false;
                weaponVFX.Stop(true);
                if (weaponMuzzleVFX != null)
                    weaponMuzzleVFX.Stop(true);
            }
        }

        liquidTank.material.SetFloat("Liquid_Fill", realLiquidValue);
        liquidTankBG.material.SetFloat("Liquid_Fill", realLiquidValueBG);
    }

    private PlayerWeapon GetPlayerWeapon()
    {
        return this;
    }

    private void ActivateWeapon(bool Activate)
    {
        if (Activate || !canAim)
        {
            if (liquidValue > liquidCapacity.x)
            {
                canFire = true;
                StartCoroutine(FiringRoutine());
                if (weaponVFX.isStopped)
                {
                    weaponVFX.Play(true);
                    if (weaponMuzzleVFX != null)
                        weaponMuzzleVFX.Play(true);
                    if (lavaPool != null)
                        lavaPool.SetActive(true);
                }

            }
            else
            {
                EventManager.DeactivateJetpack?.Invoke(weaponElement);
            }
        }
        else
        {
            canFire = false;
            weaponVFX.Stop(true);
            if (weaponMuzzleVFX != null)
                weaponMuzzleVFX.Stop(true);
            if (lavaPool != null)
                lavaPool.SetActive(false);
        }
    }

    IEnumerator FiringRoutine()
    {
        while (canFire)
        {
            yield return weaponBone.DOScale(1.8f, 0.05f).WaitForCompletion();
            yield return weaponBone.DOScale(1.0f, 0.05f).WaitForCompletion();
        }
    }
}
