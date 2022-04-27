using System;
using _DreamForest.Data;

namespace _Game.Data
{
    [Serializable]
    public class ResourcesConsumerData
    {
        public string Id;
        public Resource[] NeededResources;
    }
}