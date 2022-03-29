using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    // agents.
    [SerializeField] private GameObject Eunha;
    [SerializeField] private GameObject Winter;

    // text.
    [SerializeField] private GameObject EunhaText;
    [SerializeField] private GameObject WinterText;

    // audio.
    public AudioSource audioSource;
    [SerializeField] public AudioClip EunhaPickAudio;
    [SerializeField] public AudioClip WinterPickAudio;
    [SerializeField] public AudioClip ChooseAgentAudio;

    public static int characterSelection = 0;

    public TextMeshProUGUI countdownText;

    #region old switch variables.
    /* [SerializeField] private Button previousButton;
     [SerializeField] private Button nextButton;

     private int currentChar;
 */
    #endregion
    #region old character switch.
    /*  public void SelectChar(int _index)
      {
          previousButton.interactable = (_index != 0);
          nextButton.interactable = (_index != transform.childCount-1);
          for (int i = 0; i < transform.childCount; i++)
          {
              transform.GetChild(i).gameObject.SetActive(i == _index);
          }
      }

      public void ChangeChar(int _change)
      {
          currentChar += _change;
          SelectChar(currentChar);
      }
    */
    #endregion

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = ChooseAgentAudio;
        audioSource.Play();
    }

    public void OnEunhaSelect()
    {
        Winter.SetActive(false);
        Eunha.SetActive(true);

        // enable/disable text on ui
        EunhaText.SetActive(true);
        WinterText.SetActive(false);

        audioSource.clip = EunhaPickAudio;
        audioSource.Play();

        characterSelection = 1;
    }

    public void OnWinterSelect()
    {
        Eunha.SetActive(false);
        Winter.SetActive(true);

        // enable/disable text on ui
        EunhaText.SetActive(false);
        WinterText.SetActive(true);

        audioSource.clip = WinterPickAudio;
        audioSource.Play();

        characterSelection = 2;
    }

    public void LockInButton()
    {
        if (characterSelection != 0)
        {
            countdownText.gameObject.SetActive(true);
            StartCoroutine(ShowsCountdown());
        }
    }

    private IEnumerator ShowsCountdown()
    {
        int counter = 5;

       while(counter != 0)
       {
            countdownText.text = counter.ToString("0");

            counter -= 1;

            yield return new WaitForSeconds(1.0f);
       }

        SceneManager.LoadScene(4);
    }
}
