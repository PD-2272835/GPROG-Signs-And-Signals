using UnityEngine;

public class GooberWorking : GooberState
{
    private IGooberInteractable _Interactable;
    private float _InteractTime = 0;

    public override void EnterState(GooberContext context)
    {
        _Interactable = context.GetInteractableAtCurrentPos();

        if (_Interactable.CanGooberInteract(context))
        {
            Debug.Log("interacted with " +  _Interactable);
            _InteractTime = _Interactable.GooberInteract(context);
        }
    }

    public override void ExitState(GooberContext context)
    {
        Debug.Log("finished interacting");
        _Interactable = null;
        _InteractTime = 0;
    }

    public override void Update(GooberContext context)
    {
        if (_InteractTime <= 0f) context.ChangeState(context.Idle);
        Debug.Log(Time.deltaTime);
        _InteractTime -= Time.deltaTime;
    }
}
