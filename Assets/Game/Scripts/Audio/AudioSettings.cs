using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
	private const string MusicParameterName = "MusicVolume";
	private const string SoundParameterName = "SoundsVolume";

	private const float OnVolumeValue = 0f;
	private const float OffVolumeValue = -80f;
	
	[SerializeField] private AudioMixer _mixer;
	
	public void SetActiveMusicMixer(bool isActive)
	{
		_mixer.SetFloat(MusicParameterName, isActive ? OnVolumeValue : OffVolumeValue);
	}

	public void SetActiveSoundsMixer(bool isActive)
	{
		_mixer.SetFloat(SoundParameterName, isActive ? OnVolumeValue : OffVolumeValue);
	}
}