using UnityEngine;

public class SingletonMB<T> : MonoBehaviour where T : SingletonMB<T>
{
	public static T Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this as T;
		}
	}
}
