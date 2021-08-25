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
    // �������������� �� ������ �������
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
            // �������� �������, ������� �����, ����� �� ���� NullReference ��� ������� ���������� ������� � ������ ��������� ��� �����
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
            // �������� �������, ������� �����, ����� �� ���� NullReference ��� ������� ���������� ������� � ������ ��������� ��� �����
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
    /// ���������� ������� � ���� �������� ������
    /// </summary>
    /// <param name="item">�������, ������� ����� ������� � ���� �������� ������</param>
    private void ResetItemPosition(Item item)
    {
        item.gameObject.transform.position = FindItemParentSlot(item).gameObject.transform.position;
    }

    /// <summary>
    /// ������� ������������ ������ ��� ��������� �������� � ���������� ��. 
    /// ���� ������ �� �������, ���������� null
    /// </summary>
    /// <param name="item">�������, ������������ ������ �������� ������ �����</param>
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
