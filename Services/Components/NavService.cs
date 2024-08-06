using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Components
{
    public class NavService
    {
        public event Action OnChange;
        public List<NavItemModel> NavItems { get; private set; } = new List<NavItemModel>
        {
            new NavItemModel { Text = "Home", Url = "/", Icon = "bi bi-house-door-fill", IsVisible = true },
            new NavItemModel { Text = "Endereço", Url = "endereco", Icon = "bi bi-house", IsVisible = true },
            //new NavItem { Text = "Counter", Url = "counter", Icon = "bi bi-plus-square-fill", IsVisible = true },
            //new NavItem { Text = "Counter", Url = "counter", Icon = "bi bi-alarm-fill", IsVisible = true },
            //new NavItem { Text = "Weather", Url = "weather", Icon = "bi bi-list-nested", IsVisible = true },
            //new NavItem { Text = "Sair", Url = "login", Icon = "bi bi-list-nested", IsVisible = false },
        };

        public void AddNavItem(NavItemModel item)
        {
            NavItems.Add(item);
            NotifyStateChanged();
        }

        public void RemoveNavItem(string url)
        {
            var item = NavItems.FirstOrDefault(i => i.Url == url);
            if (item != null)
            {
                NavItems.Remove(item);
                NotifyStateChanged();
            }
        }

        public void UpdateNavItemVisibility(string url, bool isVisible)
        {
            var item = NavItems.FirstOrDefault(i => i.Url == url);
            if (item != null)
            {
                item.IsVisible = isVisible;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
