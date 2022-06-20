using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireDamagingEffect : MonoBehaviour
{
    [Header("Damaging Effect")]
    [SerializeField] private SkinnedMeshRenderer modelRenderer;
    [ColorUsage(true, true)]
    [SerializeField] private Color Color1, Color2, Color3;
    [ColorUsage(true, true)]
    [SerializeField] private Color Color1m, Color2m, Color3m;
    [SerializeField] private float speed1, speed2;
    [SerializeField] private Vector2 limit1, limit2;
    [SerializeField] private Animator anim;
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private ParticleSystem fireSmoke;
    [SerializeField] private GameObject rocksHolder;
    [SerializeField] private Rigidbody[] rocks;

    private void Start()
    {
        anim.SetFloat("Speed", 0.5f);
        int x = Random.Range(0, 2);
        if (x > 0)
            limit1.x *= -1;
         x = Random.Range(0, 2);
        if (x > 0)
            limit1.y *= -1;

        if (x > 0)
            limit2.x *= -1;
        x = Random.Range(0, 2);
        if (x > 0)
            limit2.y *= -1;

        modelRenderer.material.SetFloat("speed_0", limit1.y);
        modelRenderer.material.SetFloat("speed_1", limit2.y);
    }
    public void UpdateMaterial(int health, float maxHealth)
    {
        //speed1 = Mathf.Lerp(limit1.x, limit1.y, health / maxHealth);
        //speed2 = Mathf.Lerp(limit2.x, limit2.y, health / maxHealth);

        //modelRenderer.material.SetFloat("speed_0", speed1);
        //modelRenderer.material.SetFloat("speed_1", speed2);

        modelRenderer.material.SetColor("color_1", Color.Lerp(Color1m, Color1, health / maxHealth));
        modelRenderer.material.SetColor("color_2", Color.Lerp(Color2m, Color2, health / maxHealth));
        modelRenderer.material.SetColor("color_3", Color.Lerp(Color3m, Color3, health / maxHealth));
        anim.SetFloat("Speed",Mathf.Lerp(0.3f,1f, health / maxHealth));
    }

    public void Dissolve()
    {
        StartCoroutine(DissolveAnimation());
    }

    IEnumerator DissolveAnimation()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        fireSmoke.Play();
        fireSmoke.transform.SetParent(null);
        modelRenderer.material = dissolveMaterial;
        float amount = 0f;
        //modelRenderer.material.SetFloat("_DissolveAmount", 0.5f);
        rocksHolder.transform.SetParent(null);
        rocksHolder.SetActive(true);
        //foreach (Rigidbody rock in rocks)
        //{
        //    rock.velocity = Vector3.down * 10;
        //}
        DOTween.To(() => amount, x => amount = x, 1.0f, 1.0f).OnUpdate(() => modelRenderer.material.SetFloat("_DissolveAmount", amount));
        yield return new WaitForSecondsRealtime(1.25f);
        gameObject.SetActive(false);
    }
}
