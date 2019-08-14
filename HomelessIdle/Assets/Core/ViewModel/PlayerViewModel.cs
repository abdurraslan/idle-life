using Core.Model;
using Core.View;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;

namespace Core.ViewModel
{
    public class PlayerViewModel : UIView
    {
        public PlayerModel Player;
        public PlayerView View;
        protected override void Start()
        {
            View.SetDataContext(Player);
        }
    }
}