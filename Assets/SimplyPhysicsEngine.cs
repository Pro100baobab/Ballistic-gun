using UnityEngine;

[RequireComponent(typeof(ForceVisulizers))]
public class SimplyPhysicsEngine : MonoBehaviour
{
    private const float _fallAcceleration = 9.81f;

    [SerializeField] private float _mass = 1f;
    [SerializeField] private bool _useGravity = true;
    private Vector3 minForce;
    [SerializeField] private Vector3 _windForce;
    

    private Vector3 _gravity;
    private Vector3 _netForce;
    private Vector3 _velocity = Vector3.zero;

    private ForceVisulizers _forceVisulizers;

    private void Start()
    {
        _forceVisulizers = GetComponent<ForceVisulizers>();
    }

    private void FixedUpdate()
    {

        _netForce = Vector3.zero;
        _forceVisulizers.ForceClear();

        if (_useGravity)
        {
            _gravity = Vector3.down * _mass * _fallAcceleration;
            ApplyForce(_gravity, Color.cyan, "Gravity");
        }

        ApplyForce(_windForce, Color.yellow, "WindForce");
        _forceVisulizers.AddForce(_netForce, Color.blue, "NewForce");


        Vector3 acceleration = _netForce / _mass;
        _velocity += acceleration * Time.fixedDeltaTime;
        transform.position += _velocity * Time.deltaTime;

    }

    private void ApplyForce(Vector3 force, Color color, string name)
    {
        _netForce += force;
        _forceVisulizers.AddForce(force, color, name);

    }
}
