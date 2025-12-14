using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class BoatAutoPilot : MonoBehaviour
{
    [Range(0, 10)]
    public float maxSpeed = 3f;

    [Range(0.1f, 45f)]
    public float steeringSpeed = 4.5f;

    [Range(.01f, .5f)]
    public float maxForce = .03f;

    [Range(1, 10)]
    public float neighborhoodRadius = 4f;

    [Range(0.1f, 10f)]
    public float separationRadius = 2.4f;

    [Range(0, 3)]
    public float separationAmount = 1.1f;

    [Range(0, 3)]
    public float cohesionAmount = 0.3f;

    [Range(0, 3)]
    public float alignmentAmount = 0.5f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // On récupère notre vélocité initiale à partir de notre orientation dans le monde;
        velocity = transform.forward;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, neighborhoodRadius);
        List<BoatAutoPilot> boats = colliders.Select(collider => collider.GetComponent<BoatAutoPilot>()).ToList();
        boats.Remove(this);

        Vector3 acceleration = ComputeAcceleration(boats);
        UpdateVelocity(acceleration);
        UpdatePosition(velocity);
        UpdateRotation(velocity);
    }

    private Vector3 ComputeAcceleration(IEnumerable<BoatAutoPilot> boats)
    {
        Vector3 acceleration = Vector3.zero;

        acceleration += ComputeAlignment(boats) * alignmentAmount;
        acceleration += ComputeSeparation(boats) * separationAmount;
        acceleration += ComputeCohesion(boats) * cohesionAmount;

        return acceleration;
    }

    private void UpdateVelocity(Vector3 acceleration)
    {
        velocity += acceleration;
        velocity = LimitMagnitude(velocity, maxSpeed);
    }

    private void UpdatePosition(Vector3 velocity)
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    private void UpdateRotation(Vector3 velocity)
    {
        //transform.forward = velocity;
        transform.forward = Vector3.RotateTowards(transform.forward, velocity, Time.deltaTime * steeringSpeed, float.MaxValue);
    }

    private Vector3 ComputeAlignment(IEnumerable<BoatAutoPilot> boats)
    {
        var velocity = Vector3.zero;
        if (!boats.Any()) return velocity;

        foreach (var boat in boats)
        {
            velocity += boat.velocity;
        }

        velocity /= boats.Count();
        var steer = Steer(velocity.normalized * maxSpeed);
        return steer;
    }

    private Vector3 ComputeCohesion(IEnumerable<BoatAutoPilot> boats)
    {
        if (!boats.Any()) return Vector3.zero;

        var sumPositions = Vector3.zero;
        foreach (var boat in boats)
        {
            sumPositions += boat.transform.position;
        }

        var average = sumPositions / boats.Count();
        var direction = average - transform.position;
        var steer = Steer(direction.normalized * maxSpeed);
        return steer;
    }

    private Vector3 ComputeSeparation(IEnumerable<BoatAutoPilot> boats)
    {
        var direction = Vector3.zero;
        boats = boats.Where(boat => Vector3.Distance(transform.position, boat.transform.position) <= separationRadius);
        if (!boats.Any()) return direction;

        foreach (var boat in boats)
        {
            Vector3 difference = transform.position - boat.transform.position;
            direction += difference.normalized;
        }

        direction /= boats.Count();
        var steer = Steer(direction.normalized * maxSpeed);
        return steer;
    }

    private Vector3 Steer(Vector3 desiredVelocity)
    {
        var steer = desiredVelocity - velocity;
        steer = LimitMagnitude(steer, maxForce);
        return steer;
    }

    private Vector3 LimitMagnitude(Vector3 baseVector, float maxMagnitude)
    {
        if (baseVector.sqrMagnitude > maxMagnitude * maxMagnitude)
        {
            baseVector = baseVector.normalized * maxMagnitude;
        }

        return baseVector;
    }

    private void OnDrawGizmosSelected()
    {
        // Neighborhood radius.
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, neighborhoodRadius);

        // Separation radius.
        Gizmos.color = Color.salmon;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
}