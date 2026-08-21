using System.Collections;
using UnityEngine;

public class ExplosionTimerTrap : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private float _timeToActivate;
	[SerializeField] private LayerMask _triggerLayerMasks;
	[SerializeField] private ParticleSystem _explosionParticle;

	private MeshRenderer _mesh;

	private bool _isActivated;
	private bool _isExploded;

	private float ExplosionRadius => GetComponent<SphereCollider>().radius * transform.lossyScale.x;
	public bool IsExploded => _isExploded;
	public bool IsActivated => _isActivated;

	public void Awake()
	{
		_mesh = GetComponent<MeshRenderer>();
		_isActivated = false;
		_isExploded = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		IDamagable damagable = other.GetComponent<IDamagable>();

		if (damagable != null && _isActivated == false)
		{
			StartCoroutine(Activate());
		}
	}

	private IEnumerator Activate()
	{
		_isActivated = true;

		yield return new WaitForSeconds(_timeToActivate);

		Explode();

		_mesh.enabled = false;
		_explosionParticle.Play();
		_isExploded = true;

		yield return new WaitWhile(IsParticlePlaying);

		Destroy(gameObject);
	}

	private bool IsParticlePlaying() => _explosionParticle.isPlaying;

	private void Explode()
	{
		RaycastHit[] hits = Physics.SphereCastAll(
			transform.position,
			ExplosionRadius, 
			transform.forward,
			0.0f, 
			_triggerLayerMasks);

		foreach (RaycastHit hit in hits)
		{
			hit.transform.GetComponent<IDamagable>()?.TakeDamage(_damage);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, ExplosionRadius);
	}
}