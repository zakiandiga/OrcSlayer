using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLogHandler : MonoBehaviour
{
    private int maxMessages = 10;
    private int lineNumber = 0;
    private List<LogMessage> messageList = new List<LogMessage>();

    [SerializeField] private GameObject textObject;
    [SerializeField] private Transform logParentPanel;

    private void OnEnable()
    {
        //Player.OnPlayerTakesDamage += PlayerTakesDamage;
        //Player.OnPlayerDies += PlayerDies;
        EnemyBehaviour.OnEnemyTakesDamage += EnemyTakesDamage;
        EnemyBehaviour.OnEnemyDies += EnemyDies;
    }

    private void OnDisable()
    {
        //Player.OnPlayerTakesDamage -= PlayerTakesDamage;
        //Player.OnPlayerDies -= PlayerDies;
        EnemyBehaviour.OnEnemyTakesDamage -= EnemyTakesDamage;
        EnemyBehaviour.OnEnemyDies -= EnemyDies;
    }

    private void PlayerTakesDamage(int damage)
    {
        ShowMessages("PLAYER: Ugh, I take " + damage.ToString() + "!");        
    }
    
    private void PlayerDies(GameObject player)
    {
        ShowMessages("PLAYER: Ugh, I die!");
        //Player.OnPlayerTakesDamage -= PlayerTakesDamage;
        Player.OnPlayerDies -= PlayerDies;
    }

    private void EnemyTakesDamage(GameObject enemy, int damage)
    {
        ShowMessages(enemy.name.ToString() + ": Ouch, player hit me with " + damage.ToString() + " damage!");
    }   
    
    private void EnemyDies(GameObject enemy)
    {

    }

    private void ShowMessages(string text)
    {
        lineNumber++;

        if (messageList.Count >= maxMessages)  //Removing the oldest message on memory
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        LogMessage newMessage = new LogMessage();
        newMessage.text = "Line_" + lineNumber.ToString() + " " + text;

        GameObject newText = Instantiate(textObject, logParentPanel.transform);

        newMessage.textObject = newText.GetComponentInChildren<Text>();
        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class LogMessage
{
    public string text;
    public Text textObject;
}
