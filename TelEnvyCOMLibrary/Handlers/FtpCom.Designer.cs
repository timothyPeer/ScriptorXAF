namespace TelEnvyCOMLibrary
{
    partial class FtpCom
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
            if (disposing)
            {
                
                if (components != null)
                {
                    components.Dispose();
                }
                if (_ConnectionStatusEventHandlers != null)
                {
                    _ConnectionStatusEventHandlers.Dispose();
                    _ConnectionStatusEventHandlers = null;
                }
                if (_EndTransferEventHandlers != null)
                {
                    _EndTransferEventHandlers.Dispose();
                    _EndTransferEventHandlers = null;
                }
                if (_DirListEventEventHandlers != null)
                {
                    _DirListEventEventHandlers.Dispose();
                    _DirListEventEventHandlers = null;
                }
                if (_FtpErrorEventEventHandlers != null)
                {
                    _FtpErrorEventEventHandlers.Dispose();
                    _FtpErrorEventEventHandlers = null;
                }
                if (_FtpPITrailEventHandlers != null)
                {
                    _FtpPITrailEventHandlers.Dispose();
                    _FtpPITrailEventHandlers = null;
                }
                if (_EFtpStartTransferEventHandlers != null)
                {
                    _EFtpStartTransferEventHandlers.Dispose();
                    _EFtpStartTransferEventHandlers = null;
                }
                if (_FtpTransferEventHandlers != null)
                {
                    _FtpTransferEventHandlers.Dispose();
                    _FtpTransferEventHandlers = null;
                }
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
            this.ftp1 = new nsoftware.IPWorks.Ftp(this.components);
            ftp1.RuntimeLicense = "31504E4241414E58524634574A31303835380000000000000000000000000000000000000000000041364738424650540000564E30473250594D574D344D0000";
            // 
            // ftp1
            // 
            this.ftp1.About = "IP*Works! 2016 [Build 6588]";
          


        }

        private nsoftware.IPWorks.Ftp ftp1;
        #endregion


    }
}
