using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{

    public List<GameObject> playersList;
    public List<GameObject> activePlayersList;
	
    void Awake()
    {
	playersList = new List<GameObject>();
	activePlayersList = new List<GameObject>();
    }

    void Start()
    {
	StartCoroutine(VictoryCheck());
    }

    IEnumerator VictoryCheck()
    {
	while (playersList.Count < 2) {
	    yield return null;
	}

	while (activePlayersList.Count > 1) {
	    yield return null;
	}

	Debug.Log("Victory for " + activePlayersList[0].name + "!");

	yield return null;
    }
    
    public void RegisterPlayer(GameObject player)
    {
	playersList.Add(player);
	activePlayersList.Add(player);
    }

    public void PlayerLose(GameObject player)
    {
	activePlayersList.Remove(player);
    }
}
