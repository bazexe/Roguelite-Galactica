using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    public GameObject impactEffect;
    public Animator animator;
    public GameObject deathEffect;

    [SerializeField] bool applyCameraShake;
    //eventually set per enemy type
    [SerializeField] int score = 100;
    [SerializeField] bool isPlayer;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;

    ScoreKeeper scoreKeeper;

    public int GetHealth()
    {
        return health;
    }
    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayHit();
            ShakeCamera();
            damageDealer.Hit();
            
        }

        GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(impact, 3f);
        

    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            Debug.Log(scoreKeeper.GetScore());
        }
        GameObject death = Instantiate(deathEffect, transform.position, Quaternion.identity);
        audioPlayer.PlayExplosion();
        Destroy(death, 3f);
        Destroy(gameObject);
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
