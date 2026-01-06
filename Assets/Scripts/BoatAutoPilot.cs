using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatAutoPilot : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    public BoatsData data;

    public Vector3 acceleration;

    private void Start()
    {
        // On récupère notre vélocité initiale à partir de notre orientation dans le monde;
        velocity = transform.forward;
    }

    private void Update()
    {
        var boidColliders = Physics.OverlapSphere(transform.position, data.neighborhoodRadius);
        var boids = boidColliders.Select(boidcollider => boidcollider.GetComponent<BoatAutoPilot>()).ToList();
        boids.Remove(this);

        ComputeAcceleration(boids);
        UpdateVelocity(acceleration);
        UpdatePosition(velocity);
        UpdateRotation(velocity);
    }

    private Vector3 ComputeAcceleration(IEnumerable<BoatAutoPilot> boats)
    {
        Vector3 acceleration = Vector3.zero;

        acceleration += ComputeAlignment(boats) * data.alignmentAmount;
        acceleration += ComputeSeparation(boats) * data.separationAmount;
        acceleration += ComputeCohesion(boats) * data.cohesionAmount;

        return acceleration;
    }

    private void UpdateVelocity(Vector3 acceleration)
    {
        velocity += acceleration;
        velocity = LimitMagnitude(velocity, data.maxSpeed);
    }

    private void UpdatePosition(Vector3 velocity)
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    private void UpdateRotation(Vector3 velocity)
    {
        //transform.forward = velocity;
        transform.forward = Vector3.RotateTowards(transform.forward, velocity, Time.deltaTime * data.steeringSpeed, float.MaxValue);
    }

    private Vector3 ComputeAlignment(IEnumerable<BoatAutoPilot> boids)
    {
        var velocity = Vector3.zero;
        if (!boids.Any()) return velocity;

        foreach (var boid in boids)
        {
            velocity += boid.velocity;
        }

        velocity /= boids.Count();
        var steer = Steer(velocity.normalized * data.maxSpeed);
        return steer;
    }

    private Vector3 ComputeCohesion(IEnumerable<BoatAutoPilot> boids)
    {
        if (!boids.Any()) return Vector3.zero;

        var sumPositions = Vector3.zero;
        foreach (var boid in boids)
        {
            sumPositions += boid.transform.position;
        }

        var average = sumPositions / boids.Count();
        var direction = average - transform.position;
        var steer = Steer(direction.normalized * data.maxSpeed);
        return steer;
    }

    private Vector3 ComputeSeparation(IEnumerable<BoatAutoPilot> boids)
    {
        var direction = Vector3.zero;
        boids = boids.Where(boat => Vector3.Distance(transform.position, boat.transform.position) <= data.separationRadius);
        if (!boids.Any()) return direction;

        foreach (var boid in boids)
        {
            Vector3 difference = transform.position - boid.transform.position;
            direction += difference.normalized;
        }

        direction /= boids.Count();
        var steer = Steer(direction.normalized * data.maxSpeed);
        return steer;
    }

    private Vector3 Steer(Vector3 desiredVelocity)
    {
        var steer = desiredVelocity - velocity;
        steer = LimitMagnitude(steer, data.maxForce);
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
        // Skip if there's no BoatManager (e.g. in Prefab Edit mode)
        if (BoatManager.Singleton == null)
        {
            return;
        }

        // Neighborhood radius.
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, data.neighborhoodRadius);

        // Separation radius.
        Gizmos.color = Color.salmon;
        Gizmos.DrawWireSphere(transform.position, data.separationRadius);
    }
}