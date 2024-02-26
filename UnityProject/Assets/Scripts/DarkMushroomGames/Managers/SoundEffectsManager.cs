using DarkMushroomGames.Architecture;
using Unity.Mathematics;
using UnityEngine;

namespace DarkMushroomGames.Managers
{
    public class SoundEffectsManager : MonoBehaviourSingleton<SoundEffectsManager>
    {
        [SerializeField,Tooltip("The prefab used to play sound effects at a point in the world.")]
        private AudioSource soundEffectPrefab;
        
        public void PlaySoundEffectsClip(AudioClip audioClip, Transform spawnTransform, float volume)
        {
            var audioSource = Instantiate(soundEffectPrefab, spawnTransform.position, quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();

            var clipLength = audioClip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}
