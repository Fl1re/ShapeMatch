using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeFactory : IShapeProvider
{
    private readonly IShapeDatabase _database;
    private readonly ShapeBehaviourConfigSO _behaviorConfig;
    private System.Random rng = new();

    public ShapeFactory(IShapeDatabase database, ShapeBehaviourConfigSO behaviorConfig = null)
    {
        _database = database;
        _behaviorConfig = behaviorConfig;
    }

    public List<(string id, GameObject prefab)> GetShapes(int count)
    {
        var allShapes = _database.GetShapePrefabs();
        var result = new List<(string, GameObject)>();

        int totalTypes = Mathf.Min(count / 3, allShapes.Count);
        Shuffle(allShapes);

        foreach (var shape in allShapes.Take(totalTypes))
        {
            for (int i = 0; i < 3; i++)
                result.Add(shape);
        }

        Shuffle(result);
        return result;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}