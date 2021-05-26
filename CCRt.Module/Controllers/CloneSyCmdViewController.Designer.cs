namespace CCRt.Module.Controllers
{
    partial class CloneSyCmdViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // CloneSyCmdViewController
            // 
            this.AllowCloneWhenModified = true;
            this.TargetObjectType = typeof(CCRt.Module.BusinessObjects.SyCmdClass);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CustomCloneObject += new System.EventHandler<DevExpress.ExpressApp.CloneObject.CustomCloneObjectEventArgs>(this.CloneSyCmdViewController_CustomCloneObject);
            this.CustomGetCloneActionTargetTypes += new System.EventHandler<DevExpress.ExpressApp.CloneObject.CustomGetCloneActionTargetTypesEventArgs>(this.CloneSyCmdViewController_CustomGetCloneActionTargetTypes);
            this.Activated += new System.EventHandler(this.CloneSyCmdViewController_Activated);

        }

        #endregion
    }
}
