using UnityEngine;

public class DS_TargetFollower : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Vector3 positionOffset = Vector3.zero;

    Transform myTransform = null;

    private void Awake()
    {
        myTransform = transform;
    }

    private void Update()
    {
        myTransform.position = target.position + target.TransformDirection(positionOffset);
        myTransform.rotation = target.rotation;
    }
}
