using UnityEngine;

namespace Hedi.Me.Store
{
    public abstract class IStoreManager : MonoBehaviour
    {
        public abstract void Init(IStoreRepository storeRepository, ICoinsData coinsData);
        public abstract void Load();
    }
}