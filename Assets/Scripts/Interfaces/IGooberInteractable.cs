public interface IGooberInteractable
{
    public abstract float GooberInteract(GooberContext goober); //returns the working time for interacting with the concrete GooberInteractable
    public abstract bool CanGooberInteract(GooberContext goober);
}
