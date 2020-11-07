public interface IInteractable
{
    InteractionType Type { get; }
    bool Interact();
}

public enum InteractionType
{
    PickUp,
    WaterIt
}