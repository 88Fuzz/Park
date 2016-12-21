using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CenterOfMassChanger : MonoBehaviour
{
    public Vector3 centerOfMass;
	void Start ()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass;
	}
}