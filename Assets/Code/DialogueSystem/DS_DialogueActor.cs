using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "dialogueActor_NewDialogueActor", menuName = "Dialogue System/Dialogue Actor")]
public class DS_DialogueActor : ScriptableObject
{
    public Sprite Icon = null;
    public string Name = null;

    [Header("Events")]
    public UnityEvent OnDialogueActorStart = null;
    public UnityEvent OnDialogueActorStop = null;
}
