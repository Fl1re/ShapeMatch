using UnityEngine;

public class ShapeViewData : MonoBehaviour
{
    private SpriteRenderer _baseRenderer;
    private SpriteRenderer _colorRenderer;
    private SpriteRenderer _iconRenderer;
    private Vector2 _backOffset;
    private Vector2 _iconOffset;

    public Sprite BaseSprite => _baseRenderer.sprite;
    public Sprite IconSprite => _iconRenderer.sprite;
    public Color Color => _colorRenderer.color;
    public Vector2 BackOffset => _backOffset;
    public Vector2 IconOffset => _iconOffset;

    private void Awake()
    {
        GetShapeViewData();
    }

    private void GetShapeViewData()
    {
        _baseRenderer = GetComponent<SpriteRenderer>();
        _colorRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _iconRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _backOffset = transform.GetChild(0).GetComponent<RectTransform>().localPosition / transform.localScale.x;
        _iconOffset = transform.GetChild(1).GetComponent<RectTransform>().localPosition / transform.localScale.x;
    }
}
