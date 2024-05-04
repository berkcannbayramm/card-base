using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableCard : MonoBehaviour
{
    public bool CanPlaceable { get; set; } = true;

    public int Index => _index;

    public Barricade Barricade => _barricade;

    public DragAndDrop Card {  get; set; }

    int _index;

    Barricade _barricade;

    DragAndDrop _card;

    public void Init(Barricade barricade, Vector2 pos, int index)
    {
        _index = index;
        transform.position = pos;
        _barricade = barricade;
    }
}
