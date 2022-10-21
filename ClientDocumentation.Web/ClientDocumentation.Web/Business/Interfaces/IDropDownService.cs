using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface IDropDownService
    {
        List<DropdownListItem> GetDropdownValues(int dataTypeId, int? selectedId = null);
        List<DropdownViewModel> GetAllDropdowns();

    }
}
