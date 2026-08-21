using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentJumper
{
	private float _speed;
	private NavMeshAgent _agent;
	private MonoBehaviour _coroutineStarter;
	private Coroutine _process;
	private AnimationCurve _curve;

	public NavMeshAgentJumper(NavMeshAgent agent, float speed,  MonoBehaviour coroutineStarter, AnimationCurve curve)
	{
		_agent = agent;
		_speed = speed;
		_coroutineStarter = coroutineStarter;
		_curve = curve;
	}

	public bool InProcess => _process != null;

	public void Jump(OffMeshLinkData jumpData)
	{
		if (InProcess)
			return;

		_process = _coroutineStarter.StartCoroutine(JumpProcess(jumpData));
	}

	private IEnumerator JumpProcess(OffMeshLinkData jumpLink)
	{
		Vector3 startPos = jumpLink.startPos;
		Vector3 endPos = jumpLink.endPos;

		float distance = Vector3.Distance(startPos, endPos);
		float duration = distance / _speed;
		float progress = 0f;

		while (progress < duration)
		{
			Vector3 lerpedPosition = Vector3.Lerp(startPos, endPos, progress / duration);
			
			float yOffset = _curve.Evaluate(progress / duration);

			_agent.transform.position = lerpedPosition + Vector3.up * yOffset;

			progress += Time.deltaTime;

			yield return null;
		}

		_agent.CompleteOffMeshLink();
		_process = null;
	}
}