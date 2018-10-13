using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotControl : MonoBehaviour
{

    public Text fileText;
    public Button clearButton;
    private int thisSlotIndex;
    private SaveFile mySave;
    private SaveFileManager manager;

    public void Initialise(SaveFile newSave, SaveFileManager newManager, int newSlotIndex)
    {

        thisSlotIndex = newSlotIndex;
        manager = newManager;
        mySave = newSave;

        if (newSave != null)
        {
            
            clearButton.interactable = true;
            fileText.text = "Area : " + mySave.GetSceneName();
        }
        else
        {
            clearButton.interactable = false;
            fileText.text = "EMPTY";
        }

    }

    public void ClearSlot()
    {
        fileText.text = "EMPTY";
        mySave = null;
        manager.ClearSlot(thisSlotIndex);
        clearButton.interactable = false;
    }

    public void SelectSlot()
    {
        if (mySave == null) { manager.CreateNewSave(thisSlotIndex); }
        else { manager.LoadGame(thisSlotIndex); }
    }

}
