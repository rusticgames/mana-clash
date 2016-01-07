using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{

    public List<GameObject> playersList;
    public List<GameObject> activePlayersList;
    public UnityEngine.UI.Text notifiee;
    public string nextSceneName;

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
        if(activePlayersList.Count == 1) victoryMessage = "Victory for " + activePlayersList[0].name + "!";
        notifiee.text = victoryMessage;
        Debug.Log(victoryMessage);
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
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
