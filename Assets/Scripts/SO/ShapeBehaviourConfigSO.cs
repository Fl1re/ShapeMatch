using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Shape/Behavior Config", fileName = "NewShapeBehaviorConfig")]
public class ShapeBehaviourConfigSO : ScriptableObject
{
    [System.Serializable]
    public struct BehaviorProbability
    {
        public float normalProbability;
        public float heavyProbability;
        public float frozenProbability;
    }

    public BehaviorProbability probabilities;
    public Vector2Int frozenUnfreezeRange = new Vector2Int(3, 7);
    public float gravityScale = 2f;
}
