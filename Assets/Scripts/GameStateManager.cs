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
        yield return new WaitUntil(() => playersList.Count > 1);
        yield return new WaitWhile(() => activePlayersList.Count > 1);
        yield return new WaitForSeconds(1f);
        var victoryMessage = "Game Over: Draw";
        if(activePlayersList.Count == 1) victoryMessage = "Victory for " + activePlayersList[0].name + "!";
        Debug.Log(victoryMessage);
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
