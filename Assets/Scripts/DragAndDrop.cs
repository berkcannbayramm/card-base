using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    [field: SerializeField] public int Index { get; set; }

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 initialPosition;
    private List<PlaceableCard> _placeableCards = new List<PlaceableCard>();
    PlaceableCard _placeableObj;
    BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Kill(this);
        if (!eventData.dragging)
            transform.DOLocalMoveY(transform.localPosition.y + 50, .2f).SetId(this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        DOTween.Kill(this);
        if(!eventData.dragging)
            transform.DOLocalMoveY(transform.localPosition.y - 50, .2f).SetId(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DOTween.Kill(this);

        Vector3 rotation;

        if (_placeableObj != null)
            rotation = Vector3.forward * -10;
        else
            rotation = Vector3.zero;

        transform.DORotate(rotation, .2f).SetEase(Ease.InOutBack);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.y += CardSortManager.instance.yOffset;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out Vector2 localPoint);
        rectTransform.position = canvas.transform.TransformPoint(localPoint);
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_placeableObj != null)
        {
            _placeableObj.CanPlaceable = false;

            _placeableObj.Card = this;

            transform.SetParent(_placeableObj.transform);

            transform.DOLocalMove(Vector3.zero, .2f);

            CardSortManager.instance.Sort();
        }
        else
        {
            transform.DOLocalMove(initialPosition, .2f);
            CardSortManager.instance.Sort();
            transform.DORotate(Vector3.forward * -10, .2f).SetEase(Ease.InOutBack);
        }
    }

    public void Reset()
    {
        _boxCollider2D.enabled = false;
        transform.DORotate(Vector3.forward * -10, .2f).SetEase(Ease.InOutBack).OnComplete(()=>_boxCollider2D.enabled = true);

        foreach(var x in _placeableCards)
        {
            x.CanPlaceable = true;
        }

        _placeableCards.Clear();

        _placeableObj.Card = null;

        _placeableObj = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlaceableCard placeableCard) && placeableCard.CanPlaceable)
        {
            _placeableCards.Add(placeableCard);
            _placeableObj = placeableCard;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlaceableCard placeableCard) && placeableCard.CanPlaceable)
        {
            _placeableCards.Remove(placeableCard);
            if (_placeableCards.Count > 0)
            {
                _placeableObj = _placeableCards[_placeableCards.Count - 1];
            }
            else
            {
                _placeableObj = null;
            }
        }
    }
}
