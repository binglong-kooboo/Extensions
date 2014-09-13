using Kooboo.CMS.Web.Grid2;
using Kooboo.ComponentModel;
using Kooboo.Web.Mvc;
using Kooboo.Web.Mvc.Grid2;
using Kooboo.Web.Mvc.Grid2.Design;
using Kooboo.Web.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kooboo.Modules.EventsExplorer.Models
{
    [MetadataFor(typeof(WebEventModel))]
    [Grid(IdProperty = "FileName", Checkable = false)]
    public class WebEventModel
    {
        [GridColumn(Order = 0, GridColumnType = typeof(SortableGridColumn), GridItemColumnType = typeof(WebEventGridItemColumn))]
        public string FileName { get; set; }

        [GridColumn(Order = 1, GridColumnType = typeof(SortableGridColumn))]
        public string FullName { get; set; }

        [GridColumn(Order = 2, GridColumnType = typeof(SortableGridColumn))]
        public DateTime CreationDate { get; set; }

        [GridColumn(Order = 3, GridColumnType = typeof(SortableGridColumn))]
        public DateTime UtcCreationDate { get; set; }

        [GridColumn(Order = 4, GridColumnType = typeof(SortableGridColumn))]
        public DateTime LastModifiedDate { get; set; }

        [GridColumn(Order = 5, GridColumnType = typeof(SortableGridColumn))]
        public DateTime UtcLastModifiedDate { get; set; }

        [GridColumn(Order = 6, GridColumnType = typeof(SortableGridColumn))]
        public long Length { get; set; }
    }

    public class WebEventGridItemColumn : GridItemColumn
    {
        public WebEventGridItemColumn(IGridColumn gridColumn, object dataItem, object propertyValue)
            : base(gridColumn, dataItem, propertyValue)
        {
        }

        public override IHtmlString RenderItemColumn(ViewContext viewContext)
        {
            WebEventModel webEventModel = (WebEventModel)this.DataItem;

            string editLinkUrl = viewContext.UrlHelper()
                .Action("Details", viewContext.RequestContext.AllRouteValues()
                    .Merge("fullname", webEventModel.FullName)
                    .Merge("returnUrl", viewContext.HttpContext.Request.RawUrl));

            string editLinkText = webEventModel.FileName;

            return new HtmlString(String.Format("<a href='{0}'><img class='icon' src='{2}'/> {1}</a>", editLinkUrl, editLinkText, UrlUtility.ResolveUrl("~/Images/invis.gif")));
        }
    }
}
