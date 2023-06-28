using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerTrail : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;

        private void OnEnable()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        public void ChangeTrailMaterial(Texture2D albedoTexture)
        {
            _trailRenderer.sharedMaterial.mainTexture = albedoTexture;
        }

        public void EnableTrail()
        {
            _trailRenderer.emitting = true;
        }

        public void DisableTrail()
        {
            _trailRenderer.emitting = false;
        }
    }
}