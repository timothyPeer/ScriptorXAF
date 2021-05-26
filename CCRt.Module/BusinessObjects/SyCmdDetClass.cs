using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DisplayNameAttribute = System.ComponentModel.DisplayNameAttribute;

namespace CCRt.Module.BusinessObjects
{
 
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SyCmdDetClass : SyCustomBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SyCmdDetClass(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            //   Console.WriteLine(CmdPk.PkId);
            if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
            {
                if (CmdPk != null && Sequence == 0)
                    Sequence = CmdPk.MaxChildRows;
            }


        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
            //{
            //    if (CmdPk != null && Sequence == 0)
            //        Sequence = CmdPk.MaxChildRows;
            //}
        }

        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}



        string cmdDetCVPDescription;
        string cmdDetKVP;
        int sequence;
        Guid pkId;
        [Key(true)]
        [Browsable(false)]
        [NonCloneableAttribute]
        public Guid PkId
        {
            get => pkId;
            set => SetPropertyValue(nameof(PkId), ref pkId, value);
        }
        [DisplayName("Sequence")]
        public int Sequence
        {
            get => sequence;
            set => SetPropertyValue(nameof(Sequence), ref sequence, value);
        }

        [Size(SizeAttribute.Unlimited)]
        [DisplayName("KeyValueProperty")]
        public string CmdDetKVP
        {
            get => cmdDetKVP;
            set => SetPropertyValue(nameof(CmdDetKVP), ref cmdDetKVP, value);
        }
        
        [Size(SizeAttribute.Unlimited)]
        [DisplayName("Description")]
        public string CmdDetCVPDescription
        {
            get => cmdDetCVPDescription;
            set => SetPropertyValue(nameof(CmdDetCVPDescription), ref cmdDetCVPDescription, value);
        }
        SyCmdClass fCmdPk;
        [Association(@"SyCmdDetailReferencesSyCmd")]
        public SyCmdClass CmdPk
        {
            get { return fCmdPk; }
            set {

                SetPropertyValue<SyCmdClass>(nameof(CmdPk), ref fCmdPk, value);
                if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                {
                    //if (CmdPk != null && Sequence == 0)
                    //    Sequence = CmdPk.MaxChildRows;
                    double maxSequence = 0;
                    if (CmdPk != null)
                    {
                        foreach (SyCmdDetClass product in CmdPk.SyCmdDetails)
                        {
                            if (product.Sequence > maxSequence)
                                maxSequence = product.Sequence;
                        }

                        Sequence = Convert.ToInt32(maxSequence) + 10;
                    }

                    Session.Save(this);
                }
               
            }
        }


        private BomLevel fCheckLevel;
        [System.ComponentModel.DisplayName("LinkLevel")]
        public BomLevel CheckLevel
        {
            get { return fCheckLevel; }
            set
            {
                if (fCheckLevel == value)
                    return;
                fCheckLevel = value;
                OnChanged(nameof(CheckLevel), this, this);
            }
        }
    }
}