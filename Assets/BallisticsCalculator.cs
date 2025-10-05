using System.Collections;
using UnityEngine;

public class BallisticsCalculator : MonoBehaviour
{
    [SerializeField] private float muzzleVelocity = 50f;
    [SerializeField] private float angle = 45f;
    [SerializeField] private Transform cannon;
    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private GameObject core;
    [SerializeField] private int pointsCount;

    // x = v * cos * t
    // x = y * sin * t - 0.5 * g * t^2

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (true)
            {
                FireProjectile();
            }
        }

        DrawTrajectory(angle);

    }

    private Vector3 CalculateVelocity(float angle)
    {
        var vx = muzzleVelocity * Mathf.Cos(angle * Mathf.Deg2Rad);
        var vy = muzzleVelocity * Mathf.Sin(angle * Mathf.Deg2Rad);
        return -cannon.right * vx + cannon.up * vy;
    }

    private void DrawTrajectory(float angle)
    {
        trajectoryLine.positionCount = pointsCount;
        var StartPoint = cannon.position;
        var _velocity = CalculateVelocity(angle);

        for(var i = 0; i < pointsCount; i++)
        {
            var t = i * 0.1f;
            var point = StartPoint + _velocity * t;
            point.y -= 0.5f * 9.81f * t * t;
            trajectoryLine.SetPosition(i, point);
        }
    }

    private void FireProjectile()
    {
        GameObject newCore = Instantiate(core, cannon.position, Quaternion.identity);
        Rigidbody rb = newCore.GetComponent<Rigidbody>();

        if (rb)
        {
            Vector3 velocity = -cannon.right * muzzleVelocity;
            rb.linearVelocity = velocity;
        }
    }
}
