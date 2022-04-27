using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guns : MonoBehaviour
{
    Transform cam;

    public GameObject muzzleFlash;

    // animation.
    private Animator animator;

   // public GameObject bulletHole;

    public TextMeshProUGUI ammoText;

    // audio.
    public AudioSource audioSource;
    [SerializeField] public AudioClip audioShooting;

    // gun stats.
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float reloadTime;
    [SerializeField] float spread;
    [SerializeField] float timeBetweenShooting;

    public int magazineSize;
    public int bulletsPerTap;

    int bulletsLeft, bulletsShot;

    public bool allowButtonHold;
    bool shooting;
    bool reloading;
    bool readyToShoot;
    public bool abilityActive;

    //references
    public Camera camera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public MeterScript2 progressMeter;
    public MeterButton2 meterButton;
    public EnemyControls enemyControls;
    public WinterAbilities winterAbilities;

    private void Awake()
    {
        meterButton = GameObject.Find("UltimateMeter").GetComponent<MeterButton2>();
        progressMeter = GameObject.Find("UltimateMeter").GetComponent<MeterScript2>();
        winterAbilities = GameObject.Find("Winter").GetComponent<WinterAbilities>();

        animator = GetComponentInParent<Animator>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
        abilityActive = false;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        meterButton.currentProgress = 0;
        progressMeter.SetMaxProgress(meterButton.maxProgress);
    }

    private void Update()
    {
        progressMeter.SetProgress(meterButton.currentProgress);
        PlayerInput();

        ammoText.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void PlayerInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        // shoot weapon.
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Reload()
    {
        reloading = true;
        animator.SetBool("Reloading", true);

        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        animator.SetBool("Reloading", false);
    }

    public void Shoot()
    {
        readyToShoot = false;

        audioSource.clip = audioShooting;
        audioSource.Play();

        // bullet spread.
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

       /* if (GetComponent<Rigidbody>().velocity.magnitude > 0)
            spread = spread * 1.5f;
        else spread = "normal spread";*/

        Vector3 direction = camera.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                if (!abilityActive)
                {
                    rayHit.collider.GetComponent<Damageable>().TakeDamage(damage);
                    if (meterButton.currentProgress < 80)
                    {
                        // Debug.Log("BIGWOW");
                        meterButton.currentProgress += 7;
                    }
                }
                else
                {
                    enemyControls = rayHit.collider.GetComponent<EnemyControls>();

                    enemyControls.StopEnemy();
                    enemyControls.enabled = false;
                }
            }
        }

        // Instantiate(bulletHole, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShooting);

    }

    public void FreezeAbility()
    {
        abilityActive = true;
    }

    public void IncreaseFireRate()
    {
        bulletsShot = bulletsPerTap * 2;
        timeBetweenShooting = timeBetweenShooting / 2;
        timeBetweenShots = timeBetweenShots / 2;
    }

    public void LessDamage()
    {
        damage = damage - 35;
        Debug.Log(damage);
    }

    private void ResetFirerate()
    {
        bulletsShot = bulletsPerTap;
        timeBetweenShooting = timeBetweenShooting * 2;
        timeBetweenShots = timeBetweenShots * 2;
        Debug.Log(timeBetweenShooting);
        Debug.Log(timeBetweenShots);
    }

    private void ResetDanage()
    {
        damage = damage + 35;
        Debug.Log(damage);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void HideAmmo()
    {
        ammoText.gameObject.SetActive(false);
    }

    public void ShowAmmo()
    {
        ammoText.gameObject.SetActive(true);
    }

    #region old shooting code.
    /*  private void Awake()
      {
          cam = Camera.main.transform;
      }

      public void Shoot()
      {
          RaycastHit hit;
          if (Physics.Raycast(cam.position, cam.forward, out hit, range))
          {
              if (hit.collider.GetComponent<Damageable>() != null)
              {
                  hit.collider.GetComponent<Damageable>().TakeDamage(damage, hit.point, hit.normal);
              }
          }
      }*/
    #endregion
}

