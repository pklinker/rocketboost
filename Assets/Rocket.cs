using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource engineAudioSource;
    [SerializeField] float rcsThrust = 125;
    [SerializeField] float mainThrust = 1.9f;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        engineAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            ApplyThrust();
            Rotate();
        }
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
        if (state != State.Alive)
        {
            return;
        }

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
            case "Finish":
                print("Player finished.");
                state = State.Transcending;
                float timeInSec = 1f;
                Invoke("LoadNextScene", timeInSec); //parameterize time
                break;
            default:
                // kill player
                state = State.Dying;
                timeInSec = 1f;
                Invoke("LoadDyingScene", timeInSec); //parameterize time
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

    private void LoadDyingScene()
    {
        SceneManager.LoadScene(0);
        engineAudioSource.Stop();
        state = State.Alive;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
        state = State.Alive;
    }
}
