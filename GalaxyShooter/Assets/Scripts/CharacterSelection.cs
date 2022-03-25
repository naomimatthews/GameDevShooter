using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    [SerializeField] private GameObject Eunha;
    [SerializeField] private GameObject Winter;

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

    public void OnEunhaSelect()
    {
        Winter.SetActive(false);
        Eunha.SetActive(true);

        // enable/disable text on ui
    }

    public void OnWinterSelect()
    {
        Eunha.SetActive(false);
        Winter.SetActive(true);
    }
}
