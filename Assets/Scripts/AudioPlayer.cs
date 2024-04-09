using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip explodeClip;
    [SerializeField] [Range(0f,1f)] float explodeVolume = 1f;

        [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0f,1f)] float hitVolume = 1f;

    public void PlayShoot()
    {
        if (shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
        }
    }

    public void PlayExplosion()
    {
        if (explodeClip != null)
        {
            AudioSource.PlayClipAtPoint(explodeClip, Camera.main.transform.position, explodeVolume);
        }
    }

    public void PlayHit()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position, hitVolume);
        }
    }
}
