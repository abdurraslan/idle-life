using Core.Model;
using Core.ViewModel;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views;
using TMPro;
using UnityEngine.UI;

namespace Core.View
{
    public class PlayerView : UIView
    {
        public TextMeshProUGUI Money;
        public TextMeshProUGUI Diamond;

        protected override void Start()
        {
            BindingSet<PlayerView, PlayerModel> bindingSet = this.CreateBindingSet<PlayerView, PlayerModel>();

            //Text Bind
            bindingSet.Bind(this.Money).For(v => v.text)
                .ToExpression(vm => $"${vm.Money:0.00}").OneWay();

            bindingSet.Bind(this.Diamond).For(v => v.text)
                .To(vvm => vvm.Diamond).OneWay();

            bindingSet.Build();
        }
    }
}