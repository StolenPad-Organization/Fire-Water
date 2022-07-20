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
        yield return new WaitForSecondsRealtime(1.5f);
        for (int i = 0; i < coinList.Length; i++)
        {
            EventManager.AddMoney?.Invoke(10, coinList[i]);
            coinList[i].gameObject.SetActive(false);
        }
    }
}
