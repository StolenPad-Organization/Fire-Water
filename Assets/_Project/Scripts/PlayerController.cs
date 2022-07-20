using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform FireWeapon;
    [SerializeField] private Transform FrostWeapon;
    [SerializeField] private Animator FrostAnim;
    [SerializeField] private Animator FireAnim;
    [SerializeField] private SplineFollower splineFollower;
    [SerializeField] private GameObject FireBoy;
    [SerializeField] private Rigidbody FireBoyRigidBody;
    [SerializeField] private Collider FireBoyCollider;
    [SerializeField] private GameObject FireTank;
    [SerializeField] private GameObject FrostBoy;
    [SerializeField] private Rigidbody FrostBoyRigidBody;
    [SerializeField] private Collider FrostBoyCollider;
    [SerializeField] private GameObject FrostTank;
    private bool finish;
    private int finishedCharacters;
    [SerializeField] private CinemachineVirtualCamera vCam;

    private void OnEnable()
    {
        EventManager.PlayerDeath += Death;
        EventManager.DeactivateJetpack += DeactivateJetpack;
    }

    private void OnDisable()
    {
        EventManager.PlayerDeath -= Death;
        EventManager.DeactivateJetpack -= DeactivateJetpack;
    }

    private void Start()
    {
        FrostAnim.SetFloat("Blend", 0);
        FireAnim.SetFloat("Blend", 1);
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
            ActivateJetpack();
            finish = true;
            splineFollower.enabled = false;
            //EventManager.TriggerWin?.Invoke();
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

    private void Update()
    {
        if (finish)
        {
            transform.parent.Translate(Vector3.forward * 15 * Time.deltaTime);
        }
    }

    private void ActivateJetpack()
    {
        FrostAnim.SetBool("JetPack", true);
        FireAnim.SetBool("JetPack", true);
        FrostWeapon.GetChild(0).GetComponent<PlayerWeapon>().LockAim();
        FireWeapon.GetChild(0).GetComponent<PlayerWeapon>().LockAim();
        FireBoy.transform.DOLocalMoveY(7.0f, 0.25f).OnComplete(() => FireWeapon.GetChild(0).GetComponent<PlayerWeapon>().LockAim());
        FrostBoy.transform.DOLocalMoveY(7.0f, 0.25f).OnComplete(() => FrostWeapon.GetChild(0).GetComponent<PlayerWeapon>().LockAim());
    }

    private void DeactivateJetpack(Element element)
    {
        if (!finish) return;
        finishedCharacters++;
        if (finishedCharacters >= 2)
        {
            finish = false;
        }
        switch (element)
        {
            case Element.Fire:
                FireAnim.SetBool("JetPack", false);
                FireAnim.SetTrigger("Victory");
                FireWeapon.GetChild(0).GetComponent<PlayerWeapon>().FreeAim();
                FireBoyCollider.enabled = true;
                FireTank.SetActive(false);
                FireBoy.transform.DOLocalMoveY(0.28f, 0.25f).OnComplete(() =>
                {
                    if (FireBoy.tag == "Character")
                    {
                        FireBoyRigidBody.isKinematic = false;
                        FireBoyRigidBody.useGravity = true;
                    }
                    FireWeapon.GetChild(0).GetComponent<PlayerWeapon>().FreeAim();
                    if (finishedCharacters >= 2)
                    {
                        EventManager.TriggerWin?.Invoke();
                        vCam.Follow = null;
                    }
                    else
                    {
                        FireBoy.transform.SetParent(null);
                        FireTank.transform.SetParent(null);
                    }
                });
                break;
            case Element.Frost:
                FrostAnim.SetBool("JetPack", false);
                FrostAnim.SetTrigger("Victory");
                FrostWeapon.GetChild(0).GetComponent<PlayerWeapon>().FreeAim();
                FrostBoyCollider.enabled = true;
                FrostTank.SetActive(false);
                FrostBoy.transform.DOLocalMoveY(0.28f, 0.25f).OnComplete(() =>
                {
                    if(FrostBoy.tag == "Character")
                    {
                        FrostBoyRigidBody.isKinematic = false;
                        FrostBoyRigidBody.useGravity = true;
                    }
                    FrostWeapon.GetChild(0).GetComponent<PlayerWeapon>().FreeAim();
                    if (finishedCharacters >= 2)
                    {
                        EventManager.TriggerWin?.Invoke();
                        vCam.Follow = null;
                    }
                    else
                    {
                        FrostBoy.transform.SetParent(null);
                        FrostTank.transform.SetParent(null);
                    }
                });
                break;
            default:
                break;
        }
    }
}
