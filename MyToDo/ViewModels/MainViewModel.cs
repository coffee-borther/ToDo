using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extesions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainerProvider _container;

        public MainViewModel(IRegionManager regionManager, IContainerProvider container)
        {
            _regionManager = regionManager;
            _container = container;
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (_navigationJournal != null && _navigationJournal.CanGoBack)
                {
                    _navigationJournal.GoBack();
                }
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (_navigationJournal != null && _navigationJournal.CanGoForward)
                {
                    _navigationJournal.GoForward();
                }
            });
            LogoutCommand = new DelegateCommand(() =>
            {
                App.Logout(_container);
            });
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

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        public DelegateCommand LogoutCommand { get; private set; }

        #endregion

        private IRegionNavigationJournal _navigationJournal;

        #region Methods

        public void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar() {Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() {Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() {Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() {Icon = "CogOutline", Title = "设置", NameSpace = "SettingsView" });
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            _regionManager.Regions[PrismManger.MainViewRegionName].RequestNavigate(obj.NameSpace,
                navigationCallback => { _navigationJournal = navigationCallback.Context.NavigationService.Journal; });
        }
        #endregion

        public void Configure()
        {
            UserName = AppSession.UserName;
            CreateMenuBars();
            _regionManager.Regions[PrismManger.MainViewRegionName].RequestNavigate("IndexView");
        }
    }
}