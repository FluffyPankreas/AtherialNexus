using UnityEngine;
using System.Collections.Generic;

namespace DarkMushroomGames
{
    [RequireComponent(typeof(AudioSource))]
    public class JukeBox : MonoBehaviour
    {
        [SerializeField,Tooltip("The music tracks the jukebox can select from.")]
        private AudioClip[] musicTracks;

        private AudioSource _audioSource;
        private List<AudioClip> _tracksLeft;

        private float _trackTimeLeft = 0;
        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            PopulateRemainingTracks();
        }

        public void Start()
        {
            PlayNextTrack();
        }

        public void Update()
        {
            if (_audioSource.time >= _audioSource.clip.length)
            {
                PlayNextTrack();
            }
        }

        private void PlayNextTrack()
        {
            _audioSource.clip = GetNextMusicTrack();
            _audioSource.Play();
            _trackTimeLeft = _audioSource.clip.length;
        }

        private AudioClip GetNextMusicTrack()
        {
            if (_tracksLeft.Count <= 0)
            {
                PopulateRemainingTracks();
            }
            
            var index = Random.Range(0, _tracksLeft.Count);
            var nextTrack = _tracksLeft[index];
            _tracksLeft.RemoveAt(index);

            return nextTrack;
        }

        private void PopulateRemainingTracks()
        {
            _tracksLeft = new List<AudioClip>();
            for (int i = 0; i < musicTracks.Length; i++)
            {
                _tracksLeft.Add(musicTracks[i]);
            }
        }
    }
}
