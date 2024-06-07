using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    //var
    //assign nav mesh agent and chooses the squere in which the enemy is allowed to try and move to
    public NavMeshAgent badEnemy;
    private Transform player;
    private float squereOfMovement = 50f;

    public Material defaultMaterial;
    public Material attackMaterial;

    private Renderer rend;

    //uses sqeure of movement to disside the max and min values of the squere on the X and Z axis
    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;

    //keeps track of current position of the enemy
    private float xPosition;
    private float zPosition;
    private float yPosition;

    //set current position
    private float currentXPosition;
    private float currentZPosition;
    private float currentYPosition;

    //distance form choosen point when it can choose another random point to move to.
    public float closeEnough = 2;



    //spot player
    private bool playerSpotted = false;
    private float followTimer;
    private float stopFollowing = 10f;
    private bool noDestination = false;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        rend = GetComponent<Renderer>();
        followTimer = 30;

        //sets min and max values of the squere
        xMin = -squereOfMovement;
        xMax = squereOfMovement;
        zMin = -squereOfMovement;
        zMax = squereOfMovement;

        //starts moving randomly when spawned in
        RandomMove();

        //random spawn location
        currentXPosition = Random.Range(xMin, xMax);
        currentZPosition = Random.Range(zMin, zMax);
        transform.position = new Vector3(currentXPosition, currentYPosition, currentZPosition);
    }

    // Update is called once per frame
    void Update()
    {
        SpotPlayer();
        followTimer += Time.deltaTime;

        if (playerSpotted)
        {
            followTimer = 0;
            noDestination = true;
        } 

        if (followTimer < stopFollowing)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(transform.position, new Vector3(xPosition, yPosition, zPosition)) <= closeEnough)
        {
            RandomMove();
        }
        else if (noDestination)
        {
            RandomMove();
            noDestination = false;
        }
    }

    //move to a random position in the designated squere
    public void RandomMove()
    {
        rend.sharedMaterial = defaultMaterial;
        xPosition = Random.Range(xMin, xMax);
        zPosition = Random.Range(zMin, zMax);
        yPosition = transform.position.y;
        badEnemy.SetDestination(new Vector3(xPosition, yPosition, zPosition));
    }

    public void FollowPlayer()
    {
        rend.sharedMaterial = attackMaterial;
        badEnemy.SetDestination(player.position);
    }

    public void SpotPlayer()
    {
        Vector3 rayOrigin = transform.position;

        Vector3 rayDirection = transform.forward;

        ray = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("player"))
            {
                Debug.Log("player seen");
                playerSpotted = true;
            }
            else
            {
                playerSpotted = false;
            }
        }
    }
}
