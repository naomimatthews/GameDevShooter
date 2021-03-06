using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EunhaAbilities : MonoBehaviour
{
    Rigidbody rb;

    public GameObject dashEffect;
    public GameObject player;
    public Transform target;

    PlayerMovement playerMove;
    Guns gunScript;
    MeterButton meterButton;

    [SerializeField] GameObject playerCam;


    public MeterScript progressMeter;
    public WeaponSelection weaponSelection;

    // audio.
    public AudioSource audioSource;
    [SerializeField] public AudioClip AudioQ;
    [SerializeField] public AudioClip AudioE;
    [SerializeField] public AudioClip AudioUltimate;
    [SerializeField] public AudioClip AudioFire;

    // (Q) dash ability.
    protected float QabilityTimer;

    [SerializeField] protected float Qcooldown;
    [SerializeField] protected float Qduration;
    [SerializeField] private int boostPercentage;

    private float boostAsPercent;

    // (E) speed boost ability.
    protected float EabilityTimer;

    [SerializeField] protected float Ecooldown;
    [SerializeField] protected float Eduration;
    [SerializeField] private int speedBoostPercentage;

    private float speedBoostAsPercent;

    // (X) ultimate ability.
    bool ultActive;
    public bool ultReady;

    [SerializeField] Transform attackPoint;
    [SerializeField] Transform cam;
    [SerializeField] GameObject objectToThrow;

    public int totalThrows;
    public float throwCooldown;

    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;
    

    private void Awake()
    {
        meterButton = GameObject.Find("UltimateMeter").GetComponent<MeterButton>();
        gunScript = GameObject.Find("Guns").GetComponent<Guns>();
        weaponSelection = GameObject.Find("Guns").GetComponent<WeaponSelection>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        playerMove = GetComponent<PlayerMovement>();

        boostAsPercent = (100 + boostPercentage) / 100;

        readyToThrow = true;
    }

    void Update()
    {
        if (Time.time >= QabilityTimer && Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.clip = AudioQ;
            audioSource.Play();

            Dash();
            QabilityTimer = Time.time + Qcooldown;
        }

        if (Time.time >= EabilityTimer && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.clip = AudioE;
            audioSource.Play();

            SpeedBoost();
            EabilityTimer = Time.time + Ecooldown;
        }

        if (meterButton.currentProgress >= meterButton.maxProgress)
        {
            ultReady = true;

           // Debug.Log("ult ready");

            if (Input.GetKeyDown(KeyCode.X))
            {
                audioSource.clip = AudioUltimate;
                audioSource.Play();

                gunScript.HideAmmo();

                //disable the gun script whilst ult is active.
                GameObject gun = GameObject.Find("Guns");
                gun.GetComponent<Guns>().enabled = false;

                weaponSelection.EunhaUltKnife();
                ultActive = true;
            }

        }
        if (ultActive)
        {
            EunhaUltimate();
        }

        if (totalThrows <= 0)
        {
            ResetUltimate();
        }
    }

    private void Dash()
    {
        playerMove.Boost(boostAsPercent);
        Invoke("ResetQAbility", Qduration);
        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);
    }

    private void SpeedBoost()
    { 
        playerMove.SBBoost();
        gunScript.IncreaseFireRate();
        Invoke("ResetEAbility", Eduration);
    }

    public void EunhaUltimate()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToThrow && totalThrows > 0)
        {

            audioSource.clip = AudioFire;
            audioSource.Play();

            readyToThrow = false;

            // instantiates the knife to throw and gets its rigidbody component.
            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

            // calculates direction of knife.
            Vector3 forceDirection = cam.transform.forward;

            RaycastHit hit;

            if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
            projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

            totalThrows--;

            Invoke(nameof(ResetThrow), throwCooldown);
        }

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void ResetQAbility()
    {
        playerMove.ResetBoost(boostAsPercent);
    }

    private void ResetEAbility()
    {
        playerMove.ResetSBBoost();
    }


    private void ResetUltimate()
    {
        ultActive = false;

        meterButton.currentProgress = 0;

        gunScript.ShowAmmo();

        // re-enable the gun script when ult is finished.
        GameObject gun = GameObject.Find("Guns");
        gun.GetComponent<Guns>().enabled = true;

        weaponSelection.Start();
    }
}
