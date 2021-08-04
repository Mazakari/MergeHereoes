// Roman Baranov 20.07.2021

using UnityEngine;

public class TouchManager : MonoBehaviour 
{
    #region VARIABLES
    private static bool _isMergable = false;// ����� �� ������� ������ � ��������� ������
    /// <summary>
    /// ����� �� ������� ������
    /// </summary>
    public static bool IsMergable { get { return _isMergable; } set { _isMergable = value; } }

    private bool _isDragging = false;// �������������� �� ������ � ��������� ������

    private GameObject _draggingItem = null;// �������, ������� � ������ ������ ��������������

    #endregion

    #region UNITY Methods
    // Update is called once per frame
    void Update()
    {
        DragHandler();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������������ ������������� ��������
    /// </summary>
    private void DragHandler()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit2D = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit2D && hit2D.collider.GetComponent<Item>())
            {
                _isMergable = false;
                _isDragging = true;

                _draggingItem = hit2D.collider.gameObject;
                _draggingItem.transform.position = touchPos;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && _isDragging == true)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit2D = Physics2D.Raycast(touchPos, Vector2.zero);

            // ���� ������� �������� �� ������ �����
            //if (hit2D && hit2D.collider.name == _draggingItem.GetComponent<BoxCollider2D>().name)
            //{
            //    Debug.Log("Return Item");
            //    // ���������� ������� � ������
            //    _draggingItem.transform.position = _draggingItem.GetComponent<Item>().StartPos;
            //}
            _draggingItem = null;

            _isMergable = true;
            _isDragging = false;

        }
    }
    #endregion
}
