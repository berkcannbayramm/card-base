using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardSortManager : MonoBehaviour
{
    public static CardSortManager instance;

    [SerializeField] Transform positionRef;

    public float yOffset = 50f;

    List<Transform> _posRefArr = new List<Transform>();
    DragAndDrop[] dragAndDrops;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dragAndDrops = GetComponentsInChildren<DragAndDrop>();

        var dragDropObjects = dragAndDrops;

        for (int i = 0; i < positionRef.childCount; i++)
        {
            _posRefArr.Add(positionRef.GetChild(positionRef.childCount -1  - i).transform);
        }
    }

    public void Sort()
    {
        var dragDropObjects = GetComponentsInChildren<DragAndDrop>();

        for (int i = 0; i < dragDropObjects.Length; i++)
        {
            dragDropObjects[dragDropObjects.Length - 1 - i].transform.DOMove(_posRefArr[i].position, .2f).SetEase(Ease.InOutQuad);
        }
    }

    public void ResetCards()
    {
        for (int i = 0; i < dragAndDrops.Length; i++)
        {
            var item = dragAndDrops[i];
            item.transform.SetParent(transform);
            item.transform.SetSiblingIndex(item.Index);
            item.Reset();
        }

        Sort();
    }
}
