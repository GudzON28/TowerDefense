using UnityEngine;


[SelectionBase]
public class Tower : MonoBehaviour
{
    public WayPointBlock baseWayPointBlock;
    [SerializeField] private Transform towerTop;
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private float shootDistance;
    [SerializeField] private ParticleSystem bulletParticle;

    void Update()
    {
        SetTargetEnemy();

        if (targetEnemy)
        {
            towerTop.LookAt(targetEnemy);

            Fire();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var allEnemies = FindObjectsOfType<EnemyDamage>();
        
        if (allEnemies.Length == 0)
        {
            return;
        }

        var closestEnemy = allEnemies[0].transform;

        foreach (var enemy in allEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy.transform, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform enemyOne, Transform enemyTwo)
    {
        var distanceToPositionOne = Vector3.Distance(enemyOne.position, transform.position);
        var distanceToPositionTwo = Vector3.Distance(enemyTwo.position, transform.position);

        if (distanceToPositionOne < distanceToPositionTwo)
        {
            return enemyOne;
        }

        return enemyTwo;
    }

    private void Fire()
    {
        var distatceToEnemy = Vector3.Distance(targetEnemy.position, transform.position);

        if (distatceToEnemy <= shootDistance)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emission = bulletParticle.emission;
        emission.enabled = isActive;
    }
}
