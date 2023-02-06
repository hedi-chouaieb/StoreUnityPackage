using UnityEngine;

namespace Hedi.Me.Store
{
    public class StoreItemData : ScriptableObject
    {
        [SerializeField] private StoreItem storeItem;

        public StoreItem StoreItem { get => storeItem; set => storeItem = value; }
    }
}