using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //player variables
    [SerializeField] private float speed;
    [SerializeField] private float rpm;
    [SerializeField] private float horsePower = 0f;
    private float turnSpeed = 45f;
    public InputActionAsset playerInputActionAsset;
    private InputAction verticalInput;
    private InputAction horizontalInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> wheelColliders;
    int wheelsOnGround;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.localPosition;

        initialPosition = playerRb.transform.localPosition;
        initialRotation = playerRb.transform.localRotation;
    }
    private void OnEnable()
    {
        if(transform.CompareTag("Player1"))
        {
            verticalInput = playerInputActionAsset.FindActionMap("Player1").FindAction("Vertical");
            horizontalInput = playerInputActionAsset.FindActionMap("Player1").FindAction("Horizontal");
        }
        else if(transform.CompareTag("Player2"))
        {
            verticalInput = playerInputActionAsset.FindActionMap("Player2").FindAction("Vertical");
            horizontalInput = playerInputActionAsset.FindActionMap("Player2").FindAction("Horizontal");
        }
        verticalInput.Enable();
        horizontalInput.Enable();
    }
    private void OnDisable()
    {
        verticalInput.Disable();
        horizontalInput.Disable();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalValue = verticalInput.ReadValue<float>();
        float horizontalValue = horizontalInput.ReadValue<float>();

        if(IsOnGround())
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalValue);
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalValue);

            //rotates the car based on horizontal input
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalValue);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f);
            rpm = (speed % 30) * 40;
            
        }
        else
        {
            rpm = 0;
            speed = 0;
        }

        speedometerText.text = "Speed: " + speed.ToString() + " Km/h";
        rpmText.text = "RPM: " + rpm.ToString();

        if(transform.position.y < -5f)
        {
            playerRb.velocity = Vector3.zero;
            transform.rotation = initialRotation;
            transform.position = initialPosition;
               
        }

    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach(WheelCollider wheel in wheelColliders)
        {
            if(wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if(wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
