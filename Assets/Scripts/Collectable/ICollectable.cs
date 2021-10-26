public interface ICollectable
{
    int CollectableType { get; }//0 for coin and 1 for other thing for now.

    void GetCollected();
}