namespace CCRt.Module.Controllers
{
    partial class UpdateListPartNumberViewControllercs
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
            this.UpdateListPartNumberAction = new DevExpress.ExpressApp.Actions.SimpleAction();
            // 
            // UpdateListPartNumberAction
            // 
            this.UpdateListPartNumberAction.Caption = "Update List Part Number Action";
            this.UpdateListPartNumberAction.ConfirmationMessage = null;
            this.UpdateListPartNumberAction.Id = "UpdateListPartNumberAction";
            this.UpdateListPartNumberAction.TargetObjectType = typeof(CCRt.Module.BusinessObjects.SyCmdClass);
            this.UpdateListPartNumberAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.UpdateListPartNumberAction.ToolTip = null;
            this.UpdateListPartNumberAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.UpdateListPartNumberAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.UpdateListPartNumberAction_Execute);
            // 
            // UpdateListPartNumberViewControllercs
            // 
            this.Actions.Add(this.UpdateListPartNumberAction);
            this.ViewControlsCreated += new System.EventHandler(this.UpdateListPartNumberViewControllercs_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction UpdateListPartNumberAction;
    }
}
