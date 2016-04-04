using KS.SportsPool.Component.Caching.Implementation;
using KS.SportsPool.Component.Caching.Interface;
using KS.SportsPool.Data.DataAccess.Repository.Implementation;
using KS.SportsPool.Data.DataAccess.Repository.Interface;
using System.Web.Mvc;

namespace KS.SportsPool.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected ICacheProvider CacheProvider { get; set; }
        protected IRepositoryCollection Repository { get; set; }

        public BaseController()
        {
            CacheProvider = MemoryCacheProvider.Instance;
            Repository = new DapperRepositoryCollection(CacheProvider);
        }

        public BaseController(ICacheProvider cacheProvider, IRepositoryCollection repository)
        {
            CacheProvider = cacheProvider;
            Repository = repository;
        }
    }
}
