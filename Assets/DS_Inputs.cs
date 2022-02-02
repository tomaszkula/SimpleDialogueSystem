using UnityEngine;

public static class DS_Inputs
{
    // MOVE
    public static float HorizontalMovement => Input.GetAxis("Horizontal");
    public static float VerticalMovement => Input.GetAxis("Vertical");

    // ROTATE
    public static float HorizontalLook => Input.GetAxis("Mouse X");
    public static float VerticalLook => Input.GetAxis("Mouse Y");

    // DIALOGUE
    public static bool SkipDialogue => Input.GetKeyUp(KeyCode.E);
}
