using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    // weapons.
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Knife;
    [SerializeField] private GameObject Sniper;

    public void Start()
    {
        Knife.SetActive(false);
        Gun.SetActive(true);
    }

    public void EunhaUltKnife()
    {
        Knife.SetActive(true);
        Gun.SetActive(false);
    }

    public void SniperUlt()
    {
        Sniper.SetActive(true);
        Gun.SetActive(false);
    }
}
