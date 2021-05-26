using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCRt.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using nsoftware.IPWorks;

namespace CCRt.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ViewControllerSimpleButtonFtpWin : ViewController
    {
        SyBatch taskObj;
        string hostFileName;
        string batchName;
        object taskLock;
        string warehouse = string.Empty;
        string AssemblyParts = string.Empty;
        string ComponentParts = string.Empty;
        private string AllParts = string.Empty;
        string account_number = string.Empty;
        string plantcode = "STANDENS";
        StringBuilder sb = new StringBuilder();
        List<string> tArry = new List<string>();
        List<string> listTokenException = new List<string>();
        List<string> headerTokenException = new List<string>();
        private StringBuilder txtPITrail = new StringBuilder();

       // Rebex.Net.Ftp ftp_Rebex = new Rebex.Net.Ftp();

        public SyBatch TaskObj
        {
            get
            {
                lock (taskLock)
                {
                    return taskObj;
                }
            }
            set
            {
                lock (taskLock)
                {
                    taskObj = value;
                }
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FtpUserName { get; private set; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FtpPassword { get; private set; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FtpRemoteHost { get; private set; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LocalDirectory { get; private set; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RemoteDirectory { get; private set; }

        public ViewControllerSimpleButtonFtpWin()
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

        private string TrimAndValidate(string vString)
        {
            StringBuilder sb = new StringBuilder(vString);
            sb.Replace("\"", "");
            sb.Replace("\t", "");
            sb.TrimEnd();
            return sb.ToString().ToUpper();
        }
        void AddHeaderInfo()
        {


            tArry.Add("$! Created By Scriptor - A user controllable scripting tool. ");
            tArry.Add("$! Copyright 2021, Timothy Peer ");
            tArry.Add(string.Format("$! Date of Creation: {0} ", DateTime.Now.ToString()));
            tArry.Add("$! --------------------------------------------------- ");
            tArry.Add(string.Format("$! Batch Author: {0} ", SecuritySystem.CurrentUserName));
            tArry.Add(string.Format("$! Batch Date: {0} ", DateTime.Now.ToString()));
            tArry.Add("$! --------------------------------------------------- ");
            tArry.Add("$Set NOON ");
            tArry.Add("$Set OUTPUT=0:0:1 ");
            tArry.Add("$DEFINE DBM$BIND_BUFFERS 1024 ");
            tArry.Add("$! --------------------------------------------------- ");
        }

        private void ActionCommitFTP_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            //if (IsCurrentUserInRole('Admin'))
            System.Windows.Forms.Cursor cursor = Cursors.WaitCursor;
            {
                taskObj = (SyBatch) View.CurrentObject;

                if (taskObj != null)
                {
                    if (taskObj.FtpSources != null)
                    {

                        if (taskObj.FtpSources.Count == 0)
                        {
                            SyBatchWithOutFtpAssociationLink(
                                "FTP Source is not linked to the batch."); // null index exception. 
                            return;
                        }

                        if (taskObj.MgPartMasters.Count == 0)
                        {
                            SyBatchWithOutFtpAssociationLink(
                                "The batch is missing part numbers, link a Part Number's list to the batch."); // null index exception. 
                            return;
                        }

                        if (taskObj.MgAccountNumbers.Count == 0)
                        {
                            SyBatchWithOutFtpAssociationLink(
                                "The batch is missing the adjustment account, link an Account record to the batch."); // null index exception. 
                            return;
                        }

                        if (taskObj.MgWarehouseMasters.Count == 0)
                        {
                            SyBatchWithOutFtpAssociationLink(
                                "The batch is missing a warehouse association, link a Warehouse record to the batch."); // null index exception. 
                            return;
                        }

                        if (taskObj.FtpSources[0] != null)
                        {
                            RemoteDirectory = taskObj.FtpSources[0].RemoteDirectory;
                            LocalDirectory = taskObj.FtpSources[0].LocalDirectory;
                            FtpRemoteHost = taskObj.FtpSources[0].FtpIPAddress;
                            FtpPassword = taskObj.FtpSources[0].FtpPassword;
                            FtpUserName = taskObj.FtpSources[0].FtpUser;
                            hostFileName = $"{taskObj.BatchName}_{DateTime.Now.ToString("yyyMMdd")}";
                        }
                    }
                }
                if (GenFile())
                {
                    StringBuilder sb = new StringBuilder($"{LocalDirectory}\\{hostFileName}.ftp");
                    StringBuilder sbDcl = new StringBuilder($"{RemoteDirectory}{hostFileName}.dcl");
                    //FtpWithRebex(FtpRemoteHost, FtpUserName, FtpPassword, sb.ToString(), sbDcl.ToString());
                    FtpWithIpWorks(FtpRemoteHost, FtpUserName, FtpPassword, sb.ToString(), sbDcl.ToString());
                    CleanUp();
                }
            }
            Application.ShowViewStrategy.ShowMessage("Operation Completed!");
            cursor = Cursors.Default;

        }

        void FtpWithIpWorks(string hostName, string userName, string password, string localFile, string remotefile)
        {
            //var ftpIpworks = new FtpCom();
            Ftp ftpIpworks = new Ftp();


     
           ftpIpworks.OnStartTransfer += new Ftp.OnStartTransferHandler(ftp_onStartTransfer);
           ftpIpworks.OnError += new Ftp.OnErrorHandler(ftp_Error);
           ftpIpworks.OnPITrail += new Ftp.OnPITrailHandler(this.ftp_OnPITrail);
            string HostName = System.Net.Dns.GetHostName();
            StringBuilder sb = new StringBuilder($"{LocalDirectory}\\{hostFileName}.ftp");
            StringBuilder sbDcl = new StringBuilder($"{RemoteDirectory}{hostFileName}.dcl");
            //ftpalHost = HostName;
            ftpIpworks.RemoteHost = FtpRemoteHost;// = FtpRemoteHost;
            ftpIpworks.LocalHost = HostName;
       
            ftpIpworks.RemoteFile = sbDcl.ToString(); //  RAID:[USERS.COMMAND.Scripting]CostRollup_20210404.DCL;
            ftpIpworks.LocalFile = sb.ToString(); // string.Format("{0}\\{1}.ftp", LocalDirectory, hostFileName); // c:\envy\Scriptor\ScriptingCostRollup_20210404.ftp
            //ftpIpworks.c.Config("GuiAvailable = false");
            //   ftp.Config("UseBackgroundThread = True");
           // ftpIpworks.UserName = FtpUserName;//,FtpPassword);
            //ftpIpworks.loginpassPassword = FtpPassword;
            ftpIpworks.TransferMode = FtpTransferModes.tmASCII; //setTransferMode(TelEnvyXmlLib..tmASCII); //= nsoftware.IPWorks.FtpTransferModes.tmASCII;
            if (!ftpIpworks.Connected)
            {
                //try
                //{
                ftpIpworks.Logon();
                ftpIpworks.Upload();
                //}
                //catch (IPWorksException ex)
                //{
                //    SyBatchWithOutFtpAssociationLink(ex.Message);
                //    Console.WriteLine(ex.Message);
                //}
                //finally
                //{
                ftpIpworks.Logoff();

                //}
            }
           
         
            ftpIpworks = null;
        }

        private void ftp_OnPITrail(object sender, FtpPITrailEventArgs e)
        {
           
        }
        /*void FtpWithRebex(string hostName, string userName, string password, string localFile, string remotefile)
        {

            using (StreamWriter outputFile = new StreamWriter(localFile))
            {
                foreach (string txt in tArry)
                {
                    outputFile.WriteLine(txt);
                    sb.AppendLine(txt);
                }
            }

            try
            {
                ftp_Rebex.Connect(hostName);
                ftp_Rebex.Login(userName, password);
                ftp_Rebex.TransferType = FtpTransferType.Ascii;
                ftp_Rebex.PutFile(localFile, remotefile);
            }
            catch (Rebex.Net.FtpException ex)
            {
                Console.WriteLine($"{ex.Message}");

            ;
            }
            finally
            {
                ftp_Rebex.Disconnect();
            }
        }*/





  


        private void ftp_Error(object sender, FtpErrorEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(EventLogClass));
            EventLogClass newIssue = objectSpace.CreateObject<EventLogClass>();
            newIssue.Status = e.ErrorCode;
            newIssue.StatusMessage =
                $"Transfer Error :: lf [{string.Format("{0}{1}.ftp", LocalDirectory, hostFileName)}], rf[{$"{RemoteDirectory}{hostFileName}.DCL"}], hn[{FtpRemoteHost}],us[{FtpUserName}], er[{e.Description}]";
            newIssue.ProcessLog = taskObj;
            newIssue.UserName = SecuritySystem.CurrentUserName;
            newIssue.TaskDate = DateTime.Now;
            newIssue.StatusMessage = e.Description;
            if (objectSpace != null)
                objectSpace.CommitChanges();
        }

        private void ftp_onTransfer(object sender, FtpTransferEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(EventLogClass));
            EventLogClass newIssue = objectSpace.CreateObject<EventLogClass>();
            newIssue.Status = 0;
            newIssue.ProcessLog = taskObj;
            newIssue.StatusMessage =
                $"Transfer Underway : lf [{string.Format("{0}{1}.ftp", LocalDirectory, hostFileName)}], rf[{$"{RemoteDirectory}{hostFileName}.DCL"}], hn[{FtpRemoteHost}],us[{FtpUserName}]";
            newIssue.UserName = SecuritySystem.CurrentUserName;
            newIssue.TaskDate = DateTime.Now;
            if (objectSpace != null)
                objectSpace.CommitChanges();
        }

        private void ftp_onStartTransfer(object sender, FtpStartTransferEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(EventLogClass));
            EventLogClass newIssue = objectSpace.CreateObject<EventLogClass>();
            newIssue.Status = 0;
            newIssue.ProcessLog = taskObj;
            newIssue.StatusMessage =
                $"Transfer Started : lf [{$"{LocalDirectory}{hostFileName}.ftp"}], rf[{$"{RemoteDirectory}{hostFileName}.DCL"}], hn[{FtpRemoteHost}],us[{FtpUserName}]";
            newIssue.UserName = SecuritySystem.CurrentUserName;
            newIssue.TaskDate = DateTime.Now;
            if (objectSpace != null)
                objectSpace.CommitChanges();
        }

        private void ftp_onEndTransfer(object sender, FtpEndTransferEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(EventLogClass));
            EventLogClass newIssue = objectSpace.CreateObject<EventLogClass>();
            newIssue.Status = 0;
            newIssue.ProcessLog = taskObj;
            newIssue.StatusMessage =
                $"Transfer Ended  : lf [{$"{LocalDirectory}{hostFileName}.ftp"}], rf[{$"{RemoteDirectory}{hostFileName}.DCL"}], hn[{FtpRemoteHost}],us[{FtpUserName}]";
            newIssue.UserName = SecuritySystem.CurrentUserName;
            newIssue.TaskDate = DateTime.Now;
            objectSpace.CommitChanges();
        }
        bool GenFile()
        {
            listTokenException.Clear();
            headerTokenException.Clear();
            tArry.Clear();
            SyBatch task = (SyBatch)View.CurrentObject;
            task.LastTransferredBy = SecuritySystem.CurrentUserName;
            task.LastTransferredDate = DateTime.Now;
            SortingCollection sortCollection = new SortingCollection
            {
                new SortProperty("Sequence", DevExpress.Xpo.DB.SortingDirection.Ascending)
            };
            task.SyCmds.Sorting = sortCollection;
            bool bValid = true;
            // this can be avoided if the collection is changed to a o-o relationship between sybatch and the object. Using a collection
            // just for future collections - today which are not supported.  the reason, the schema changes requiring considerable 
            // modification and potential impact to the users.  I will attempt to limit the count to 1 using a view controller. TBD
            for (int m = 0; m < task.MgWarehouseMasters.Count();)
            {
                MgWarehouseMaster pObj = task.MgWarehouseMasters[m];
                warehouse = pObj.WarehouseCode;
                break;
            }
            // process assemblies and component lists
            for (int m = 0; m < task.MgPartMasters.Count();)
            {
                MgPartMaster pObj = task.MgPartMasters[m];

                AssemblyParts = pObj.Assemply_Parts;
                ComponentParts = pObj.Component_Parts;

                break;
            }
            for (int m = 0; m < task.MgAccountNumbers.Count();)
            {
                MgAccounts pObj = task.MgAccountNumbers[m];
                account_number = pObj.AccountNumber;
                break;
            }
            AddHeaderInfo();
            for (int i = 0; i < task.SyCmds.Count(); i++)
            {
                SyCmdClass cmd = task.SyCmds[i]; // start with the first command task

                addClassInfo(ref cmd, ref task);
                addFooter(ref cmd);
            }

            return bValid;
        }
        void addFooter(ref SyCmdClass cmd)
        {
            tArry.Add($"$! End of File {cmd.CmdName} - {cmd.CmdDescription}");
        }
        void addClassInfo(ref SyCmdClass cmdObj, ref SyBatch task, string partNumber = null)
        {

            ProcessLevelValidation ProcessMethodUsedByCommand = ProcessLevelValidation.none;
            bool bUseException = false;
            SyCmdClass cmd = cmdObj;
            cmd.SortDetails();

            if (cmdObj.CmdName.Contains("MAU520"))
            {
                Console.WriteLine("CM");
            }
            if (cmd.SyCmdProperties.Count == 1)
            {
                bUseException = true;
                SyCmdDetClass cmdD = cmd.SyCmdDetails[0];
            
                ProcessMethodUsedByCommand = cmd.ActionLevel;
                for (int i = 0; i < cmd.SyCmdDetails.Count(); i++)
                {
                    if (cmd.SyCmdDetails[i].CmdDetKVP.Contains("$"))
                    {
                        headerTokenException.Add(TrimAndValidate(cmd.SyCmdDetails[i].CmdDetKVP));
                    }
                    else
                    if (cmd.SyCmdDetails[i].CmdDetKVP.Contains(plantcode))  // add plantcode
                    {
                        headerTokenException.Add(TrimAndValidate(cmd.SyCmdDetails[i].CmdDetKVP));    // Add Plantcode
                                                                                                     //      headerTokenException.Add(cmd.SyCmdDetails[++i].CmdDetKVP); // add command and increment counter [i]
                    }
                    else
                    if (cmd.SyCmdDetails[i].CmdDetKVP.Contains("**PART_NUMBER_ITERATIVE**"))
                    {
                        break; // we have collected all the preceding prompts
                    }
                    else
                        listTokenException.Add(TrimAndValidate(cmd.SyCmdDetails[i].CmdDetKVP)); // add the prompt
                }
            }
            if (task.MgPartMasters.Count == 0) // throw a message
            {

            }
            MgPartMaster batch = task.MgPartMasters[0] as MgPartMaster;
            AssemblyParts = batch.Assemply_Parts;
            ComponentParts = batch.Component_Parts;
            cmd.SortDetails();
            tArry.Add(string.Format("$! Processing File {0} - {1}", cmd.CmdName, cmd.CmdDescription));
            string detLine = string.Empty;

            if (bUseException)
            {
                char[] cArry = string.Format("{0}", Environment.NewLine).ToCharArray();

                CommandProperties propty = cmd.SyCmdProperties[0];

                string[] tStringList = AssemblyParts.Split(cArry);
                if (!tStringList.Equals(Environment.NewLine))
                {

                    if (cmd.ActionLevel == ProcessLevelValidation.both_levels)
                    {
                        string joinX = tStringList.ToString();

                        string xListOfParts_String = string.Concat(
                            TrimAndValidate(ComponentParts),
                            "\r\n",
                            TrimAndValidate(AssemblyParts));
                        ProcessIterativeSection(ref cmd, cmd.Refactor("**PART_NUMBER_ITERATIVE**", "**PART_NUMBER_ITERATIVE**", xListOfParts_String));
                    }
                    else
                    {
                        ProcessIterativeSection(ref cmd, cmd.Refactor("**PART_NUMBER_ITERATIVE**", "**PART_NUMBER_ITERATIVE**", AssemblyParts));
                    }

                    tArry.Add(TrimAndValidate("E"));
                    tArry.Add(TrimAndValidate("E"));
                }
                else
                {
                    foreach (string tS in tStringList) // should contain at two elements a partnumber and a NewLine
                    {
                        tArry.Add(TrimAndValidate("tS"));
                    }

                }

            }
            else
            {
                Console.WriteLine(string.Format(" Before Loop - {0} ",cmd.CmdName));
                for (int j = 0; j < cmd.SyCmdDetails.Count; j++)
                {

                    SyCmdDetClass cmdD = cmd.SyCmdDetails[j];
                    string xPartString = TrimAndValidate(cmdD.CmdDetKVP);
                    //detLine = cmdD.CmdDetKVP;

                    if (xPartString.Contains("**ACCOUNT_NUMBER**"))
                    {
                        xPartString = cmd.Refactor(cmdD.CmdDetKVP, "**ACCOUNT_NUMBER**", account_number);
                        //  tArry.Add();
                    }
                    if (xPartString.Contains("**ACCOUNT_NUMBER_CR_TO_CONTINUE**"))
                    {
                        tArry.Add(TrimAndValidate(cmd.Refactor(xPartString, "**ACCOUNT_NUMBER_CR_TO_CONTINUE**", account_number)));
                        tArry.Add(TrimAndValidate(""));
                        detLine = Environment.NewLine; // we will check for this when we attempt the delimiter check
                    }

                    Console.WriteLine(cmd.CmdName);
                    if (cmd.ActionLevel == ProcessLevelValidation.both_levels)
                    {
                        xPartString = string.Concat(
                            TrimAndValidate(cmd.Refactor(xPartString, "**COMPONENT_PARTS**", ComponentParts)), 
                                TrimAndValidate(cmd.Refactor(xPartString, "**ASSEMBLY_PARTS**", AssemblyParts)));
                    }
                    else
                    {
                        if (cmd.CmdName.Contains("MGU157"))
                        {
                            Console.WriteLine("TEST");
                        }
                        if (xPartString.Contains("**COMPONENT_PARTS**"))
                        {
                            char[] cArry = string.Format("{0}", Environment.NewLine).ToCharArray();
                            xPartString =
                                TrimAndValidate(cmd.Refactor(xPartString, "**COMPONENT_PARTS**", ComponentParts));
                        }

                        if (xPartString.Contains("**ASSEMBLY_PARTS**"))
                        {
                            char[] cArry = string.Format("{0}", Environment.NewLine).ToCharArray();
                            xPartString =
                                TrimAndValidate(cmd.Refactor(xPartString, "**ASSEMBLY_PARTS**", AssemblyParts));
                        }
                    }

                    if (xPartString.Contains("**WAREHOUSE_CR_TO_CONTINUE**"))
                    {
                        tArry.Add(cmd.Refactor(xPartString, "**WAREHOUSE_CR_TO_CONTINUE**", warehouse));
                        tArry.Add("");
                        detLine = Environment.NewLine; // we will check for this when we attempt the delimiter check
                    }
                    if (xPartString.Contains("**WAREHOUSE**"))
                    {
                        //tArry.Add(TrimAndValidate(cmd.Refactor(cmdD.CmdDetKVP, "**WAREHOUSE**", warehouse)));
                        xPartString = TrimAndValidate(cmd.Refactor(xPartString, "*WAREHOUSE**", warehouse));

                    }
                    if (xPartString.Contains("**PLANTCODE**"))
                    {
                        xPartString = TrimAndValidate(cmd.Refactor(xPartString, "**PLANTCODE**", plantcode));
                    }
                    if (xPartString.Contains("**DATETIME**"))
                    {
                        xPartString = TrimAndValidate(cmd.Refactor(xPartString, "**DATETIME**",
                            DateTime.Now.ToString("yyyyMMdd")));
                        //tArry.Add(tempString);
                    }


                    if (!xPartString.Contains("**"))     // filter tokens
                    {
                        if ((xPartString.Contains("NewLine")) || (detLine.Contains("CR")))
                        {
                            xPartString = string.Format("{0}", Environment.NewLine);
                        }
                        tArry.Add(TrimAndValidate(xPartString));
                    }
                    else
                        tArry.Add(TrimAndValidate(xPartString));
                }


            }
        }

       
        void ProcessIterativeSection(ref SyCmdClass cmd, string listOfElementsDelimitedByEndOfLine)
        {
            int cntOfElement = listTokenException.Count();
            char[] cArry = string.Format("{0}", Environment.NewLine).ToCharArray();
            string[] tStringList = listOfElementsDelimitedByEndOfLine.Split(cArry);
            if (!listOfElementsDelimitedByEndOfLine.Equals(Environment.NewLine))
            {
                foreach (string tS in tStringList)
                {

                    if (!string.IsNullOrEmpty(tS))
                    {
                        bool bValid = true;
                        if (tS.Equals(Environment.NewLine))
                        {
                            bValid = false;
                        }
                        if (string.IsNullOrEmpty(tS))
                        {
                            bValid = false;
                        }
                        for (int i = 0; i < headerTokenException.Count; i++)
                        {
                            tArry.Add(headerTokenException[i]);
                        }
                        for (int i = 0; i < listTokenException.Count; i++)
                        {
                            tArry.Add(listTokenException[i]);
                        }

                        tArry.Add(TrimAndValidate(tS));
                        tArry.Add(TrimAndValidate("E")); // exit the command.
                                                         //foreach (string strElement in listTokenException)
                                                         //{
                                                         //    tArry.Add(strElement);
                                                         //}
                    }
                }
            }
            else
            {
                foreach (string tS in tStringList) // should be a two elements a partnumber and a NewLine
                {
                    tArry.Add(TrimAndValidate(tS));
                }

            }
        }
        void CleanUp()
        {

            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(EventLogClass));
            objectSpace?.CommitChanges();

            tArry.Clear();
        }

        void SyBatchWithOutFtpAssociationLink(string caption)
        {

            MessageOptions options = new MessageOptions
            {
                Duration = 10000,
                Type = InformationType.Error,
                Web = {Position = InformationPosition.Right},
                Win = {Caption = caption, Type = WinMessageType.Flyout},
                OkDelegate = () =>
                {
                    IObjectSpace os = Application.CreateObjectSpace(typeof(nonPersistentConfirmationFtpNoLink));
                    DetailView newTaskDetailView =
                        Application.CreateDetailView(os, os.CreateObject<nonPersistentConfirmationFtpNoLink>());
                    Application.ShowViewStrategy.ShowViewInPopupWindow(newTaskDetailView);
                }
            };
            // options.Message = string.Format("The selected batch is not LINKED to an FTP Source.");
            //  "FTP Source is not linked to the batch.";

        }

        private void ViewControllerSimpleButtonFtpWin_Activated(object sender, EventArgs e)
        {

        }

        private void ViewControllerSimpleButtonFtpWin_AfterConstruction(object sender, EventArgs e)
        {
            // ftp_Rebex.TransferProgressChanged += TransferProgressChanged;
            // ftp_Rebex.LogWriter = new FileLogWriter(@"c:\envy\scriptor\Session.log")
            // {
            //     Level = LogLevel.Info
            // };
        }

        private void ViewControllerSimpleButtonFtpWin_Deactivated(object sender, EventArgs e)
        {
            // ftp_Rebex.TransferProgressChanged -= TransferProgressChanged;
            // ftp_Rebex = null;
        }
    }
}
   /*

    Cases handled by this routine

    Name                        

    Component Only Parts           
    Assembly Only Parts
    Both Component and Assembly Parts
    Iterative sequenced Assembly Parts  <Consideration: component parts are not sequenced as iterative parts.>


    */