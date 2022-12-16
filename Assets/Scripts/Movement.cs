using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust(); 
        }
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotate();
        }
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  //freezing default physics system to manual control.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //unfreezing default physics system to take control.
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void StopRotate()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

}
