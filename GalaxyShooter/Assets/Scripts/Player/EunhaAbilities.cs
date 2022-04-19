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
    public float ultTimer;

    [SerializeField] protected float ultDuration;

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
        playerMove = GetComponent<PlayerMovement>();

        boostAsPercent = (100 + boostPercentage) / 100;

        ultTimer = ultDuration;

        readyToThrow = true;
    }

    void Update()
    {
        if (Time.time >= QabilityTimer && Input.GetKeyDown(KeyCode.Q))
        {
            Dash();
            QabilityTimer = Time.time + Qcooldown;
        }

        if (Time.time >= EabilityTimer && Input.GetKeyDown(KeyCode.E))
        {
            SpeedBoost();
            EabilityTimer = Time.time + Ecooldown;
        }

        if (meterButton.currentProgress == meterButton.maxProgress)
        {
            ultReady = true;
            Debug.Log("ult ready");

            if (Input.GetKeyDown(KeyCode.X) && readyToThrow && totalThrows > 0)
            {
                Debug.Log("x pressed");

                //disable the gun script whilst ult is active.
                GameObject gun = GameObject.Find("Guns");
                gun.GetComponent<Guns>().enabled = false;

                weaponSelection.EunhaUltKnife();
                if (Input.GetKeyDown(KeyCode.Mouse0) && readyToThrow && totalThrows > 0)
                {
                    EunhaUltimate();
                }
            }

        }
        if (ultActive)
        {
            ultTimer -= Time.deltaTime;
        }

        if (ultTimer <= 0)
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
       ultActive = true;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ReserThrow), throwCooldown);
    }

    private void ReserThrow()
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
    }
}
