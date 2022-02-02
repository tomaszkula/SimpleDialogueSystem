using UnityEngine;

public class DS_Triggerer : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider _other)
    {
        DS_ITriggerable _iTriggerable = _other?.GetComponent<DS_ITriggerable>();
        _iTriggerable?.Trigger(this);
    }
}
