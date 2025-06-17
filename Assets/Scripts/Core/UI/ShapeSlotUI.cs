using UnityEngine;
using UnityEngine.UI;

public class ShapeSlotUI : MonoBehaviour
{
    [SerializeField] private Image baseImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image colorImage;

    public void Setup(Sprite baseSprite, Sprite iconSprite, Color color,Vector2 iconOffset, Vector2 backOffset)
    {
        if (baseImage != null)
            baseImage.sprite = baseSprite;

        if (iconImage != null)
        {
            iconImage.sprite = iconSprite;
            iconImage.GetComponent<RectTransform>().localPosition = iconOffset;
        }

        if (colorImage != null)
        {
            colorImage.sprite = baseSprite;
            colorImage.color = color;
            colorImage.GetComponent<RectTransform>().localPosition = backOffset;
        }
    }
}
