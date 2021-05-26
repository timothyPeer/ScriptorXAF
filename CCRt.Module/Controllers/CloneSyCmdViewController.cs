using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCRt.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.CloneObject;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace CCRt.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CloneSyCmdViewController : CloneObjectViewController
    {
        public CloneSyCmdViewController()
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

        private void CloneSyCmdViewController_Activated(object sender, EventArgs e)
        {
            CloneObjectViewController cloneObjectController =
           Frame.GetController<CloneObjectViewController>();
            if (cloneObjectController != null)
            {
                cloneObjectController.CloneObjectAction.Items.Clear();
                ChoiceActionItem myItem =
                    new ChoiceActionItem(View.ObjectTypeInfo.Name, View.ObjectTypeInfo.Type);

                cloneObjectController.CloneObjectAction.Items.Add(myItem);
            }
        }

        private void CloneSyCmdViewController_CustomGetCloneActionTargetTypes(object sender, CustomGetCloneActionTargetTypesEventArgs e)
        {
                e.Handled = true;
                e.TargetTypes.Clear();
                e.TargetTypes.Add(
                    Application.Model.BOModel[View.ObjectTypeInfo.Type.FullName],
                    View.ObjectTypeInfo.Type);
            }

        private void CloneSyCmdViewController_CustomCloneObject(object sender, CustomCloneObjectEventArgs e)
        {
            //SyCmdClass xxs = e.SourceObject as SyCmdClass;
            //IObjectSpace objectSpace = View.ObjectSpace;
            //SyCmdClass xxc = objectSpace.CreateObject<SyCmdClass>();
            //e.ClonedObject = xxc;
            //e.TargetObjectSpace = objectSpace;

            //Guid personParamValue = xxs.PkId;
            //CriteriaOperator personCriteria = CriteriaOperator.Parse("[PkId]=?", personParamValue);
            //SyCmdDetClass space = objectSpace.CreateObject<SyCmdDetClass>();
            //xxc.CmdName = string.Format("{0}_CLONED", xxs.CmdName);
            //xxc.CmdDescription = xxs.CmdDescription;
            //xxc.Sequence = xxs.SyCmdDetails.Count() + 1;
            //foreach (SyCmdDetClass clsX in ObjectSpace.GetObjects<SyCmdDetClass>(personCriteria))
            //{
            //    ((SyCmdClass)e.ClonedObject).SyCmdDetails.Add(e.TargetObjectSpace.GetObject(clsX));
            //}
            //objectSpace.CommitChanges();
        }

    }
}
