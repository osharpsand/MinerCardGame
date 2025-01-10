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

public enum MoveType
{
  GrabTokens,
  BuyMine,
  ReserveMine,
  PeekAtTopCard
}

public class Card
{
  public CardType Type;
  public int[] Requirements = new int[6];
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

  public static Player GetActivePlayer()
  {
    return GameManager.players[CurrentTurn];
  } 

  public Dictionary<Currency, int> Tokens = new Dictionary<Currency, int>
  {
    { Currency.Money,  0 },
    { Currency.Iron,   0 },
    { Currency.Copper, 0 },
    { Currency.Coal,   0 },
    { Currency.Silver, 0 },
    { Currency.Gold,   0 }
  };
  
  public int[] CardBonuses = new int[6];
  public List<Card> ReservedMines = new List<Card>();
  public List<Card> OwnedMines    = new List<Card>();
  public int PlayerId;
  public string PlayerName;

  public Player(string PlayerName)
  {
    this.PlayerName = PlayerName;
    this.PlayerId = PlayerCount;

    PlayerCount++;
  }
}

class GameManager : MonoBehaviour
{
  public static CardDeck deck;
  public static List<Player> players;
  public static Dictionary<Currency, int> tokenPiles;
  
  void Start() 
  {
    //Ititialize The Game
    Initialize();
  }

  /*
  I'm not sure I will need this
  void Update() 
  {
    
  }
  */

  public void Initialize() 
  {
    InitializeCards();
    InitializePlayers();
    InitializeTokens();

    void InitializeCards() 
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

    void InitializePlayers() 
    {
      string[] playerNames = StartScreen.Players;

      foreach(string playerName in playerNames)
      {
        Player currentPlayer = new Player();
      }
    }

    void InitializeTokens()
    {
      foreach (Currency currencyType in Enum.GetValues(typeof(Currency)))
      {
        int tokenAmount;

        //There Should Be A Different Amount Of Money Tokens Than The Rest
        switch (currencyType)
        {
          case Currency.Money:
            tokenAmount = 5;
            break;
          default:
            tokenAmount = 7;
            break;
        }

        tokenPiles.Add(currencyType, tokenAmount);
      }
    }
  }

  public void MakeMove(MoveType Move)
  {

  }
}
