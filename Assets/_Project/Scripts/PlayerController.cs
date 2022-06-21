using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform FireWeapon;
    [SerializeField] private Transform FrostWeapon;
    [SerializeField] private Animator FrostAnim;
    [SerializeField] private Animator FireAnim;
    [SerializeField] private SplineFollower splineFollower;

    private void OnEnable()
    {
        EventManager.PlayerDeath += Death;
    }

    private void OnDisable()
    {
        EventManager.PlayerDeath -= Death;
    }

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

        if (other.CompareTag("Finish"))
        {
            EventManager.TriggerWin?.Invoke();
        }
    }

    private void Death()
    {
        splineFollower.enabled = false;
        FrostAnim.SetTrigger("Death");
        FireAnim.SetTrigger("Death");
        FrostWeapon.parent.gameObject.SetActive(false);
        FireWeapon.parent.gameObject.SetActive(false);
    }
}
