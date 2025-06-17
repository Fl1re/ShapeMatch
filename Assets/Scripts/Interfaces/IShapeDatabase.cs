using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShapeDatabase
{
    List<(string id, GameObject prefab)> GetShapePrefabs();
}

