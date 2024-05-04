using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToCanvas : MonoBehaviour
{
    [SerializeField] Barricade[] barricades;
    [SerializeField] PlaceableCard[] placeableCards;
    [SerializeField] float yOffset;

    private void Start()
    {
        SetPos();
    }

    void SetPos()
    {
        for (int i = 0; i < barricades.Length; i++)
        {
            Vector3 worldPos = barricades[i].transform.position;

            worldPos.y += yOffset;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            placeableCards[i].Init(barricades[i], screenPos, i);
        }
    }
}
