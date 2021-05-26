namespace CCRt.Module.Win.Controllers
{
    partial class ViewControllerSimpleButtonFtpWin
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
            this.components = new System.ComponentModel.Container();
            this.ActionCommitFTP = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ActionCommitFTP
            // 
            this.ActionCommitFTP.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.ActionCommitFTP.Caption = "Action Commit FTP";
            this.ActionCommitFTP.Category = "Save";
            this.ActionCommitFTP.ConfirmationMessage = "Continue Processing";
            this.ActionCommitFTP.Id = "ActionCommitFTP";
            this.ActionCommitFTP.TargetObjectType = typeof(CCRt.Module.BusinessObjects.SyBatch);
            this.ActionCommitFTP.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.ActionCommitFTP.ToolTip = null;
            this.ActionCommitFTP.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ActionCommitFTP.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ActionCommitFTP_Execute);
            // 
            // ViewControllerSimpleButtonFtpWin
            // 
            this.Actions.Add(this.ActionCommitFTP);
            this.TargetObjectType = typeof(CCRt.Module.BusinessObjects.SyBatch);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AfterConstruction += new System.EventHandler(this.ViewControllerSimpleButtonFtpWin_AfterConstruction);
            this.Activated += new System.EventHandler(this.ViewControllerSimpleButtonFtpWin_Activated);
            this.Deactivated += new System.EventHandler(this.ViewControllerSimpleButtonFtpWin_Deactivated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction ActionCommitFTP;
    }
}
