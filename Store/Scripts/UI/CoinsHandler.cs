using UnityEngine;
using UnityEngine.Events;

namespace Hedi.Me.Store
{
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField] private CoinsData currentCoins;
        [SerializeField] private UnityEventString onUpdateCoins;

        private void Start()
        {
            UpdateCoins(currentCoins.Coins);
        }

        private void OnEnable()
        {
            currentCoins.AddListener(UpdateCoins);
        }

        private void OnDisable()
        {
            currentCoins.RemoveListener(UpdateCoins);
        }

        private void UpdateCoins(int coins)
        {
            onUpdateCoins.Invoke(coins.ToString());
        }

        [System.Serializable] private class UnityEventString : UnityEvent<string> { }
    }
}
