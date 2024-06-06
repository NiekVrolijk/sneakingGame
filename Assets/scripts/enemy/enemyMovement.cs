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
    private float squereOfMovement = 50f;

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

    // Start is called before the first frame update
    void Start()
    {
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
        //if close enough to the previous choosen point choose another
        if (Vector3.Distance(transform.position, new Vector3(xPosition, yPosition, zPosition)) <= closeEnough)
        {
            RandomMove();
        }
    }

    //move to a random position in the designated squere
    public void RandomMove()
    {
        xPosition = Random.Range(xMin, xMax);
        zPosition = Random.Range(zMin, zMax);
        yPosition = transform.position.y;
        badEnemy.SetDestination(new Vector3(xPosition, yPosition, zPosition));
    }
}
