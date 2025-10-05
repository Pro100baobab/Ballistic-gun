using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
	[Header("Отрисовка")]
	[SerializeField] private int _pointsCount = 60;
	[SerializeField] private float _timeStep = 0.05f;
	[SerializeField] private float _widthLine = 0.02f;

	[Header("Физика воздуха")]
	[SerializeField] private float _mass = 1f;                // кг
	[SerializeField] private float _radius = 0.1f;            // м
	[SerializeField] private float _dragCoefficient = 0.47f;  // сфера
	[SerializeField] private float _airDensity = 1.225f;      // кг/м^3
	[SerializeField] private Vector3 _wind = Vector3.zero;    // м/с

	[SerializeField] private bool _useAir = true;
	[SerializeField] private Transform _muzzle;
	[SerializeField] private float _launchSpeed = 20f;
	[SerializeField] private Vector3 _localLaunchAxis = Vector3.forward;

	private LineRenderer _line;
	private float _area; // pi r^2

	public Transform Muzzle => _muzzle;

	private void Awake()
	{
		_line = GetComponent<LineRenderer>();
		_line.useWorldSpace = true;
		_line.startWidth = _widthLine;
		_line.endWidth = _widthLine;
		_line.material = new Material(Shader.Find("Sprites/Default"));
		_area = Mathf.PI * _radius * _radius;
	}

	public void SetPhysicalParams(float m, float r, float Cd, float rho, Vector3 w)
	{
		_mass = Mathf.Max(0.001f, m);
		_radius = Mathf.Max(0.001f, r);
		_dragCoefficient = Mathf.Max(0f, Cd);
		_airDensity = Mathf.Max(0f, rho);
		_wind = w;
		_area = Mathf.PI * _radius * _radius;
	}

	public void DrawVacuum(Vector3 startPosition, Vector3 startVelocity)
	{
		if (_pointsCount < 2) _pointsCount = 2;
		_line.positionCount = _pointsCount;
		for (int i = 0; i < _pointsCount; i++)
		{
			float t = i * _timeStep;
			Vector3 p = startPosition + startVelocity * t + 0.5f * Physics.gravity * (t * t);
			_line.SetPosition(i, p);
		}
	}

	public void DrawWithAirEuler(Vector3 startPosition, Vector3 startVelocity)
	{
		Vector3 p = startPosition;
		Vector3 v = startVelocity;
		if (_pointsCount < 2) _pointsCount = 2;
		_line.positionCount = _pointsCount;

		_line.SetPosition(0, p);
		for (int i = 1; i < _pointsCount; i++)
		{
			// ускорение: g + Fd/m, Fd = -0.5*rho*Cd*A*|v_rel|*v_rel
			Vector3 vRel = v - _wind;
			float speed = vRel.magnitude;
			Vector3 drag = speed > 1e-6f ? (-0.5f * _airDensity * _dragCoefficient * _area * speed) * vRel : Vector3.zero;
			Vector3 a = Physics.gravity + drag / _mass;

			v += a * _timeStep;
			Vector3 nextP = p + v * _timeStep;

			// Обрезка превью по столкновению
			Vector3 dir = nextP - p;
			float dist = dir.magnitude;
			if (dist > 1e-6f)
			{
				if (Physics.SphereCast(p, _radius, dir.normalized, out RaycastHit hit, dist))
				{
					_line.SetPosition(i, hit.point);
					_line.positionCount = i + 1;
					return;
				}
			}

			p = nextP;
			_line.SetPosition(i, p);
		}
	}

	private void Update()
	{
		if (_muzzle == null) return;
		Vector3 dir = _muzzle.TransformDirection(_localLaunchAxis).normalized;
		Vector3 v0 = dir * _launchSpeed;
		if (_useAir) DrawWithAirEuler(_muzzle.position, v0);
		else DrawVacuum(_muzzle.position, v0);
	}

	public void DrawPreview(Vector3 startPosition, Vector3 startVelocity, bool withAir)
	{
		if (withAir) DrawWithAirEuler(startPosition, startVelocity);
		else DrawVacuum(startPosition, startVelocity);
	}
}


