using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DS_DialogueSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Image dialogueActorIconImage = null;
    [SerializeField] TextMeshProUGUI dialogueActorNameTMP = null;
    [SerializeField] TextMeshProUGUI messageTMP = null;

    [Header("Animator")]
    [SerializeField] Animator dialogueSystemPanelAnimator = null;
    [SerializeField] float openDialogueSystemPanelDelay = 1f;

    static readonly int IS_OPENED_ID = Animator.StringToHash("isOpened");

    static WaitForSeconds openDialogueSystemPanelWaiter = null;

    [Header("Variables")]
    [SerializeField] float typeMessageLetterDelay = 0.01f;

    static WaitForSeconds typeMessageLetterWaiter = null;

    [Header("Events")]
    public UnityEvent<DS_DialogueScene> OnDialogueSceneStart = null;
    public UnityEvent<DS_DialogueScene> OnDialogueSceneStop = null;

    static DS_DialogueScene cachedDialogueScene = null;
    static DS_DialogueActor cachedDialogueActor = null;
    static bool waitForInputToContinue = false;
    static Coroutine playCoroutine = null;

    static DS_DialogueSystem instance = null;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            openDialogueSystemPanelWaiter = new WaitForSeconds(openDialogueSystemPanelDelay);
            typeMessageLetterWaiter = new WaitForSeconds(typeMessageLetterDelay);
        }
    }

    public static void Play(DS_DialogueScene _dialogueScene)
    {
        if(instance == null)
        {
            return;
        }

        Stop();
        playCoroutine = instance.StartCoroutine(playDialogueScene(_dialogueScene));
    }

    private static void Stop()
    {
        if(playCoroutine != null)
        {
            instance.StopCoroutine(playCoroutine);
            playCoroutine = null;
        }

        instance.dialogueSystemPanelAnimator?.SetBool(IS_OPENED_ID, false);

        cachedDialogueActor?.OnDialogueActorStop?.Invoke();
        cachedDialogueScene?.OnDialogueSceneStop?.Invoke();

        instance.OnDialogueSceneStop?.Invoke(cachedDialogueScene);

        cachedDialogueActor = null;
        cachedDialogueScene = null;
    }

    private static IEnumerator playDialogueScene(DS_DialogueScene _dialogueScene)
    {
        cachedDialogueScene = _dialogueScene;
        waitForInputToContinue = _dialogueScene.WaitForInputToContinue;

        instance.OnDialogueSceneStart?.Invoke(_dialogueScene);
        _dialogueScene.OnDialogueSceneStart?.Invoke();

        instance.dialogueSystemPanelAnimator?.SetBool(IS_OPENED_ID, true);
        yield return openDialogueSystemPanelWaiter;

        List<DS_Dialogue> _dialogues = _dialogueScene.Dialogues;
        for (int i = 0; i < _dialogues.Count; i++)
        {
            DS_Dialogue _dialogue = _dialogues[i];
            yield return displayDialogue(_dialogue);
        }

        Stop();
    }

    private static IEnumerator displayDialogue(DS_Dialogue _dialogue)
    {
        DS_DialogueActor _dialogueActor = _dialogue.DialogueActor;
        if(_dialogueActor == null)
        {
            yield break;
        }

        if(_dialogueActor != cachedDialogueActor)
        {
            cachedDialogueActor?.OnDialogueActorStop?.Invoke();

            cachedDialogueActor = _dialogueActor;

            cachedDialogueActor?.OnDialogueActorStart?.Invoke();
            instance.dialogueActorIconImage.sprite = cachedDialogueActor.Icon;
            instance.dialogueActorNameTMP.text = cachedDialogueActor.Name;
        }

        string _message = _dialogue.Message;
        if(_message == null)
        {
            yield break;
        }

        instance.messageTMP.text = _message;
        instance.messageTMP.maxVisibleCharacters = 0;
        for (int i = 0; i < _message.Length; i++)
        {
            instance.messageTMP.maxVisibleCharacters = i + 1;
            yield return typeMessageLetterWaiter;
        }

        if(waitForInputToContinue)
        {
            yield return new WaitUntil(() => DS_Inputs.SkipDialogue);
        }
    }
}
