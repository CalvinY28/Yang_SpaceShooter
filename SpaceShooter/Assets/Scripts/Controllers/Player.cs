﻿using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public float radius = 3f; // for task 1
    public int circlePoints = 8; // for task 1

    public float radius2; // for task 2
    public int numberOfPowerups; // for task 2
    public GameObject powerupPrefab; // for task 2

    ///////////////////////////////////////////////////////////// FOR SPACE SHOOTER ASSINGMENT

    public float pickupRadius = 1f;
    //public GameObject[] powerupPrefabs; dont need
    public GameObject astroidPrefab;
    public bool isHyperSpeedActive = false;

    private void Start()
    {
        //accleration = targetSpeed / timeToReachSpeed;
        SpawnPowerups(radius2, numberOfPowerups);
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

        /////////////////////////////// WEEK 4 ////////////////////////////////

        EnemyRadar(radius, circlePoints); // task 1

        //SpawnPowerups(radius2, numberOfPowerups); // task 2

        ////////////////////////////////////////////////////////////////////// FOR SPACE SHOOTER ASSINGMENT
        
        PickupPowerups();

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

        float accleration = maxSpeed / acclerationTime; // accleration calculation
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

        for (int i = 0; i < circlePoints; i++) // so i can have multiple points ( i only need 8)
        {
            //Debug.Log(Mathf.PI * 2); just checking pi
            // i can also just plug in 3.1415...f or 6. whatever
            float anlge = i * Mathf.PI * 2f / circlePoints; // must multiply by 2 becasue 2pi radians in a full circle

            Vector3 pointOnCricle = new Vector3(Mathf.Cos(anlge), Mathf.Sin(anlge), 0) * radius + transform.position; // from class

            //Vector3 nextPoint = new Vector3 (Mathf.Cos(anlge + circlePoints ), Mathf.Sin(anlge + circlePoints), 0) * radius + transform.position; // plus a little more
            Vector3 nextPoint = new Vector3(Mathf.Cos(anlge + Mathf.PI * 2f / circlePoints), Mathf.Sin(anlge + Mathf.PI * 2f / circlePoints), 0) * radius + transform.position;
            // i honeslty just kept plugging values in here cus the math hurt head. I copied the equation i used for angle.
            Debug.DrawLine(pointOnCricle, nextPoint, Color.green, 0.1f);

        }

        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
        // for color changing just use the same code but in an if statement for distance and circle radius
        if (distanceToEnemy <= radius)
        {
            for (int i = 0; i < circlePoints; i++)
            {
                float anlge = i * Mathf.PI * 2f / circlePoints;

                Vector3 pointOnCricle = new Vector3(Mathf.Cos(anlge), Mathf.Sin(anlge), 0) * radius + transform.position;

                Vector3 nextPoint = new Vector3(Mathf.Cos(anlge + Mathf.PI * 2f / circlePoints), Mathf.Sin(anlge + Mathf.PI * 2f / circlePoints), 0) * radius + transform.position;

                Debug.DrawLine(pointOnCricle, nextPoint, Color.red, 0.1f);
            }
        }

    }

    public void SpawnPowerups(float radius2, int numberOfPowerups)
    {

        //for (int i = 0; i < numberOfPowerups; i++)
        //{
        //    float angle = i * Mathf.PI * 2f / numberOfPowerups;

        //    Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius; // basically same as task 1

        //    spawnPosition += transform.position;

        //    Instantiate(powerupPrefab, spawnPosition, Quaternion.identity); // quteitninon identity
        //}

        StartCoroutine(SpawnPowerupTimer(radius2, numberOfPowerups));

    }

    private IEnumerator SpawnPowerupTimer(float radius2, int numberOfPowerups)
    {

        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfPowerups;
            Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            spawnPosition += transform.position;

            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity); // quteitninon identity
            //yield return new WaitForSeconds(5f);

            //int randomIndex = Random.Range(0, powerupPrefabs.Length);
            //GameObject selectedPowerup = powerupPrefabs[randomIndex]; // do not need
            //Instantiate(selectedPowerup, spawnPosition, Quaternion.identity);

            yield return null;
        }
        //yield return new WaitForSeconds(5f);
    }

    public void PickupPowerups()
    {
        // had to search for how to use tags
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup"); // find powers ups and loop
        foreach (GameObject powerup in powerups)
        {
            // distance between player and power up
            float distanceToPowerup = Vector3.Distance(transform.position, powerup.transform.position);

            if (distanceToPowerup <= pickupRadius)
            {
                Destroy(powerup);
                int randomPowerup = Random.Range(0, 4);
                switch (randomPowerup)
                {
                    case 0:
                        HyperSpeed();
                        break;
                    case 1:
                        Magnet();
                        break;
                    case 2:
                        Shield();
                        break;
                    case 3:
                        Homeing();
                        break;
                }
            }
        }
    }

    public void HyperSpeed()
    {
        if (isHyperSpeedActive)
        {
            Debug.Log("Hyperspeed is already active!");
            return;
        }

        Debug.Log("hyperspeed");
        StartCoroutine(speedTimer()); // hyper speed stacks may or may not fix

        IEnumerator speedTimer()
        {
            isHyperSpeedActive = true;
            float originalMaxSpeed = maxSpeed;
            maxSpeed *= 10f;

            yield return new WaitForSeconds(5f);

            maxSpeed = originalMaxSpeed;

            isHyperSpeedActive = false;
        }
    }


    public void Magnet()
    {
        Debug.Log("magnet");
        StartCoroutine(magnetize());

        IEnumerator magnetize()
        {
            GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
            float magnetSpeed = 3f;

            while (true) // smooth pull no blinking or teleporting
            {
                bool allPowerupsReachedPlayer = true;

                foreach (GameObject powerup in powerups)
                {
                    if (powerup == null)
                    {
                        continue; // continue the code even if power up is gone
                    }

                    Vector3 directionToPlayer = (transform.position - powerup.transform.position).normalized;
                    float distanceToPlayer = Vector3.Distance(transform.position, powerup.transform.position);
                    powerup.transform.position += directionToPlayer * magnetSpeed * Time.deltaTime;

                    if (distanceToPlayer > 0.1f)
                    {
                        allPowerupsReachedPlayer = false;
                    }
                }

                if (allPowerupsReachedPlayer)
                {
                    break; // stop loop
                }

                yield return null;
            }
        }
    }

    public void Shield()
    {
        float orbitRadius = 2f;
        float orbitSpeed = 50f;
        //float orbitAngle = 0f;
        int numberOfAsteroids = 1; // could not get this to work so I just set this to one to revert back to where I was orginally
        float destroyRadius = 1f;

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfAsteroids; // Angle for each asteroid
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * orbitRadius;
            StartCoroutine(OrbitAsteroidAroundPlayer(spawnPosition, angle, orbitRadius, orbitSpeed));
        }

        Debug.Log("shield");

        IEnumerator OrbitAsteroidAroundPlayer(Vector3 initialPosition, float initialAngle, float orbitRadius, float orbitSpeed)
        {

            float orbitAngle = initialAngle;

            //Vector3 spawnPosition = transform.position + new Vector3(orbitRadius, 0f, 0f);
            //Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(initialAngle), Mathf.Sin(initialAngle), 0f) * orbitRadius;
            GameObject shieldAsteroid = Instantiate(astroidPrefab, initialPosition, Quaternion.identity);

            // keep orbiting the asteroid while running
            while (true)
            {
                orbitAngle += orbitSpeed * Time.deltaTime;

                float x = transform.position.x + Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * orbitRadius;
                float y = transform.position.y + Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * orbitRadius;

                shieldAsteroid.transform.position = new Vector3(x, y, 0f);

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(shieldAsteroid.transform.position, enemy.transform.position);
                    if (distanceToEnemy <= destroyRadius)
                    {

                        Vector3 pushDirection = (enemy.transform.position - shieldAsteroid.transform.position);
                        float pushForce = 5f;

                        enemy.transform.position += pushDirection * pushForce;

                        Destroy(shieldAsteroid);
                        Debug.Log("SHEILD DOWN");
                        yield break;
                    }
                }

                yield return null;
            }
        }
    }

    public void Homeing()
    {
        Debug.Log("homing");
    }

}
