using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "dialogueScene_NewDialogueScene", menuName = "Dialogue System/Dialogue Scene")]
public class DS_DialogueScene : ScriptableObject
{
    public List<DS_Dialogue> Dialogues = new List<DS_Dialogue>();
    public bool WaitForInputToContinue = true;

    [Header("Events")]
    public UnityEvent OnDialogueSceneStart = null;
    public UnityEvent OnDialogueSceneStop = null;
}
