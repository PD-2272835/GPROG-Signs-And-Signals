using UnityEngine;

public class BerryBush : Occupier, IGooberInteractable
{
    [SerializeField] private SpriteRenderer _SelectionOutline;

    private float _GooberInteractionTime;
    private bool _HasBerries;
    

    public override void Initialize()
    {
        _HasBerries = true;
        _GooberInteractionTime = 2f;

        if (_SelectionOutline == null)
        {
            _SelectionOutline = transform.Find("SelectionOutline").gameObject.GetComponent<SpriteRenderer>();
            if (_SelectionOutline == null) throw new System.Exception("No Outline Object or Renderer found, cannot implement ISelectable");
        }

        _SelectionOutline.enabled = _IsSelected;
    }
    

    
    public bool CanGooberInteract(GooberContext goober)
    {
        if (_HasBerries) return true;
        return false;
    }

    public float GooberInteract(GooberContext goober)
    {
        return _GooberInteractionTime;
    }


    public override void SetSelected(bool selected)
    {
        _IsSelected = selected;
        _SelectionOutline.enabled = selected;
    }
}
