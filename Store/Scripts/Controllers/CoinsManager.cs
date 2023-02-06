using UnityEngine;
using UnityEngine.Events;

namespace Hedi.Me.Store
{
    public class CoinsManager : ICoinsManager
    {
        [SerializeField] private ICoinsRepository coinsRepository;
        [SerializeField] private ICoinsData currentCoins;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="coinsRepository"></param>
        /// <param name="coinsData"></param>
        public override void Init(ICoinsRepository coinsRepository, ICoinsData coinsData)
        {
            this.coinsRepository = coinsRepository;
            this.currentCoins = coinsData;
        }

        /// <summary>
        /// Add coins to the user from reward or wins or anything else.
        /// </summary>
        /// <param name="coinData"></param>
        public override void AddCoins(CoinTypeData coinData)
        {
            coinsRepository.RequestAddCoins(coinData.TypeName, OnRequestAddCoinsSuccess, OnRequestAddCoinsError);
        }

        /// <summary>
        /// Load the user coins data.
        /// </summary>
        /// <param name="coinData"></param>
        public override void Load()
        {
            coinsRepository.GetUserCoins(OnGetUserCoinsSuccess, OnGetUserCoinsError);
        }

        private void OnRequestAddCoinsSuccess(int coins)
        {
            this.currentCoins.Coins = coins;
        }

        private void OnRequestAddCoinsError(string error)
        {
            Debug.LogError(error, this.gameObject);
        }

        private void OnGetUserCoinsSuccess(int coins)
        {
            this.currentCoins.Coins = coins;
        }

        private void OnGetUserCoinsError(string error)
        {
            Debug.LogError(error, this.gameObject);
        }

        private class UnityEventInt : UnityEvent<int> { }
    }
}
