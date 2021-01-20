using System;
using UnityEngine;

public class FirstToFiveGame : MonoBehaviour
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

    private float nextReset = Mathf.Infinity;
    private bool reseting = false;

    private void ResetPosition()
    {
        var ballBody = Ball.GetComponent<Rigidbody2D>();
        ballBody.position = Vector2.zero;
        ballBody.velocity = Vector2.zero;
        Orange.Player.GetComponent<Rigidbody2D>().position = Vector2.zero + Vector2.left * 3;
        Blue.Player.GetComponent<Rigidbody2D>().position = Vector2.zero + Vector2.right * 3;
    }

    private void Update()
    {
        if (reseting)
        {
            nextReset -= Time.deltaTime;
            if (nextReset < 0)
            {
                ResetPosition();
                nextReset = Mathf.Infinity;
                reseting = false;
            }
        }
    }

    public void RecordGoal(Side side)
    {
        if (reseting) return; // no record if we are between 2 actions
        Side scored = Side.Blue;
        if (side == Side.Orange)
        {
            Blue.Score++;
            if (Blue.ScoreContainer.transform.childCount > Blue.Score - 1)
            {
                Blue.ScoreContainer.transform.GetChild(Blue.Score - 1).gameObject.SetActive(true);
            }
        }

        if (side == Side.Blue)
        {
            Orange.Score++;
            if (Orange.ScoreContainer.transform.childCount > Orange.Score - 1)
            {
                Orange.ScoreContainer.transform.GetChild(Orange.Score - 1).gameObject.SetActive(true);
            }

            scored = Side.Orange;
        }

        if (Orange.Score < 5 && Blue.Score < 5)
        {
            Debug.Log("Buuuuuuut !\n" + scored.ToString() + " marque !");
        }
        else
        {
            Debug.Log("Fin de partie !\n" + scored.ToString() + " gagne !");
            EventManager<MonoBehaviour>.TriggerEvent("GameFinished", this);
        }

        nextReset = 3f;
        reseting = true;
    }

    public void OnEnable()
    {
        Blue.Score = 0;
        Orange.Score = 0;
        foreach (Transform s in Orange.ScoreContainer.transform) s.gameObject.SetActive(false);
        foreach (Transform s in Blue.ScoreContainer.transform) s.gameObject.SetActive(false);
        ResetPosition();
        EventManager<Side>.StartListening("Goal", RecordGoal);
    }

    public void OnDisable()
    {
        EventManager<Side>.StopListening("Goal", RecordGoal);
    }
}