using System;
using UnityEngine;

namespace Hedi.Me.Store
{
    public abstract class IStoreRepository : MonoBehaviour
    {
        public abstract void GetStoreItemStateList(Action<StoreItemState[]> onGetStoreItemStateListSuccess, Action<string> onGetStoreItemStateListError);
        public abstract void RequestPurchase(StoreItem storeItem, Action<StoreItem,int> onPurchaseSuccess, Action<StoreItem, string> onPurchaseError);
    }
}
