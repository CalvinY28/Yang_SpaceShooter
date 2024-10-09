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

    void Start()
    {
        
    }

    void Update()
    {
        ApplyGravitationalPull();
    }

    void ApplyGravitationalPull()
    {
        // looked at past lectures and work
        Vector3 directionToPlanet = transform.position - player.position; // Distance between the planet and the player
        float distanceToPlanet = directionToPlanet.magnitude;
    
        // If the player is in pull radius then get pulled
        if (distanceToPlanet < pullRadius)
        {
            directionToPlanet.Normalize(); // Normalize

            float gravitationalForce = Mathf.Lerp(pullStrength, 0f, distanceToPlanet / pullRadius); // force with respect to distance

            Vector3 pullVector = directionToPlanet * gravitationalForce * Time.deltaTime;
            player.position += pullVector;

            if (distanceToPlanet < orbitDistance)
            {
                isOrbiting = true;
            }
        }
    }

    void InOrbit()
    {

    }

}
