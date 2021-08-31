using System.Collections.Generic;
using UnityEngine;

public class CanvasWordUIManager : MonoBehaviour
{
    [SerializeField] private List<PanelLetterController> sixLetterInPanelChar;
    [SerializeField] private List<PanelLetterController> sevenLetterInPanelChar;
    [SerializeField] private List<PanelLetterController> eightLetterInPanelChar;


    public List<PanelLetterController> ShowLetters(int stringLenght)
    {
        var panelLetter = stringLenght switch
        {
            6 => sixLetterInPanelChar,
            7 => sevenLetterInPanelChar,
            _ => eightLetterInPanelChar
        };
        ResetLetters(panelLetter);
        return panelLetter;
    }

    private void ResetLetters(List<PanelLetterController> panelLetter)
    {
        foreach (var six in sixLetterInPanelChar)
        {
            six.SetGameObjectVisibility(false);
        }

        foreach (var seven in sevenLetterInPanelChar)
        {
            seven.SetGameObjectVisibility(false);
        }

        foreach (var eight in eightLetterInPanelChar)
        {
            eight.SetGameObjectVisibility(false);
        }

        foreach (var letter in panelLetter)
        {
            letter.SetGameObjectVisibility(true);
        }
    }
}