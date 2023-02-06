using System;
using UnityEngine;

namespace Hedi.Me.Store
{
    /// <summary>
    /// This class is an example of implementation of IStoreRepository using unity Player Prefs
    /// but you can use the same abstract base to load the data from files or web api
    /// </summary>
    public class PlayerPrefStoreRepository : IStoreRepository
    {
        [SerializeField] private string storeItemsKey = "StoreItem";
        [SerializeField] private ICoinsRepository coinsRepository;
        [SerializeField] private StoreItemData[] storeItems;

        public override void GetStoreItemStateList(Action<StoreItemState[]> onGetStoreItemStateListSuccess, Action<string> onGetStoreItemStateListError)
        {
            var storeItemStates = new StoreItemState[storeItems.Length];
            for (int indexStoreItems = 0; indexStoreItems < storeItemStates.Length; indexStoreItems++)
            {
                string key = CreateKey(storeItems[indexStoreItems].StoreItem);
                storeItemStates[indexStoreItems] = new StoreItemState(storeItems[indexStoreItems].StoreItem,
                                                                        PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1);
            }

            onGetStoreItemStateListSuccess(storeItemStates);
        }

        public override void RequestPurchase(StoreItem storeItem, Action<StoreItem, int> onPurchaseSuccess, Action<StoreItem, string> onPurchaseError)
        {
            string key = CreateKey(storeItem);

            if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1)
            {
                onPurchaseError(storeItem, $"Store Item ({storeItem}) already purchased.");
                return;
            }

            coinsRepository.RequestRemoveCoins(storeItem.Cost,
                                                (coins) => OnRequestRemoveCoinsSuccess(storeItem, onPurchaseSuccess, key, coins),
                                                (error) => OnRequestRemoveCoinsError(error, storeItem, onPurchaseError));
        }

        private void OnRequestRemoveCoinsSuccess(StoreItem storeItem, Action<StoreItem, int> onPurchaseSuccess, string key, int coins)
        {
            PlayerPrefs.SetInt(key, 1);
            onPurchaseSuccess(storeItem, coins);
        }

        private void OnRequestRemoveCoinsError(string error, StoreItem storeItem, Action<StoreItem, string> onPurchaseError)
        {
            onPurchaseError(storeItem, error);
        }

        private string CreateKey(StoreItem storeItem)
        {
            return storeItemsKey + "_" + storeItem.Id.ToString();
        }
    }
}
