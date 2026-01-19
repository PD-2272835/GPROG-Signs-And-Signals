using UnityEngine;

public class BerryBush : AbstractOccupier, IGooberInteractable
{

    bool hasBerries;

    public override void InitializeOccupier()
    {
        hasBerries = true;
    }
    

    
    public bool CanGooberInteract(GooberContext goober)
    {
        throw new System.NotImplementedException();
    }

    public void GooberInteract(GooberContext goober)
    {
        throw new System.NotImplementedException();
    }



    public override bool GetSelected()
    {
        throw new System.NotImplementedException();
    }

    public override void SetSelected(bool selected)
    {
        throw new System.NotImplementedException();
    }
}
