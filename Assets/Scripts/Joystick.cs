using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform joystickBackground;

    public Vector2 value;

    private float canvasScaleFactor;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        joystickBackground = transform.parent.GetComponent<RectTransform>();

        canvasScaleFactor = transform.parent.parent.parent.GetComponent<Canvas>().scaleFactor;
    }

    public void MoveStick(Vector2 position)
    {
        Vector2 diff = position - (Vector2)joystickBackground.position;
        Vector2 modifiedDiff = diff * canvasScaleFactor;
        modifiedDiff /= rectTransform.sizeDelta * 0.5f;
        value = Vector2.ClampMagnitude(modifiedDiff, 1f);

        modifiedDiff = value * rectTransform.sizeDelta * 0.5f;
        rectTransform.localPosition = modifiedDiff;
    }

    public void ResetStick()
    {
        rectTransform.localPosition = Vector2.zero;
        value = Vector2.zero;
    }
}
