namespace Legacy
{
    public interface IConsumer
    {
        void PerformOnFilled();
        void PerformOnReceiveResource(PlayerStacking from);
    }
}