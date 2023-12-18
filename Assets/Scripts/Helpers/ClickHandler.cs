//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;

//public class ClickHandler :  IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,
//    IBeginDragHandler, IDragHandler, IEndDragHandler
//{

//    private bool isDragging = false;

//    [SerializeField] public PointerEventData.InputButton validButton;

//    UnityEvent  _event;

//    private void Check()
//    {

//    }
//    public void OnPointerDown(PointerEventData eventData)
//    {
//        if (eventData.button != validButton) return;
//        _event.Invoke();
//    }

//    public void OnDown()
//    {

//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        if (eventData.button != PointerEventData.InputButton.Left)
//        {
//            // Ignore non-left mouse button up events
//            eventData.pointerPress = null;
//            eventData.rawPointerPress = null;
//        }
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (eventData.button == PointerEventData.InputButton.Left)
//        {
//            // Handle left-click logic here
//            Debug.Log("Left-click detected");
//        }
//    }

//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        if (eventData.button == PointerEventData.InputButton.Left)
//        {
//            isDragging = true;
//            // Handle drag start logic here
//            Debug.Log("Drag started");
//        }
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        if (isDragging && eventData.button == PointerEventData.InputButton.Left)
//        {
//            // Handle drag logic here
//            Debug.Log("Dragging...");
//        }
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        if (isDragging && eventData.button == PointerEventData.InputButton.Left)
//        {
//            isDragging = false;
//            // Handle drag end logic here
//            Debug.Log("Drag ended");
//        }
//    }
//}
