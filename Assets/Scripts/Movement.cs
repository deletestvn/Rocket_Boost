using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 originalPos;
    Quaternion originalRot;
    [SerializeField] float thrustSpeed = 1000.0f;
    [SerializeField] float rotateSpeed = 60.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = gameObject.transform.position;
        originalRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        resetObject();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Boost!!!");
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
        }   
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    void resetObject()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.transform.position = originalPos;
            gameObject.transform.rotation = originalRot;
            rb.velocity = Vector3.zero;
        }
    }
}

