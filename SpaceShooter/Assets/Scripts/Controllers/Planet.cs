using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float pullRadius = 10f;
    public float pullStrength = 5f;

    public Transform player;

    public float orbitDistance = 1.5f;
    public float orbitSpeed = 50f;
    private bool isOrbiting = false;
    public float orbitAngle;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isOrbiting)
        {
            ApplyGravitationalPull();
        }
        else
        {
            InOrbit();
        }

        if (isOrbiting == false)
        {
            Debug.Log("is orbirting is false");
        }
        else
        {
            Debug.Log("is orbiting is true");
        }

    }

    void ApplyGravitationalPull()
    {
        // looked at past lectures and work
        Vector3 directionToPlanet = transform.position - player.position; // Distance between the planet and the player
        float distanceToPlanet = directionToPlanet.magnitude;
    
        // If the player is in pull radius then get pulled
        if (distanceToPlanet < pullRadius)
        {
            //directionToPlanet.Normalize(); // Normalize

            float gravitationalForce = Mathf.Lerp(pullStrength, 0f, distanceToPlanet / pullRadius); // force with respect to distance

            Vector3 pullVector = directionToPlanet * gravitationalForce * Time.deltaTime;
            player.position += pullVector;

            if (distanceToPlanet < orbitDistance)
            {
                isOrbiting = true;

                // checking to make sure it orbits in the right spot
                Vector3 check = player.position - transform.position;
                orbitAngle = Mathf.Atan2(check.y, check.x);
            }

        }
        else
        {
            isOrbiting = false;
        }
    }

    void InOrbit()
    {
        orbitAngle += orbitSpeed * Time.deltaTime * Mathf.Deg2Rad;  // multiply by deg2rad

        float x = transform.position.x + Mathf.Cos(orbitAngle) * orbitDistance;
        float y = transform.position.y + Mathf.Sin(orbitAngle) * orbitDistance;

        player.position = new Vector3(x, y, 0f);

        // make sure it orbits in the right spot this is wrong
        //player.position = (player.position - transform.position).normalized * orbitDistance + transform.position;

        // stopping the player from constantly orbiting
        float currentDistanceToPlanet = Vector3.Distance(player.position, transform.position);
        if (currentDistanceToPlanet > orbitDistance)
        {
            isOrbiting = false;
        }

    }

}
