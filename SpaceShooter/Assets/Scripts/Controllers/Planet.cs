using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float pullRadius = 10f;
    public float pullStrength = 5f;

    //public Transform player;

    public float orbitDistance = 1.5f;
    public float orbitSpeed = 50f;
    //private bool isOrbiting = false;
    //public float orbitAngle;

    public List<Transform> objectsToPullList;
    private List<bool> isOrbitingList;
    private List<float> orbitAnglesList;

    void Start()
    {
        isOrbitingList = new List<bool>(new bool[objectsToPullList.Count]);
        orbitAnglesList = new List<float>(new float[objectsToPullList.Count]);
    }

    void Update()
    {
        for (int i = 0; i < objectsToPullList.Count; i++)
        {
            if (!isOrbitingList[i])
            {
                ApplyGravitationalPull(i);
            }
            else
            {
                InOrbit(i);
            }
        }

        //if (isOrbiting == false)
        //{
        //    Debug.Log("is orbirting is false");
        //}
        //else
        //{
        //    Debug.Log("is orbiting is true");
        //}

    }

    void ApplyGravitationalPull(int index)
    {
        // looked at past lectures and work
        Transform obj = objectsToPullList[index];
        Vector3 directionToPlanet = transform.position - obj.position; // Distance between the planet and the player
        float distanceToPlanet = directionToPlanet.magnitude;
    
        // If the player is in pull radius then get pulled
        if (distanceToPlanet < pullRadius)
        {
            //directionToPlanet.Normalize(); // Normalize

            float gravitationalForce = Mathf.Lerp(pullStrength, 0f, distanceToPlanet / pullRadius); // force with respect to distance

            Vector3 pullVector = directionToPlanet * gravitationalForce * Time.deltaTime;
            obj.position += pullVector;

            if (distanceToPlanet < orbitDistance)
            {
                isOrbitingList[index] = true;

                // checking to make sure it orbits in the right spot
                Vector3 check = obj.position - transform.position;
                orbitAnglesList[index] = Mathf.Atan2(check.y, check.x);
            }

        }
        else
        {
            isOrbitingList[index] = false;
        }
    }

    void InOrbit(int index)
    {
        orbitAnglesList[index] += orbitSpeed * Time.deltaTime * Mathf.Deg2Rad;  // multiply by deg2rad

        float x = transform.position.x + Mathf.Cos(orbitAnglesList[index]) * orbitDistance;
        float y = transform.position.y + Mathf.Sin(orbitAnglesList[index]) * orbitDistance;

        objectsToPullList[index].position = new Vector3(x, y, 0f);

        // make sure it orbits in the right spot this is wrong
        //player.position = (player.position - transform.position).normalized * orbitDistance + transform.position;

        // stopping the player from constantly orbiting
        float currentDistanceToPlanet = Vector3.Distance(objectsToPullList[index].position, transform.position);
        if (currentDistanceToPlanet > orbitDistance)
        {
            isOrbitingList[index] = false;
        }

    }

}
