using DarkMushroomGames.Architecture;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

namespace DarkMushroomGames.Managers
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        private const string MasterVolumeKey = "Volume.Master";
        private const string MusicVolumeKey = "Volume.Music";
        private const string EffectsVolumeKey = "Volume.Effects";
        
        [SerializeField,Tooltip("The prefab used to play sound effects at a point in the world.")]
        private AudioSource soundEffectPrefab;

        [SerializeField,Tooltip("The mixer the sound manager is currently using.")]
        private AudioMixer currentMixer;

        public void OnEnable()
        {
            if(!PlayerPrefs.HasKey(MasterVolumeKey))
            {
                Debug.Log("Master volume key not found. Setting volume to max.");
                PlayerPrefs.SetFloat(MasterVolumeKey, 1);
                PlayerPrefs.SetFloat(MusicVolumeKey, 1);
                PlayerPrefs.SetFloat(EffectsVolumeKey, 1);
                
                PlayerPrefs.Save();
            }
            
            SetMasterVolume(PlayerPrefs.GetFloat(MasterVolumeKey));
            SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey));
            SetEffectsVolume(PlayerPrefs.GetFloat(EffectsVolumeKey));
        }
        
        /// <summary>
        /// Set's the current mixer's named group to the specified volume.
        /// </summary>
        /// <param name="groupName">The name of the current mixer's group to change the volume.</param>
        /// <param name="level">The new volume level.</param>
        private void SetGroupVolume(string groupName, float level)
        {
            currentMixer.SetFloat(groupName, Mathf.Log10(level) * 20f);
        }

        private float GetGroupVolume(string groupName)
        {
            var level = PlayerPrefs.GetFloat(groupName);
            return level;
        }

        public float GetMasterVolume()
        {
            return GetGroupVolume(MasterVolumeKey);
        }
        
        public float GetMusicVolume()
        {
            return GetGroupVolume(MusicVolumeKey);
        }
        
        public float GetEffectsVolume()
        {
            return GetGroupVolume(EffectsVolumeKey);
        }

        public void SetMasterVolume(float level)
        {
            SetGroupVolume("MasterVolume", level);
            
            PlayerPrefs.SetFloat(MasterVolumeKey, level);
            PlayerPrefs.Save();
        }
        
        public void SetMusicVolume(float level)
        {
            SetGroupVolume("MusicVolume", level);
            
            PlayerPrefs.SetFloat(MusicVolumeKey, level);
            PlayerPrefs.Save();
        }
        
        public void SetEffectsVolume(float level)
        {
            SetGroupVolume("EffectsVolume", level);
            
            PlayerPrefs.SetFloat(EffectsVolumeKey, level);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Plays an audio clip at the provided position.
        /// </summary>
        /// <param name="audioClip">The audio clip to be played.</param>
        /// <param name="spawnTransform">The location that the audio source will be played at.</param>
        /// <param name="volume">The volume at which to play the clip. Default is 1.</param>
        public void PlaySoundEffectClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
        {
            var audioSource = Instantiate(soundEffectPrefab, spawnTransform.position, quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();

            var clipLength = audioClip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
        
        /// <summary>
        /// Plays a random clip from the given clips at the provided position.
        /// </summary>
        /// <param name="audioClip">The audio clips to select from to be played.</param>
        /// <param name="spawnTransform">The location that the audio source will be played at.</param>
        /// <param name="volume">The volume at which to play the clip. Default is 1.</param>
        public void PlaySoundEffectClip(AudioClip[] audioClip, Transform spawnTransform, float volume = 1f)
        {
            var index = UnityEngine.Random.Range(0, audioClip.Length);
            PlaySoundEffectClip(audioClip[index], spawnTransform, volume);
        }
    }
}
