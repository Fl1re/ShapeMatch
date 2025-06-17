using UnityEngine;

public class ShapeData
{
    public string Id;
    public readonly Sprite BaseSprite;
    public readonly Sprite IconSprite;
    public Vector2 IconOffset;
    public Vector2 BackOffset;
    public Color Color;

    public ShapeData(string id, Sprite baseSprite, Sprite iconSprite, Color color,Vector2 backOffset, Vector2 iconOffset)
    {
        Id = id;
        BaseSprite = baseSprite;
        IconSprite = iconSprite;
        Color = color;
        IconOffset = iconOffset;
        BackOffset = backOffset;
    }

    public bool Matches(ShapeData other)
    {
        return BaseSprite == other.BaseSprite &&
               IconSprite == other.IconSprite &&
               Color.Equals(other.Color);
    }
}