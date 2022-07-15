using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class EnemyController : MonoBehaviour
{
    public Element EnemyElement;
    [SerializeField] private float speed;
    [SerializeField] public int health;
    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject[] Models;
    [SerializeField] private NavMeshAgent agent;
    public Transform player;
    [SerializeField] private ParticleSystem deathVFX;
    public Transform model;
    [SerializeField] private Transform Water;
    [SerializeField] private float minYPos;
    [SerializeField] private FireDamagingEffect fireDamagingEffect;
    [SerializeField] private FrostDamagingEffect frostDamagingEffect;
    bool Invincible;
    [SerializeField] private GameObject Coin;

    void Start()
    {
        maxHealth = health;
        //int x = Random.Range(0, Models.Length);
        //for (int i = 0; i < Models.Length; i++)
        //{
        //    if(i == x)
        //    {
        //        Models[i].SetActive(true);
        //    }
        //    else
        //    {
        //        Models[i].SetActive(false);
        //    }
        //}
    }

    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(player != null && agent.gameObject.activeSelf && agent.enabled)
            agent.SetDestination(player.position);
    }
    public void TakeDamage(int damage)
    {
        if (Invincible) return;

        //model.transform.localScale = Vector3.one;
        //model.transform.DOScale(Vector3.one * 0.8f, 0.15f).OnComplete(() => model.transform.DOScale(Vector3.one, 0.15f));
        //model.transform.DOShakePosition(0.25f).OnComplete(() => model.transform.localPosition = Vector3.zero);
        health -= damage;
        healthBar.UpdateHealthUI(health / maxHealth);
        if(EnemyElement == Element.Frost)
        {
            Water.localScale = new Vector3(Mathf.Lerp(1.5f, 0.5f, health / maxHealth), 1.5f, Mathf.Lerp(1.5f, 0.5f, health / maxHealth));
            model.localPosition = new Vector3(0, Mathf.Lerp(minYPos, 0f, health / maxHealth), 0);
            if(frostDamagingEffect != null)
            {
                frostDamagingEffect.PlayHitEffect();
            }
        }
        else
        {
            if(fireDamagingEffect != null)
            {
                fireDamagingEffect.UpdateMaterial(health, maxHealth);
                fireDamagingEffect.StartFog();
                //model.transform.localScale = Vector3.one;
                //model.transform.DOScale(Vector3.one * 0.9f, 0.15f).OnComplete(() => model.transform.DOScale(Vector3.one, 0.15f));
                //model.transform.DOShakePosition(0.15f).OnComplete(() => model.transform.localPosition = Vector3.zero);
            }

        }
        if (health <= 0)
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            Coin.transform.SetParent(null);
            Coin.SetActive(true);
            EventManager.AddMoney?.Invoke(100, transform);
            //deathVFX.transform.SetParent(null);
            //deathVFX.Play();
            if (EnemyElement == Element.Frost)
            {
                agent.speed = 0;
                agent.enabled = false;
                Invincible = true;
                Destroy(gameObject, 0.4f);
            }
            else
            {
                Invincible = true;
                fireDamagingEffect.Dissolve();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Invincible)
        {
            other.tag = "Untagged";
            //Destroy(transform.parent.gameObject);
            //EventManager.TriggerLose?.Invoke();
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        EventManager.PlayerDeath?.Invoke();
        yield return new WaitForSecondsRealtime(3.0f);
        EventManager.TriggerLose?.Invoke();
    }
}
