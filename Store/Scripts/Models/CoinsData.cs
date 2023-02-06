using UnityEngine;
using UnityEngine.Events;

namespace Hedi.Me.Store
{
    [CreateAssetMenu(menuName = "StorePackage/CoinsData")]
    public class CoinsData : ICoinsData
    {
        [SerializeField] private int coins;
        [SerializeField] private UnityEventInt onUpdateCurrentCoins = new UnityEventInt();

        public override int Coins
        {
            get => this.coins;
            set
            {
                this.coins = value;
                onUpdateCurrentCoins?.Invoke(coins);
            }
        }

        public override void AddListener(UnityAction<int> listener)
        {
            onUpdateCurrentCoins?.AddListener(listener);
        }


        public override void RemoveListener(UnityAction<int> listener)
        {
            onUpdateCurrentCoins?.RemoveListener(listener);
        }

        private class UnityEventInt : UnityEvent<int> { }
    }
}
