using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private Operator Operator;
    [SerializeField] private Element element;
    [SerializeField] private float amount;
    [SerializeField] private TextMeshProUGUI gateText;
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
        }
    }

    private void DoAction(PlayerWeapon playerWeapon) 
    {
        switch (Operator)
        {
            case Operator.Plus:
                playerWeapon.liquidValue += amount;
                break;
            case Operator.Minus:
                playerWeapon.liquidValue -= amount;
                break;
            case Operator.Devide:
                playerWeapon.liquidValue /= amount;
                break;
            case Operator.Multiply:
                playerWeapon.liquidValue *= amount;
                break;
            default:
                break;
        }
    }
}
