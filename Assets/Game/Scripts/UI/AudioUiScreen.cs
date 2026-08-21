using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioUiScreen : MonoBehaviour
{
	[SerializeField] private Toggle _musicToggle;
	[SerializeField] private Toggle _soundToggle;
	[SerializeField] private AudioSettings _audioSettings;

	private void Awake()
	{
		_musicToggle.onValueChanged.AddListener(_audioSettings.SetActiveMusicMixer);
		_soundToggle.onValueChanged.AddListener(_audioSettings.SetActiveSoundsMixer);
	}

	private void OnDestroy()
	{
		_musicToggle.onValueChanged.RemoveAllListeners();
		_soundToggle.onValueChanged.RemoveAllListeners();
	}
}
