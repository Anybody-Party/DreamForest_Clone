using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform joystickBackground;
    private Joystick joystick;

    private RectTransform rectTransform;

    private float canvasScaleFactor;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        joystickBackground = transform.GetChild(0).GetComponent<RectTransform>();
        joystick = joystickBackground.transform.GetChild(0).GetComponent<Joystick>();
    
        canvasScaleFactor = GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnPointerDown(PointerEventData pointData)
    {
        joystickBackground.gameObject.SetActive(true);

        Vector2 diff = pointData.position - (Vector2)rectTransform.position;
        Vector2 modifiedDiff = diff / canvasScaleFactor;
        
        joystickBackground.localPosition = modifiedDiff;
    }

    public void OnPointerUp(PointerEventData pointData)
    {
        joystickBackground.gameObject.SetActive(false);

        joystick.ResetStick();
    }

    public void OnDrag(PointerEventData pointData)
    {
        joystick.MoveStick(pointData.position);
    }
}
