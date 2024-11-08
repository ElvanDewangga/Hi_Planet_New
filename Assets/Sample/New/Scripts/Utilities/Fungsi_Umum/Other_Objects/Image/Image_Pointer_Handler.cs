using UnityEngine;
using UnityEngine.EventSystems;

public class Image_Pointer_Handler : MonoBehaviour, IPointerUpHandler {
    public RectTransform imageRectTransform; // Drag your UI Image's RectTransform here
    public GameObject targetObject; // Drag the GameObject you want to check here

    public void OnPointerUp(PointerEventData eventData) {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);

        // Convert the target object's position to screen point
        Vector2 targetScreenPoint = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        Debug.Log("Target Screen Point: " + targetScreenPoint);

        Vector2 targetLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRectTransform, targetScreenPoint, eventData.pressEventCamera, out targetLocalPoint);
        Debug.Log("Target Local Point: " + targetLocalPoint);

        // Check if the target object's position is within the UI Image
        if (imageRectTransform.rect.Contains(targetLocalPoint)) {
            Debug.Log("Objek target berada di dalam area UI Image!");
        } else {
            Debug.Log("Objek target berada di luar area UI Image!");
        }
    }
}