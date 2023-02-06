using UnityEngine;

namespace Hedi.Me.Store
{
    /// <summary>
    /// Facade defines a higher-level interface that makes the store system easier to use. 
    /// </summary>
    public class StoreFacade : MonoBehaviour
    {
        [SerializeField] private IStoreManager storeManager;
        [SerializeField] private ICoinsManager coinsManager;
        [SerializeField] private ICoinsRepository coinsRepository;
        [SerializeField] private IStoreRepository storeRepository;
        [SerializeField] private ICoinsData coinsData;

        private void OnValidate()
        {
            if (!storeManager) { Debug.LogError("Store Manager is null", this.gameObject);}
            if (!coinsManager) { Debug.LogError("Coins Manager is null", this.gameObject);}
            if (!coinsRepository) { Debug.LogError("Coins Repository is null", this.gameObject);}
            if (!storeRepository) { Debug.LogError("Store Repository is null", this.gameObject);}
            if (!coinsData) { Debug.LogError("Coins Data is null", this.gameObject);}
        }

        private void Start()
        {
            coinsManager.Init(coinsRepository, coinsData);
            storeManager.Init(storeRepository, coinsData);
            Load();
        }

        public void Load()
        {
            coinsManager.Load();
            storeManager.Load();
        }

        public void AddCoins(CoinTypeData coinData)
        {
            coinsManager.AddCoins(coinData);
        }
    }
}