using Kooboo.CMS.Common.Runtime.Dependency;
using Kooboo.CMS.Sites.Extension.UI;
using Kooboo.CMS.Sites.Extension.UI.TopToolbar;
using Kooboo.Web.Mvc.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Kooboo.Globalization;

namespace Kooboo.Modules.EventsExplorer.UI
{
    [Dependency(typeof(IToolbarProvider), Key = "EventsExporerToolbarProvider")]
    public class EventsExporerToolbarProvider : IToolbarProvider
    {
        public MvcRoute[] ApplyTo
        {
            get
            {
                return new[] {new MvcRoute(){
                    Action="Diagnosis",
                    Controller="System",
                    Area="Sites"
                } };
            }
        }

        public IEnumerable<ToolbarButton> GetButtons(RequestContext requestContext)
        {
            return new[] { 
                new ToolbarButton(){
                    CommandText="View Events",CommandTarget=new MvcRoute{Action="Index",Controller="Home",Area=EventsExplorerAreaRegistration.Name},
                    IconClass="preview",
                    HtmlAttributes=new Dictionary<string,object>{
                        {"title","View WebEvents".Localize()}
                    }
                }
            };
        }

        public IEnumerable<ToolbarGroup> GetGroups(RequestContext requestContext)
        {
            return Enumerable.Empty<ToolbarGroup>();
        }
    }
}
