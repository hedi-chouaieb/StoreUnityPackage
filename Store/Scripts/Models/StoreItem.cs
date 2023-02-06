using System.Collections.Generic;
using UnityEngine;

namespace Hedi.Me.Store
{
    [System.Serializable]
    public class StoreItem
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private int cost;
        [SerializeField] private Sprite image;

        public StoreItem()
        {
        }

        public StoreItem(int id, string name, int cost, Sprite image)
        {
            this.id = id;
            this.name = name;
            this.cost = cost;
            this.image = image;
        }

        public string Name { get => name; set => name = value; }
        public int Cost { get => cost; set => cost = value; }
        public Sprite Image { get => image; set => image = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}