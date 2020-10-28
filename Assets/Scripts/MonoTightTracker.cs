using UnityEngine;

public class MonoTightTracker : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset;

    void LateUpdate()
    {
        transform.position = Target.position + Offset;
    }
}