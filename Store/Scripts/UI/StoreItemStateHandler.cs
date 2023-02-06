using System;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.Me.Store
{
    public class StoreItemStateHandler : IStoreItemStateHandler
    {
        [SerializeField] private Sprite defaultImage;
        [SerializeField] private UnityEvent onInitialized;
        [SerializeField] private UnityEvent onClear;
        [SerializeField] private UnityEventString onUpdateName;
        [SerializeField] private UnityEventString onUpdateCost;
        [SerializeField] private UnityEventSprite onUpdateImage;
        [SerializeField] private UnityEventBool onPurchased;
        [SerializeField] private UnityEventBool onCanPurchase;

        private StoreItemState currentStoreItemState;
        private ICoinsData currentCoins;
        private Action<StoreItemState> onPurchase;

        public override StoreItemState StoreItemState => this.currentStoreItemState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storeItemState"></param>
        /// <param name="currentCoins"></param>
        /// <param name="onPurchase"></param>
        public override void Init(StoreItemState storeItemState, ICoinsData currentCoins, Action<StoreItemState> onPurchase)
        {
            this.currentStoreItemState = storeItemState;
            this.currentCoins = currentCoins;
            this.onPurchase = onPurchase;
            onUpdateName?.Invoke(currentStoreItemState.StoreItem.Name);
            onUpdateCost?.Invoke(currentStoreItemState.StoreItem.Cost.ToString());
            onUpdateImage?.Invoke(currentStoreItemState.StoreItem.Image);
            onPurchased?.Invoke(currentStoreItemState.Purchased);
            onInitialized?.Invoke();
        }

        /// <summary>
        /// Clear UI elements
        /// </summary>
        public override void Clear()
        {
            onClear?.Invoke();
            this.currentStoreItemState = null;
            this.currentCoins = null;
            this.onPurchase = null;
            onUpdateName?.Invoke(string.Empty);
            onUpdateCost?.Invoke(string.Empty);
            onUpdateImage?.Invoke(defaultImage);
            onPurchased?.Invoke(false);
        }

        /// <summary>
        /// Call by the button to on purchase action
        /// </summary>
        public void Purchase()
        {
            if (currentStoreItemState.Purchased)
            {
                Debug.LogError($"currentStoreItemState ({currentStoreItemState}) is already purchased.", this.gameObject);
                return;
            }
            if (currentStoreItemState.StoreItem.Cost > currentCoins.Coins)
            {
                Debug.LogError($"Not enough coins ({currentCoins.Coins}) to purchase currentStoreItemState ({currentStoreItemState}).", this.gameObject);
                return;
            }
            onPurchase?.Invoke(currentStoreItemState);
        }

        private void OnCoinsUpdated(int currentCoins)
        {
            onCanPurchase?.Invoke(currentCoins >= currentStoreItemState.StoreItem.Cost);
        }

        private void OnEnable()
        {
            currentCoins?.AddListener(OnCoinsUpdated);
        }

        private void OnDisable()
        {
            currentCoins?.RemoveListener(OnCoinsUpdated);
        }

        [System.Serializable] private class UnityEventBool : UnityEvent<bool> { }
        [System.Serializable] private class UnityEventString : UnityEvent<string> { }
        [System.Serializable] private class UnityEventSprite : UnityEvent<Sprite> { }

    }
}
