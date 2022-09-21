using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Common.Models;
using MyToDo.Extesions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public SettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBars();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
        }

        #region Properties

        [AllowNull] private ObservableCollection<MenuBar> _menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get => _menuBars;
            set
            {
                _menuBars = value;
                RaisePropertyChanged(nameof(_menuBars));
            }
        }

        #endregion

        #region Command

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        #endregion

        #region Method

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            _regionManager.Regions[PrismManger.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }

        public void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", NameSpace = "" });
            MenuBars.Add(new MenuBar() { Icon = "Information", Title = "关于更多", NameSpace = "AboutView" });
        }
        #endregion
    }
}