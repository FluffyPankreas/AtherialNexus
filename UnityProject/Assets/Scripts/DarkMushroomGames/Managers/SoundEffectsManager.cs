using DarkMushroomGames.Architecture;
using Unity.Mathematics;
using UnityEngine;

namespace DarkMushroomGames.Managers
{
    public class SoundEffectsManager : MonoBehaviourSingleton<SoundEffectsManager>
    {
        [SerializeField,Tooltip("The prefab used to play sound effects at a point in the world.")]
        private AudioSource soundEffectPrefab;

        /// <summary>
        /// Plays an audio clip at the provided position.
        /// </summary>
        /// <param name="audioClip">The audio clip to be played.</param>
        /// <param name="spawnTransform">The location that the audio source will be played at.</param>
        /// <param name="volume">The volume at which to play the clip. Default is 1.</param>
        public void PlaySoundEffectsClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
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
        public void PlaySoundEffectsClip(AudioClip[] audioClip, Transform spawnTransform, float volume = 1f)
        {
            var index = UnityEngine.Random.Range(0, audioClip.Length);
            PlaySoundEffectsClip(audioClip[index], spawnTransform, volume);
        }
    }
}
