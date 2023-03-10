using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocillate : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }  // prevents NaN error
        float cycles = Time.time / period;    // Continually growing over time
        
        const float tau = Mathf.PI * 2;   // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;   // Recalculated to go from 0 to 1
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
