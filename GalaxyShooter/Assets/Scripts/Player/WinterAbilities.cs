using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterAbilities : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    public Transform target;

    [SerializeField] PlayerMovement playerMove;
    [SerializeField] MeterButton meterButton;
    [SerializeField] GameObject playerCam;

    Guns gunScript;

    public WeaponSelection weaponSelection;

    public MeterScript progressMeter;
    public int currentProgress;
    public int maxProgress = 80;

    // audio.
    public AudioSource audioSource;
    [SerializeField] public AudioClip AudioQ;
    [SerializeField] public AudioClip AudioE;
    [SerializeField] public AudioClip AudioUltimate;
    [SerializeField] public AudioClip AudioFire;

    // (Q) freeze ability.
    protected float QabilityTimer;
    [SerializeField] protected float Qcooldown;
    public float Qduration = 5.0f;

    // (E) firerate ability.
    protected float EabilityTimer;
    [SerializeField] protected float Ecooldown;
    [SerializeField] protected float Eduration;

    // (X) ultimate ability.
    bool ultActive;
    public bool ultReady;

    private void Start()
    {

        playerMove = GetComponent<PlayerMovement>();
        gunScript = GameObject.Find("BarrettMRAD").GetComponent<Guns>();
        meterButton = GameObject.Find("UltimateMeter").GetComponent<MeterButton>();
        weaponSelection = GameObject.Find("Gun").GetComponent<WeaponSelection>();

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
                GameObject gun = GameObject.Find("BarrettMRAD");
                gun.GetComponent<Guns>().enabled = false;

                weaponSelection.SniperUlt();
                ultActive = true;
            }
        }
    }

    private void WinterAbilityQ()
    {
        Debug.Log("Q ability active");

        audioSource.clip = AudioQ;
        audioSource.Play();

        gunScript.FreezeAbility();

        Invoke("ResetQAbility", Qduration);
    }

    private void WinterAbilityE()
    {
        audioSource.clip = AudioE;
        audioSource.Play();

        gunScript.IncreaseFireRate();
        gunScript.LessDamage();
    }

    private void ResetQAbility()
    {
      
    }

    private void ResetEAbility()
    {
        playerMove.ResetSBBoost();
    }

    private void ResetUltimate()
    {
        ultActive = false;

        meterButton.currentProgress = 0;

        weaponSelection.Start();
    }

}
