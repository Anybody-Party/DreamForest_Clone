namespace Legacy
{
    public interface IConsumer
    {
        void PerformOnFilled();
        void PerformOnReceiveResource(StackableItem item, PlayerStacking from);
    }
}