using System;
using UnityEngine;

public class BallRoyaleGame : MonoBehaviour
{
    [Serializable]
    public struct TeamData
    {
        public int Score;
        public GameObject Player;
        public GameObject ScoreContainer;
    }

    public GameObject Ball;
    public TeamData Orange;
    public TeamData Blue;

    private void ResetGame()
    {
        var ballBody = Ball.GetComponent<Rigidbody2D>();
        ballBody.position = Vector2.zero;
        ballBody.velocity = Vector2.zero;
        Orange.Player.GetComponent<Rigidbody2D>().position = Vector2.zero + Vector2.left * 5;
        Blue.Player.GetComponent<Rigidbody2D>().position = Vector2.zero + Vector2.right * 5;
        Blue.Score = 5;
        Orange.Score = 5;
        
        foreach (Transform s in Orange.ScoreContainer.transform) s.gameObject.SetActive(true);
        foreach (Transform s in Blue.ScoreContainer.transform) s.gameObject.SetActive(true);
    }

    public void RecordGoal(Side side)
    {
        Side scored = Side.Blue;
        if (side == Side.Orange)
        {
            Blue.Score--;
            for (int i = 0; i < Blue.ScoreContainer.transform.childCount; i++)
            {
                Blue.ScoreContainer.transform.GetChild(i).gameObject.SetActive(i < Blue.Score);
            }
        }

        if (side == Side.Blue)
        {
            Orange.Score--;
            for (int i = 0; i < Blue.ScoreContainer.transform.childCount; i++)
            {
                Blue.ScoreContainer.transform.GetChild(i).gameObject.SetActive(i < Blue.Score);
            }

            scored = Side.Orange;
        }

        if (Orange.Score > 0 && Blue.Score > 0)
        {
            Debug.Log("Ça rentre côté " + scored);
        }
        else
        {
            Debug.Log("Fin de partie !\n" + scored.ToString() + " gagne !");
            EventManager<MonoBehaviour>.TriggerEvent("GameFinished", this);
        }
    }
    
    
    public void OnEnable()
    {
        ResetGame();
        EventManager<Side>.StartListening("Goal", RecordGoal);
    }

    public void OnDisable()
    {
        EventManager<Side>.StopListening("Goal", RecordGoal);
    }
}