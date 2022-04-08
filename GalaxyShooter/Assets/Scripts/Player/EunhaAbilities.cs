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
    public float rotationSpeed;
    // public Vector3 rotationDirection;

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

            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("x pressed");
                EunhaUltimate();
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

        weaponSelection.EunhaUltKnife();
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
