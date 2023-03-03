using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private int damagePoints = 1;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private AudioClip enemyDamageFX;
    [SerializeField] private AudioClip enemyDeathFX;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        DamageHit(other);
    }

    private void DamageHit(GameObject other)
    {
        hitParticle.Play();
        hitPoints -= damagePoints;
        audioSource.PlayOneShot(enemyDamageFX);

        if (hitPoints <= 0)
        {
            DestroyEnemy(deathParticle);

            var destroyedEnemy = FindObjectOfType<EnemySpawner>();
            var score = destroyedEnemy.GetScore();

            score.text = (Convert.ToInt32(score.text) + 1).ToString();
        }
    }

    public void DestroyEnemy(ParticleSystem fx)
    {
        var destroyFX = Instantiate(fx, transform.position, Quaternion.identity);

        destroyFX.Play();

        var fxDuration = destroyFX.main.duration;

        AudioSource.PlayClipAtPoint(enemyDeathFX, Camera.main.gameObject.transform.position);

        Destroy(destroyFX.gameObject, fxDuration);
        Destroy(gameObject);
    }
}
