using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    [SerializeField] private ShapesSO shapeDatabaseSO;
    [SerializeField] private ShapeBehaviourConfigSO behaviorConfig;
    [SerializeField] private ShapePool shapePool;
    [SerializeField] private ShapeSelector shapeSelector;
    [SerializeField] private Transform spawnLine;
    [SerializeField] private float spacing = 1f;
    [SerializeField] private int initialFigureCount = 60;
    [SerializeField] private float spawnInterval = 0.05f;
    [SerializeField] private float spawnHeight = 5f;

    private IShapeProvider shapeProvider;
    private ISpawnPositionProvider positionProvider;
    private IShapePool pool;
    
    public event Action<bool> OnSpawningStateChanged;
    
    public int InitialFigureCount => initialFigureCount;

    private bool isSpawning;
    

    private void Start()
    {
        var shapeDatabase = new ShapesSOAdapter(shapeDatabaseSO);
        shapeProvider = new ShapeFactory(shapeDatabase, behaviorConfig);

        pool = shapePool;
        shapePool.Init(shapeSelector);

        float lineWidth = spawnLine.localScale.x;
        int cellsPerLine = Mathf.FloorToInt(lineWidth / spacing);

        Vector3 spawnTopPos = spawnLine.position + new Vector3(0, spawnHeight, 0);
        positionProvider = new LinePositionProvider(spawnTopPos - new Vector3(lineWidth / 2, 0, 0), spacing, cellsPerLine);

        if (GetAllShapeCount() == 0)
        {
            StartCoroutine(SpawnRoutine(initialFigureCount));
        }
    }

    public void RespawnBoard(bool isFullReset)
    {
        if (isSpawning)
            return;

        int count = GetAllShapeCount();;

        if (isFullReset)
            ClearAllBoard();
        else
            ClearBoard();

        StopAllCoroutines();
        StartCoroutine(SpawnRoutine(count));
    }

    private IEnumerator SpawnRoutine(int count)
    {
        isSpawning = true;
        OnSpawningStateChanged?.Invoke(true);

        var shapes = shapeProvider.GetShapes(count);
        var spawnedShapes = new List<Shape>();

        foreach (var (id, _) in shapes)
        {
            GameObject shapeGO = pool.GetShape(id);
            if (shapeGO == null) continue;

            Vector3 spawnPos = positionProvider.GetNextPosition();
            shapeGO.transform.position = spawnPos;
            shapeGO.transform.SetParent(transform);

            foreach (var shape in spawnedShapes)
            {
                shape.SetInteractable(true);
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        foreach (var shape in spawnedShapes)
        {
            shape.SetInteractable(true);
        }

        isSpawning = false;
        OnSpawningStateChanged?.Invoke(false);
    }

    private void ClearBoard()
    {
        foreach (var shapeTransform in GetAllShapeList())
        {
            var shape = shapeTransform.GetComponent<Shape>();
            if (shape != null && shape.gameObject.activeSelf)
                pool.ReturnShape(shape.ShapeId, shape.gameObject);
        }
    }

    private void ClearAllBoard()
    {
        foreach (var shapeTransform in GetAllShapeList())
        {
            var shape = shapeTransform.GetComponent<Shape>();
            if (shape != null)
                pool.ReturnShape(shape.ShapeId, shape.gameObject);
        }
    }

    private List<Transform> GetAllShapeList()
    {
        var allShapeList = new List<Transform>();
        foreach (Transform child in transform)
            allShapeList.Add(child);
        return allShapeList;
    }

    private int GetAllShapeCount()
    {
        int count = 0;
        foreach (Transform child in GetAllShapeList())
        {
            if (child.GetComponent<Shape>() != null)
                count++;
        }
        return count;
    }
}