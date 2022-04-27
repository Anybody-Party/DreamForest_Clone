namespace _DreamForest.Legacy
{
    public interface IConsumer
    {
        void PerformOnFilled();
        void PerformOnReceiveResource();
    }
}