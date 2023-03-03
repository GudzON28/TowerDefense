using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private int towerLimit = 4;
    private Queue<Tower> towerQueue = new();

    public void AddTower(WayPointBlock baseWayPointBlock)
    {
        var towersCount = towerQueue.Count;

        if (towersCount < towerLimit)
        {
            InstantiateNewTower(baseWayPointBlock);
        }
        else
        {
            MoveTowerToNewPosition(baseWayPointBlock);
        }
    }

    private void MoveTowerToNewPosition(WayPointBlock newBaseWayPointBlock)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.transform.position = newBaseWayPointBlock.transform.position;
        oldTower.baseWayPointBlock.isPlaceable = true;
        newBaseWayPointBlock.isPlaceable = false;
        oldTower.baseWayPointBlock = newBaseWayPointBlock;      
        
        towerQueue.Enqueue(oldTower);
    }

    private void InstantiateNewTower(WayPointBlock wayPointBlock)
    {
        var newTower = Instantiate(towerPrefab, wayPointBlock.transform.position, Quaternion.identity);

        newTower.transform.parent = transform;
        wayPointBlock.isPlaceable = false;
        newTower.baseWayPointBlock = wayPointBlock;
        towerQueue.Enqueue(newTower);
    }
}
