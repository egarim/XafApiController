using DevExpress.ExpressApp.Blazor.Templates;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XafApiController.Blazor.Server
{
    public class MyMainWindowTemplate: WindowTemplate
    {
        public MyMainWindowTemplate()
        {

        }
        protected override RenderFragment CreateControl()
        {
            return NewMainWindow.Creator(this);
        }
    }
}
