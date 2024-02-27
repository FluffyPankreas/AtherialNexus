using DarkMushroomGames.Architecture;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

namespace DarkMushroomGames.Managers
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        [SerializeField,Tooltip("The prefab used to play sound effects at a point in the world.")]
        private AudioSource soundEffectPrefab;

        [SerializeField,Tooltip("The mixer the sound manager is currently using.")]
        private AudioMixer currentMixer;

        /// <summary>
        /// Set's the current mixer's named group to the specified volume.
        /// </summary>
        /// <param name="groupName">The name of the current mixer's group to change the volume.</param>
        /// <param name="level">The new volume level.</param>
        private void SetGroupVolume(string groupName, float level)
        {
            currentMixer.SetFloat(groupName, Mathf.Log10(level) * 20f);
        }

        public void SetMasterVolume(float level)
        {
            SetGroupVolume("MasterVolume", level);
        }

        public void SetMusicVolume(float level)
        {
            SetGroupVolume("MusicVolume", level);
        }
        
        public void SetEffectsVolume(float level)
        {
            SetGroupVolume("EffectsVolume", level);
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
