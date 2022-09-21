using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class DtoBase
    {
        [AllowNull]private string _id;

        public string ID
        {
            get => _id;
            set { _id = value; }
        }

        [AllowNull]private string _createDateTime;

        public string CreateDateTime
        {
            get => _createDateTime;
            set { _createDateTime = value; }
        }

        [AllowNull]private string _updateDateTime;

        public string UpdateDateTime
        {
            get => _updateDateTime;
            set { _updateDateTime = value; }
        }
    }
}
