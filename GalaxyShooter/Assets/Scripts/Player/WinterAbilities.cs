using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterAbilities : MonoBehaviour
{
    Rigidbody rb;
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

    // (Q) freeze ability.
    protected float QabilityTimer;
    [SerializeField] protected float Qcooldown;
    [SerializeField] protected float Qduration;

    // (E) firerate ability.
    protected float EabilityTimer;
    [SerializeField] protected float Ecooldown;
    [SerializeField] protected float Eduration;

    // (X) ultimate ability.
    bool ultActive;
    public bool ultReady;
    public float ultTimer;
    [SerializeField] protected float ultDuration;

    private void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        gunScript = GetComponent<Guns>();
        meterScript = GetComponent<MeterButton>();

    }

    private void Update()
    {
        if (Time.time >= QabilityTimer && Input.GetKeyDown(KeyCode.Q))
        {
            WinterAbilityQ();
            QabilityTimer = Time.time + Qcooldown;
        }

        if (Time.time >= EabilityTimer && Input.GetKeyDown(KeyCode.E))
        {
            WinterAbilityE();
            EabilityTimer = Time.time + Ecooldown;
        }
    }

    private void WinterAbilityQ()
    {

    }

    private void WinterAbilityE()
    {
        gunScript.IncreaseFireRate();
        gunScript.LessDamage();
    }

    private void Ultimate()
    {
         
    }

}
