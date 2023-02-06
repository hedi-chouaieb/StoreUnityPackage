using UnityEngine;

namespace Hedi.Me.Store
{
    /// <summary>
    /// Enum of reward type like perfect win or simple win
    /// </summary>
    [CreateAssetMenu(menuName = "StorePackage/CoinTypeData")]
    public class CoinTypeData : ScriptableObject
    {
        [SerializeField] private string typeName;

        public string TypeName { get => typeName; }
    }
}