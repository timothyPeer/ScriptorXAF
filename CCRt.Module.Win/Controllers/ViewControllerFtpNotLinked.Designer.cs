namespace CCRt.Win
{
    partial class ViewControllerFtpNotLinked
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
            this.viewControllerFtpNotLinkedAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // viewControllerFtpNotLinkedAction
            // 
            this.viewControllerFtpNotLinkedAction.AcceptButtonCaption = "Ok";
            this.viewControllerFtpNotLinkedAction.CancelButtonCaption = null;
            this.viewControllerFtpNotLinkedAction.Caption = "Messages";
            this.viewControllerFtpNotLinkedAction.ConfirmationMessage = null;
            this.viewControllerFtpNotLinkedAction.Id = "viewControllerFtpNotLinkedAction";
            this.viewControllerFtpNotLinkedAction.TargetObjectType = typeof(CCRt.Module.BusinessObjects.SyBatch);
            this.viewControllerFtpNotLinkedAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.viewControllerFtpNotLinkedAction.ToolTip = null;
            this.viewControllerFtpNotLinkedAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // ViewControllerFtpNotLinked
            // 
            this.Actions.Add(this.viewControllerFtpNotLinkedAction);
            this.TargetObjectType = typeof(CCRt.Module.BusinessObjects.SyCmdClass);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction viewControllerFtpNotLinkedAction;
    }
}
