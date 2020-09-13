using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource engineAudioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1.7f;

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
            Vector3 force = Vector3.up * mainThrust;
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
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        rigidBody.freezeRotation = true; // take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {
            
            // Rotating Left
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Rotating Right
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
        rigidBody.constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                print("safe zone");
                break;
            case "Fuel":
                // refuel rocket
                print("fuel");
                break;
            default:
                // kill player
                print("boom");
                break;
        }
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
     //   if (collision.relativeVelocity.magnitude > 2)
     //       audioSource.Play();
    }

}
