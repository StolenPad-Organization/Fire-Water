using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    [SerializeField] private Operator Operator;
    [SerializeField] private Element element;
    [SerializeField] private float amount;
    [SerializeField] private TextMeshProUGUI gateText;
    [SerializeField] private ParticleSystem gainVFX;
    [SerializeField] private ParticleSystem drainVFX;
    void Start()
    {
        switch (Operator)
        {
            case Operator.Plus:
                gateText.text = "+" + amount;
                break;
            case Operator.Minus:
                gateText.text = "-" + amount;
                break;
            case Operator.Devide:
                gateText.text = "/" + amount;
                break;
            case Operator.Multiply:
                gateText.text = "*" + amount;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (element)
            {
                case Element.Fire:
                    DoAction(EventManager.GetFireWeapon?.Invoke());
                    break;
                case Element.Frost:
                    DoAction(EventManager.GetFrostWeapon?.Invoke());
                    break;
                default:
                    break;
            }
            gameObject.SetActive(false);
        }
    }

    private void DoAction(PlayerWeapon playerWeapon) 
    {
        switch (Operator)
        {
            case Operator.Plus:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValueBG += amount;
                playerWeapon.liquidValueBG = Mathf.Clamp(playerWeapon.liquidValueBG, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValue, x => playerWeapon.liquidValue = x, playerWeapon.liquidValueBG, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                gainVFX.Play();
                break;
            case Operator.Minus:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValue -= amount;
                playerWeapon.liquidValue = Mathf.Clamp(playerWeapon.liquidValue, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValueBG, x => playerWeapon.liquidValueBG = x, playerWeapon.liquidValue, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                drainVFX.Play();
                break;
            case Operator.Devide:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValue /= amount;
                playerWeapon.liquidValue = Mathf.Clamp(playerWeapon.liquidValue, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValueBG, x => playerWeapon.liquidValueBG = x, playerWeapon.liquidValue, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                drainVFX.Play();
                break;
            case Operator.Multiply:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValueBG *= amount;
                playerWeapon.liquidValueBG = Mathf.Clamp(playerWeapon.liquidValueBG, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValue, x => playerWeapon.liquidValue = x, playerWeapon.liquidValueBG, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                gainVFX.Play();
                break;
            default:
                break;
        }
    }
}
