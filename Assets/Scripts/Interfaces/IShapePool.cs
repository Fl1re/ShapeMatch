using UnityEngine;

public interface IShapePool
{
    GameObject GetShape(string id);
    void ReturnShape(string id, GameObject obj);
}