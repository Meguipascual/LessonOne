using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horsePower = 20f;
    [SerializeField] private float turnSpeed = 45f;
    [SerializeField] float speed;
    [SerializeField] private GameObject centerOfMass;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] TextMeshProUGUI speedometerText; 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Moves the car forward based on vertical input
        
        if (speed < 180)
        {
            playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);
        }
        
        //Rotates the car based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f); //2.237 for Mph
        speedometerText.SetText("Speed: " + speed + " Km/h" );
    }
}
