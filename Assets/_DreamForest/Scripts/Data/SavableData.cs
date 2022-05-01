using System;
using System.Collections.Generic;
using _DreamForest.Legacy;
using _Game.Data;
using RH.Utilities.Saving;

namespace _DreamForest.Data
{
    [Serializable]
    public class SavableData : ISavableData
    {
        public string SaveKey => "Save";

        public List<Resource> Resources = new List<Resource>
        {
            new Resource{ Amount = 0, Type = ResourceType.Gold},
            new Resource{ Amount = 0, Type = ResourceType.Grass},
            new Resource{ Amount = 0, Type = ResourceType.Wood},
            new Resource{ Amount = 0, Type = ResourceType.Milk},
        };

        public List<ResourcesConsumerData> ResourcesConsumers = new List<ResourcesConsumerData>();
        public int AxeLevel;
        public List<string> FelledTrees = new List<string>();
    }
}