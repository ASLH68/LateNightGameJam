using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public PlayerControls control;

    Vector2 move;

    float lastFrameX;
    private bool holdingRight = false;
    private bool holdingLeft = false;
    [SerializeField] public Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite pixel;
    [SerializeField] AudioSource walkSound;

    void Awake()
    {
        control = new PlayerControls();

        control.Gameplay.Right.performed += ctx => holdingRight = true;
        control.Gameplay.Right.canceled += ctx=> holdingRight = false;

        control.Gameplay.Left.performed += ctx => holdingLeft = true;
        control.Gameplay.Left.canceled += ctx => holdingLeft = false;

        lastFrameX = transform.position.x;
    }

    void Start()
    {
        animator.SetBool("IsPixel", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingRight)
        {
            walkSound.volume = 1;

            Vector2 moveVelocity = new Vector2(1, 0) * 5f * Time.deltaTime;
            transform.Translate(moveVelocity, Space.Self);

            Vector3 flip = transform.localScale;
            flip.x = 0.4512983f;
            transform.localScale = flip;
        }

        if(!holdingRight && !holdingLeft)
        {
            walkSound.volume = 0;

            transform.Translate(Vector2.zero, Space.Self);
        }

        if(holdingLeft)
        {
            walkSound.volume = 1;

            Vector2 moveVelocity = new Vector2(-1, 0) * 5f * Time.deltaTime;
            transform.Translate(moveVelocity, Space.Self);

            Vector3 flip = transform.localScale;
            flip.x = -0.4512983f;
            transform.localScale = flip;
        }

        // Using comparison instead of equal to account for float
        // precision errors
        if (Mathf.Abs(transform.position.x - lastFrameX) < 0.001F)
        {
            animator.SetBool("canWalk", false);
        }
        else
        {
            animator.SetBool("canWalk", true);
        }

        lastFrameX = transform.position.x;
    }



    public void switchSprite()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pixel;
        animator.SetBool("IsPixel", true);
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
