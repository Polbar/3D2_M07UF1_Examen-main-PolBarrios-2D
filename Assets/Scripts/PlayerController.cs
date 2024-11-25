using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    private float horizontalInput;
    [SerializeField] private float characterSpeed = 4.5f;
    [SerializeField] private float jumpForce = 5f;

    private Animator characterAnimator;
    
    [SerializeField] private GameObject finishCanvas;

    private int coinCount = 0;
    [SerializeField] private Text coinText;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            characterAnimator.SetBool("IsRunning", true);
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false);
        }

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Jump") && GroundSensor.isGrounded)
        {
            Jump();
        }

        characterAnimator.SetBool("IsJumping", !GroundSensor.isGrounded);
    }

    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;

            if (coinText != null)
            {
                coinText.text = "Coins: " + coinCount;
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            if (finishCanvas != null)
            {
                finishCanvas.SetActive(true);
            }
        }
    }
}
