using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Hedi.Me.Store
{
    /// <summary>
    /// This class is an example of implementation of ICoinsRepository using unity Player Prefs
    /// but you can use the same abstract base to load the data from files or web api
    /// </summary>
    public class PlayerPrefCoinsRepository : ICoinsRepository
    {
        [SerializeField] private int defaultUserCoins;
        [SerializeField] private string coinsKey = "Coins";
        [SerializeField] private List<RewardData> rewards;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onGetUserCoinsSuccess"></param>
        /// <param name="onGetUserCoinsError"></param>
        public override void GetUserCoins(Action<int> onGetUserCoinsSuccess, Action<string> onGetUserCoinsError)
        {
            if (!PlayerPrefs.HasKey(coinsKey))
            {
                PlayerPrefs.SetInt(coinsKey, defaultUserCoins);
            }

            onGetUserCoinsSuccess(PlayerPrefs.GetInt(coinsKey));
        }

        public override void RequestAddCoins(string typeName, Action<int> onRequestAddCoinsSuccess, Action<string> onRequestAddCoinsError)
        {
            var reward = rewards.FirstOrDefault(r => r.CoinType.TypeName == typeName);

            if (reward == null)
            {
                onRequestAddCoinsError.Invoke($"reward of Type Name ({typeName}) not founded.");
                return;
            }

            if (!PlayerPrefs.HasKey(coinsKey))
            {
                PlayerPrefs.SetInt(coinsKey, defaultUserCoins);
            }

            PlayerPrefs.SetInt(coinsKey, PlayerPrefs.GetInt(coinsKey) + reward.Amount);

            onRequestAddCoinsSuccess(PlayerPrefs.GetInt(coinsKey));
        }

        public override void RequestRemoveCoins(int amount, Action<int> OnRequestRemoveCoinsSuccess, Action<string> OnRequestRemoveCoinsError)
        {
            if (!PlayerPrefs.HasKey(coinsKey))
            {
                PlayerPrefs.SetInt(coinsKey, defaultUserCoins);
            }
            var currentCoins = PlayerPrefs.GetInt(coinsKey);
            if (currentCoins < amount)
            {
                OnRequestRemoveCoinsError?.Invoke($"Current Coins ({currentCoins}) is less then amount to remove ({amount}).");
                return;
            }

            PlayerPrefs.SetInt(coinsKey, currentCoins - amount);

            OnRequestRemoveCoinsSuccess(PlayerPrefs.GetInt(coinsKey));
        }
    }
}
