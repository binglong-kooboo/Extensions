using Kooboo.CMS.Sites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Kooboo.CMS.Common.Persistence.Non_Relational;
using Kooboo.Modules.EventsExplorer.Models;
using Kooboo.Web.Mvc.Paging;
using Kooboo.CMS.Web;
using System.IO;
using Kooboo.CMS.Common;

namespace Kooboo.Modules.EventsExplorer.Controllers
{
    public class HomeController : Kooboo.CMS.Sites.AreaControllerBase
    {
        private readonly IBaseDir _baseDir;
        public HomeController(IBaseDir baseDir)
        {
            _baseDir = baseDir;
        }


        public ActionResult Index(string siteName, string sortField, string sortDir, int? page, int? pageSize)
        {
            Site.Current = SiteHelper.Parse(siteName).AsActual();
            IEnumerable<FileInfo> logFiles = Enumerable.Empty<FileInfo>();

            var logDir = Path.Combine(Site.Current.PhysicalPath, "WebEvents");
            var pagedList = new PagedList<WebEventModel>(new List<WebEventModel>(), 0, 0);
            if (Directory.Exists(logDir))
            {
                var files = Directory.EnumerateFiles(logDir, "*.log", SearchOption.TopDirectoryOnly);
                logFiles = files.Select(it => new FileInfo(it));
                pagedList = logFiles.AsQueryable()
                  .Select(it => new WebEventModel()
                  {
                      FileName = it.Name,
                      FullName = it.FullName.Replace(_baseDir.Cms_DataPhysicalPath, ""),
                      Length = it.Length,
                      CreationDate = it.CreationTime,
                      UtcCreationDate = it.CreationTimeUtc,
                      LastModifiedDate = it.LastWriteTime,
                      UtcLastModifiedDate = it.LastWriteTimeUtc
                  })
                  .SortBy(sortField, sortDir)
                  .ToPagedList(page ?? 1, pageSize ?? 50);
            }

            return View(pagedList);
        }

        public ActionResult Details(string siteName, string fullName, string returnUrl)
        {
            string file = _baseDir.Cms_DataPhysicalPath + fullName;
            if (System.IO.File.Exists(file))
            {
                using (var sr = new StreamReader(file, Encoding.Default))
                {
                    var model = new WebEventDetailsModel
                    {
                        FileInfo = new FileInfo(file),
                        Body = sr.ReadToEnd()
                    };
                    return View(model);
                }
            }
            return Redirect(returnUrl);
        }
    }
}
