using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class EnemyMovement : MonoBehaviour
{
    public bool isChangePosition = false;
    private PathFinder pathFinder;
    private EnemyDamage enemyDamage;
    [SerializeField][Range(0f, 15f)] private float speed = 1f;
    [SerializeField][Range(0f, 15f)] private float moveStep;
    [SerializeField] private ParticleSystem castleDamageParticle;
    private Castle castle;
    private Vector3 targetPosition;

    void Start()
    {
        enemyDamage = GetComponent<EnemyDamage>();
        pathFinder = FindObjectOfType<PathFinder>();
        castle = FindObjectOfType<Castle>();
        var wayPointBlocks = pathFinder.GetWay();

        StartCoroutine(EnemyMove(wayPointBlocks));
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveStep);
    }

    private IEnumerator EnemyMove(List<WayPointBlock> wayPointBlocks)
    {
        foreach (var wayPointBlock in wayPointBlocks)
        {
            transform.LookAt(wayPointBlock.transform);
            targetPosition = wayPointBlock.transform.position;

            yield return new WaitForSeconds(speed);
        }

        var lastBlock = wayPointBlocks.Last().transform.position;
        lastBlock.x = lastBlock.x + 10;
        //transform.position = lastBlock;
        targetPosition = lastBlock;

        yield return new WaitForSeconds(speed);

        enemyDamage.DestroyEnemy(castleDamageParticle);
        
        castle.Damage(1);
    }
}
