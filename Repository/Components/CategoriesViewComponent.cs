using CK_ASP_NET_CORE.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CK_ASP_NET_CORE.Repository.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public CategoriesViewComponent(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Categories.ToListAsync());
    }
}
