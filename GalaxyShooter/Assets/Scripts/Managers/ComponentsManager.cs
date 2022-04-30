using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsManager : MonoBehaviour
{
    private void Awake()
    {
        // enable the enemycontrols script.
        GameObject enemy = GameObject.Find("Enemy1");
        enemy.GetComponent<EnemyManager>().enabled = true;

        GameObject enemy2 = GameObject.Find("Enemy2");
        enemy2.GetComponent<EnemyManager>().enabled = true;

        GameObject enemy3 = GameObject.Find("Enemy3");
        enemy3.GetComponent<EnemyManager>().enabled = true;

        GameObject enemy4 = GameObject.Find("Enemy4");
        enemy4.GetComponent<EnemyManager>().enabled = true;

        GameObject enemy5 = GameObject.Find("Enemy5");
        enemy5.GetComponent<EnemyManager>().enabled = true;
    }


}
