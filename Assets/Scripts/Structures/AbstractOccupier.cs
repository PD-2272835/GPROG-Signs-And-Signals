using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public abstract class AbstractOccupier : MonoBehaviour, ISelectable, IOccupier
{
    protected Sprite sprite;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D hitBox;

    public virtual void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hitBox = gameObject.GetComponent<Collider2D>();

        if (!spriteRenderer) spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        if (!hitBox) hitBox = gameObject.AddComponent<Collider2D>();

        spriteRenderer.sprite = sprite;
    }

    public abstract void InitializeOccupier();

    public abstract void SetSelected(bool selected);
    public abstract bool GetSelected();

}
