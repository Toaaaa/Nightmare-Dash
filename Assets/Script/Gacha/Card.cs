using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card 
{
    public string cardName;
    public Sprite cardImage;
    public string cardEffect;
    public string cardType;
    public int weight;

    public Card() { }
    public Card(Card card)
    {
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        this.cardEffect = card.cardEffect;
        this.cardType = card.cardType;
        this.weight = card.weight;
    }
}
