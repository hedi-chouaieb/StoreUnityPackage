using UnityEngine;

namespace Hedi.Me.Store
{
    [System.Serializable]
    public class StoreItemState
    {
        [SerializeField] private StoreItem storeItem;
        [SerializeField] private bool purchased;

        public StoreItemState()
        {
        }

        public StoreItemState(StoreItem storeItem, bool purchased)
        {
            this.storeItem = storeItem;
            this.purchased = purchased;
        }

        public StoreItem StoreItem { get => storeItem; set => storeItem = value; }
        public bool Purchased { get => purchased; set => purchased = value; }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}