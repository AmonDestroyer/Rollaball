using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public int allowedJumps = 2;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public PlayerInput palyerInput;

    private Rigidbody rb;
    private int count;
    private int jumpsUsed;
    private float movementX;
    private float movementY;
    private float movementZ;
    private bool groundedPlayer;
    private bool secondJumpAvailable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        jumpsUsed = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        Debug.Log($"({movementX},{movementY})");
    }       

    void OnJump()
    {
        if (jumpsUsed < allowedJumps)
        {
            jumpsUsed++;
            movementZ = 20.0f;
        }
        Debug.Log($"Jump");
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementZ, movementY);
        rb.AddForce(movement * speed);
        movementZ = 0.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            jumpsUsed = 0;
        }
    }
}
