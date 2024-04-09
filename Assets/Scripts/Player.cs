using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField]
    float moveSpeed = 1.5f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;

    [SerializeField] float paddingTop;

    [SerializeField] float paddingBot;
    public Animator animator;
    Shooter shooter;
    Vector2 minBounds;
    Vector2 maxBounds;


    void Awake() 
    {
        shooter = GetComponent<Shooter>();
    }
    
    void Start() 
    {
        InitBounds();
    }
    void Update()
    {
        MoveShip();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));

    }

    void MoveShip()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x  + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBot, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        
        if(Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame && Keyboard.current.dKey.wasPressedThisFrame)
        {
            animator.SetBool("UpPressed", true);
        }
        else
        {
            animator.SetBool("UpPressed", false);
        }

        if(Keyboard.current.sKey.wasPressedThisFrame|| Keyboard.current.sKey.wasPressedThisFrame && Keyboard.current.dKey.wasPressedThisFrame)
        {
            animator.SetBool("DownPressed", true);
        }
        else
        {
            animator.SetBool("DownPressed", false);
        }
        
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
