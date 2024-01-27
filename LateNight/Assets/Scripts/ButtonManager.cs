using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    DialogueBranching currentNPC;
    public static ButtonManager staticInstance;

    private void Awake()
    {
        staticInstance = this;
    }

    public void ButtonClicked(int buttonChoice)
    {
        if (currentNPC != null)
            currentNPC.GetResponse(buttonChoice);
    }

    public void UpdateCurrentNPC(DialogueBranching newNPC)
    {
        currentNPC = newNPC;
    }
}
