using UnityEngine;
using UnityEngine.EventSystems;

public class CommandBlockUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private int originalIndex;
    private CanvasGroup canvasGroup;
    public Commands commandName;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalIndex = transform.GetSiblingIndex();
        transform.SetParent(originalParent.parent); // sortir du layout pour le drag
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // On cherche où replacer le block
        transform.SetParent(originalParent);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // trouver l'index adéquat
        int newIndex = 0;
        foreach (Transform child in originalParent)
        {
            if (transform.position.y > child.position.y) break;
            newIndex++;
        }

        transform.SetSiblingIndex(newIndex);
        CommandDispatcher.instance.GetAllCommands();
    }
}
