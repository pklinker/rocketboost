using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Moves an object the distance indicated in the movement vector and then resets
 * it back to the starting position.
 */
public class Repeater : MonoBehaviour
{
    [SerializeField] Vector3 endingVector = new Vector3(0f, 0f, 0f);
    Vector3 endingPostion = new Vector3(0f, 0f, 0f);

    [SerializeField] float speed = 2f;

    Vector3 startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        endingPostion = startingPosition + endingVector;
        if (speed <= Mathf.Epsilon)
        {
            speed = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float amountMoved = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, endingPostion, amountMoved);
        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, endingPostion) < 0.001f)
        {
            // Move to starting location.
            transform.position = startingPosition;
        }
    }
}
