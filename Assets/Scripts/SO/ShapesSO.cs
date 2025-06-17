using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Shape/Shape Data", fileName = "NewShapeData")]
public class ShapesSO : ScriptableObject
{
    [System.Serializable]
    public struct ShapeInfo
    {
        public string id;
        public GameObject prefab;
        [Range(0f, 1f)] public float probability;
    }

    public List<ShapeInfo> shapes;
}