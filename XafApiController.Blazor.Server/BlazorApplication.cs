using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.ExpressApp.Xpo;
using XafApiController.Blazor.Server.Services;
using System.Diagnostics;
using System.Collections.Generic;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates;

namespace XafApiController.Blazor.Server {
    public partial class XafApiControllerBlazorApplication : BlazorApplication {
        public XafApiControllerBlazorApplication() {
            InitializeComponent();
            this.CustomizeTemplate += XafApiControllerBlazorApplication_CustomizeTemplate;
        }
        bool isSecured = true;
        public XafApiControllerBlazorApplication(bool isSecured = true) : this()
        {
            this.isSecured = isSecured;
        }
        List<IFrameTemplate> templates = new List<IFrameTemplate>();
        private void XafApiControllerBlazorApplication_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
        {
            //Debug.WriteLine(e.Template.ToString());
            //if (!templates.Contains(e.Template))
            //    templates.Add(e.Template);


            //var MainWindowsTempalte = e.Template as WindowTemplate;
            //if(MainWindowsTempalte!=null)
            //{
            //    foreach (IActionControlContainer iActionControlContainer in MainWindowsTempalte.ViewActionContainers)
            //    {
            //        Debug.WriteLine(iActionControlContainer.ActionCategory);
            //    }
            //    var test = 1;
            //}
        }
        //protected override IFrameTemplate CreateDefaultTemplate(TemplateContext context)
        //{
        //    //DetailView
        //    if (context == TemplateContext.ApplicationWindow)
        //    {
        //        //return new MyMainWindow();
        //       return new MyMainWindowTemplate() { AboutInfoString = AboutInfo.Instance.GetAboutInfoString(this) };
        //    }
        //    return base.CreateDefaultTemplate(context);
        //}

        protected override void OnSetupStarted() {
            base.OnSetupStarted();
            IConfiguration configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            if(configuration.GetConnectionString("ConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("ConnectionString");
            }
#if EASYTEST
            if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("EasyTestConnectionString");
            }
#endif
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        }
        //protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
        //    IXpoDataStoreProvider dataStoreProvider = GetDataStoreProvider(args.ConnectionString, args.Connection);
        //    args.ObjectSpaceProviders.Add(new SecuredObjectSpaceProvider((ISelectDataSecurityProvider)Security, dataStoreProvider, true));
        //    args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        //}

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            IXpoDataStoreProvider dataStoreProvider = GetDataStoreProvider(args.ConnectionString, args.Connection);
            //TODO we need to use the XPObjectSpaceProvider object fro the shared application because there is no security.
            if (isSecured)
            {
                args.ObjectSpaceProviders.Add(new SecuredObjectSpaceProvider((ISelectDataSecurityProvider)Security, dataStoreProvider, true));
            }
            else
            {
                args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(dataStoreProvider, true));
            }
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            XpoDataStoreProviderAccessor accessor = ServiceProvider.GetRequiredService<XpoDataStoreProviderAccessor>();
            lock(accessor) {
                if(accessor.DataStoreProvider == null) {
                    accessor.DataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, true);
                }
            }
            return accessor.DataStoreProvider;
        }
        private void XafApiControllerBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                string message = "The application cannot connect to the specified database, " +
                    "because the database doesn't exist, its version is older " +
                    "than that of the application or its schema does not match " +
                    "the ORM data model structure. To avoid this error, use one " +
                    "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
