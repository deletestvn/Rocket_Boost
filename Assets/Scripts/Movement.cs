using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 1000.0f;
    [SerializeField] float rotateSpeed = 60.0f;
    [SerializeField] AudioClip engineSFX;

    [SerializeField] ParticleSystem engineVFX;
    [SerializeField] ParticleSystem leftThruster1VFX;
    [SerializeField] ParticleSystem leftThruster2VFX;
    [SerializeField] ParticleSystem rightThruster1VFX;
    [SerializeField] ParticleSystem rightThruster2VFX;

    Rigidbody rb;
    AudioSource audioSource;
    Vector3 originalPos;
    Quaternion originalRot;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!audioSource.isPlaying) audioSource.PlayOneShot(engineSFX);
            if (!engineVFX.isPlaying) engineVFX.Play();
        }
        else
        {
            audioSource.Stop();
            engineVFX.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
            if(!rightThruster1VFX.isPlaying) rightThruster1VFX.Play();
            if(!rightThruster2VFX.isPlaying) rightThruster2VFX.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
            if(!leftThruster1VFX.isPlaying) leftThruster1VFX.Play();
            if(!leftThruster2VFX.isPlaying) leftThruster2VFX.Play();
        }
        else
        {
            rightThruster1VFX.Stop();
            rightThruster2VFX.Stop();
            leftThruster1VFX.Stop();
            leftThruster2VFX.Stop();
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

