using System;
using UnityEngine;

namespace Hedi.Me.Store
{
    public abstract class ICoinsRepository : MonoBehaviour
    {
        public abstract void GetUserCoins(Action<int> onGetUserCoinsSuccess, Action<string> onGetUserCoinsError);
        public abstract void RequestAddCoins(string typeName, Action<int> onRequestAddCoinsSuccess, Action<string> onRequestAddCoinsError);
        public abstract void RequestRemoveCoins(int cost, Action<int> OnRequestRemoveCoinsSuccess, Action<string> OnRequestRemoveCoinsError);
    }
}
