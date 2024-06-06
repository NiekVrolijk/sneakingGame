using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //i was working on making the player slowed if they got hit but didn't finish it. might add it later for fun tho

    //var

    // x and z speed for running and walking (and for doing so while in the air)
    [SerializeField] private float currentSpeed;
    [SerializeField] public static float walkingSpeed = 3f;
    [SerializeField] public static float runningSpeed = 5f;
    //[SerializeField] public static float AirWSpeed = 2.1f;
    //[SerializeField] public static float AirRSpeed = 3.5f;
    [SerializeField] public static float AirWSpeed = 3f;
    [SerializeField] public static float AirRSpeed = 5f;

    //jump (y speed)
    public float gravity = -30f;
    private float baseLineGravity;
    public float jumpHeight = 3f;

    //double jump
    private int doubleJump;
    private int doubleJumpAmount = 1;
    private float jumpTimer;
    private float canJump = 0.4f;

    //vertor 3's for movement
    private Vector3 moveDirection;
    private Vector3 moveDirection2;
    private Vector3 moveDirectionX;
    private Vector3 moveDirectionZ;
    private Vector3 velocity;

    //assigns character controller
    public CharacterController characterController;

    //player health
    public static float playerHealth = 100f;
    private GameObject HP;
    private TMPro.TMP_Text currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        //makes default speed walking speed and baselinegravity = to gravity
        currentSpeed = walkingSpeed;
        baseLineGravity = gravity;
        characterController = GetComponent<CharacterController>();

        //health
        HP = GameObject.Find("health");
        currentHealth = HP.GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //makes player health not shoot into -"_" before death and sends player to the death scene/screan "the death screne"
        //playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        //if (playerHealth <= 0)
        //{
        //    SceneManager.LoadScene("deathScrene");
        //}

        Move();
        //reset doublejump
        if (characterController.isGrounded)
        {
            doubleJump = doubleJumpAmount;
        }
        //run double jump timer
        jumpTimer += Time.deltaTime;

        //slowing effect
        //timer += Time.deltaTime;
    }

    private void LateUpdate()
    {
        //update health on screan
        //currentHealth.text = playerHealth.ToString() + "/100";
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

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded) //walk
        {
            Walk();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded) //run
        {
            Run();
        }

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && !characterController.isGrounded) //walk while in air
        {
            AirWalk();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !characterController.isGrounded) //run while in air
        {
            AirRun();
        }

        if (Input.GetKey(KeyCode.Space) && doubleJump > 0 && canJump <= jumpTimer && !characterController.isGrounded) //jump in air
        {
            jump();
            //double jump
            doubleJump -= 1;
            jumpTimer = 0f;
        }

        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) && canJump <= jumpTimer) //normal jump
            {
                jump();
                jumpTimer = 0f;
            }

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
        moveDirection *= walkingSpeed/* * slowedCount*/;
    }
    private void Run()
    {
        moveDirection *= runningSpeed/* * slowedCount*/;
    }

    private void AirWalk()
    {
        moveDirection *= AirWSpeed/* * slowedCount*/;
    }
    private void AirRun()
    {
        moveDirection *= AirRSpeed/* * slowedCount*/;
    }

    public void jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
    private void idle()
    {


    }

    ////if you hit a wall mid air reset double jump
    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //    if (hit.gameObject.tag == "wallJumpObstacle" && !characterController.isGrounded)
    //    {
    //        doubleJump = doubleJumpAmount;
    //    }

    //}
}
