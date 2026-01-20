using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public abstract class Occupier : MonoBehaviour, ISelectable, IInitializable
{
    [SerializeField] protected Sprite _Sprite;
    protected SpriteRenderer _SpriteRenderer; 
    protected Collider2D _HitBox;
    protected bool _IsSelected = false;

    public virtual void Start()
    {
        _SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _HitBox = gameObject.GetComponent<Collider2D>();

        if (!_SpriteRenderer) _SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        if (!_HitBox) _HitBox = gameObject.AddComponent<Collider2D>();

    }

    public abstract void Initialize();

    public abstract void SetSelected(bool selected);
    public virtual bool GetSelected()
    {
        return _IsSelected;
    }
}
