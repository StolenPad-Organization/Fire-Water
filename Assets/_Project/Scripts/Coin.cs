using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform[] coinList;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(GiveMoney());
    }

    IEnumerator GiveMoney()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        for (int i = 0; i < coinList.Length; i++)
        {
            EventManager.AddMoney?.Invoke(20, coinList[i]);
            coinList[i].gameObject.SetActive(false);
        }
    }
}
