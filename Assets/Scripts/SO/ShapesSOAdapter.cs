using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapesSOAdapter : IShapeDatabase
{
    private readonly ShapesSO _shapesSO;

    public ShapesSOAdapter(ShapesSO shapesSO)
    {
        _shapesSO = shapesSO;
    }

    public List<(string id, GameObject prefab)> GetShapePrefabs()
    {
        return _shapesSO.shapes
            .Select(s => (s.id, s.prefab))
            .ToList();
    }

    public float GetProbability(string id)
    {
        return _shapesSO.shapes.Find(s => s.id == id).probability;
    }
}
