using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _muzzle;
	[SerializeField] private TrajectoryRenderer _trajectory;
	[SerializeField] private GameObject _projectilePrefab;

	[Header("Launch")]
	[SerializeField] private float _launchSpeed = 25f;
	[SerializeField] private Vector3 _localLaunchAxis = Vector3.forward;

	[Header("Random projectile params")]
	[SerializeField] private Vector2 _massRange = new Vector2(0.5f, 5f);
	[SerializeField] private Vector2 _radiusRange = new Vector2(0.05f, 0.25f);
	[SerializeField] private float _dragCoefficient = 0.47f;
	[SerializeField] private float _airDensity = 1.225f;
	[SerializeField] private Vector3 _wind = Vector3.zero;

	[Header("Movement")]
	[SerializeField] private float _moveSpeed = 6f;
	[SerializeField] private float _turnSpeedDeg = 90f;

	[SerializeField] private float _elevateSpeedDeg = 60f;

	private void Update()
	{
		HandleMovement();

		if (_muzzle != null && _trajectory != null)
		{
			float mass = Random.Range(_massRange.x, _massRange.y);
			float radius = Random.Range(_radiusRange.x, _radiusRange.y);
			_trajectory.SetPhysicalParams(mass, radius, _dragCoefficient, _airDensity, _wind);
			Vector3 launchDir = _muzzle.TransformDirection(_localLaunchAxis).normalized;
			Vector3 v0 = launchDir * _launchSpeed;
			_trajectory.DrawPreview(_muzzle.position, v0, true);
			if (Keyboard.current.spaceKey.wasPressedThisFrame)
			{
				Fire(v0, mass, radius);
			}
		}
	}

	private void HandleMovement()
	{
		if (Keyboard.current == null) return;
		Vector2 moveAxis = Vector2.zero;
		if (Keyboard.current.wKey.isPressed) moveAxis.y += 1f;
		if (Keyboard.current.sKey.isPressed) moveAxis.y -= 1f;
		if (Keyboard.current.dKey.isPressed) moveAxis.x += 1f;
		if (Keyboard.current.aKey.isPressed) moveAxis.x -= 1f;

		Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
		Vector3 delta = right * moveAxis.x * _moveSpeed * Time.deltaTime;
		transform.position += delta;

		if (_muzzle != null && Mathf.Abs(moveAxis.y) > 0f)
		{
			_muzzle.Rotate(Vector3.forward, -moveAxis.y * _elevateSpeedDeg * Time.deltaTime, Space.Self);
		}

		float yaw = 0f;
		if (Keyboard.current.qKey.isPressed) yaw -= 1f;
		if (Keyboard.current.eKey.isPressed) yaw += 1f;
		if (Mathf.Abs(yaw) > 0f)
		{
			transform.Rotate(Vector3.up, yaw * _turnSpeedDeg * Time.deltaTime, Space.World);
		}
	}

	private void Fire(Vector3 initialVelocity, float mass, float radius)
	{
		if (_projectilePrefab == null || _muzzle == null) return;
		GameObject newCore = Instantiate(_projectilePrefab, _muzzle.position, Quaternion.identity);
		var rb = newCore.GetComponent<Rigidbody>();
		if (rb == null) rb = newCore.AddComponent<Rigidbody>();
		var col = newCore.GetComponent<SphereCollider>();
		if (col == null) col = newCore.AddComponent<SphereCollider>();
		col.radius = radius;

		var qd = newCore.GetComponent<QuadraticDrag>();
		if (qd == null) qd = newCore.AddComponent<QuadraticDrag>();
		qd.SetPhysicalParams(mass, radius, _dragCoefficient, _airDensity, _wind, initialVelocity);
	}
}


