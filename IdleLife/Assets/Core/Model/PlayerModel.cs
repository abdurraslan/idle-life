using System;
using Loxodon.Framework.ViewModels;
using UnityEngine;

namespace Core.Model
{
    [System.Serializable]
    public class PlayerModel : ViewModelBase
    {
        [SerializeField]
        private float money;
        public float Money
        {
            get => money;
            set => Set(ref money, value, "Money");
        }
         
        [SerializeField]
        private float diamond;
        public float Diamond
        {
            get => diamond;
            set => Set(ref diamond, value, "Diamond");
        }
    }
}