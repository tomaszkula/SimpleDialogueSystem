using UnityEngine;

public class DS_DialogueTrigger : MonoBehaviour, DS_ITriggerable
{
    [SerializeField] DS_DialogueScene dialogueScene = null;

    public void Trigger(DS_Triggerer _trigger)
    {
        DS_DialogueSystem.Play(dialogueScene);
    }
}
