using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance; // use this to create a new position when position is reached
    public float maxFloatDistance;
    //public float minFloatDistance;
    public Vector3 targetPosition;

    void Start()
    {
        NewPosition();
    }

    void Update()
    {
        MoveTo();
    }

    public void NewPosition()
    {
        //targetPosition = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), transform.position.z);

        float randomX = Random.Range(-maxFloatDistance, maxFloatDistance);
        float randomY = Random.Range(-maxFloatDistance, maxFloatDistance);

        targetPosition = transform.position + new Vector3(randomX, randomY, 0);
    }

    public void MoveTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //if (transform.position == targetPosition) // i dont even need arrival distance but i can use it anyway
        if (Vector3.Distance(transform.position, targetPosition) < arrivalDistance )
        {
            NewPosition();
        }
    }
}
