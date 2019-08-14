using System;
using Core.Model;
using Core.Utils;
using Loxodon.Framework.ViewModels;
using UnityEngine;

namespace Core.ViewModel
{
    [System.Serializable]
    public class JobViewModel : ViewModelBase
    {
        /// <summary>
        /// This will handled by unity inspector
        /// </summary>
        public JobModel Model;

        [NonSerialized] public PlayerModel Player;
        [NonSerialized] private JobMathOperation operation;

        private float currentWorkerPrice;
        private float currentUpgradePrice;
        private float currentEarning;
        private float maxExp;
        private float price;
        private bool canPlayerBuy;
        private int clickCount;
        
        /// <summary>
        /// 
        /// </summary>
        public bool CanPlayerBuy
        {
            get => canPlayerBuy;
            private set => Set(ref canPlayerBuy, value, nameof(CanPlayerBuy));
        }

        /// <summary>
        /// Get's or sets the current upgrading price.
        /// </summary>
        public float CurrentUpgradePrice
        {
            get => currentUpgradePrice;
            private set => Set(ref currentUpgradePrice, value, nameof(CurrentUpgradePrice));
        }

        /// <summary>
        /// Get's or sets the current upgrading price.
        /// </summary>
        public float CurrentEarning
        {
            get => currentEarning;
            private set => Set(ref currentEarning, value, nameof(CurrentEarning));
        }

        /// <summary>
        /// Get's or sets the current upgrading price.
        /// </summary>
        public float MaxExp
        {
            get => maxExp;
            private set => Set(ref maxExp, value, nameof(MaxExp));
        }

        /// <summary>
        /// Get's or sets the current worker price
        /// </summary>
        public float CurrentWorkerPrice
        {
            get => currentWorkerPrice;
            private set => Set(ref currentWorkerPrice, value, nameof(CurrentWorkerPrice));
        }

        /// <summary>
        /// Get's or sets the current worker price
        /// </summary>
        public float Price
        {
            get => price;
            private set => Set(ref price, value, nameof(Price));
        }


        public void Initialize(PlayerModel player)
        {
            this.Player = player;
            this.operation = new JobMathOperation(Model, Player);
            this.Price = operation.GetJobInitialPrice();
            this.CurrentUpgradePrice = this.operation.GetCurrentUpgradePrice();
            this.CurrentWorkerPrice = this.operation.GetCurrentWorkerPrice();
            this.CurrentEarning = this.operation.GetCurrentEarning();
            this.MaxExp = this.operation.GetMaxExp();
            this.CanPlayerBuy = this.operation.CanPlayerBuy();
        }


        public void OnUpgradeClicked()
        {
            Player.Money -= CurrentUpgradePrice;
            Model.UpgradeCount++;
            Model.Exp++;
            if (Model.Exp >= MaxExp)
            {
                Model.Exp = 0;
                Model.Level++;
                this.MaxExp = this.operation.GetMaxExp();
            }
            this.CurrentUpgradePrice = operation.GetCurrentUpgradePrice();
            this.CurrentEarning = operation.GetCurrentEarning();
        }

        public void OnBuyWorkerClicked()
        {
            Player.Money -= CurrentWorkerPrice;
            Model.WorkerCount++;
            CurrentWorkerPrice = operation.GetCurrentWorkerPrice();
        }

        public void OnWorkClicked()
        {
            Model.WorkProgress += operation.GetPlayerWorkProgress();
        }

        public void OnFixedUpdate()
        {
            Model.WorkProgress += (operation.GetCurrentWorkerProgress());
            this.CanPlayerBuy = operation.CanPlayerBuy();
            if (Model.WorkProgress > 1)
            {
                JobCompleted();
            }
        }

        private void JobCompleted()
        {
            Model.WorkProgress = 0;
            Player.Money += CurrentEarning;
        }

        public void OnBuyClicked()
        {
            Model.IsSold = true;
            Player.Money -= price;
        }
    }
}