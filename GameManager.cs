using UnityEngine;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public enum CardType
{
  Mine,
  Billionare
}

public enum Currency
{
  Money, // Wildcard currency
  Iron,
  Copper,
  Coal,
  Silver,
  Gold
}

public class Card
{
  public CardType Type;
  public int[] Requirements = new int[Enum.GetValues(typeof(Currency)).Length];
  public int Investors;
  public int Level;
  public Currency Bonus;

  public Card(CardType type, int[] requirements, int investors, int level, Currency bonus) 
  {
    private bool isMine = (type == CardType.Mine);
    
    Type = type;
    Requirements = requirements;
    Investors = investors;
    Level = isMine ? level : 4;
    Bonus = isMine ? bonus : Currency.Money;
  }
}

class CardDeck : Stack<Card> 
{
  public CardDeck(Card[] cards) 
  {
    foreach (var card in cards) 
    {
      this.Push(card);
    }
  }

  public void Shuffle() 
  {
    List<Card> cards = new List<Card>(this);
    Random rng = new Random();
    int iteration = Cards.Count;

    while (iteration > 1) 
    {
      iteration--;
      int j = rng.next(iteration + 1);
      Card swapping = cards[j];
      cards[j] = cards[iteration];
      cards[iteration] = swapping;
    }

    this.Clear();
    foreach (Card card in cards) 
    {
      Push(card);
    }
  }
}

class Player
{
  public static int PlayerCount = 0;
  public static int CurrentTurn = 0;
  
  public Dictionary<Currency, int> Tokens = new Dictionary<Currency, int>
  {
    { Currency.Money,  0 },
    { Currency.Iron,   0 },
    { Currency.Copper, 0 },
    { Currency.Coal,   0 },
    { Currency.Silver, 0 },
    { Currency.Gold,   0 }
  };
  
  public List<Card> ReservedMines = new List<Card>();
  public List<Card> OwnedMines    = new List<Card>();
  public string PlayerName;

  public Player(string PlayerName)
  {
    this.PlayerName = PlayerName;

    PlayerCount++;
  }
}

class GameManager : MonoBehaviour
{
  public CardDeck deck;
  public List<Player> players;
  
  void Start() 
  {
    Initialize();
  }

  void Update() 
  {
    
  }

  public void Initialize() 
  {
    InitializeCards();
    InitializePlayers();
  }

  public void InitializeCards() 
  {
    
    string filePath = Application.dataPath + "/Data/Cards.json";

    if (!File.Exists(filePath)) 
    {
      Debug.LogError($"JSON file not found at: {filePath}");
      return;
    }

    try 
    {
      string jsonContent = File.ReadAllText(filePath);
      Card[] cardsArray = JsonSerializer.Deserialize<Card[]>(jsonContent);

      deck = new CardDeck(cardsArray);
    }
    catch (System.Exception ex) 
    {
      Debug.LogError($"Error loading cards from JSON: {ex.Message}");
    }
  }

  public void InitializePlayers() 
  {
    string[] playerNames = StartScreen.Players;

    foreach(string playerName in playerNames)
    {
      Player currentPlayer = new Player();
    }
  }
}
