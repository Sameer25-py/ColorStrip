using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SoundButton : MonoBehaviour
    {
        public Sprite Mute, UnMute;

        public AudioSource AudioSource;

        private Button _button;

        private void OnEnable()
        {
            _button              = GetComponent<Button>();
            _button.image.sprite = AudioSource.mute ? Mute : UnMute;
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            AudioSource.mute     = !AudioSource.mute;
            _button.image.sprite = AudioSource.mute ? Mute : UnMute;
        }
    }
}