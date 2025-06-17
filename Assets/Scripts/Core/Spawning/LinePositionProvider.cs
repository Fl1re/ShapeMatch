using UnityEngine;

public class LinePositionProvider : MonoBehaviour, ISpawnPositionProvider
{
    private Vector3 spawnTopStartPos;
    private float spacing;
    private int cellsPerLine;
    private int index = 0;

    public LinePositionProvider(Vector3 spawnTopStartPos, float spacing, int cells)
    {
        this.spawnTopStartPos = spawnTopStartPos;
        this.spacing = spacing;
        this.cellsPerLine = cells;
    }

    public Vector3 GetNextPosition()
    {
        if (index >= cellsPerLine)
            index = 0;

        float offset = index * spacing;
        Vector3 pos = spawnTopStartPos + new Vector3(offset, 0, 0);
        index++;
        return pos;
    }
}
