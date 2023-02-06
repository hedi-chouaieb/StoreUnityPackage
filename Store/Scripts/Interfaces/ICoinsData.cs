using UnityEngine;
using UnityEngine.Events;

namespace Hedi.Me.Store
{
    public abstract class ICoinsData : ScriptableObject
    {
        public abstract int Coins { get; set; }
        public abstract void AddListener(UnityAction<int> listener);
        public abstract void RemoveListener(UnityAction<int> listener);
    }
}
