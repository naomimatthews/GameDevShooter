using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowing : MonoBehaviour
{
    public GameObject[] knives;

    public int totalThrows;
    public int throwCooldown;

    bool readyToThrow;

    public float throwForce;
    public float throwUpwardForce;

    public Transform atttackPoint;
    public Transform cam;

    [SerializeField] GameObject knife1;
    [SerializeField] GameObject knife2;
    [SerializeField] GameObject knife3;
    [SerializeField] GameObject knife4;
    [SerializeField] GameObject knife5;

    private void Start()
    {
        readyToThrow = true;

        knives = new GameObject[5];

        knives[0] = knife1;
        knives[1] = knife2;
        knives[2] = knife3;
        knives[3] = knife4;
        knives[4] = knife5;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && readyToThrow && totalThrows > 0)
        {
            Throwing();
        }
    }

    private void Throwing()
    {
        GameObject projectile = Instantiate(knives, atttackPoint.position, cam.rotation);
    }
}
