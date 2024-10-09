using Codice.CM.Common.Checkin.Partial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    //public float sightDistance;
    //public float visionAngle;

    public Transform targetTransform;
    public float angularSpeed;

    public float detectionRadius;
    public float detectionAngle;

    //private LineRenderer linerenderer;

    void Start()
    {

    }



    void Update()
    {
        //Convert this into an angle
        Vector3 currentFacingDirection = transform.up;
        float facingAngle = Mathf.Atan2(currentFacingDirection.y, currentFacingDirection.x) * Mathf.Rad2Deg;

        //Convert this into an angle
        Vector3 targetDirection = targetTransform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        float deltaAngle = Mathf.DeltaAngle(facingAngle, targetAngle);
        Debug.Log(deltaAngle);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.red);
        Debug.DrawLine(transform.position, targetDirection + transform.position, Color.blue);
        if (deltaAngle > 0)
        {
            transform.Rotate(0f, 0f, angularSpeed * Time.deltaTime);
        }
        else if (deltaAngle < 0)
        {
            transform.Rotate(0f, 0f, -angularSpeed * Time.deltaTime);
        }







        Vector3 lookingDirection = transform.up;
        //Switching from vector to angle in terms of the looking directoin
        float lookingAngle = Mathf.Atan2(lookingDirection.y, lookingDirection.x) * Mathf.Rad2Deg;

        //Tilting the angle to the left and right to get the limits of the field of view for the vision cone
        float leftAngle = lookingAngle + detectionAngle / 2;
        float rightAngle = lookingAngle - detectionAngle / 2;

        //Switching from angle to vector in terms of the limits of the field of view for the vision cone
        Vector3 leftVector = new Vector3(Mathf.Cos(leftAngle * Mathf.Deg2Rad), Mathf.Sin(leftAngle * Mathf.Deg2Rad));
        Vector3 rightVector = new Vector3(Mathf.Cos(rightAngle * Mathf.Deg2Rad), Mathf.Sin(rightAngle * Mathf.Deg2Rad));





        //Is the object too far to be visible
        bool targetIsCloseEnough = Vector3.Distance(transform.position, targetTransform.position) < detectionRadius;

        //Is the object in the field of view?
        bool targetIsInFOV = lookingAngle < leftAngle && lookingAngle > rightAngle;

        Color lineColour;
        if (targetIsCloseEnough && targetIsInFOV)
        {
            lineColour = Color.green;
        }
        else
        {
            lineColour = Color.red;
        }

        Debug.DrawLine(transform.position, leftVector * detectionRadius + transform.position, lineColour);
        Debug.DrawLine(transform.position, rightVector * detectionRadius + transform.position, lineColour);


    }

    void VisionCones(float SD, float VA)
    {
        // getting forward direction
        Vector3 forward = transform.forward;
        Vector3 position = transform.position;

        // left and right boundarys
        Vector3 leftBound = Quaternion.Euler(0, -VA, 0) * forward * SD;
        Vector3 rightBound = Quaternion.Euler(0, VA, 0) * forward * SD;


        //DrawArc();
    }



    void DrawArc()
    {
        Vector3 postion = transform.position;
    }

}
