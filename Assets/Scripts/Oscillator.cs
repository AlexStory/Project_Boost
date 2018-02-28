using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] private Vector3 movementVector = new Vector3(10f, 10f, 10f);

    [Range(0,1)]
    [SerializeField]
    private float movementFactor;

    private Vector3 _startingPosition;
    [SerializeField] private float period = 2f;

	// Use this for initialization
	void Start ()
	{
	    _startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (period <= Mathf.Epsilon) return;
	    Oscillate();
	}

    private void Oscillate()
    {
        float gameCycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(gameCycles * tau);

        movementFactor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = _startingPosition + offset;
    }
}
