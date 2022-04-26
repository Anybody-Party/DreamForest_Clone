using System;
using RH.Utilities.Saving;

namespace _Game.Data
{
    [Serializable]
    public class SavableData : ISavableData
    {
        public string Key => "Save";

        public int Money = 0;
    }
}