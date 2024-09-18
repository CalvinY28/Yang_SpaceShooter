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

    public float playerSpeed = 0.01f; // for task 1
    //public float timeToReachSpeed = 3f;
    //public float targetSpeed = 0.01f;
    //public float accleration;

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

        PlayerMovement(playerSpeed); // I tried plugging in a vector3 on accident, realized i needed a float

    }

    public void PlayerMovement(float movement) // player controller method
    {
        if (Input.GetKey(KeyCode.UpArrow)) // forgot i needed input to getting a key. originally just did "GetKey"
        {
            Vector3 velocity = new Vector3(0f, movement); // x y           y movement = up         -movement = down                 x movement = right       -movement = left
            //Vector3 velocity = Vector3.zero;
            //velocity += accleration * transform.up * Time.deltaTime;
            //accleration = targetSpeed / timeToReachSpeed;
            transform.position += velocity; // broken for now **
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 velocity = new Vector3(0f, -movement);
            transform.position += velocity;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 velocity = new Vector3(movement, 0f);
            transform.position += velocity;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 velocity = new Vector3(-movement, 0f);
            transform.position += velocity;
        }

    }

}
