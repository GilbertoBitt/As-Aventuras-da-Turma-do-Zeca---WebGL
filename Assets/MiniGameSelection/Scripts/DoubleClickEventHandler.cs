using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoubleClickEventHandler :OverridableMonoBehaviour, IPointerDownHandler {

    public UnityEvent OnDoubleClick = new UnityEvent();
    private int clickCount;
    private float lastestClickTimer;
    public float doubleClickDelay = 0.5f;

    public virtual void OnPointerDown(PointerEventData eventData) {
        clickCount++;
        if (clickCount == 1) lastestClickTimer = Time.time;

        if (clickCount > 1 && Time.time - lastestClickTimer < doubleClickDelay) {
            clickCount = 0;
            lastestClickTimer = 0;
            //Debug.Log("Double Click: " + this.GetComponent<RectTransform>().name);
            OnDoubleClick.Invoke();

        } else if (clickCount > 2 || Time.time - lastestClickTimer > 1) clickCount = 0;
    }

}
