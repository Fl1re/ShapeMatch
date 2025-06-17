using System.Collections.Generic;
using UnityEngine;

public interface IShapeProvider
{
    List<(string id, GameObject prefab)> GetShapes(int count);
}
