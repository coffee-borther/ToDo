using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ImTools;
using Microsoft.Win32;
using MyToDo.Common;
using MyToDo.Common.Models;
using MyToDo.Extesions;
using MyToDo.Services;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using ToDoDto = MyToDo.Shared.Dtos.ToDoDto;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private IToDoService _service;
        private readonly IDialogHostService dialogHost;

        public ToDoViewModel(IToDoService service, IContainerProvider containerProvider) : base(containerProvider)
        {
            _service = service;
            ToDoDtos = new ObservableCollection<ToDoDto>();
            dialogHost = containerProvider.Resolve<IDialogHostService>();
        }

        #region Prop

        private ObservableCollection<ToDoDto> _toDoDtos;

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get => _toDoDtos;
            set
            {
                _toDoDtos = value;
                RaisePropertyChanged();
            }
        }

        private bool _isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get => _isRightDrawerOpen;
            set
            {
                _isRightDrawerOpen = value;
                RaisePropertyChanged();
            }
        }

        private ToDoDto _currentDto;

        public ToDoDto CurrentDto
        {
            get => _currentDto;
            set
            {
                _currentDto = value;
                RaisePropertyChanged();
            }
        }

        private string _searchString;

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        #region Command

        public DelegateCommand<string> ExecuteCommand => new DelegateCommand<string>(Execute);

        public DelegateCommand<ToDoDto> SelectedCommand => new DelegateCommand<ToDoDto>(Selected);

        public DelegateCommand<ToDoDto> DeleteCommand => new DelegateCommand<ToDoDto>(Delete);

        #endregion

        #region Method

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Add":
                    Add();
                    break;
                case "Search":
                    GetDataAsync();
                    break;
                case "Save":
                    Save();
                    break;
            }
        }

        public async void GetDataAsync()
        {
            int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;
            UpdateLoading(true);
            var todoResult = await _service.GetAllFilterAsync(new ToDoParameters()
            {
                PageIndex = 0,
                PageSize = 100,
                SearchString = SearchString,
                Status = Status
            });
            if (todoResult.Status)
            {
                ToDoDtos.Clear();
                foreach (var item in todoResult.Result.Items)
                {
                    ToDoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext.Parameters.ContainsKey("Value"))
                SelectedIndex = navigationContext.Parameters.GetValue<int>("Value");
            else
                SelectedIndex = 0;
                GetDataAsync();
        }

        private void Add()
        {
            CurrentDto = new ToDoDto();
            IsRightDrawerOpen = true;
        }

        private async void Selected(ToDoDto obj)
        {
            try
            {
                UpdateLoading(true);
                var selectedResult = await _service.GetFirstOfDefaultAsync(obj.Id);

                if (selectedResult.Status)
                {
                    CurrentDto = selectedResult.Result;
                    IsRightDrawerOpen = true;
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) || string.IsNullOrWhiteSpace(CurrentDto.Content)) return;
            UpdateLoading(true);
            try
            {
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await _service.UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var todo = ToDoDtos.FirstOrDefault(t => t.Id.Equals(CurrentDto.Id));
                        if (todo != null)
                        {
                            todo.Title = CurrentDto.Title;
                            todo.Content = CurrentDto.Content;
                            todo.Status = CurrentDto.Status;
                        }
                    }
                }
                else
                {
                    var AddResult = await _service.AddAsync(CurrentDto);
                    if (AddResult.Status)
                    {
                        ToDoDtos.Add(AddResult.Result);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                IsRightDrawerOpen = false;
                UpdateLoading(false);
            }
        }

        private async void Delete(ToDoDto obj)
        {
            var dialogResult = await dialogHost.Question("温馨提示", $"确认删除待办事项{obj.Title}吗？");
            if (dialogResult.Result != ButtonResult.OK) return;
            UpdateLoading(true);
            try
            {
                var deleteResult = await _service.DeleteAsync(obj.Id);
                if (deleteResult.Status)
                {
                    var model = ToDoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                    {
                        ToDoDtos.Remove(model);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        #endregion
    }
}