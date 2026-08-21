using UnityEngine;

public class ObjectSpawner
{
	private GameObject _prefab;

	public ObjectSpawner(GameObject prefab)
	{
		_prefab = prefab;
	}
	
	public void Spawn(Vector3 position)
	{
		GameObject.Instantiate(_prefab, position, Quaternion.identity);
	}
}