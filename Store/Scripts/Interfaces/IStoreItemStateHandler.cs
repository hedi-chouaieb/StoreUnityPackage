using UnityEngine;

namespace Hedi.Me.Store
{
    public abstract class IStoreItemStateHandler : MonoBehaviour
    {
        public abstract StoreItemState StoreItemState { get; }
        public abstract void Init(StoreItemState storeItemState, ICoinsData currentCoins, System.Action<StoreItemState> purchase);
        public abstract void Clear();
    }
}