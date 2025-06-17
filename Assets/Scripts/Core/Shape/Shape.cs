using UnityEngine;

public class Shape : MonoBehaviour
{
    private ShapeViewData viewData;
    public string ShapeId { get; private set; }

    private IShapeSelectionHandler _selectionHandler;
    
    private bool isFrozen = false;
    private int matchesToUnfreeze = 0;
    private bool isInteractable = true;

    private Rigidbody2D rb;

    public void Initialize(string id, IShapeSelectionHandler handler)
    {
        ShapeId = id;
        _selectionHandler = handler;
    }

    private void Awake()
    {
        viewData = GetComponent<ShapeViewData>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        ShapeSpawnerEvents.OnMatch += DecreaseUnfreezeCounter;
    }

    private void OnDisable()
    {
        ShapeSpawnerEvents.OnMatch -= DecreaseUnfreezeCounter;
    }

    public ShapeData ToShapeData()
    {
        return new ShapeData(
            id: ShapeId,
            baseSprite: viewData.BaseSprite,
            iconSprite: viewData.IconSprite,
            color: viewData.Color,
            backOffset: viewData.BackOffset,
            iconOffset: viewData.IconOffset
        );
    }
    
    public void SetFrozen(int matchesRequired)
    {
        isFrozen = true;
        matchesToUnfreeze = matchesRequired;
        SetIcedMaterial(1);
    }

    private void SetIcedMaterial(int IceStrength)
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_OverlayStrength", IceStrength);
    }

    public void SetHeavy(float gravityScale)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }
    
    public void DecreaseUnfreezeCounter()
    {
        if (!isFrozen) return;

        matchesToUnfreeze--;
        if (matchesToUnfreeze <= 0)
        {
            Unfreeze();
        }
    }

    private void Unfreeze()
    {
        isFrozen = false;
        SetIcedMaterial(0);
    }
    
    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    private void OnMouseDown()
    {
        if (!isInteractable || isFrozen) return;
        _selectionHandler?.SelectShape(this);
    }
}
