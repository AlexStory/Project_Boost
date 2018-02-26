using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody _rigidBody;
    AudioSource _audioSource;
    private State state;
    [SerializeField] float speed = 250f;
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float timeBetweenLevels = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip rocketExplosion;
    [SerializeField] AudioClip introSound;

    [SerializeField] ParticleSystem engineParticleSystem;
    [SerializeField] ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem successParticleSystem;

	// Use this for initialization
	private void Start ()
	{
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
	    state = State.Alive;
	}
	
	// Update is called once per frame
	private void Update ()
	{
        if (state != State.Alive) return;

	    HandleThrust();
        HandleRotation();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccess();
                break;
            default:
                StartDeath();
                break;
        }
    }

    private void StartDeath()
    {
        state = state = State.Dying;
        _audioSource.Stop();
        _audioSource.PlayOneShot(rocketExplosion);
        explosionParticleSystem.Play();
        //gameObject.SetActive(false);
        Invoke("ResetLevel", timeBetweenLevels);
    }

    private void StartSuccess()
    {
        state = State.Transcending;
        _audioSource.Stop();
        _audioSource.PlayOneShot(introSound);
        successParticleSystem.Play();
        Invoke("LoadNextScene", timeBetweenLevels);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            ApplyThrust();
        }
        else
        {
            _audioSource.Pause();
            engineParticleSystem.Stop();
        }
       
    }

    private void ApplyThrust()
    {
        _rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngine);
        }

        engineParticleSystem.Play();
    }

    private void HandleRotation()
    {
        _rigidBody.freezeRotation = true;

        var rotationSpeed = speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        _rigidBody.freezeRotation = false;
    }


    enum State { Alive, Dying, Transcending }
}
