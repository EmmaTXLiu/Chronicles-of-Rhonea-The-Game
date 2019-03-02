using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveStory : MonoBehaviour
{
    private float speed = 4f;            // The speed that the player will move at.
    private float rotateSpeed = 0.2f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

    private Camera cam;


    public Animator zanim;


    private float smooth;

    RefManager instance;

    void Start()
    {
        instance = RefManager.refManager;

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        smooth = 15f;

        cam = instance.cam;

    }


    void FixedUpdate()
    {
        // Store the input axes.
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        //move function
        Move(v, h);

        //animation function
        Animating(v, h);

        if (Input.GetButton("V"))
            speed = 10f;

        else speed = 4f;


    }



    void Move(float v, float h)
    {
        //store the camera forward and right vectors
        Vector3 forwardDir = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 rightDir = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z);


        if (Input.GetButton("Vertical") && Input.GetButton("Horizontal"))
        {
            movement = forwardDir * v + rightDir * h;
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            if (v != 0f || h != 0f) //when moving, change player's rotation based on camera
            {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotateSpeed);
            }


        }

        else if (Input.GetButton("Vertical"))
        {

            movement = forwardDir;
            movement = movement.normalized * v * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);


            if (v != 0f || h != 0f) //when moving, change player's rotation based on camera
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotateSpeed);
            }

        }

        else if (Input.GetButton("Horizontal"))
        {

            movement = rightDir;
            movement = movement.normalized * h * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            if (v != 0f || h != 0f) //when moving, change player's rotation based on camera
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotateSpeed);
            }

        }

    }



    void Animating(float v, float h)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = v != 0f || h != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
        zanim.SetBool("IsWalking", walking);
    }
}