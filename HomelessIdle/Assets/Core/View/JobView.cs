using System;
using System.Collections.Generic;
using Core.Model;
using Core.ViewModel;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class JobView : UIView
    {
        public Image JobImage;
        public Image MaxProgressBackground;

        public Slider WorkProgress;
        public Slider ExpProgress;
        public TextMeshProUGUI Exp;
        public TextMeshProUGUI WorkerCount;
        public TextMeshProUGUI CurrentEarning;
        public TextMeshProUGUI UpgradePrice;
        public TextMeshProUGUI WorkerPrice;
        public TextMeshProUGUI Level;
        public TextMeshProUGUI JobPrice;

        public Button WorkButton;
        public Button UpgradeButton;
        public Button BuyWorkerButton;
        private GameObject buyWorkerGameObject;
        public Button BuyButton;
        public Image BuyButtonBackground;

        public TextMeshProUGUI JobDescription;

        public GameObject SoldPanel;
        public GameObject BuyPanel;

        public ParticleSystem LevelUpParticleSystem;
        public ParticleSystem ProgressCompletedParticleSystem;

        protected override void Start()
        {
            buyWorkerGameObject = BuyWorkerButton.gameObject;

            BindingSet<JobView, JobViewModel> bindingSet = this.CreateBindingSet<JobView, JobViewModel>();

            //One Time Bind
            bindingSet.Bind(this.JobDescription).For(v => v.text)
                .To(vvm => vvm.Model.Description).OneTime();

            bindingSet.Bind(this.JobImage).For(v => v.sprite)
                .To(vvm => vvm.Model.Icon).OneTime();

            bindingSet.Bind(this.BuyButtonBackground).For(v => v.color)
                .ToExpression(vvm => vvm.CanPlayerBuy ? Color.green : Color.white)
                .OneWay();

            bindingSet.Bind(this.BuyButton).For(v => v.interactable)
                .ToExpression(vvm => vvm.CanPlayerBuy).OneWay();

            bindingSet.Bind(this.WorkProgress).For(v => v.value)
                .To(vvm => vvm.Model.WorkProgress);

            bindingSet.Bind(this.Exp).For(v => v.text)
                .ToExpression(vvm => string.Concat(vvm.Model.Exp, "/", vvm.MaxExp));

            bindingSet.Bind(this.WorkerCount).For(v => v.text)
                .To(vvm => vvm.Model.WorkerCount).OneWay();

            bindingSet.Bind(this.WorkerPrice).For(v => v.text)
                .ToExpression(vvm => $"${vvm.CurrentWorkerPrice:0.00}").OneWay();

            bindingSet.Bind(this.UpgradePrice).For(v => v.text)
                .ToExpression(vvm => $"${vvm.CurrentUpgradePrice:0.00}").OneWay();

            bindingSet.Bind(this.CurrentEarning).For(v => v.text)
                .ToExpression(vvm => $"${vvm.CurrentEarning:0.00}").OneWay();

            bindingSet.Bind(this.JobPrice).For(v => v.text)
                .ToExpression(vvm => $"${vvm.Price}").OneTime();

            //Button bind
            bindingSet.Bind(this.UpgradeButton).For(v => v.onClick)
                .To(vvm => vvm.OnUpgradeClicked);
            bindingSet.Bind(this.BuyWorkerButton).For(v => v.onClick)
                .To(vvm => vvm.OnBuyWorkerClicked);
            bindingSet.Bind(this.BuyButton).For(v => v.onClick)
                .To(vvm => vvm.OnBuyClicked);
            bindingSet.Bind(this.WorkButton).For(v => v.onClick)
                .To(vvm => vvm.OnWorkClicked);

            //Expression Bind
            bindingSet.Bind(this.WorkButton).For(v => v.interactable)
                .ToExpression(vvm => vvm.Model.IsSold);
            bindingSet.Bind(this.UpgradeButton).For(v => v.interactable)
                .ToExpression(vvm => vvm.Player.Money >= vvm.CurrentUpgradePrice)
                .OneWay();
            bindingSet.Bind(this.Level).For(v => v.text)
                .ToExpression(vvm => vvm.Model.Level + "lvl").OneWay();

            bindingSet.Bind(this.buyWorkerGameObject).For(v => v.activeSelf)
                .ToExpression(vvm => vvm.Model.WorkerCount > 0)
                .OneWay();

            bindingSet.Bind(this.ExpProgress).For(v => v.value)
                .ToExpression(vvm => (float) vvm.Model.Exp / vvm.MaxExp)
                .OneWay();

            bindingSet.Bind(this.BuyWorkerButton).For(v => v.interactable)
                .ToExpression(vvm => vvm.Player.Money >= vvm.CurrentWorkerPrice)
                .OneWay();
            bindingSet.Bind(this.BuyPanel).For(v => v.activeSelf)
                .ToExpression(vvm => !vvm.Model.IsSold);

            bindingSet.Bind(this.SoldPanel).For(v => v.activeSelf)
                .To(vvm => vvm.Model.IsSold);

            bindingSet.Build();
        }
    }
}