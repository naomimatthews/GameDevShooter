using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guns : MonoBehaviour
{
    Transform cam;

    public GameObject muzzleFlash;

    private Animator animator;

   // public GameObject bulletHole;

    public TextMeshProUGUI text;

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

    //references
    public Camera camera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        PlayerInput();

        text.SetText(bulletsLeft + " / " + magazineSize);
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
    }

    public void Shoot()
    {
        readyToShoot = false;

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
                rayHit.collider.GetComponent<Damageable>().TakeDamage(damage);
        }

       // Instantiate(bulletHole, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShooting);

    }

    private void ResetShot()
    {
        readyToShoot = true;
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

