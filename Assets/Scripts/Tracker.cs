using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public GameObject Target;
    public GameObject Target2;

    public Vector2 DeadWindow;

    public float FollowSpeed = 0.02f;
    private Rect _rect;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_rect.center, _rect.size);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetPos = Vector3.Lerp(Target.transform.position, Target2.transform.position, 0.5f);
        _rect = new Rect((Vector2) transform.position - DeadWindow * 0.5f, DeadWindow);
        if (!_rect.Contains(targetPos))
        {
            var z = transform.position.z;
            var nextPos = Vector3.Lerp(transform.position, targetPos, 0.02f);
            nextPos.z = z;
            transform.position = nextPos;
        }

        var distance = Vector3.Distance(Target.transform.position, Target2.transform.position);
        _camera.orthographicSize = Mathf.Clamp(distance * 0.5f + 1, 2f, 10f);
    }
}