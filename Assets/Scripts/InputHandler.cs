using UnityEngine;
using UnityEngine.EventSystems;
public class InputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            WordSelectionController.Instance.OnDragStart(Input.mousePosition);
        else if (Input.GetMouseButton(0))
            WordSelectionController.Instance.OnDrag(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
            WordSelectionController.Instance.OnDragEnd();
    }
}