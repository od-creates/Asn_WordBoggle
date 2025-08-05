using UnityEngine;
using UnityEngine.EventSystems;
public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public void OnPointerDown(PointerEventData e) => WordSelectionController.Instance.OnDragStart(e.position);
    public void OnDrag(PointerEventData e) => WordSelectionController.Instance.OnDrag(e.position);
    public void OnPointerUp(PointerEventData e) => WordSelectionController.Instance.OnDragEnd();
}