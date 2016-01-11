using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{

    public List<GameObject> playersList;
    public List<GameObject> activePlayersList;
    public Text notifiee;
    public string nextSceneName;
    public GameObject playerInfoPrefab;
    public HorizontalLayoutGroup playerInfoLayout;

    void Reset()
    {
        nextSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

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
        yield return new WaitForSeconds(0.5f);
        var victoryMessage = "Game Over: Draw";
        if (activePlayersList.Count == 1) victoryMessage = "Victory for " + activePlayersList[0].name + "!";
        notifiee.text = victoryMessage;
        Debug.Log(victoryMessage);
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        yield return null;
    }

    public void invokeMenu()
    {
        Debug.Log("somebody wants a menu");
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