using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    //private Vector3 velocity = new Vector3(0.01f, 0f); // movement as variable
    //private Vector3 velocity = Vector3.zero;
    //private float speed = 0.001f;

    //public float playerSpeed = 0.01f; // for task 1A
    private Vector3 currentVelocity = Vector3.zero; // for task 1B
    public float maxSpeed = 5f; // for task 1B
    public float acclerationTime = 1f; // for task 1B
    public float declerationTime = 2f; // for task 1C

    /////////////////////////////////////////////////////////////// WEEK 4

    public float radius;
    public List<int> circlePoints = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

    private void Start()
    {
        //accleration = targetSpeed / timeToReachSpeed;
    }

    void Update()
    {
        //transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y); // moving x 0.01 every update
        // transform.position += Vector3.right * 0.01f; // <--- this also works
        //transform.position += velocity; // <--- this too

        //velocity = Vector3.zero;
        //transform.position += velocity.normalized * speed * Time.deltaTime; // <--- NORMALIZE TO MAKE VECTOR CONSISTENT AND TIME.DELTATIME TO KEEP FRAMERATE SAME

        //velocity += acceleration * transform.up * Time.deltaTime; // Delta time needed on both (eg. 5fps vs 500)
        //transform.position += velocity * Time.deltaTime;

        //////////////////////////  Tasks  ////////////////////////////////////////////////////////////////////////////////////

        //PlayerMovementVelocity(playerSpeed); // I tried plugging in a vector3 on accident, realized i needed a float // Task 1A

        PlayerMovementAccleration(maxSpeed, acclerationTime, declerationTime); // Task 1B 1C

    }

    //public void PlayerMovementVelocity(float movement) // player controller method task 1A
    //{
    //    if (Input.GetKey(KeyCode.UpArrow)) // forgot i needed input to getting a key. originally just did "GetKey"
    //    {
    //        Vector3 velocity = new Vector3(0f, movement); // x y           y movement = up         -movement = down                 x movement = right       -movement = left
    //        transform.position += velocity;
    //    }
    //
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        Vector3 velocity = new Vector3(0f, -movement);
    //        transform.position += velocity;
    //    }
    //
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        Vector3 velocity = new Vector3(movement, 0f);
    //        transform.position += velocity;
    //    }
    //
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        Vector3 velocity = new Vector3(-movement, 0f);
    //        transform.position += velocity;
    //    }
    //
    //}

    public void PlayerMovementAccleration(float maxSpeed, float acclerationTime, float declerationTime) // player controller method task 1B
    {
        Vector3 velocity = Vector3.zero; // i need this here to start velocity at zero

        float accleration = maxSpeed/acclerationTime; // accleration calculation
        float decleration = maxSpeed / declerationTime;
        float Xspeed = 0f;
        float Yspeed = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Yspeed = maxSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Yspeed = -maxSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Xspeed = maxSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Xspeed = -maxSpeed;
        }

        //currentVelocity.x += transform.position * accleration * Time.deltaTime;
        //transform.position += currentVelocity.x * Xspeed * accleration * Time.deltaTime;

        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, Xspeed, accleration * Time.deltaTime); // utilizing .x and .y for x and y values
        currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, Yspeed, accleration * Time.deltaTime);

        transform.position += currentVelocity * Time.deltaTime;

        //////Task 1C 

        if (Xspeed == 0f) // if i stop adding gas decelerate.
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, Xspeed, decleration * Time.deltaTime); // the same thing
        }

        if (Yspeed == 0f)
        {
            currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, Yspeed, decleration * Time.deltaTime);
        }

    }


    //////////////////////////////// WEEK 4 




    public void EnemyRadar(float radius, int circlePoints)
    {
        
    }






}
