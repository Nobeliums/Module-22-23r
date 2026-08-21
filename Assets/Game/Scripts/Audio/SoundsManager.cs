using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
	[SerializeField] private AudioClip _explosionSFX;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private List<ExplosionTimerTrap> _exploders;
	
	private Dictionary<ExplosionTimerTrap, bool> _isExplodersPlayed;

	private void Awake()
	{
		_isExplodersPlayed = new Dictionary<ExplosionTimerTrap, bool>();
		foreach (ExplosionTimerTrap exploder  in _exploders)
		{
			_isExplodersPlayed.Add(exploder, false);
		}
	}

	private void Update()
	{
		CheckIsExplodersNeedSoundPlay();
	}

	private void CheckIsExplodersNeedSoundPlay()
	{
		foreach (ExplosionTimerTrap exploder in _exploders)
		{
			if (exploder.IsExploded && !_isExplodersPlayed[exploder])
			{
				_audioSource.PlayOneShot(_explosionSFX);
				_isExplodersPlayed[exploder] = true;
			}
		}
	}
}