using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //var

    // x and z speed for running and walking (and for doing so while in the air)
    [SerializeField] private float currentSpeed;
    [SerializeField] public static float speed = 2.5f;

    //jump (y speed)
    public float gravity = -30f;
    private float baseLineGravity;
    public float jumpHeight = 3f;

    //vertor 3's for movement
    private Vector3 moveDirection;
    private Vector3 moveDirection2;
    private Vector3 moveDirectionX;
    private Vector3 moveDirectionZ;
    private Vector3 velocity;

    //assigns character controller
    public CharacterController characterController;

    //start invulnerable
    public GameObject normalBody;
    public GameObject invulnerableBody;

    private float startTimer = 0;
    private float endInvulnerability = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //makes default speed walking speed and baselinegravity = to gravity
        currentSpeed = speed;
        baseLineGravity = gravity;
        characterController = GetComponent<CharacterController>();

        normalBody.SetActive(false);
        invulnerableBody.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    { 
    Invulnerable();
    } while (startTimer < endInvulnerability) ;
    }

    private void Invulnerable()
    {
        Debug.Log("hoi");
        startTimer += Time.deltaTime;
        if (startTimer >= endInvulnerability)
        {
            normalBody.SetActive(true);
            invulnerableBody.SetActive(false);
        }
    }

    private void Move()
    {
        //movement 

        //gravity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //move inputs
        float MoveZ = Input.GetAxis("Vertical");
        float MoveX = Input.GetAxis("Horizontal");

        moveDirectionZ = new Vector3(0, 0, MoveZ);
        moveDirectionX = new Vector3(MoveX, 0, 0);
        moveDirection = transform.TransformDirection(moveDirectionX + moveDirectionZ);

        if (moveDirection != Vector3.zero && characterController.isGrounded) //walk
        {
            Walk();
        }

        if (characterController.isGrounded)
        {
            //if not doing anything don't do anything
            if (moveDirection != Vector3.zero)// idle
            {
                idle();
            }
        }
        //movement speed based on current speed
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        //applies gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    //move void's
    private void Walk()
    {
        moveDirection *= speed/* * slowedCount*/;
    }
    private void idle()
    {

    }
}
