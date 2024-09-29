using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    public float angle; // task 3 i can make this public to check my angles... it seems like the angle keeps going higher and higher. Might be able to clamp it and loop but im lazy.
    public float speed; // task 3
    public float radius; // task 3

    void Start()
    {
        
    }

    void Update()
    {
        OrbitalMotion(radius, speed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        float xPos = target.position.x + Mathf.Cos(angle) * radius; // left and right
        float yPos = target.position.y + Mathf.Sin(angle) * radius; // up and down

        angle += speed * Time.deltaTime;

        transform.position = new Vector3(xPos, yPos, 0f);
    }

}
