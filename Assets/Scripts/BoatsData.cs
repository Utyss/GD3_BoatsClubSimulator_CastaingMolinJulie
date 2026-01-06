using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "BoatsData", menuName = "Scriptable Objects/BoatsData")]
public class BoatsData : ScriptableObject
{
	//Import des variables des bateaux pour les modifier en temps réel
	[SerializeField]
	[Range(0, 10)]
	public float maxSpeed = 6f;

    [SerializeField]
    [Range(0.1f, 45f)]
	public float steeringSpeed = 4.5f;

    [SerializeField]
    [Range(.01f, .5f)]
	public float maxForce = .03f;

    [SerializeField]
    [Range(1, 10)]
	public float neighborhoodRadius = 4f;

    [SerializeField]
    [Range(0.1f, 10f)]
	public float separationRadius = 2.4f;

    [SerializeField]
    [Range(0, 3)]
	public float separationAmount = 1.1f;

    [SerializeField]
    [Range(0, 3)]
	public float cohesionAmount = 0.3f;

    [SerializeField]
    [Range(0, 3)]
	public float alignmentAmount = 0.5f;

        //Raccord entre les variables et le scriptable object pour les modifier sans ouvrir le code
    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    public float SteeringSpeed
    {
        get { return steeringSpeed; }
    }

    public float MaxForce
    {
        get { return maxForce; }
    }

    public float NeighborhoodRadius
    {
        get { return neighborhoodRadius; }
    }

    public float SeparationRadius
    {
        get { return separationRadius; }
    }

    public float SeparationAmount
    {
        get { return separationAmount; }
    }

    public float CohesionAmount
    {
        get { return cohesionAmount; }
    }

    public float AlignmentAmount
    {
        get { return alignmentAmount; }
    }
}
