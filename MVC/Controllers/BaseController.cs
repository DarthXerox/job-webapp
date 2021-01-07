using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class BaseController : Controller
    {
        private const string pageSizeSession = "PageSizeSession";

        protected int PageSize
        {
            get
            {
                var pageSize = HttpContext.Session.GetInt32(pageSizeSession);
                if (!pageSize.HasValue)
                {
                    pageSize = 10;
                    HttpContext.Session.SetInt32(pageSizeSession, pageSize.Value);
                }
                return pageSize.Value;
            }
            set => HttpContext.Session.SetInt32(pageSizeSession, value);
        }
    }
}
