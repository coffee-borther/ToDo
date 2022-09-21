using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using MyToDo.Common;
using MyToDo.Extesions;
using Prism.Events;
using Prism.Services.Dialogs;

namespace MyToDo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IDialogHostService _dialogHost;

        public MainView(IEventAggregator aggregator, IDialogHostService dialogHost)
        {
            _dialogHost = dialogHost;
            InitializeComponent();
            aggregator.ResgiterMessage(arg =>
            {
                Snackbar.MessageQueue.Enqueue(arg);
            });
            aggregator.Resgiter(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new ProgressView();
                }
            });

            MainWindowOperation();
        }

        private void MainWindowOperation()
        {
            this.btnMin.Click += (sender, args) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            this.btnMax.Click += (sender, args) =>
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else this.WindowState = WindowState.Normal;
            };

            this.btnClose.Click += async (sender, args) =>
            {
                var dialogResult = await _dialogHost.Question("温馨提示", "确认退出系统？");
                if (dialogResult.Result != ButtonResult.OK) return;
                this.Close();
            };

            ColorZone.MouseDoubleClick += (sender, args) =>
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else this.WindowState = WindowState.Normal;
            };

            ColorZone.MouseMove += (sender, args) =>
            {
                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.MenuBar.SelectionChanged += (sender, args) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
        }
    }
}
