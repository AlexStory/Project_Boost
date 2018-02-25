﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;
    public float speed = 100;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        } else
        {
            audioSource.Pause();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}