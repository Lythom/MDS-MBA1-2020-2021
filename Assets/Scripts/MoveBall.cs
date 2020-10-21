using System;
using UnityEngine;

public class MoveBall : MonoBehaviour {
    public float Speed = 3;
    public float ShapeRecoverRate = 0.05f;

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
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            GetComponent<SpriteRenderer>().color = Color.red;
            _nextChangeTime = DateTime.Now.AddMilliseconds(150);
            transform.localScale = new Vector3(1.1f, 0.7f, 1f);
        }
    }
}