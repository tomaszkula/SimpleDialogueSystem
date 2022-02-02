using UnityEngine;

public class DS_PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform head = null;

    [Header("Variables")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;

    bool isDialogueSceneEnabled = false;
    float xAngle = 0f;

    Transform myTransform = null;
    Rigidbody myRigidbody = null;

    private void Awake()
    {
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotate();
    }

    private void FixedUpdate()
    {
        move();
    }

    public void OnDialogueSceneToggle(bool _isEnabled)
    {
        isDialogueSceneEnabled = _isEnabled;
    }

    private void move()
    {
        float _horizontal = isDialogueSceneEnabled ? 0f : DS_Inputs.HorizontalMovement;
        float _vertical = isDialogueSceneEnabled ? 0f : DS_Inputs.VerticalMovement;

        Vector3 _direction = new Vector3(_horizontal, 0f, _vertical);
        Vector3 _transformedDirection = myTransform.TransformDirection(_direction);
        _transformedDirection.y = 0f;
        if (_direction.magnitude >= 1f)
        {
            Vector3 _normalizedDirection = _transformedDirection.normalized;
            Vector3 _velocity = _normalizedDirection * moveSpeed;
            _velocity.y = myRigidbody.velocity.y;
            myRigidbody.velocity = _velocity;
        }
        else
        {
            Vector3 _velocity = _transformedDirection * moveSpeed;
            _velocity.y = myRigidbody.velocity.y;
            myRigidbody.velocity = _velocity;
        }
    }

    private void rotate()
    {
        float _horizontal = isDialogueSceneEnabled ? 0f : DS_Inputs.HorizontalLook;
        myTransform.Rotate(_horizontal * rotateSpeed * Time.deltaTime * Vector3.up);

        float _vertical = isDialogueSceneEnabled ? 0f : DS_Inputs.VerticalLook;
        xAngle += -_vertical * rotateSpeed * Time.deltaTime;
        xAngle = Mathf.Clamp(xAngle, -90f, 90f);
        head.localRotation = Quaternion.Euler(xAngle * Vector3.right);
    }
}
