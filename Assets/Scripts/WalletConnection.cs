using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetafabSdk;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class WalletConnection : MonoBehaviour
{
	public Chain chain = Chain.MATICMUMBAI;

	public string playerUsername = "sample-player";
	public string playerPassword = "password";

	public string currencyName = "My Test Coin";
	public string currencySymbol = "$MYTEST";

	public async void GetWallet()
    {
		Metafab.SecretKey = "game_sk_Xn0Z3fU3oKZZhcvPkGWZJs158rvWq3hxzo/YdsuPGDfsl7r4HadjQKR7orATGzwr";
		Metafab.Password = "Virtucious@101";
		Debug.Log($"Getting player...");
		var players = await Metafab.PlayersApi.GetPlayers();
		var player = players[players.Count - 1];
		Debug.Log($"Got player {player}.");
	}


	
}
