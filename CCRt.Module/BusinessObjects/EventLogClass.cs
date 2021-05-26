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
    [Persistent("ProcessLog")]
    [ImageName("ChartHorizontalAxis_LogScale")]
    [NavigationItem("System")]
    [XafDisplayName("Log")]
    public class EventLogClass : SyCustomBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public EventLogClass(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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

        string comment;
        string userName;
        DateTime taskDate;
        string statusMessage;
        int status;
        string logEvent;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LogEvent
        {
            get => logEvent;
            set => SetPropertyValue(nameof(LogEvent), ref logEvent, value);
        }
        Guid pkId;
        [Key(true)]
        [Browsable(false)]
        public Guid PkId
        {
            get => pkId;
            set => SetPropertyValue(nameof(PkId), ref pkId, value);
        }
        public int Status
        {
            get => status;
            set => SetPropertyValue(nameof(Status), ref status, value);
        }


        [Size(SizeAttribute.Unlimited)]
        public string StatusMessage
        {
            get => statusMessage;
            set => SetPropertyValue(nameof(StatusMessage), ref statusMessage, value);
        }
        SyBatch fkBatch;
        [Browsable(false)]
        [Association(@"SyBatchReferencesLogMaster")]
        public SyBatch ProcessLog
        {
            get { return fkBatch; }
            set { SetPropertyValue<SyBatch>(nameof(ProcessLog), ref fkBatch, value); }
        }

        public DateTime TaskDate
        {
            get { return taskDate; }
            set
            {
                if (taskDate == value)
                    return;
                taskDate = value;
                OnChanged(nameof(TaskDate), this, this);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string UserName
        {
            get => userName;
            set => SetPropertyValue(nameof(UserName), ref userName, value);
        }

        
        [Size(SizeAttribute.Unlimited)]
        public string Comment
        {
            get => comment;
            set => SetPropertyValue(nameof(Comment), ref comment, value);
        }

    }
}