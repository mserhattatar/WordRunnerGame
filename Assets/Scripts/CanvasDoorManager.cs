using System.Collections.Generic;
using UnityEngine;

public class CanvasDoorManager : MonoBehaviour
{
    public static CanvasDoorManager instance;

    public GameObject doorImageUIPrefarb;
    List<GameObject> letterUIList = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (letterUIList.Count <= 0) return;
        var tempList = new List<GameObject>();
        foreach (var go in letterUIList)
        {
            if (go.GetComponent<DoorPanelImageController>().SetDoorPanelImagePos())
                tempList.Add(go);
        }

        foreach (var go in tempList)
        {
            letterUIList.Remove(go);
            Destroy(go);
        }

        tempList.Clear();
    }

    public void CanvasDoorUIMovement(Vector3 startDoorPos, PanelLetterController hiddenLetter, string doorLetter)
    {
        var vec = Camera.main.WorldToScreenPoint(startDoorPos);
        var letterBox = Instantiate(doorImageUIPrefarb, gameObject.transform);
        letterBox.transform.position = vec;
        letterBox.GetComponentInChildren<DoorPanelImageController>().SetDoorPanelImageText(doorLetter);
        letterBox.GetComponentInChildren<DoorPanelImageController>().HiddenLetterTarget(hiddenLetter);
        letterUIList.Add(letterBox);
    }
}