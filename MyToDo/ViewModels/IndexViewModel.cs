using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extesions;
using MyToDo.Services;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IDialogHostService _dialog;

        private readonly IToDoService _todoService;
        private readonly IMemoService _memoService;
        private readonly IRegionManager regionManager;

        public IndexViewModel(IDialogHostService dialog, IContainerProvider containerProvider) : base(containerProvider)
        {
            _dialog = dialog;
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            Welcome = $"你好{AppSession.UserName}，今天是" + DateTime.Today.ToLongDateString() + " " + DateTime.Today.DayOfWeek;
            _todoService = containerProvider.Resolve<IToDoService>();
            _memoService = containerProvider.Resolve<IMemoService>();
            regionManager = containerProvider.Resolve<IRegionManager>();
        }

        #region Prop

        private ObservableCollection<TaskBar> _taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get => _taskBars;
            set
            {
                _taskBars = value;
                RaisePropertyChanged(nameof(_taskBars));
            }
        }

        [AllowNull] private string _welcome;

        public string Welcome
        {
            get => _welcome;
            set
            {
                _welcome = value;
                RaisePropertyChanged(nameof(_welcome));
            }
        }

        private SummaryDto _summary;

        public SummaryDto Summary
        {
            get => _summary;
            set
            {
                _summary = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command

        public DelegateCommand<string> ExecuteCommand => new DelegateCommand<string>(Execute);
        public DelegateCommand<ToDoDto> EditToDoCommand => new DelegateCommand<ToDoDto>(AddToDo);
        public DelegateCommand<MemoDto> EditMemoCommand => new DelegateCommand<MemoDto>(AddMemo);
        public DelegateCommand<ToDoDto> ToDoCompletedCommand => new DelegateCommand<ToDoDto>(Completed);
        public DelegateCommand<TaskBar> NavigateCommand => new DelegateCommand<TaskBar>(Navigate);

        #endregion

        #region Method

        private async void Completed(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var updateResult = await _todoService.UpdateAsync(obj);
                if (updateResult.Status)
                {
                    var todo = Summary.ToDoList.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (todo != null)
                    {
                        Summary.CompletedCount += 1;
                        Summary.CompletedRatio = (Summary.CompletedCount / (double)Summary.Sum).ToString("0%");
                        Summary.ToDoList.Remove(todo);
                        Refresh();
                    }
                    aggregator.SendMessage("已完成！");
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar()
                {Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "ToDoView"});
            TaskBars.Add(new TaskBar()
                {Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = "ToDoView"});
            TaskBars.Add(new TaskBar()
                {Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = ""});
            TaskBars.Add(new TaskBar()
                {Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = "MemoView"});
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "AddToDo":
                    AddToDo(null);
                    break;
                case "AddMemo":
                    AddMemo(null);
                    break;
            }
        }

        private async void AddToDo(ToDoDto model)
        {
            DialogParameters parameters = new DialogParameters();
            if (model != null)
                parameters.Add("Value", model);
            var dialogResult = await _dialog.ShowDialog("AddToDoView", parameters);
            if (dialogResult.Result == ButtonResult.OK)
            {
                try
                {
                    UpdateLoading(true);
                    var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value");
                    if (todo.Id > 0)
                    {
                        var updateResult = await _todoService.UpdateAsync(todo);
                        if (updateResult.Status)
                        {
                            var todoModel = Summary.ToDoList.FirstOrDefault(t => t.Id.Equals(todo.Id));
                            if (todoModel != null)
                            {
                                todoModel.Title = todo.Title;
                                todoModel.Content = todo.Content;
                            }
                        }
                    }
                    else
                    {
                        var AddResult = await _todoService.AddAsync(todo);
                        if (AddResult.Status)
                        {
                            Summary.Sum += 1;
                            Summary.ToDoList.Add(AddResult.Result);
                            Summary.CompletedRatio = (Summary.CompletedCount / (double)Summary.Sum).ToString("0%");
                            Refresh();
                        }
                    }
                }
                finally
                {
                    UpdateLoading(false);
                }
            }
        }

        private async void AddMemo(MemoDto model)
        {
            DialogParameters parameters = new DialogParameters();
            if (model != null)
                parameters.Add("Value", model);
            var dialogResult = await _dialog.ShowDialog("AddMemoView", parameters);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                if (memo.Id > 0)
                {
                    var updateResult = await _memoService.UpdateAsync(memo);
                    if (updateResult.Status)
                    {
                        var memoModel = Summary.MemoList.FirstOrDefault(t => t.Id.Equals(memo.Id));
                        if (memoModel != null)
                        {
                            memoModel.Title = memo.Title;
                            memoModel.Content = memo.Content;
                        }
                    }
                }
                else
                {
                    var AddResult = await _memoService.AddAsync(memo);
                    if (AddResult.Status)
                    {
                        Summary.MemoList.Add(AddResult.Result);
                    }
                }
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var summaryResult = await _todoService.GetSummaryAsync();
            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                Refresh();
            }
            base.OnNavigatedTo(navigationContext);
        }

        private void Refresh()
        {
            TaskBars[0].Content = Summary.Sum.ToString();
            TaskBars[1].Content = Summary.CompletedCount.ToString();
            TaskBars[2].Content = Summary.CompletedRatio;
            TaskBars[3].Content = Summary.MemoCount.ToString();
        }

        private void Navigate(TaskBar obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Target))
                return;
            NavigationParameters parameters = new NavigationParameters();
            if (obj.Title == "已完成")
            {
                parameters.Add("Value", 2);
            }
            regionManager.Regions[PrismManger.MainViewRegionName].RequestNavigate(obj.Target, parameters);
        }

        #endregion
    }
}