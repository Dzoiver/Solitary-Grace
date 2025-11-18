using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Optional: Add visual feedback for beginning drag (e.g., change color)
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the UI element's position based on mouse/touch movement
        rectTransform.anchoredPosition += eventData.delta / rectTransform.lossyScale.x;
        // Note: eventData.delta is in screen space, divide by lossyScale.x for accurate canvas movement
        gameObject.transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Optional: Add visual feedback for ending drag (e.g., revert color)
        // Optional: Implement logic for dropping the element (e.g., into a slot)
    }
}
