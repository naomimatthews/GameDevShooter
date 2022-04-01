using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EunhaAbilities : MonoBehaviour
{
    Rigidbody rb;
    public GameObject dashEffect;
    public GameObject player;
    public Transform target;

    [SerializeField] PlayerMovement playerMove;
    [SerializeField] Guns gunScript;
    [SerializeField] MeterButton meterScript;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject ultCam;


    public MeterScript progressMeter;
    public int currentProgress;
    public int maxProgress = 80;

    // (Q) dash ability.
    protected float QabilityTimer;
    [SerializeField] protected float Qcooldown;
    [SerializeField] protected float Qduration;
    [SerializeField] private int boostPercentage;
    private float boostAsPercent;

    // (E) dash ability.
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


    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        gunScript = GetComponent<Guns>();
        meterScript = GetComponent<MeterButton>();

        playerCam.SetActive(true);
        ultCam.SetActive(false);

        boostAsPercent = (100 + boostPercentage) / 100;
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

        if (currentProgress == 80)
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
            ultActive = false;

            playerCam.SetActive(true);
            ultCam.SetActive(false);

            ultTimer = ultDuration;

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

        playerCam.SetActive(false);
        ultCam.SetActive(true);
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
      // stop shooting.
      // stop rotating.
      // reset dial.
    }
}
