using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);

    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    //[Range(0,1)] [SerializeField]   remove from inspector later
    float movementFactor; // 0 for no movement, 1 for full movement
    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update

    void Start()
    {
        startingPosition = transform.position;
        if (period <= Mathf.Epsilon)
        {
            period = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // protect against period = 0;
 
        float cycles = Time.time / period;

        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
