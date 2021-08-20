using TMPro;
using UnityEngine;

public class PanelLetterController : MonoBehaviour
{
    public TextMeshProUGUI letter;

    public void UpdateLetter(string writeLetter)
    {
        letter.text = writeLetter;
    }

    public void SetLetterVisibility(bool setActive)
    {
        letter.gameObject.SetActive(setActive);
    }

}