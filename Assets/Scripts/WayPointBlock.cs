using UnityEngine;
using UnityEngine.UIElements;

[SelectionBase]
public class WayPointBlock : MonoBehaviour
{
    public WayPointBlock exploredFrom;
    public bool isExlored = false;
    public bool isPlaceable = true;
    private const int sizeField = 10;

    public int GetGridSize()
    {
        return sizeField;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / sizeField),
            Mathf.RoundToInt(transform.position.z / sizeField)
            );
    }

    public void SetTopColor(Color color)
    {
        var topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse) && isPlaceable)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }
}
