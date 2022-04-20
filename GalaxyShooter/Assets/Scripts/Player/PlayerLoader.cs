using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private GameObject Eunha;
    [SerializeField] private GameObject Winter;

    void Start()
    {
        if(CharacterSelection.characterSelection == 1)
        {
            Eunha.SetActive(true);
            Winter.SetActive(false);
        }
        else
        {
            Winter.SetActive(true);
            Eunha.SetActive(false);
        }
    }
}
