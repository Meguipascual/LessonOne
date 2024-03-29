using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horsePower = 20f;
    [SerializeField] private float turnSpeed = 45f;
    [SerializeField] float speed;
    [SerializeField] float rpm;
    [SerializeField] private GameObject centerOfMass;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Moves the car forward based on vertical input
        if (IsCarOnGround())
        {

            if (speed < 180)
            {
                playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);
            }

            //Rotates the car based on horizontal input
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f); //2.237 for Mph
            speedometerText.SetText("Speed: " + speed + " Km/h");

            rpm = (speed % 25) * 100;
            rpmText.SetText("RPM: " + rpm);
        }
    }
    bool IsCarOnGround() 
    {
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded) 
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround == 4) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
