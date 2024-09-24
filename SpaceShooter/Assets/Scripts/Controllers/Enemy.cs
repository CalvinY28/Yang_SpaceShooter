using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acclerationTime = 2f;
    public float declerationTime = 1f;
    public float waitTime = 3f;
    public Vector3 patrolAreaMinimum;
    public Vector3 patrolAreaMaximum;
    public Vector3 targetPosition;
    public float currentSpeed = 0f;

    public bool isMoving = false; // bool for t/f


    private void Start()
    {
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (isMoving) // if its moving move to the random location
        {
            MoveTo();
        }
    }

    public void MoveTo()
    {
        Vector3 direction = (targetPosition - transform.position).normalized; // normalize here becasue it kept going crazy in length for the vector
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    //public void EnemyMovement()
    //{
                     // everything needs to be in an IEnumerator becasue return type?
    //}

    IEnumerator Patrol()
    {
        //step 1 choose random position
        //step 2 acclerate
        //step 3 decelerate
        //step 4 wait

        while (true)
        {
            //step 1
            // random position to move to
            targetPosition = new Vector3(Random.Range(patrolAreaMinimum.x, patrolAreaMaximum.x), Random.Range(patrolAreaMinimum.y, patrolAreaMaximum.y), transform.position.z); //just put transform . position z here by itself so it dosent move

            yield return StartCoroutine(Accelerate()); // step 2
            yield return StartCoroutine(Decelerate()); // step 3
            //yield return WaitForSeconds(2); 
            yield return new WaitForSeconds(waitTime); // step 4
        }

        //yield return null; // i need this
    }

    IEnumerator Accelerate()
    {
        isMoving = true; // acclerating means moving

        while (currentSpeed < maxSpeed) // make sure enemy keeps acclerating until it reaches max speed orginially had an if statement
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, maxSpeed/acclerationTime * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Decelerate()
    {
        while (currentSpeed > 0) // the opposite basically
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, maxSpeed/declerationTime * Time.deltaTime);
            yield return null;
        }
    }

}
