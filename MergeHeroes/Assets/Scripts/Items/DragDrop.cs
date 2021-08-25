// Roman Baranov 11.08.2021

using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region VARIABLES
    private RectTransform _rectTransform = null;
    private Canvas _canvas = null;
    private CanvasGroup _canvasGroup = null;

    private bool _isDragging = false;
    // перет€гиваетс€ ли сейчас предмет
    public bool IsDragging { get { return _isDragging; } set { _isDragging = value; } }
    
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = FindObjectOfType<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    #endregion

    #region EVENTS
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.GetComponent<Item>().IsEquipped)
        {
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
            _isDragging = true;
            Debug.Log("OnBeginDrag");
        }
        else
        {
            // ќбнул€ем предмет, который тащим, чтобы не было NullReference при попытке перетащить предмет в €чейку инвентар€ дл€ мержа
            eventData.pointerDrag = null;
            Debug.Log("OnBeginDrag - Item is Equipped!");
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!eventData.pointerDrag.GetComponent<Item>().IsEquipped)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

            Debug.Log("OnDrag");
        }
        else
        {
            // ќбнул€ем предмет, который тащим, чтобы не было NullReference при попытке перетащить предмет в €чейку инвентар€ дл€ мержа
            eventData.pointerDrag = null;
            Debug.Log("OnDrag - Item is Equipped!");
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (_isDragging)
        {
            ResetItemPosition(GetComponent<Item>());
        }
        Debug.Log("OnEndDrag");
    }
    #endregion

    /// <summary>
    /// ¬озвращает предмет в свою исходную €чейку
    /// </summary>
    /// <param name="item">ѕредмет, который нужно вернуть в свою исходную €чейку</param>
    private void ResetItemPosition(Item item)
    {
        item.gameObject.transform.position = FindItemParentSlot(item).gameObject.transform.position;
    }

    /// <summary>
    /// Ќаходит родительскую €чейку дл€ заданного предмета и возвращает ее. 
    /// ≈сли €чейка не найдена, возвращает null
    /// </summary>
    /// <param name="item">ѕредмет, родительскую €чейку которого наужно найти</param>
    /// <returns>ItemSlot</returns>
    private ItemSlot FindItemParentSlot(Item item)
    {
        for (int i = 0; i < Inventory.Slots.Count; i++)
        {
            if (Inventory.Slots[i].ItemSlotID == item.ParentSlotId)
            {
                return Inventory.Slots[i];
            }
        }

        return null;
    }



}
