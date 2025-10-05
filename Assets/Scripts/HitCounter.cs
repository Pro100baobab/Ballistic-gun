using UnityEngine;

public class HitCounter : MonoBehaviour
{
	public static HitCounter Instance { get; private set; }
	[SerializeField] private int _hits;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void RegisterHit()
	{
		_hits++;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 300, 20), $"Hits: {_hits}");
	}
}


