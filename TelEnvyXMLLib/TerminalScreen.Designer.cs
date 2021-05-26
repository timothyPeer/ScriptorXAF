namespace TelEnvyXmlLib
{
    partial class TerminalScreen
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
            this.ipport1 = new nsoftware.IPWorks.Ipport(this.components);
            this.telnet1 = new nsoftware.IPWorks.Telnet(this.components);
            // 
            // ipport1
            // 
            this.ipport1.About = "IP*Works! 2016 [Build 6588]";
            this.ipport1.OnConnected += new nsoftware.IPWorks.Ipport.OnConnectedHandler(this.ipport1_OnConnected);
            this.ipport1.OnConnectionStatus += new nsoftware.IPWorks.Ipport.OnConnectionStatusHandler(this.ipport1_OnConnectionStatus);
            this.ipport1.OnDataIn += new nsoftware.IPWorks.Ipport.OnDataInHandler(this.ipport1_OnDataIn);
            this.ipport1.OnDisconnected += new nsoftware.IPWorks.Ipport.OnDisconnectedHandler(this.ipport1_OnDisconnected);
            this.ipport1.OnError += new nsoftware.IPWorks.Ipport.OnErrorHandler(this.ipport1_OnError);
            this.ipport1.OnReadyToSend += new nsoftware.IPWorks.Ipport.OnReadyToSendHandler(this.ipport1_OnReadyToSend);
            // 
            // telnet1
            // 
            this.telnet1.About = "IP*Works! 2016 [Build 6588]";
            this.telnet1.OnCommand += new nsoftware.IPWorks.Telnet.OnCommandHandler(this.telnet1_OnCommand);
            this.telnet1.OnConnected += new nsoftware.IPWorks.Telnet.OnConnectedHandler(this.telnet1_OnConnected);
            this.telnet1.OnConnectionStatus += new nsoftware.IPWorks.Telnet.OnConnectionStatusHandler(this.telnet1_OnConnectionStatus);
            this.telnet1.OnDataIn += new nsoftware.IPWorks.Telnet.OnDataInHandler(this.telnet1_OnDataIn);
            this.telnet1.OnDisconnected += new nsoftware.IPWorks.Telnet.OnDisconnectedHandler(this.telnet1_OnDisconnected);
            this.telnet1.OnDo += new nsoftware.IPWorks.Telnet.OnDoHandler(this.telnet1_OnDo);
            this.telnet1.OnDont += new nsoftware.IPWorks.Telnet.OnDontHandler(this.telnet1_OnDont);
            this.telnet1.OnError += new nsoftware.IPWorks.Telnet.OnErrorHandler(this.telnet1_OnError);
            this.telnet1.OnReadyToSend += new nsoftware.IPWorks.Telnet.OnReadyToSendHandler(this.telnet1_OnReadyToSend);
            this.telnet1.OnSubOption += new nsoftware.IPWorks.Telnet.OnSubOptionHandler(this.telnet1_OnSubOption);
            this.telnet1.OnWill += new nsoftware.IPWorks.Telnet.OnWillHandler(this.telnet1_OnWill);
            this.telnet1.OnWont += new nsoftware.IPWorks.Telnet.OnWontHandler(this.telnet1_OnWont);

        }

        #endregion
        private nsoftware.IPWorks.Ipport ipport1;
        private nsoftware.IPWorks.Telnet telnet1;
    }
}
