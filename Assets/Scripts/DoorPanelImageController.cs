using TMPro;
using UnityEngine;

public class DoorPanelImageController : MonoBehaviour
{
    public Vector3 targetPos;
    public PanelLetterController hiddenLetter;

    public bool SetDoorPanelImagePos()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 2000f * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < 1f)
        {
            hiddenLetter.SetLetterVisibility(true);
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public void HiddenLetterTarget(PanelLetterController hiddenLetterTarget)
    {
        targetPos = hiddenLetterTarget.transform.position;
        hiddenLetter = hiddenLetterTarget;
    }

    public void SetDoorPanelImageText(string doorLetter)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = doorLetter;
    }
}