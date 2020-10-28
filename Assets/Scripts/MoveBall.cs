using System;
using UnityEngine;

public class MoveBall : MonoBehaviour {
    public float Speed = 3;
    public float ShapeRecoverRate = 0.05f;

    public GameObject OnPlayerCollisionFXPrefab;
    public GameObject OnGoalCollisionFXPrefab;
    public GameObject OnBallCollisionFXPrefab;

    private Rigidbody2D rigidbody2D;

    private DateTime _nextChangeTime = DateTime.Now;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (_nextChangeTime < DateTime.Now) {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        rigidbody2D.rotation = Mathf.Rad2Deg * Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, ShapeRecoverRate);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GetComponent<SpriteRenderer>().color = Color.red;
            _nextChangeTime = DateTime.Now.AddMilliseconds(150);
            transform.localScale = new Vector3(1.1f, 0.7f, 1f);
            Instantiate(OnPlayerCollisionFXPrefab, (Vector3) other.contacts[0].point, Quaternion.identity);
        }
        
        if (other.gameObject.CompareTag("Ball")) {
            Instantiate(OnBallCollisionFXPrefab, (Vector3) other.contacts[0].point, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Goal")) {
            Instantiate(OnGoalCollisionFXPrefab, this.transform.position, Quaternion.identity);
            GameManager.instance.RecordGoal(transform.position.x < 0 ? Side.Orange : Side.Blue);
        }
    }
}