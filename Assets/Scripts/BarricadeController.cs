using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    PlaceableCard[] placeableCards;
    Character[] characters;
    private void Start()
    {
        placeableCards = FindObjectsOfType<PlaceableCard>();
        characters = FindObjectsOfType<Character>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetSolidersPosition();
        }
    }
    void SetSolidersPosition()
    {
        for (int i = 0; i < placeableCards.Length; i++)
        {
            PlaceableCard PlaceableCard = placeableCards[i];
            Character currChar = FindCharacterByIndex(PlaceableCard.Card.Index);
            currChar.SetAgentTarget(PlaceableCard.Barricade.CrouchPosition);
        }
    }

    Character FindCharacterByIndex(int index)
    {
        foreach (Character character in characters)
        {
            if (index == character.Index)
            {
                return character;
            }
        }
        return null;
    }
}
