using UnityEngine;

public interface ISpawnPositionProvider
{
    Vector3 GetNextPosition();
}