using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;
    public Color redColor = Color.red;
    public Color blueColer = Color.blue;

    void Start()
    {
        //Vector3 redVector = AngletoVector(redAngle);
        //Vector3 blueVector = AngletoVector(blueAngle);

        //Debug.Log(redVector);
        //Debug.Log(blueVector);

        //Draw(redVector, redColor);
        //Draw(blueVector, blueColer);
    }


    void Update()
    {
        Vector3 redVector = AngletoVector(redAngle);
        Vector3 blueVector = AngletoVector(blueAngle);

        //Debug.Log(redVector);
        //Debug.Log(blueVector);

        Draw(redVector, redColor);
        Draw(blueVector, blueColer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dot = Vector3.Dot(redVector, blueVector);
            Debug.Log("dot product is = " + dot);
            Debug.Log("red vecotr is " + redVector);
            Debug.Log("blue vector is " + blueVector);
        }

    }

    Vector3 AngletoVector(float angle)
    {
        float toRadians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(toRadians);
        float y = Mathf.Sin(toRadians);

        return new Vector3(x, y);
    }

    void Draw(Vector3 vector, Color color)
    {
        Debug.DrawLine(Vector3.zero, new Vector3(vector.x, vector.y, 0), color);
    }

}
