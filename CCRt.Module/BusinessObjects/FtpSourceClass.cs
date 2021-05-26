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

namespace CCRt.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem("Action")]
    [XafDisplayName("Server Info")]
    public class FtpSourceClass : SyCustomBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public FtpSourceClass(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            remoteDirectory = "nv$disk:[envy.AutoScripts]";
            localDirectory = @"c:\envy\AutoScripts\";
     
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void DoEndEditAction()
        {
            base.DoEndEditAction();
        }

        protected override void OnSaved()
        {
            base.OnSaved();

            System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(this.localDirectory);
            if (!dInfo.Exists)
                dInfo.Create();
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


        string localDirectory;
        string remoteDirectory;
        SyBatch syBatchId;
        string ftpPassword;
        string ftpUser;
        string ftpIPAddress;

        //   MaskedTextBox.Mask = ###.###.###.###
        //MaskedTextBox.ValidatingType = typeof(System.Net.IPAddress);
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]

        public string FtpIPAddress
        {
            get => ftpIPAddress;
            set => SetPropertyValue(nameof(FtpIPAddress), ref ftpIPAddress, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FtpUser
        {
            get => ftpUser;
            set => SetPropertyValue(nameof(FtpUser), ref ftpUser, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [PasswordPropertyText(true)]
        public string FtpPassword
        {
            get => ftpPassword;
            set => SetPropertyValue(nameof(FtpPassword), ref ftpPassword, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RemoteDirectory
        {
            get => remoteDirectory;
            set => SetPropertyValue(nameof(RemoteDirectory), ref remoteDirectory, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LocalDirectory
        {
            get => localDirectory;
            set => SetPropertyValue(nameof(LocalDirectory), ref localDirectory, value);
        }
        Guid pkId;
        [Key(true)]
        [Browsable(false)]
        public Guid PkId
        {
            get => pkId;
            set => SetPropertyValue(nameof(PkId), ref pkId, value);
        }
        [Association("FtpSourceClass")]
           
        public SyBatch SyBatchId
        {
            get => syBatchId;
            set => SetPropertyValue(nameof(SyBatchId), ref syBatchId, value);
        }
    }
}