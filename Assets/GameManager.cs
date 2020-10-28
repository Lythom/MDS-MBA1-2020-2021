using System;
using UnityEngine;

public enum Side {
    Orange,
    Blue
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Serializable]
    public struct TeamData {
        public int Score;
        public GameObject Player;
        public GameObject ScoreContainer;
    }

    public GameObject Ball;
    public TeamData Orange;
    public TeamData Blue;

    public GameObject GameOverText;
    public GameObject GoalText;

    private float nextReset = Mathf.Infinity;

    private void Start() {
        instance = this;
        ResetPosition();
    }

    private void ResetPosition() {
        GameOverText.SetActive(false);
        GoalText.SetActive(false);
        var ballBody = Ball.GetComponent<Rigidbody2D>();
        ballBody.position = Vector2.zero;
        ballBody.velocity = Vector2.zero;
        Orange.Player.GetComponent<Rigidbody2D>().position = Vector2.zero + Vector2.left * 3;
        Blue.Player.GetComponent<Rigidbody2D>().position = Vector2.zero + Vector2.right * 3;
    }

    private void Update() {
        nextReset -= Time.deltaTime;
        if (nextReset < 0) {
            ResetPosition();
            nextReset = Mathf.Infinity;
        }
    }

    public void RecordGoal(Side side) {
        Side scored = Side.Blue;
        if (side == Side.Orange) {
            Blue.Score++;
            if (Blue.ScoreContainer.transform.childCount > Blue.Score - 1) {
                Blue.ScoreContainer.transform.GetChild(Blue.Score - 1).gameObject.SetActive(true);
            }
        }
        if (side == Side.Blue) {
            Orange.Score++;
            if (Orange.ScoreContainer.transform.childCount > Orange.Score - 1) {
                Orange.ScoreContainer.transform.GetChild(Orange.Score - 1).gameObject.SetActive(true);
            }
            scored = Side.Orange;
        }
        if (Orange.Score < 5 && Blue.Score < 5) {
            GoalText.SetActive(true);
            GoalText.GetComponent<TextMesh>().text = "Buuuuuuut !\n" + scored.ToString() + " marque !";
        } else {
            GameOverText.SetActive(true);
            GameOverText.GetComponent<TextMesh>().text = "Fin de partie !\n" + scored.ToString() + " gagne !";
            Blue.Score = 0;
            Orange.Score = 0;
            foreach (Transform s in Orange.ScoreContainer.transform) s.gameObject.SetActive(false);
            foreach (Transform s in Blue.ScoreContainer.transform) s.gameObject.SetActive(false);
        }
        nextReset = 3f;
    }
}
