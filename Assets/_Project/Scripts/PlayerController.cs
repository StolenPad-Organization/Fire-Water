using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform FireWeapon;
    [SerializeField] private Transform FrostWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedSwitch"))
        {
            //FireWeapon.transform.DORotate(new Vector3(0, FireWeapon.transform.rotation.y + 180, 0), 0.5f);
            FireWeapon.eulerAngles += Vector3.up * 180;
        }

        if (other.CompareTag("BlueSwitch"))
        {
            //FrostWeapon.transform.DORotate(new Vector3(0, FrostWeapon.transform.rotation.y + 180, 0), 0.5f);
            FrostWeapon.eulerAngles += Vector3.up * 180;
        }
    }
}
