using System;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float Speed = 3;

    private DateTime _nextOrientation = DateTime.Now;
    private Vector3 _orientation = Vector3.right;

    void Update()
    {
        if (_nextOrientation < DateTime.Now)
        {
            _orientation = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
            _nextOrientation = DateTime.Now.Add(TimeSpan.FromSeconds(1));
        }

        transform.position = transform.position + _orientation * (Speed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(_orientation.y, _orientation.x));
    }
}