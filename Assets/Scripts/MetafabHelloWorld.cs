using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MetafabSdk;
using UnityEngine;

public class MetafabHelloWorld : MonoBehaviour
{
	async UniTaskVoid Start()
	{
		Debug.Log("Authing game...");
		var response = await Metafab.GamesApi.AuthGame(Config.Email, Config.Password, default);
		Debug.Log($"{response}");
	}
}