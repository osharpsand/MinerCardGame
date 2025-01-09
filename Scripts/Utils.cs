using System;
using System.Collections.Generic;

public static class Utils 
{
  public bool CanPlayerTakeTokens(Dictionary<Currency, int> tokensWanted)
  {
    //Setup
    int amountOfTokensWanted = 0;
    int tokenTypesAvaliable = GetTokenTypesAvaliable().Count;

    foreach (KeyValuePair<Currency, int> tokensOfTypeWanted in tokensWanted)
    {
      Currency tokenWanted = tokensOfTypeWanted.key;
      int amountWanted = tokensOfTypeWanted.value;

      if (GameManager.tokenPiles[tokenWanted] < amountWanted)
      {
        return false;
      } 
      else
      {
        amountOfTokensWanted += amountWanted;
      }

    }

    

    if (amountOfTokensWanted == 2) 
    {
      //If there is only one type of token they are trying to take, they can grab two of it.
      return (tokensWanted.Count == 1);
    } 
    else if (amountOfTokensWanted == 3)
    {
      int differentTokens = tokensWanted.Count;
      //Check if there is 3 different types of tokens. If true then they can take it.
      if (differentTokensWanted == 3) { return true; }

      //Players can grab multiple of one color as long as there isn't enough different token colors for them to pick from. They can only grab all 3 of one color if it is the only color avaliable.
      if (differentTokensWanted == 2 && tokenTypesAvaliable == 2) { return true; }
      if (differentTokensWanted == 1 && tokenTypesAvaliable == 1) { return true; }

      //Return false if it was invalid.
    }

    return false;
  }

  public List<Currency> GetTokenTypesAvaliable()
  {
    List<Currency> tokenTypesAvaliable = new List<Currency>();

    foreach (KeyValuePair<Currency, int> tokensAvaliable in GameManager.tokenPiles)
    {
      //If There Is Any Tokens Of That Type, Add It To The List;
      if (tokensAvaliable.value > 0)
      {
        tokenTypesAvaliable.Add(tokensAvaliable.key);
      }
    }

    return tokenTypesAvaliable;
  }
}
