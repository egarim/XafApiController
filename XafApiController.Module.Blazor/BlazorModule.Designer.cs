namespace XafApiController.Module.Blazor {
    partial class XafApiControllerBlazorModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            // 
            // XafApiControllerBlazorModule
            // 
            this.RequiredModuleTypes.Add(typeof(XafApiController.Module.XafApiControllerModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.FileAttachments.Blazor.FileAttachmentsBlazorModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.Blazor.ReportsBlazorModuleV2));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.Blazor.ValidationBlazorModule));
        }

        #endregion
    }
}
