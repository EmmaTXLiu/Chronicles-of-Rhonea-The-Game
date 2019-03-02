using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveGame : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    private float rotateSpeed = 0.05f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
 
    private Camera cam;


    public float blinkdistance;
    public float blinkCD;
    float currentCD;
    RaycastHit hit;
    public ParticleSystem blinkEffect;
    private Image blinkCDImage;

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
        blinkCDImage = instance.blinkCD;
        Cursor.visible = false;
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

        //blink function
        if (currentCD <= 0f)
        {
            if (Input.GetButton("Fire3")) //if blink is off cd and player wants to blink
            {
                Blink(v, h);
            }

            else //if blink is off cd but player does not want to blink, keep currentCD at 0
            {
                currentCD = 0f;

            }
        }
        else //if blink is on CD, subtract time
        {
            currentCD -= Time.deltaTime;
            //blinkCDImage.fillAmount = currentCD;
        }

        //update blink CD UI
        blinkCDImage.fillAmount = currentCD / blinkCD;

    }


    void Blink(float v, float h)
    {
        Vector3 forwardDir = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 rightDir = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z);
        Vector3 direction = forwardDir * v + rightDir * h;

        if (playerRigidbody.SweepTest(direction, out hit, blinkdistance)) //if player will collide when blinking
        {
            blinkdistance = Vector3.Distance(hit.point, playerRigidbody.transform.position);

            //move player only to the collider, can't go through it
            playerRigidbody.MovePosition(playerRigidbody.transform.position + (blinkdistance * direction));

            //reset cd
            currentCD = blinkCD;

            //play effect
            blinkEffect.Play();
        }
        else
        {

            //move player rigidbody forward by the blinkdistance
            playerRigidbody.MovePosition(playerRigidbody.transform.position + (blinkdistance * direction));

            //reset cd
            currentCD = blinkCD;

            //play effect
            blinkEffect.Play();
        }

    }

    
        void Move(float v, float h)
    {
        //store the camera forward and right vectors
        Vector3 forwardDir = new Vector3 (cam.transform.forward.x, 0f, cam.transform.forward.z);
        Vector3 rightDir = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z);

        //if (v != 0f || h != 0f) //when moving, change player's rotation based on camera
        //{

        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forwardDir), rotateSpeed);

        //}

        if (Input.GetButton("Vertical") && Input.GetButton("Horizontal"))
        {
            movement = forwardDir * v + rightDir * h;
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            if (v != 0f || h != 0f) //when moving, change player's rotation based on camera
            {
                //always face outward
                if (v < 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-movement), rotateSpeed);

                else if (v >= 0)
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
                //always face forward
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forwardDir), rotateSpeed);

            }

        }

        else if (Input.GetButton("Horizontal"))
        {

            movement = rightDir;
            movement = movement.normalized * h * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            if (v != 0f || h != 0f) //when moving, change player's rotation based on camera
            {
                //face inward when moving left/right
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-movement), rotateSpeed);

            }
        }
    }



    void Animating(float v, float h)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = v != 0f || h != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}