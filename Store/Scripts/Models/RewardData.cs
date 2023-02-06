using UnityEngine;

namespace Hedi.Me.Store
{
    [CreateAssetMenu(menuName = "StorePackage/RewardData")]
    public class RewardData : ScriptableObject
    {
        [SerializeField] private CoinTypeData coinType;
        [SerializeField] private int amount;

        public CoinTypeData CoinType { get => coinType; }
        public int Amount { get => amount; }
    }
}