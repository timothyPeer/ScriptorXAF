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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace CCRt.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("HorizontalLines")]
    [CreatableItem]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem("Batches")]
    [XafDisplayName("Batch")]
    public class SyBatch : SyCustomBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SyBatch(Session session)
            : base(session)
        {
        }

        protected override void OnSaving()
        {

            XPCollection<SyCmdClass> colDet = GetCollection<SyCmdClass>(nameof(SyCmds));
            base.OnSaving();
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            SortingCollection sortCollection = new SortingCollection
            {
                new SortProperty("Sequence", DevExpress.Xpo.DB.SortingDirection.Ascending)
            };

            SyCmds.Sorting = sortCollection;
            for (int i = 0; i < SyCmds.Count; i++)
            {
                Console.WriteLine(SyCmds[i].Sequence);
            }
        }

        [Key(true)]
        [Browsable(false)]
        public Guid PkId
        {
            get => pkId;
            set => SetPropertyValue(nameof(PkId), ref pkId, value);
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

        DateTime lastTransferredDate;
        string lastTransferredBy;
        object propertyName;
        int maxChildRows;

        Guid pkId;
        string batchName;

        [Appearance("Single", Visibility = ViewItemVisibility.Hide, Criteria = "MaxChildRows <> 0", Context = "DetailView")]
        public int MaxChildRows
        {
            get
            {
                XPCollection<SyCmdClass> colDet = GetCollection<SyCmdClass>(nameof(SyCmds));
                return colDet.Count;
            }

        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [DisplayName("BatchName")]
        public string BatchName
        {
            get => batchName;
            set => SetPropertyValue(nameof(BatchName), ref batchName, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [DisplayName("LastTransferredBy")]
        public string LastTransferredBy
        {
            get => lastTransferredBy;
            set => SetPropertyValue(nameof(LastTransferredBy), ref lastTransferredBy, value);
        }

        [DisplayName("LastTransferredBy")]
        public DateTime LastTransferredDate
        {
            get => lastTransferredDate;
            set => SetPropertyValue(nameof(LastTransferredDate), ref lastTransferredDate, value);
        }
        [Association(@"SyCmdReferencesSyBatch")]
        //int maxSequence = 0;
        //if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
        //{
        //    XPCollection<SyCmdClass> colDet = GetCollection<SyCmdClass>(nameof(SyCmds));
        //    foreach (SyCmdClass det in colDet)
        //    {
        //        maxSequence = det.Sequence + 10;
        //    }
        //    //colDet.CriteriaString = $"BatchId = '{PkId.ToString()}'";
        //    MaxChildRows = colDet.Count + 1;
        //    //colDet.CriteriaString = string.Empty;
        //    return colDet;
        //}
        //else
        public XPCollection<SyCmdClass> SyCmds => GetCollection<SyCmdClass>(nameof(SyCmds));

        [Association(@"FtpSourceClass")]
        public XPCollection<FtpSourceClass> FtpSources
        {
            get
            {
                //if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                //{
                //    XPCollection<FtpSourceClass> colDet = GetCollection<FtpSourceClass>(nameof(FtpSources));
                //    colDet.CriteriaString = string.Format("SyBatchId = '{0}'", pkId.ToString());
                //    MaxChildRows = colDet.Count + 1;
                //    colDet.CriteriaString = string.Empty;
                //    return colDet;
                //}
                //else
                    return GetCollection<FtpSourceClass>(nameof(FtpSources));
            }

        }
        [Association(@"SyBatchReferencesMgAccountNumbers")]
        public XPCollection<MgAccounts> MgAccountNumbers
        {
            get
            {

                if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                {
                    XPCollection<MgAccounts> colDet = GetCollection<MgAccounts>(nameof(MgAccountNumbers));
                    colDet.CriteriaString = string.Format("Oid = '{0}'", pkId.ToString());
                    colDet.CriteriaString = string.Empty;
                    return colDet;
                }
                else
                    return GetCollection<MgAccounts>(nameof(MgAccountNumbers));
            }
        }
        [Association(@"SyBatchReferencesMgPartMaster")]
        public XPCollection<MgPartMaster> MgPartMasters
        {

            get
            {
                if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                {
                    XPCollection<MgPartMaster> colDet = GetCollection<MgPartMaster>(nameof(MgPartMasters));
                    colDet.CriteriaString = string.Format("Oid = '{0}'", pkId.ToString());
                    colDet.CriteriaString = string.Empty;
                    return colDet;
                }
                else
                    return GetCollection<MgPartMaster>(nameof(MgPartMasters));

            }

        }


        [Association(@"SyBatchReferencesMgWarehouseMaster")]
        public XPCollection<MgWarehouseMaster> MgWarehouseMasters
        {

            get
            {
                if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                {
                    XPCollection<MgWarehouseMaster> colDet = GetCollection<MgWarehouseMaster>(nameof(MgWarehouseMasters));
                    colDet.CriteriaString = string.Format("Oid = '{0}'", pkId.ToString());
                    colDet.CriteriaString = string.Empty;
                    return colDet;
                }
                else
                    return GetCollection<MgWarehouseMaster>(nameof(MgWarehouseMasters));
            }

        }
        [Association(@"SyBatchReferencesLogMaster")]
        public XPCollection<EventLogClass> ProcessLogs
        {

            get
            {
                if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                {
                    XPCollection<EventLogClass> colDet = GetCollection<EventLogClass>(nameof(ProcessLogs));
                    colDet.CriteriaString = string.Format("Pkid = '{0}'", pkId.ToString());
                    colDet.CriteriaString = string.Empty;
                    return colDet;
                }
                else
                    return GetCollection<EventLogClass>(nameof(ProcessLogs));
            }

        }


    }
}