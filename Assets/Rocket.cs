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
        ApplyThrust();
        Rotate();
    }

    /**
     * Apply thrust and play effects/sounds.
     */
    private void ApplyThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 force = Vector3.up;
            // add thrust
            rigidBody.AddRelativeForce(force);
            if (!engineAudioSource.isPlaying)
                engineAudioSource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // turn off engine noise when thrusters stop
            engineAudioSource.Stop();
        }
       
    }

    /**
     * Rotate the rocket based on input.
     */
    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
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
        rigidBody.freezeRotation = false; // resume physics control of rotation
        rigidBody.constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }


}
