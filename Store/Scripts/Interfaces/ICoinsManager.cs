using UnityEngine;

namespace Hedi.Me.Store
{
    public abstract class ICoinsManager : MonoBehaviour
    {
        public abstract void Init(ICoinsRepository coinsRepository, ICoinsData coinsData);
        public abstract void AddCoins(CoinTypeData coinData);
        public abstract void Load();
    }
}
