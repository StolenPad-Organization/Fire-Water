using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class UiManager : MonoBehaviour
{
    [Header("Money Variables")]
    [SerializeField] private int collectedMoney;
    [SerializeField] private int totalMoney;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Transform moneyTarget;
    [SerializeField] private Transform canvasSpace;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private float VFXDuration;
    private int bonus = 1;

    private void OnEnable()
    {
        EventManager.AddMoney += AddMoney;
        EventManager.UpdateScore += UpdateScore;
        EventManager.TriggerWin += SetFinalMoney;
    }

    private void OnDisable()
    {
        EventManager.AddMoney -= AddMoney;
        EventManager.UpdateScore -= UpdateScore;
        EventManager.TriggerWin -= SetFinalMoney;
    }

    void Start()
    {
        collectedMoney = 0;
        totalMoney = Database.Instance.GetMoney();
        moneyText.text = totalMoney + collectedMoney + "";
        levelText.text = "Level " + Database.Instance.GetLevelData().LevelTextValue;
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
            collectedMoney += amount;
            moneyText.text = totalMoney + collectedMoney + "";
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        });
    }

    private void UpdateScore(int amount)
    {
        bonus = amount;
    }

    private void SetFinalMoney()
    {
        collectedMoney *= bonus;
        moneyText.text = totalMoney + collectedMoney + "";
        Database.Instance.SetMoney(totalMoney + collectedMoney);
    }
}
