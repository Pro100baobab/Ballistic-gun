using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Target Prefab")]
    [SerializeField] private GameObject _targetPrefab;

    [Header("Spawn")]
    [SerializeField] private Vector3 _areaSize = new Vector3(20f, 0f, 20f);
    [SerializeField] private int _initialCount = 5;
    [SerializeField] private float _spawnHeight = 1.0f;

    [Header("Random target params")]
    [SerializeField] private Vector2 _massRange = new Vector2(0.5f, 5f);
    [SerializeField] private Vector2 _radiusRange = new Vector2(0.1f, 0.4f);
    [SerializeField] private Vector2 _horizontalSpeedRange = new Vector2(1.0f, 4.0f);

    private void Start()
    {
        for (int i = 0; i < _initialCount; i++)
        {
            SpawnOne();
        }
    }

    public GameObject SpawnOne()
    {
        if (_targetPrefab == null)
        {
            // Create simple sphere if no prefab provided
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Target";
            SetupTarget(sphere);
            return sphere;
        }

        GameObject go = Instantiate(_targetPrefab);
        SetupTarget(go);
        return go;
    }

    private void SetupTarget(GameObject go)
    {
        float radius = Random.Range(_radiusRange.x, _radiusRange.y);
        float mass = Random.Range(_massRange.x, _massRange.y);
        Vector3 localScale = Vector3.one * (radius * 2f);
        go.transform.localScale = localScale;

        Vector3 center = transform.position;
        Vector3 half = _areaSize * 0.5f;

        // ƒобавл€ем случайное смещение по высоте от -5 до 5
        float randomHeightOffset = Random.Range(-5f, 5f);
        Vector3 pos = new Vector3(
            Random.Range(center.x - half.x, center.x + half.x),
            center.y + _spawnHeight + randomHeightOffset,
            Random.Range(center.z - half.z, center.z + half.z)
        );
        go.transform.position = pos;

        var col = go.GetComponent<SphereCollider>();
        if (col == null) col = go.AddComponent<SphereCollider>();
        col.isTrigger = false;
        col.radius = 0.5f;

        var rb = go.GetComponent<Rigidbody>();
        if (rb == null) rb = go.AddComponent<Rigidbody>();

        rb.mass = mass;
        //rb.useGravity = true;
        rb.useGravity = false;
        rb.linearDamping = 0f;

        // «адаем движение в направлении -Vector3.right (локальна€ ось X)
        Vector3 dir = -Vector3.right;
        float speed = Random.Range(_horizontalSpeedRange.x, _horizontalSpeedRange.y);
        rb.linearVelocity = dir * speed;
    }
}