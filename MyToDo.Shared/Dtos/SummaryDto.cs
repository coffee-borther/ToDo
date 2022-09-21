using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class SummaryDto : DtoBase
    {
        private int _sum;

        public int Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                RaisePropertyChanged();
            }
        }

        private int _completedCount;

        public int CompletedCount
        {
            get => _completedCount;
            set
            {
                _completedCount = value;
                RaisePropertyChanged();
            }
        }

        private int _memoCount;

        public int MemoCount
        {
            get => _memoCount;
            set
            {
                _memoCount = value;
                RaisePropertyChanged();
            }
        }

        private string _completedRatio;

        public string CompletedRatio
        {
            get => _completedRatio;
            set
            {
                _completedRatio = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ToDoDto> _todoList;

        public ObservableCollection<ToDoDto> ToDoList
        {
            get => _todoList;
            set
            {
                _todoList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MemoDto> _memoList;

        public ObservableCollection<MemoDto> MemoList
        {
            get => _memoList;
            set
            {
                _memoList = value;
                RaisePropertyChanged();
            }
        }
    }
}
