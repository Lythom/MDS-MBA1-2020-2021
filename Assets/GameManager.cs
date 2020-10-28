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

    private float nextReset = Mathf.Infinity;

    private void Start() {
        instance = this;
        ResetPosition();
    }

    private void ResetPosition() {
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
        if (side == Side.Orange) {
            Blue.Score++;
            if (Blue.ScoreContainer.transform.childCount > Blue.Score) {
                Blue.ScoreContainer.transform.GetChild(Blue.Score).gameObject.SetActive(true);
            }
        }
        if (side == Side.Blue) {
            Orange.Score++;
            if (Orange.ScoreContainer.transform.childCount > Orange.Score) {
                Orange.ScoreContainer.transform.GetChild(Orange.Score).gameObject.SetActive(true);
            }
        }
        nextReset = 3f;
    }
}
