using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    private int currentStar = 0;
    private float elapsedTime = 0f;

    private void Update()
    {
        DrawConstellation();
    }

    public void DrawConstellation()
    {
        Vector3 startPos = starTransforms[currentStar].position; // start position getting position vector of star 0 in the list
        //Vector3 endPos = starTransforms[(currentStar + 1)].position;
        Vector3 endPos = starTransforms[(currentStar + 1) % starTransforms.Count].position; // end position getting postition of next star and wrap around back to the first

        elapsedTime += Time.deltaTime; // so u can change the speed with framerate
        float t = Mathf.Clamp01(elapsedTime/drawingTime); // mathclamp01 learned from peers it basically makes sure the variable is between 0 and 1

        //Vector3 currentLinePosition = Vector3.Lerp(startPos, endPos)
        Vector3 currentLinePosition = Vector3.Lerp(startPos, endPos, t); // using the star positions to interpolate with respect to t
        Debug.DrawLine(startPos, currentLinePosition, Color.white);

        if (t == 1f) // 1 means finished drawing 0 means no drawing
        {
            elapsedTime = 0f;
            currentStar = (currentStar + 1) % starTransforms.Count;
        }
    }

}
