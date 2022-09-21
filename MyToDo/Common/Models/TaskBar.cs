using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MyToDo.Common.Models
{
    /// <summary>
    /// 任务栏
    /// </summary>
    public class TaskBar : BindableBase
    {
        /// <summary>
        /// 图标
        /// </summary>
        [AllowNull] private string icon;

        public string Icon
        {
            get => icon;
            set { icon = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        [AllowNull] private string title;

        public string Title
        {
            get => title;
            set { title = value; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        [AllowNull] private string content;

        public string Content
        {
            get => content;
            set
            {
                content = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        [AllowNull] private string color;

        public string Color
        {
            get => color;
            set { color = value; }
        }

        /// <summary>
        /// 触发对象
        /// </summary>
        [AllowNull] private string target;

        public string Target
        {
            get => target;
            set { target = value; }
        }
    }
}