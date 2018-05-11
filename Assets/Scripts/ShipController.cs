using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {
    public float paramDivider = 30f;

    private AudioSource flyingSound;
    private Rigidbody rb;

    public Color trailColor;
    public Color boostingTrailColor;
    public Color ultraBoostingTrailColor;

    private float accelerationAxis, turningAxis, tiltAxis, spinAxis;
    private float accelerationAxisSmooth, turningAxisSmooth, tiltAxisSmooth, spinAxisSmooth;

    private bool isBoosting = false;
    private float boostMultiplier;
    public float boostAmount = 200f;
    private float boostBuildup = 1f;

    private float t = 16.66f;

    private int consecutiveCoins = 0;
    private int lastConsecutiveCoins = 0;
    private float consecutiveCoinsTimer = 0f;

    private TrailRenderer[] trails;

    public float inactivityTime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        flyingSound = GetComponent<AudioSource>();
        trails = GetComponentsInChildren<TrailRenderer>(true);
    }

	// Update is called once per frame
	void Update () {
        UpdateAxisValues();
        UpdateShipMovement();
        UpdateFlyingSound();
        UpdateConsecutiveCoins();
    }
    
    void UpdateAxisValues()
    {
        if (Application.isMobilePlatform)
        {
            accelerationAxis = Mathf.Clamp(Input.acceleration.y / 0.2f, -1f, 1f);
            turningAxis = CrossPlatformInputManager.GetAxis("Horizontal");
            tiltAxis = CrossPlatformInputManager.GetAxis("Tilt");
            spinAxis = CrossPlatformInputManager.GetAxis("Spin");
        }
        else
        {
            accelerationAxis = Input.GetAxis("Vertical");
            turningAxis = Input.GetAxis("Horizontal");
            tiltAxis = Input.GetAxis("Tilt");
            spinAxis = Input.GetAxis("Spin");

            if (!isBoosting && Input.GetButtonDown("Boost"))
            {
                isBoosting = true;
            }
        }
    }

    void UpdateShipMovement()
    {
        if (inactivityTime > 0f) inactivityTime -= Time.deltaTime;

        accelerationAxisSmooth = Mathf.Lerp(accelerationAxisSmooth, accelerationAxis, t * Time.deltaTime);
        turningAxisSmooth = Mathf.Lerp(turningAxisSmooth, turningAxis, t * Time.deltaTime);
        tiltAxisSmooth = Mathf.Lerp(tiltAxisSmooth, tiltAxis, t * Time.deltaTime);
        spinAxisSmooth = Mathf.Lerp(spinAxisSmooth, spinAxis, t * Time.deltaTime);

        if (inactivityTime <= 0f) rb.AddRelativeForce(new Vector3(0, 0, accelerationAxisSmooth * (8333.33f * boostMultiplier * Time.deltaTime)));
        rb.AddRelativeTorque(new Vector3(tiltAxisSmooth * (50f * Time.deltaTime / paramDivider),
            turningAxisSmooth * (100f * Time.deltaTime / paramDivider), spinAxisSmooth * (33.33f * Time.deltaTime / paramDivider)));

        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 8.33f * Time.deltaTime);
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, 8.33f * Time.deltaTime);

        if (isBoosting)
        {
            if (boostAmount > 10f)
            {
                boostAmount -= Time.deltaTime * (10f * rb.velocity.sqrMagnitude);
                //bool ultraBoost = boostAmount > 100f;

                //if (ultraBoost) boostMultiplier = Mathf.Lerp(boostMultiplier, 3f, boostBuildup * Time.deltaTime);
                /*else*/ boostMultiplier = Mathf.Lerp(boostMultiplier, 1.5f, boostBuildup * Time.deltaTime);

                for (int i = 0; i < trails.Length; i++)
                {
                    /*if (ultraBoost)
                    {
                        trails[i].startColor = Color.Lerp(trails[i].startColor, ultraBoostingTrailColor, boostBuildup * Time.deltaTime);
                        trails[i].endColor = Color.Lerp(trails[i].startColor, ultraBoostingTrailColor, boostBuildup * Time.deltaTime);
                        trails[i].widthMultiplier = Mathf.Lerp(trails[i].widthMultiplier, 0.03f, boostBuildup * Time.deltaTime);
                    }
                    else
                    {*/
                        trails[i].startColor = Color.Lerp(trails[i].startColor, boostingTrailColor, boostBuildup * Time.deltaTime);
                        trails[i].endColor = Color.Lerp(trails[i].startColor, boostingTrailColor, boostBuildup * Time.deltaTime);
                        trails[i].widthMultiplier = Mathf.Lerp(trails[i].widthMultiplier, 0.02f, boostBuildup * Time.deltaTime);
                    //}
                }
            }
            else
            {
                isBoosting = false;
            }
        }
        else
        {
            boostMultiplier = Mathf.Lerp(boostMultiplier, 1f, boostBuildup * Time.deltaTime);
            for (int i = 0; i < trails.Length; i++)
            {
                trails[i].startColor = Color.Lerp(trails[i].startColor, trailColor, boostBuildup * Time.deltaTime);
                trails[i].endColor = Color.Lerp(trails[i].startColor, trailColor, boostBuildup * Time.deltaTime);
                trails[i].widthMultiplier = Mathf.Lerp(trails[i].widthMultiplier, 0.01f, boostBuildup * Time.deltaTime);
            }
        }

    }

    void UpdateFlyingSound()
    {
        flyingSound.volume = rb.velocity.sqrMagnitude;
        flyingSound.pitch = Mathf.Lerp(flyingSound.pitch, 1f + rb.velocity.sqrMagnitude + rb.angularVelocity.sqrMagnitude * 0.02f, 10f * Time.deltaTime);
    }

    void UpdateConsecutiveCoins()
    {
        if (consecutiveCoins == lastConsecutiveCoins)
        {
            consecutiveCoinsTimer += Time.deltaTime;

            if (consecutiveCoinsTimer > 2f) consecutiveCoins = 0;
        }
        else
        {
            consecutiveCoinsTimer = 0f;
        }

        lastConsecutiveCoins = consecutiveCoins;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            consecutiveCoins++;
            other.gameObject.GetComponent<CoinBehavior>().Collect(consecutiveCoins);
        } else if (other.CompareTag("RaceCheckpoint"))
        {
            other.gameObject.GetComponent<RaceCheckpoint>().Score(other.material.name);
            boostAmount += 5f;
        } else if (other.CompareTag("SprintCheckpoint"))
        {
            other.gameObject.GetComponent<SprintCheckpoint>().Score(other.material.name);
            boostAmount += 10f;
        }
    }
}
