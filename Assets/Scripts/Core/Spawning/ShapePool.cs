using System.Collections.Generic;
using UnityEngine;

public class ShapePool : MonoBehaviour, IShapePool
{
    [SerializeField] private ShapesSO shapeDatabase;
    [SerializeField] private ShapeBehaviourConfigSO behaviorConfig;
    [SerializeField] private int preloadCountPerShape = 3;
    
    private Dictionary<string, Queue<GameObject>> pool = new();
    private IShapeSelectionHandler _selectionHandler;

    public void Init(IShapeSelectionHandler selectionHandler)
    {
        _selectionHandler = selectionHandler;

        foreach (var shape in shapeDatabase.shapes)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < preloadCountPerShape; i++)
            {
                GameObject obj = CreateShapeInstance(shape.id, shape.prefab);
                queue.Enqueue(obj);
            }
            pool[shape.id] = queue;
        }
    }

    public GameObject GetShape(string id)
    {
        if (!pool.TryGetValue(id, out var queue) || queue.Count == 0)
            return null;

        var obj = queue.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnShape(string id, GameObject obj)
    {
        if (!pool.ContainsKey(id))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool[id].Enqueue(obj);
    }

    private GameObject CreateShapeInstance(string id, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform, true);
        obj.SetActive(false);

        var shape = obj.GetComponent<Shape>();
        if (shape != null)
        {
            shape.Initialize(id, _selectionHandler);
            ApplyRandomBehavior(shape);
        }

        return obj;
    }
    
    private void ApplyRandomBehavior(Shape shape)
    {
        float total = behaviorConfig.probabilities.normalProbability +
                      behaviorConfig.probabilities.heavyProbability +
                      behaviorConfig.probabilities.frozenProbability;

        float roll = Random.value * total;

        if (roll < behaviorConfig.probabilities.frozenProbability)
        {
            int unfreezeAfter = Random.Range(
                behaviorConfig.frozenUnfreezeRange.x,
                behaviorConfig.frozenUnfreezeRange.y + 1);
            shape.SetFrozen(unfreezeAfter);
        }
        else if (roll < behaviorConfig.probabilities.frozenProbability + behaviorConfig.probabilities.heavyProbability)
        {
            shape.SetHeavy(behaviorConfig.gravityScale);
        }
    }
}
