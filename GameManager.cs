using UnityEngine;
using System;
using System.Collections.Generic;

public enum CardType {
  Mine,
  Billionare
}

public enum Currency {
  Money, // Wildcard currency
  Iron,
  Copper,
  Coal,
  Silver,
  Gold
}

public class Card {

  public CardType Type;
  public int[] Requirements = new int[Enum.GetValues(typeof(Currency)).length];
  public int[] Investors;
  public Currency Bonus;

  public Card(CardType type, int[] requirements, int investors, Currency bonus = Currency.Money) {
    Type = type;
    Requirements = requirements;
    Investors = investors;
    Bonus = (type == CardType.Mine) ? bonus : Currency.Money;
  }
  
}

class CardStack : Stack<Card> {


  public void Shuffle() {
    List<Card> cards = new List<Card>(this);
    Random rng = new Random();
    int iteration = Cards.Count;

    while (iteration > 1) {
      iteration--;
      int j = rng.next(iteration + 1);
      Card swapping = cards[j];
      cards[j] = cards[iteration];
      cards[iteration] = swapping;
    }

    Clear();
    foreach (Card card in cards) {
      Push(card);
    }
  }
}

class GameManager : MonoBehaviour {

  void Awake() {

  }

  void Update() {

  }
  
}
