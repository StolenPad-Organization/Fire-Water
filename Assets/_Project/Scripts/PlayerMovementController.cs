using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private float playerSpeed = 20;
    [SerializeField] private float bounds;
    [Space]
    [Header("Input Properties")]
    [SerializeField] private float clampDelta;
    [SerializeField] private float inputSpeed;
    #region MovementChecks
    bool canMove;
    bool canMoveForward;
    bool onceStart;
    #endregion
    private Vector3 startMousePosition;

    private void OnEnable()
    {
        //EventManager.obstacleCollision += ObstacleCollision;
        //EventManager.mergeSwordEnding += MergeStart;
        //EventManager.startMovement += ActivateMovement;
        //EventManager.moveOtherGate += MoveOtherGate;
    }

    private void OnDisable()
    {
        //EventManager.obstacleCollision -= ObstacleCollision;
        //EventManager.mergeSwordEnding -= MergeStart;
        //EventManager.startMovement -= ActivateMovement;
        //EventManager.moveOtherGate -= MoveOtherGate;
    }

    private void Awake()
    {
        //playerSpeed = Database.Instance.GetPlayerData().PlayerMovementSpeed;
        //inputSpeed = Database.Instance.GetPlayerData().PlayerSwerveSpeed;

        ActivateMovement();
    }

    //private void ObstacleCollision() // When Sword's handle hit the any obstacle, this method activate
    //{
    //    canMoveForward = false;
    //    transform.DOJump(new Vector3(transform.position.x,0.5f,transform.position.z - 6f),2,1,0.75f,false).OnComplete(()=>
    //    {
    //        canMoveForward = true;
    //    });
    //}

    //private void MergeStart() //When Sword merge is started at the level ending, this method activate
    //{
    //    canMove = false;
    //    canMoveForward = false;
    //}

    private void Update()
    {
        InputController();
        //if (canMoveForward)
        //    transform.Translate(Vector3.back * Time.deltaTime * playerSpeed);
    }

    private void ActivateMovement()
    {
        canMoveForward = true;
        canMove = true;
    }

    //private void MoveOtherGate(string gateName)
    //{
    //    canMove = false;
    //    if (gateName == "Speed")
    //        transform.DOMoveX(EventManager.getSpeedGatePosition.Invoke(), 0.25f);
    //    else if (gateName == "Damage")
    //        transform.DOMoveX(EventManager.getDamageGatePosition.Invoke(), 0.25f);
    //}

    private void InputController()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            
            if (!onceStart)
            {
                onceStart = true;
               
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (canMove)
            {
                Vector3 currentVector = startMousePosition - Input.mousePosition;
                startMousePosition = Input.mousePosition;
                currentVector = new Vector3(currentVector.x, 0f, 0f);

                Vector3 moveForce = Vector3.ClampMagnitude(currentVector, clampDelta);

                transform.localPosition += -moveForce * inputSpeed * Time.fixedDeltaTime;

                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -bounds, bounds), transform.localPosition.y, transform.localPosition.z);
            }
        }
#endif

#if !UNITY_EDITOR

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startMousePosition = touch.position;

                if (!onceStart)
                {
                    onceStart = true;

                }
            }

            if(touch.phase == TouchPhase.Moved)
            {
                if (canMove)
                {
                    Vector3 currentVector = startMousePosition - new Vector3(touch.position.x, touch.position.y,0);
                    startMousePosition = touch.position;
                    currentVector = new Vector3(currentVector.x, 0f, 0f);

                    Vector3 moveForce = Vector3.ClampMagnitude(currentVector, clampDelta);

                    transform.localPosition += -moveForce * inputSpeed * Time.fixedDeltaTime;

                    transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -bounds, bounds), transform.localPosition.y, transform.localPosition.z);
                }
            }
        }

#endif
    }
}
