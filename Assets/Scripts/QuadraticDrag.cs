using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class QuadraticDrag : MonoBehaviour
{
	[SerializeField] private float mass = 1f;
	[SerializeField] private float radius = 0.1f;
	[SerializeField] private float dragCoefficient = 0.47f;
	[SerializeField] private float airDensity = 1.225f;
	[SerializeField] private Vector3 wind = Vector3.zero;

	private Rigidbody rb;
	private float area;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		area = Mathf.PI * radius * radius;
	}

	private void FixedUpdate()
	{
		Vector3 vRel = rb.linearVelocity - wind;
		float speed = vRel.magnitude;
		if (speed < 1e-6f) return;

		Vector3 drag = -0.5f * airDensity * dragCoefficient * area * speed * vRel;
		rb.AddForce(drag, ForceMode.Force);
	}

	public void SetPhysicalParams(float m, float r, float Cd, float rho, Vector3 w, Vector3 initialVelocity)
	{
		mass = Mathf.Max(0.001f, m);
		radius = Mathf.Max(0.001f, r);
		dragCoefficient = Mathf.Max(0f, Cd);
		airDensity = Mathf.Max(0f, rho);
		wind = w;
		rb.mass = mass;
		rb.linearDamping = 0f;
		rb.useGravity = true;
		rb.linearVelocity = initialVelocity;
		area = Mathf.PI * radius * radius;
	}
}


