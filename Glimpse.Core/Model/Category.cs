
namespace Glimpse.Core.Model
{
    public class Category
    {
        public Category(Categories categories)
        {
            Categories = categories;
        }

        public Categories Categories { get; set; }
    }
}
