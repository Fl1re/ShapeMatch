using UnityEngine;

public class ShapeSelector : MonoBehaviour, IShapeSelectionHandler
{
    [SerializeField] private ActionBar actionBar;

    public void SelectShape(Shape shape)
    {
        actionBar.AddShape(shape);
        shape.gameObject.SetActive(false);
    }
}
