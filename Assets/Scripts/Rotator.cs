using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 rotationRate = new Vector3();

    Quaternion startingRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        startingRotation = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
 
        transform.Rotate(rotationRate, Space.Self);
    }
}
