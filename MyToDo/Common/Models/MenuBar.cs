using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    /// <summary>
    /// 系统导航菜单实体类
    /// </summary>
    public class MenuBar
    {
        [AllowNull] private string _icon;

        public string Icon
        {
            get => _icon;
            set { _icon = value; }
        }

        [AllowNull]private string _title;

        public string Title
        {
            get => _title;
            set { _title = value; }
        }

        [AllowNull] private string _nameSpace;

        public string NameSpace
        {
            get => _nameSpace;
            set { _nameSpace = value; }
        }
    }
}
