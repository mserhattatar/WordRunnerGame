using System;
using TMPro;
using UnityEngine;

public class PanelLetterController : MonoBehaviour
{
    public TextMeshProUGUI letter;

    public void UpdateLetter(string writeletteretter)
    {
        letter.text = writeletteretter;
    }

    public void SetLetterVisibility(bool setActive)
    {
        letter.gameObject.SetActive(setActive);
    }
}