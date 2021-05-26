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
    [ImageName("Properties")]
    [NavigationItem("Lists")]
    [XafDisplayName("Properties")]
    [Persistent("CommandProperties")]
    public class CommandProperties : SyCustomBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public CommandProperties(Session session)
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

        string commandToken;
        bool useIterator;
        string commandName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CommandName
        {
            get => commandName;
            set => SetPropertyValue(nameof(CommandName), ref commandName, value);
        }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CommandToken
        {
            get => commandToken;
            set => SetPropertyValue(nameof(CommandToken), ref commandToken, value);
        }
        public bool UseIterator
        {
            get => useIterator;
            set => SetPropertyValue(nameof(UseIterator), ref useIterator, value);
        }
        Guid pkId;
        [Key(true)]
        [Browsable(false)]
        public Guid PkId
        {
            get => pkId;
            set => SetPropertyValue(nameof(PkId), ref pkId, value);
        }
        SyCmdClass fkSyCmdClass;
        [Browsable(false)]
        [Association(@"CommandPropertiesReferencesSyCmd")]
        public SyCmdClass Properties
        {
            get { return fkSyCmdClass; }
            set { SetPropertyValue<SyCmdClass>(nameof(Properties), ref fkSyCmdClass, value); }
        }



        BomLevel partlevel;

        [System.ComponentModel.DisplayName("LinkLevel")]
        public BomLevel Linklevel
        {
            get { return partlevel; }
            set
            {
                if (partlevel == value)
                    return;
                partlevel = value;
                OnChanged(nameof(Linklevel), this, this);
            }
        }
    }
}