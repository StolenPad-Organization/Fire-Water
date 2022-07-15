using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class Gate : MonoBehaviour
{
    [SerializeField] private Operator Operator;
    [SerializeField] private Element element;
    [SerializeField] private float amount;
    [SerializeField] private TextMeshProUGUI gateText;
    [SerializeField] private ParticleSystem gainVFX;
    [SerializeField] private ParticleSystem drainVFX;
    [SerializeField] private Renderer Indication;
    [SerializeField] private Material goodMat;
    [SerializeField] private Material badMat;
    void Start()
    {
        switch (Operator)
        {
            case Operator.Plus:
                gateText.text = "+" + amount;
                Indication.material = goodMat;
                break;
            case Operator.Minus:
                gateText.text = "-" + amount;
                Indication.material = badMat;
                break;
            case Operator.Devide:
                gateText.text = "/" + amount;
                Indication.material = badMat;
                break;
            case Operator.Multiply:
                gateText.text = "x" + amount;
                Indication.material = goodMat;
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
            //gameObject.SetActive(false);
            transform.DOMoveY(-10f, 0.5f).OnComplete(() => gameObject.SetActive(false));
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
                gainVFX.transform.SetParent(null);
                gainVFX.Play();
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            case Operator.Minus:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValue -= amount;
                playerWeapon.liquidValue = Mathf.Clamp(playerWeapon.liquidValue, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValueBG, x => playerWeapon.liquidValueBG = x, playerWeapon.liquidValue, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                drainVFX.transform.SetParent(null);
                drainVFX.Play();
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case Operator.Devide:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValue /= amount;
                playerWeapon.liquidValue = Mathf.Clamp(playerWeapon.liquidValue, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValueBG, x => playerWeapon.liquidValueBG = x, playerWeapon.liquidValue, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                drainVFX.transform.SetParent(null);
                drainVFX.Play();
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case Operator.Multiply:
                playerWeapon.matchLiquid = false;
                playerWeapon.liquidValueBG *= amount;
                playerWeapon.liquidValueBG = Mathf.Clamp(playerWeapon.liquidValueBG, playerWeapon.liquidCapacity.x, playerWeapon.liquidCapacity.y);
                DOTween.To(() => playerWeapon.liquidValue, x => playerWeapon.liquidValue = x, playerWeapon.liquidValueBG, 0.4f)
                    .OnComplete(() => playerWeapon.matchLiquid = true);
                gainVFX.transform.SetParent(null);
                gainVFX.Play();
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            default:
                break;
        }
    }
}
