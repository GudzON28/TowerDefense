using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(WayPointBlock))]
public class CubeEditor : MonoBehaviour
{
    private WayPointBlock wayPointBlock;

    private void Awake()
    {
        wayPointBlock = GetComponent<WayPointBlock>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        var textMesh = GetComponentInChildren<TextMesh>();
        var labelName = $"{wayPointBlock.GetGridPosition().x},{wayPointBlock.GetGridPosition().y}";

        textMesh.text = labelName;
        gameObject.name = $"Cube_{labelName}";
    }

    private void SnapToGrid()
    {
        var sizeField = wayPointBlock.GetGridSize();

        transform.position = new Vector3(wayPointBlock.GetGridPosition().x * sizeField, 0f, wayPointBlock.GetGridPosition().y * sizeField);
    }
}
