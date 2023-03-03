using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] [Range(1f, 20f)] private float spawnInterval = 4f;
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private AudioClip enemySpawnSoundFX;

    void Start()
    {
        scoreText.text = "0";
        StartCoroutine(DelayEnemySpawn());
    }

    public Text GetScore()
    {
        return scoreText;
    }

    private IEnumerator DelayEnemySpawn()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(enemySpawnSoundFX);

            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            newEnemy.transform.parent = transform;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
