using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public PlayerControls control;

    Vector2 move;
    private bool holdingRight = false;
    private bool holdingLeft = false;

    void Awake()
    {
        control = new PlayerControls();

        control.Gameplay.Right.performed += ctx => holdingRight = true;
        control.Gameplay.Right.canceled += ctx=> holdingRight = false;

        control.Gameplay.Left.performed += ctx => holdingLeft = true;
        control.Gameplay.Left.canceled += ctx => holdingLeft = false;

        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingRight)
        {
            Vector2 moveVelocity = new Vector2(1, 0) * 5f * Time.deltaTime;
            transform.Translate(moveVelocity, Space.Self);
        }
        if(!holdingRight || !holdingLeft)
        {
            transform.Translate(Vector2.zero, Space.Self);
        }

        if(holdingLeft)
        {
            Vector2 moveVelocity = new Vector2(-1, 0) * 5f * Time.deltaTime;
            transform.Translate(moveVelocity, Space.Self);
        }
        

        
    }
    private void OnEnable()
    {
        control.Gameplay.Enable();
    }

    private void OnDisable()
    {
        control.Gameplay.Disable();
    }

}
