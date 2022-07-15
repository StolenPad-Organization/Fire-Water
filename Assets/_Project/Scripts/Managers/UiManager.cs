using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class UiManager : MonoBehaviour
{
    [Header("Money Variables")]
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Transform moneyTarget;
    [SerializeField] private Transform canvasSpace;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private float VFXDuration;

    private void OnEnable()
    {
        EventManager.AddMoney += AddMoney;
    }

    private void OnDisable()
    {
        EventManager.AddMoney -= AddMoney;
    }

    void Start()
    {
        money = 0;
        moneyText.text = money + "";
    }

    private void AddMoney(int amount, Transform target)
    {
        Vector3 pos = target.position;
        pos = Camera.main.WorldToScreenPoint(pos);
        pos.z = 0;
        GameObject moneyClone = Instantiate(moneyPrefab, pos, Quaternion.identity, canvasSpace);

        moneyClone.transform.DOScale(0.5f, VFXDuration);
        moneyClone.transform.DOMove(moneyTarget.position, VFXDuration).OnComplete(() => 
        {
            Destroy(moneyClone);
            money += amount;
            moneyText.text = money + "";
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        });
    }
}
