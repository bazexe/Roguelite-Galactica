using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 1f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0f;


    AudioPlayer audioPlayer;
    Vector3 shootingSide;
    Coroutine fireRoutine;
    Rigidbody2D laser;
    [HideInInspector] public bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
            shootingSide = -transform.right;
        }
        else
        {
            shootingSide = transform.right;
        }
    }

    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if (isFiring && fireRoutine == null)
        {
            fireRoutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            laser = instance.GetComponent<Rigidbody2D>();
            if (laser != null)
            {
                laser.velocity = shootingSide * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);
            float timeToNextProjectile = Random.Range(firingRate - fireRateVariance, firingRate + fireRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFireRate, float.MaxValue);
            audioPlayer.PlayShoot();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
    
}
