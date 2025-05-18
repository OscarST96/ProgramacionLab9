using UnityEngine;

public class FleePlayer : MonoBehaviour
{
    public Transform target;

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 dir = transform.position - target.position;
        dir.y = 0;
        GetComponent<SteeringAgent>().ApplySteering(dir.normalized);
    }
}
