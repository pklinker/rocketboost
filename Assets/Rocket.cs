using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource engineAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        engineAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // turn off engine noise when thrusters stop
            engineAudioSource.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Rotating Left
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Rotating Right
            transform.Rotate(-Vector3.forward);
        }
    }

    /**
     * Apply thrust and play effects/sounds.
     */
    private void ApplyThrust()
    {
        Vector3 force = Vector3.up;
        // add thrust
        rigidBody.AddRelativeForce(force);
        if (!engineAudioSource.isPlaying)
            engineAudioSource.Play();
    }
}
