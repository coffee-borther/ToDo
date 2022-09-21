using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class ToDoDto : DtoBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _content;

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }

        private int _status;

        public int Status
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }
    }
}
