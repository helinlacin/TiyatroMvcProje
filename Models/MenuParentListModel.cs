using EntityLayer;

namespace TiyatroProje.Models
{
    public class MenuParentListModel
    {
        public Menu MenuModel { get; set; }
        public IEnumerable<Menu> MenuListModel { get; set; }
    }
}
