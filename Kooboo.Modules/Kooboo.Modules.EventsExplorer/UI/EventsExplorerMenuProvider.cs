using Kooboo.CMS.Common.Runtime.Dependency;
using Kooboo.CMS.Sites.Extension.UI.GlobalSidebarMenu;
using Kooboo.Web.Mvc.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kooboo.Modules.EventsExplorer.UI
{
    [Dependency(typeof(IGlobalSidebarMenuItemProvider))]
    public class EventsExplorerMenuProvider : IGlobalSidebarMenuItemProvider
    {
        public IEnumerable<MenuItem> GetMenuItems(ControllerContext controllerContext)
        {
            var service = new ModuleGlobalSidebarMenuItemProvider();
            return new[] { 
                new MenuItem() { Action = "Index", Controller = "Home", Area = EventsExplorerAreaRegistration.Name, Name = "EventsExplorer", Text = "Events Explorer" } 
            };
        }
    }
}
