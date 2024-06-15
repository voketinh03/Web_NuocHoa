using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CK_ASP_NET_CORE.Repository;

namespace CK_ASP_NET_CORE.Repository.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public BrandsViewComponent(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Brands.ToListAsync());
    }
}
