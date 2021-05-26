using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DisplayNameAttribute = System.ComponentModel.DisplayNameAttribute;
using System.Runtime.Remoting.Contexts;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace CCRt.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Task")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem("Batches")]
    [XafDisplayName("Task")]
    [Custom("IsClonable", "True")]
    public class SyCmdClass : SyCustomBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).

        ProcessLevelValidation fActionLevel;
        bool bInitialized = true;
        public SyCmdClass(Session session)
            : base(session)
        {
        }
        public void SortDetails()
        {
            SortingCollection sortCollection = new SortingCollection
            {
                new SortProperty("Sequence", DevExpress.Xpo.DB.SortingDirection.Ascending)
            };
            SyCmdDetails.Sorting = sortCollection;
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).

            SortDetails();
            ;
#if DEBUG
            for (var i = 0; i < SyCmdDetails.Count; i++)
            {
                Console.WriteLine(SyCmdDetails[i].Sequence);
            }
#endif
            isClone = false;

            if (string.IsNullOrEmpty(listOfPlantCode))
                listOfPlantCode = "STANDENS";

            if (username == null)
                bInitialized = false;


            fActionLevel = ProcessLevelValidation.none;
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



        public int Sequence
        {
            get => sequence;
            set => SetPropertyValue(nameof(Sequence), ref sequence, value);
        }
        DateTime updateDateTime;
        User updateUserName;
        User username;
        DateTime entryDateTime;
        string adjustmentAccountNumber;
        string listOfWarehouses;
        string listOfAccounts;
        string listOfPartNumbers;
        string listOfPlantCode;
        bool isClone;
        int sequence;
        int maxChildRows;
        //       SyBatch batchIPkd;
        string cmdDescription;
        string cmdName;
        Guid pkId;
        [Key(true)]
        [Browsable(false)]
        public Guid PkId
        {
            get => pkId;
            set => SetPropertyValue(nameof(PkId), ref pkId, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [DisplayName("Command")]
        public string CmdName
        {
            get => cmdName;
            set => SetPropertyValue(nameof(CmdName), ref cmdName, value);
        }

        [Appearance("Single", Visibility = ViewItemVisibility.Hide, Criteria = "MaxChildRows <> 0", Context = "DetailView")]
        public int MaxChildRows
        {
            get
            {
                XPCollection<SyCmdDetClass> colDet = GetCollection<SyCmdDetClass>(nameof(SyCmdDetails));
                return colDet.Count;
            }
        }
        [DisplayName("ActionLevel")]
        public ProcessLevelValidation ActionLevel
        {
            get => fActionLevel;
            set => SetPropertyValue(nameof(ActionLevel), ref fActionLevel, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [DisplayName("Description")]
        public string CmdDescription
        {
            get => cmdDescription;
            set => SetPropertyValue(nameof(CmdDescription), ref cmdDescription, value);
        }

        [NonCloneableAttribute]
        public bool AccountDescrip
        {
            get => isClone;
            set => SetPropertyValue(nameof(AccountDescrip), ref isClone, value);
        }
        //public SyBatch BatchIPkd
        //{
        //    get => batchIPkd;
        //    set => SetPropertyValue(nameof(BatchIPkd), ref batchIPkd, value);
        //}
        SyBatch fBatchId;
        [Association(@"SyCmdReferencesSyBatch")]
        public SyBatch BatchId
        {
            get { return fBatchId; }
            set { SetPropertyValue<SyBatch>(nameof(BatchId), ref fBatchId, value); }
        }
        [Association(@"SyCmdDetailReferencesSyCmd")]
        public XPCollection<SyCmdDetClass> SyCmdDetails
        {
            get
            {
                //int maxSequence = 0;
                //if (!Session.IsObjectsLoading && !Session.IsObjectsSaving)
                //{
                //    
                //    XPCollection<SyCmdDetClass> colDet = GetCollection<SyCmdDetClass>(nameof(SyCmdDetails));
                //    //colDet.CriteriaString = string.Format("CmdPk = '{0}'", PkId.ToString());

                //    foreach (SyCmdDetClass det in colDet)
                //    {
                //        maxSequence = det.Sequence+10;
                //    }
                //    //colDet.CriteriaString = string.Empty;
                //    return colDet;
                //}
                //else
                    return GetCollection<SyCmdDetClass>(nameof(SyCmdDetails));
            }
        }

        [Association(@"CommandPropertiesReferencesSyCmd")]
        public XPCollection<CommandProperties> SyCmdProperties
        {
            get
            {
                return GetCollection<CommandProperties>(nameof(SyCmdProperties));
            }
        }

        public DateTime EntryDateTime
        {
            get => entryDateTime;
        }


        public         User Username
        {
            get => username;
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public User UpdateUserName
        {
            get => updateUserName;
        }
        
        public DateTime UpdateDateTime
        {
            get => updateDateTime;
        }

        public string Refactor( string cmdDetKVP, string token, string replacedText)
        {
               StringBuilder sb = new StringBuilder(cmdDetKVP);
               sb.Replace(token, replacedText);
               return sb.ToString();
        }

        protected override void OnSaving()
        {
            
            if (!bInitialized)
            {
                username = SecuritySystem.CurrentUser as User;
                entryDateTime = DateTime.Now;
            }
            updateDateTime = DateTime.Now;
            updateUserName = SecuritySystem.CurrentUser as User;
            base.OnSaving();
        }
    }
}