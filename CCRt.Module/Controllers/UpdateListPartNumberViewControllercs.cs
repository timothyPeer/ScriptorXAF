﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCRt.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace CCRt.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class UpdateListPartNumberViewControllercs : ViewController
    {
        public UpdateListPartNumberViewControllercs()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void UpdateListPartNumberViewControllercs_ViewControlsCreated(object sender, EventArgs e)
        {

        }

        private void UpdateListPartNumberAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            MgPartMaster task = (MgPartMaster)View.CurrentObject;
            //foreach (MgPartMaster note in e.PopupWindowViewSelectedObjects)
            //{
            //    if (!string.IsNullOrEmpty(task.ListOfPartNumbers))
            //    {
            //        task.ListOfPartNumbers += Environment.NewLine;
            //    }
            //    task.ListOfPartNumbers += note.Text;
            //}
            //if (((DetailView)View).ViewEditMode == ViewEditMode.View)
            //{
            //    View.ObjectSpace.CommitChanges();
            //}
        }
    }
}
