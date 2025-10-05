using UnityEngine;

// Attach to projectile prefab to count hits when colliding with targets
public class ProjectileHit : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider == null) return;
		if (collision.collider.attachedRigidbody != null && collision.collider.gameObject != this.gameObject)
		{
			// Consider anything with Rigidbody (excluding self) as a potential target
			HitCounter.Instance?.RegisterHit();
		}
	}
}


