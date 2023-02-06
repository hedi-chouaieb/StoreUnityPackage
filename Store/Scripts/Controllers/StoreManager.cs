using System.Linq;
using UnityEngine;

namespace Hedi.Me.Store
{
    /// <summary>
    /// StoreManager control the communication between the ui and the repository
    /// </summary>
    public class StoreManager : IStoreManager
    {
        [SerializeField] private IStoreRepository storeRepository;
        [SerializeField] private ICoinsData currentCoins;
        [SerializeField] private IStoreItemStateHandler[] storeItemStateHandlers;

        private StoreItemState[] currentStoreItemsStates;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storeRepository"></param>
        /// <param name="coinsData"></param>
        public override void Init(IStoreRepository storeRepository, ICoinsData coinsData)
        {
            this.storeRepository = storeRepository;
            this.currentCoins = coinsData;
        }

        /// <summary>
        /// Load the store items and display them using store item state handlers
        /// </summary>
        public override void Load()
        {
            storeRepository.GetStoreItemStateList(OnGetStoreItemStateListSuccess, OnGetStoreItemStateListError);
        }

        private void UpdateStoreItemStateHandlers()
        {
            int indexStoreItemStateHandlers = 0;
            while (indexStoreItemStateHandlers < storeItemStateHandlers.Length)
            {
                if (indexStoreItemStateHandlers < currentStoreItemsStates.Length)
                {
                    storeItemStateHandlers[indexStoreItemStateHandlers].Init(currentStoreItemsStates[indexStoreItemStateHandlers], currentCoins, Purchase);
                }
                else // if the number store item state handlers is more than store items
                {
                    storeItemStateHandlers[indexStoreItemStateHandlers].Clear();
                }

                indexStoreItemStateHandlers++;
            }
        }

        private void Purchase(StoreItemState itemToPurchase)
        {
            if (itemToPurchase.Purchased)
            {
                Debug.LogError("Item already purchased.", this.gameObject);
                return;
            }

            if (itemToPurchase.StoreItem.Cost > currentCoins.Coins)
            {
                Debug.LogError($"User does not have enough coins ({currentCoins.Coins}).", this.gameObject);
                return;
            }

            storeRepository.RequestPurchase(itemToPurchase.StoreItem, OnPurchaseSuccess, OnPurchaseError);
        }

        private void OnPurchaseSuccess(StoreItem storeItem, int coins)
        {
            this.currentCoins.Coins = coins;

            var storeItemsState = currentStoreItemsStates.FirstOrDefault(s => s.StoreItem.Id == storeItem.Id);
            if (storeItemsState == null)
            {
                Debug.LogError($"Can not find storeItemsState ({storeItem.ToString()}) in currentStoreItemsStates.", this.gameObject);
                Load();
                return;
            }
            storeItemsState.Purchased = true;
            var storeItemsStateHandler = storeItemStateHandlers.FirstOrDefault(s => CheckStoreItemState(s.StoreItemState, storeItem));
            if (storeItemsStateHandler == null)
            {
                Debug.LogError($"Can not find storeItemsStateHandler of ({storeItem.ToString()}) in storeItemStateHandlers.", this.gameObject);
                Load();
                return;
            }
            storeItemsStateHandler.Init(storeItemsState, currentCoins, Purchase);
        }

        private bool CheckStoreItemState(StoreItemState storeItemState, StoreItem storeItem)
        {
            return storeItemState != null && storeItemState.StoreItem.Id == storeItem.Id;
        }

        private void OnPurchaseError(StoreItem storeItem, string error)
        {
            Debug.LogError(error);
            Load();
        }

        private void OnGetStoreItemStateListSuccess(StoreItemState[] storeItemsStates)
        {
            this.currentStoreItemsStates = storeItemsStates;
            UpdateStoreItemStateHandlers();
        }

        private void OnGetStoreItemStateListError(string error)
        {
            Debug.LogError(error);
        }

    }
}