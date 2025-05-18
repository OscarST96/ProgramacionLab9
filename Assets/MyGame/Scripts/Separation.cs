using UnityEngine;

public class Separation : MonoBehaviour
{
    public float radius = 3f;

    void FixedUpdate()
    {
        Collider[] neighbors = Physics.OverlapSphere(transform.position, radius);
        Vector3 separationForce = Vector3.zero;
        int count = 0;

        foreach (var neighbor in neighbors)
        {
            if (neighbor.gameObject == gameObject) continue;

            Vector3 away = transform.position - neighbor.transform.position;
            away.y = 0;

            if (away.magnitude > 0)
            {
                separationForce += away.normalized / away.magnitude;
                count++;
            }
        }

        if (count > 0)
        {
            separationForce /= count;
            GetComponent<SteeringAgent>().ApplySteering(separationForce);
        }
    }
}
