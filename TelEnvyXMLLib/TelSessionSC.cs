
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using TelEnvyXmlLib.Abstract;
using TelEnvyXmlLib.Directives;
using TelEnvyXmlLib.Enums;
using TelEnvyXmlLib.EventArgs;
using TelEnvyXmlLib.Exceptions;
using TelEnvyXmlLib.Helper;

namespace TelEnvyXmlLib
{
    /* 
     * Log output should be directed from the NLog.Conf file. 
     * 
     *
     */


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A tel session screen. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.TerminalScreenSC"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class TelSessionSC : TerminalScreenSC
    {
        /// <summary>   The file to process. </summary>
        private string _fileToProcess;  /* The file to process */
        /// <summary>   The synchronise root. </summary>
        private readonly object _syncRoot = new object();   /* The synchronise root */
        /// <summary>   True to released for processing. </summary>
        private bool ReleasedForProcessing = true;  /* True to released for processing */


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Finalizes an instance of the TelEnvyXmlLib.TelSessionSC class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        ~TelSessionSC()
        {
            this.Disconnect();
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TelSessionSC class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TelSessionSC()
        {
            InitializeComponent();
            TransactionTimestamp = DateTime.Now;
            //Logger.Initialize(@"c:\envy\test.txt");
            SocketDisconnected += TelSessionSC_SocketDisconnected;
            this.ReleaseForProcessing += TelSessionSC_ReleaseForProcessing;
            SocketConnected += TelSessionSC_SocketConnected;
         
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by TelSessionSC for socket connected events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Socket event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void TelSessionSC_SocketConnected(object sender, SocketEventArgs e)
        {
            StaticLogger.LogInfo(ModuleLogName, string.Format("Socket Connected to {0}:{1}.",getSocketClient().hostName,getSocketClient().port));
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by TelSessionSC for release for processing events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Release for processing event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void TelSessionSC_ReleaseForProcessing(object sender, ReleaseForProcessingEventArgs e)
        {
            ReleasedForProcessing = e.ReadyToSend;
        }


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TelSessionSC class. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <param name="container">    The container.</param>
    ///-------------------------------------------------------------------------------------------------

    public TelSessionSC(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            TransactionTimestamp = DateTime.Now;
            SocketDisconnected += TelSessionSC_SocketDisconnected;
            this.ReleaseForProcessing += TelSessionSC_ReleaseForProcessing;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by TelSessionSC for socket disconnected events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Socket event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void TelSessionSC_SocketDisconnected(object sender, SocketEventArgs e)
        {
            StaticLogger.LogTrace(ModuleLogName, "Socket Disconnected.");
        }

        #region Events

        /// <summary>   Occurs when Change Log File Name. </summary>
        public event EventHandler<TelLogWriterEventArg> ChangeLogFileName;  /* Occurs when Change Log File Name. */

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the change log file name action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        A TelLogWriterEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnChangeLogFileName(object sender, TelLogWriterEventArg e)
        {
            EventHandler<TelLogWriterEventArg> handler = ChangeLogFileName;
            if (handler != null)
                handler(sender, e);
        }
        /// <summary>   Occurs when [log written]. </summary>
        public event EventHandler<TelLogWriterEventArg> LogWriting; /* Occurs when Log Writing. */
        /// <summary>   Occurs before TelEnvyXmlLib XML node is processed by the TexSession. </summary>
        public event EventHandler<TeLSessionNodeProcessEventArg> NodeProcessing;    /* Occurs when Node Processing. */
        /// <summary>   Occurs after a TelEnvyXmlLib XML node is processed by the TexSession. </summary>
        public event EventHandler<TeLSessionNodeProcessEventArg> NodeProcessed; /* Occurs when Node Processed. */

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The Event-handler passes captured data from the TelEnvyXmlLib session to the
        ///             application.
        ///             </summary>
        ///-------------------------------------------------------------------------------------------------

        public event EventHandler<GrabChangedEventArgs> GrabChanged;    /* Occurs when Grab Changed. */

        /// <summary>   Occurs when the node canceled. </summary>
        public event EventHandler<TeLSessionNodeCancelEventArg> NodeCanceled;   /* Occurs when Node Canceled. */

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the log written action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        A TelLogWriterEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnLogWritten(object sender, TelLogWriterEventArg e)
        {
            EventHandler<TelLogWriterEventArg> handler = LogWriting;
            if (handler != null)
                handler(sender, e);
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the node canceled action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        A TeLSessionNodeCancelEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnNodeCanceled(object sender, TeLSessionNodeCancelEventArg e)
        {
            EventHandler<TeLSessionNodeCancelEventArg> handler = NodeCanceled;
            if (handler != null)
                handler(sender, e);
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the node processing action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="e">    A TeLSessionNodeProcessEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

		protected void OnNodeProcessing(TeLSessionNodeProcessEventArg e)
        {
            if (NodeProcessing != null)
            {
                NodeProcessing(this, e);
            }
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the node processed action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="e">    A TeLSessionNodeProcessEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

		protected void OnNodeProcessed(TeLSessionNodeProcessEventArg e)
        {
            if (NodeProcessed != null)
            {
                NodeProcessed(this, e);
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the grab changed event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="e">    Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

		protected void OnGrabChanged(GrabChangedEventArgs e)
        {
            if (GrabChanged != null)
            {
                GrabChanged(this, e);
            }
        }



        #endregion

        #region Public methods

        /// <summary>   The timer lock. </summary>
        object timerLock = new object();    /* The timer lock */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging trace. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

		public void SetLoggingTrace()
        {
            //TelEnvyLogLevel = NLog.LogLevel.Trace;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging debug. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

		public void SetLoggingDebug()
        {
            //TelEnvyLogLevel = NLog.LogLevel.Debug;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging information. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void SetLoggingInfo()
        {//
         //   TelEnvyLogLevel = NLog.LogLevel.Info;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging error. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void SetLoggingError()
        {
            //TelEnvyLogLevel = NLog.LogLevel.Error;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging off. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void SetLoggingOff()
        {
           // TelEnvyLogLevel = NLog.LogLevel.Off;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging suffix unique identifier. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void SetLoggingSuffixGUID()
        {
            LogOpt = EnumLoggingOptions.GUID;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Configure settings. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="_settings">    Options for controlling the operation.</param>
        ///-------------------------------------------------------------------------------------------------

        public void ConfigureSettings(LoginInfo _settings)
        {
            settings = _settings;

            int screenRows = _settings.TeLScreenLength > 0 ? _settings.TeLScreenLength : 24;
            int screenColumns = _settings.TeLScreenWidth > 0 ? _settings.TeLScreenWidth : 132;
            //int timeOut = _settings.TeLServerTimeout > 0 ? _settings.TeLServerTimeout : DefaultServerTimeout;
            //if (_helper != null)
            //{
            //    this._helper.Terminal.SetScreenSize(screenColumns, screenRows);
            //    this._helper.sendDataWaitForDataTimeout = _settings.TeLServerTimeout;
            //    this._helper.receiveDataWaitForDataTimeout = _settings.TeLServerTimeout;
            //}
            settings.TeLHostPort = _settings.TeLHostPort;
            settings.TeLHostname = _settings.TeLHostname;
            setScreenGeometry(screenRows, screenColumns);


        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the file described by fileToProcess. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="fileToProcess">    The file to process.</param>
        ///-------------------------------------------------------------------------------------------------

        public void ProcessFile(string fileToProcess)
        {
            logIncrementing = 0;
            recordIncrementing = 0;
            debugIncrementing = 0;
           
            TransactionTimestamp = DateTime.Now;

            if (string.IsNullOrEmpty(fileToProcess))
            {
               // logger.Error(ex, "FileToProcess-File cannot be empty."); // render the exception with ${exception}
               // throw ;
            }

            lock (_syncRoot)
            {
                _fileToProcess = fileToProcess;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileToProcess);
                Process(xmlDoc);
                //string txt = GetXMLProcessedPath();

            }
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the XML. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="ArgumentException"> Thrown when one or more arguments have unsupported or
        ///                                      illegal values.</exception>
        ///
        /// <param name="xmlData">  Information describing the XML.</param>
        /// <param name="fileName"> Filename of the file.</param>
        ///-------------------------------------------------------------------------------------------------

		public void ProcessXml(string xmlData, string fileName)
        {

            if (string.IsNullOrEmpty(xmlData))
            {
                //logger.Error(new ArgumentException("xmlData-XmlData cannot be empty.")); // render the exception with ${exception}
                //throw;
            }
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName cannot be empty.", "fileName");

            lock (_syncRoot)
            {
                _fileToProcess = fileName;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlData);
                Process(xmlDoc);
            }
        }

        #endregion
        #region fields
        /// <summary>   Options for controlling the operation. </summary>
        public LoginInfo settings;  /* Options for controlling the operation */
        /// <summary>   True to canceling. </summary>
        private bool _canceling;    /* True to canceling */
        /// <summary>   The session. </summary>
        Session _session;   /* The session */
        /// <summary>   The log enabled. </summary>
        bool _logEnabled;   /* True to enable, false to disable the log */
        //private readonly bool LoggingEnabled;
        //private readonly bool DebuggingEnabled;
        //    private readonly int? ServerPort = 23;
        //     private StringBuilder _receivedData = null;
        /*     private int countOfSocketBytes=0;*/
        //     private string _matchedData;
        //   private string _moduleName;



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the filename of the log file. </summary>
        ///
        /// <value> The filename of the log file. </value>
        ///-------------------------------------------------------------------------------------------------

        public string LogFileName { get; private set; }


        #endregion
        #region Main methods


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Cancels this TelSessionSC. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Cancel()
        {
            _canceling = true;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process this TelSessionSC. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLXMLInvalidTagException"> Thrown when a Te LXML Invalid Tag error condition
        ///                                              occurs.</exception>
        ///
        /// <param name="xmlDoc">   The XML document.</param>
        ///-------------------------------------------------------------------------------------------------

        public void Process(XmlDocument xmlDoc)
        {
            // Initialize counters;
            TransactionTimestamp = DateTime.Now;
            logIncrementing = 0;
            recordIncrementing = 0;
            debugIncrementing = 0;
           

            try
            {
                // parse the whole XML first
                _session = TeLSessionXmlParser.Parse(xmlDoc.DocumentElement) as Session;
              // TelEnvyXmlLib.Logging.Log.instance.Trace("Process.XmlDocument-{0}\r", xmlDoc.ToString());
            }
            catch (Exception ex)
            {
              // TelEnvyXmlLib.StaticLogger.LogError(this.GetType(),"Error-Process.XmlDocument-{0}\r{1}", xmlDoc.ToString(),ex.Message);
                throw new TeLXMLInvalidTagException(xmlDoc.ToString(), ex);
            }
            finally
            {

            }

            if (_session == null)
            {
                  throw new TeLXMLInvalidTagException(string.Format("TeLXMLInvalidTag:\n An invalid tag found.\nExpected <Session> as a document root node and found {0}", xmlDoc.DocumentType.Name));
            }

            try
            {
                // process parsed nodes
                OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true));
                Process(_session, 0);
                IsFaulted = false;
            }
            catch (Exception ex)
            {
                IsFaulted = true;
                StaticLogger.LogError(ModuleLogName, ex, string.Format("Process: XMLDocument\n"));

                // Exception = ex;
            }
            finally
            {
                base.Disconnect();
                logIncrementing++;
                debugIncrementing++;
            }
        }



        //private void Process(XmlDocument xmlDoc)
        //{
        //	// parse the whole XML first
        //	_session = ComSessionXmlParser.Parse(xmlDoc.DocumentElement) as ComSessionSessionNode;
        //	if (_session == null)
        //              throw new TeLXMLInvalidTag(string.Format("TeLXMLInvalidTag:\n An invalid tag found.\nExpected <Session> as a document root node and found {0}",xmlDoc.DocumentType.Name));

        //	try
        //	{
        //		// process parsed nodes
        //		Process(_session, 0);				
        //	}
        //	finally
        //	{
        //		if (_logger != null)
        //			_logger.Close();

        //		if (_helper != null)
        //		{
        //			if (_helper.Terminal.Recorder != null)
        //			{
        //				_helper.Terminal.Recorder.Close();
        //				_helper.Terminal.Recorder = null;
        //			}

        //			_helper.Terminal.Dispose();
        //		}
        //	}
        //}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process this TelSessionSC. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLXMLInvalidTagException"> Thrown when a Te LXML Invalid Tag error condition
        ///                                              occurs.</exception>
        /// <exception cref="TeLInvalidCastException">   Thrown when a Te L Invalid Cast error condition
        ///                                              occurs.</exception>
        ///
        /// <param name="cNode">    The node.</param>
        /// <param name="level">    The level.</param>
        ///-------------------------------------------------------------------------------------------------

        private void Process(TeLSessionNode cNode, int level)
        {
            if (cNode.Data == null)
            {
                StaticLogger.LogTrace(ModuleLogName,  string.Format("Process {0}{1}", "".PadLeft(level), cNode.TeLTag));
            }
            else
            {
                StaticLogger.LogTrace(ModuleLogName, string.Format("Process {0}{1} [{2}]", "".PadLeft(level), cNode.TeLTag, cNode.Data));
            }
            string startTime = DateTime.Now.ToString();
         
            //int doEventCnt = 0;
             
            //do
            //{
            //    string endTime = DateTime.Now.ToString();
            //    if ((doEventCnt % 20) == 0)
            //    {
            //        if ((!getSocketClient().Connected) && (!ReleasedForProcessing))
            //        {
            //            StaticLogger.LogError(ModuleLogName,new TeLMessageException("Process:DoEventCnt"), "Process:cNode:Level socket is disconnect, and not released for processing.");
            //            throw new TeLMessageException("The socket to the host was closed during a critical operation.");
            //        }
            //        try
            //        {
            //            if (getSocketClient().Connected == true)
            //            {
            //                
            //                StaticLogger.LogTrace(ModuleLogName, "DoEvents Called");
            //            }
            //        }
            //        catch(Exception e)
            //        {
            //            StaticLogger.LogError(ModuleLogName, e, "Process:cNode:Level");
            //           // Console.WriteLine("test");
            //        }
            //    }
            //    duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            //   
            //      //  if (duration.Seconds >= 6) throw new TeLMessageException(string.Format("Exceeded Reasonable Timeout 6 secs to wait for a response from the socket. Socket Connected: {0} - SocketHost-Port: {1}-{2}.", getIPInstance().Connected.ToString(), getIPInstance().RemoteHost, getIPInstance().RemotePort));

            //    doEventCnt++;
            //} while (!ReleasedForProcessing );
            ReleasedForProcessing = false;      // wait for processing any data from the next command sequence
            TimeSpan span = DateTime.Parse(DateTime.Now.ToString()).Subtract(DateTime.Parse(startTime));
           

            if (_canceling)
                return;

            //string pad = new string(' ', level * 2);
            //Console.WriteLine(pad + cNode.Tag);
            OnNodeProcessing(new TeLSessionNodeProcessEventArg(cNode, level, this.ModuleLogName) );
            switch (cNode.TeLTag)
            {
                
                case XmlTag.Session:
                    {
                        Session ssNodeS = cNode as Session;
                        if (ssNodeS != null)
                        {
                            if (ssNodeS.SocketType.ToUpper() == "STREAM")
                            {
                           //     OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true));
                                //                                 nsoftware.IPWorks.Ipport sPort = getIPInstance();
                                //                                 if (!string.IsNullOrEmpty(ssNodeS.EOL))
                                //                                     sPort.EOL = ssNodeS.EOL;
                                //                               //  sPort.Config(string.Format("UseBackGroundThread={0}", ssNodeS.UseBackgroundThread));
                                //                                 sPort.Config(string.Format("ConnectionTimeOut={0}", ssNodeS.ConnectionTimeout));
                                //                                 sPort.Config(string.Format("Linger={0}", ssNodeS.SocketLinger));
                                //                                 sPort.Config(string.Format("TCPKeepAlive={0}", ssNodeS.TCPKeepAlive));
                                /*
                                 * Rule: MaxLineLength
                                 * If an EOL string is found in the input stream before MaxLineLength bytes are received, the DataIn event is fired with the EOL parameter set to True, and the buffer is reset.
                                 * If no EOL is found, and MaxLineLength bytes are accumulated in the buffer, the DataIn event is fired with the EOL parameter set to False, and the buffer is reset.
                                 * The minimum value for MaxLineLength is 256 bytes. The default value is 2048 bytes. The maximum value is 65536 bytes.
                                 */
                                //                                 if ((ssNodeS.MaxLineLength == -1))
                                //                                     sPort.Config(string.Format("MaxLineLength={0}", 65536)); 
                                //                                 else
                                //                                     sPort.Config(string.Format("MaxLineLength={0}", ssNodeS.MaxLineLength));
                                getSocketClient().SetSocketSendBuffers( ssNodeS.OutBufferSize);
                                getSocketClient().SetSocketReceiveBuffers( ssNodeS.InBufferSize);
                                /*
                                 * Rule: TcpNoDelay
                                 *When true, the socket will send all data that is ready to send at once. When false, the socket will send smaller buffered packets of data at small intervals. This is known as the Nagle algorithm.
                                 * By default, this config is set to false. 
                                 */
                                /* sPort.Config(string.Format("TcpNoDelay={0}", ssNodeS.TcpNoDelay));*/
                                if (!string.IsNullOrEmpty(ssNodeS.EOL))
                                {
                                    getSocketClient().EOL = ssNodeS.EOL;
                                }
                                StaticLogger.LogTrace(ModuleLogName, string.Format("EOL\t\t:{0}\nConnectionTimeOut: {1}\nSocketLinger: {2}\nTCPKeepAlive: {3}\nMaxLineLength: {4}\nOutBufferSize:{5}\nInBufferSize:{6}\nTcpNoDelay:{7}",
                                    ssNodeS.EOL, ssNodeS.ConnectionTimeout, ssNodeS.SocketLinger, ssNodeS.TCPKeepAlive, ssNodeS.MaxLineLength, ssNodeS.OutBufferSize, ssNodeS.InBufferSize,ssNodeS.TcpNoDelay));
                            }
                            if (ssNodeS.SocketType.ToUpper() == "FORM")
                            {
                            //    nsoftware.IPWorks.Telnet sPort = getTNInstance();
                            //    //if (!string.IsNullOrEmpty(ssNodeS.EOL))
                            //    //    sPort.EOL = ssNodeS.EOL;
                            //    ////  sPort.Config(string.Format("UseBackGroundThread={0}", ssNodeS.UseBackgroundThread));
                            //    sPort.Config(string.Format("ConnectionTimeOut={0}", ssNodeS.ConnectionTimeout));
                            //    sPort.Config(string.Format("Linger={0}", ssNodeS.SocketLinger));
                            //    sPort.Config(string.Format("TCPKeepAlive={0}", ssNodeS.TCPKeepAlive));
                            //    /*
                            //     * Rule: MaxLineLength
                            //     * If an EOL string is found in the input stream before MaxLineLength bytes are received, the DataIn event is fired with the EOL parameter set to True, and the buffer is reset.
                            //     * If no EOL is found, and MaxLineLength bytes are accumulated in the buffer, the DataIn event is fired with the EOL parameter set to False, and the buffer is reset.
                            //     * The minimum value for MaxLineLength is 256 bytes. The default value is 2048 bytes. The maximum value is 65536 bytes.
                            //     */
                            //    if (!(ssNodeS.MaxLineLength == -1))
                            //        sPort.Config(string.Format("MaxLineLength={0}", ssNodeS.MaxLineLength));

                            //    sPort.Config(string.Format("OutBufferSize={0}", ssNodeS.OutBufferSize));
                            //    sPort.Config(string.Format("InBufferSize={0}", ssNodeS.InBufferSize));
                            //    /*
                            //     * Rule: TcpNoDelay
                            //     *When true, the socket will send all data that is ready to send at once. When false, the socket will send smaller buffered packets of data at small intervals. This is known as the Nagle algorithm.
                            //     * By default, this config is set to false. 
                            //     */

                                StaticLogger.LogTrace(ModuleLogName, string.Format("EOL\t\t:{0}\nConnectionTimeOut: {1}\nSocketLinger: {2}\nTCPKeepAlive: {3}\nMaxLineLength: {4}\nOutBufferSize:{5}\nInBufferSize:{6}\nTcpNoDelay:{7}",
                                    ssNodeS.EOL, ssNodeS.ConnectionTimeout, ssNodeS.SocketLinger, ssNodeS.TCPKeepAlive, ssNodeS.MaxLineLength, ssNodeS.OutBufferSize, ssNodeS.InBufferSize, ssNodeS.TcpNoDelay));
                            }
                        }
                        OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true));
                        TeLSessionGroupNode lNode = cNode as TeLSessionGroupNode;
                        foreach (TeLSessionNode node in lNode.Nodes)
                        {
                            Process(node, level + 1);
                        }
                    }
                    break;
                case XmlTag.SessSeq:
                case XmlTag.GroupCollection:
                    SessSeq ssNode = cNode as SessSeq;
                    if (ssNode != null)
                    {
                        
                        getSocketClient().setHostName(ssNode.ServerName);
                        getSocketClient().setHostPort(ssNode.ServerPort.Value);
                        StaticLogger.LogTrace(ModuleLogName, string.Format("ServerName:{0}\nServerPort:{1}", ssNode.ServerName, ssNode.ServerPort.Value));
                        terminalMode = ssNode.Mode; // we need to set the terminal mode as terminal mode determines the geometry of the buffer and, how a connection is made to the host.

                        if (terminalMode == TerminalMode.Form) setScreenGeometry(ssNode.PageLength.Value, ssNode.PageWidth.Value);
                        if (terminalMode == TerminalMode.Stream)
                        {
                            TerminationString = ssNode.TerminationString.ToUpper();
                            setConnectionToSocket(); // credentials are not needed since we are communicating with a TCP Port Directly
                        }
                    }
                    OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true));
                    TeLSessionGroupNode gNode = cNode as TeLSessionGroupNode;
                    foreach (TeLSessionNode node in gNode.Nodes)
                    {
                        Process(node, level + 1);
                    }
                    break;

                case XmlTag.If:
                    If ifNode = cNode as If;
                    List<TeLSessionNode> ifNodesToProcess = ResolveCondition(ifNode.Condition) ? ifNode.ConditionNodes : ifNode.ElseNodes;
                    if (ifNodesToProcess != null)
                    {
                        foreach (TeLSessionNode node in ifNodesToProcess)
                        {
                            Process(node, level + 1);
                        }
                    }
                    break;

                case XmlTag.While:
                    While whileNode = cNode as While;
                    while (ResolveCondition(whileNode.Condition))
                    {
                        foreach (TeLSessionNode node in whileNode.ConditionNodes)
                        {
                            Process(node, level + 1);
                        }
                    }
                    break;

                case XmlTag.SendData:
                    SendData(cNode.Data);
                    break;
                case XmlTag.SendEnter:
                    SendData(string.Format("{0}\n", cNode.Data));
                    break;
                case XmlTag.SendTab:
                    SendData(string.Format("{0}\t", cNode.Data));
                    break;

                case XmlTag.SendSpace:
                    SendData(string.Format("{0} ", cNode.Data));
                    break;
                case XmlTag.SendPF1:
                case XmlTag.SendPF2:
                case XmlTag.SendPF3:
                case XmlTag.SendPF4:
                    SendPF(1 + cNode.TeLTag - XmlTag.SendPF1, cNode.Data);
                    break;

                case XmlTag.SendF1:
                case XmlTag.SendF2:
                case XmlTag.SendF3:
                case XmlTag.SendF4:
                case XmlTag.SendF5:
                case XmlTag.SendF6:
                case XmlTag.SendF7:
                case XmlTag.SendF8:
                case XmlTag.SendF9:
                case XmlTag.SendF10:
                case XmlTag.SendF11:
                case XmlTag.SendF12:
                    SendF(1 + cNode.TeLTag - XmlTag.SendF1);
                    break;
                case XmlTag.SendLogin:
                    {
                        if (terminalMode == TerminalMode.Form)
                        {
                            LoginPair gLogin = cNode as LoginPair;
                            ConnectAndLogin(gLogin);

                            SendEnter(gLogin.userName);
                            if (!string.IsNullOrEmpty(gLogin.password))
                            {
                                SendData(gLogin.password);
                            }
                        }
                        else
                        {
                            throw new TeLXMLInvalidTagException("The mode is configured as stream while attempting to send a SendLogin tag - which is not allowed.");
                        }
                        
                    }
                    break;
                case XmlTag.Expect:
                    Expect eNode = cNode as Expect;
                    string result = Expect(cNode.Data);
                    if (eNode.Grab)
                    {
                        string[] lines = result.Split('\n');
                        //lines = lines.Select(line => line.Trim('\r')).ToArray<string>();
                        //byte[] byteArray = lines[0].ToArray<byte>();
                        OnGrabChanged(new GrabChangedEventArgs(cNode.Name, lines));
                        //nsoftware.IPWorks.Telnet sPort = getTNInstance();
                      
                    }
                    break;
                case XmlTag.WaitForDataWholeScreen:
                    WaitForData wfdwsNode = cNode as WaitForData;
                    WaitForDataWholeScreen(cNode.Data, wfdwsNode.Timeout);
                    break;
                case XmlTag.WaitForDataOneRow:
                    WaitForData wfdorNode = cNode as WaitForData;
                    WaitForDataOneRow(cNode.Data, wfdorNode.Row.Value, wfdorNode.Timeout);
                    break;
                case XmlTag.WaitForDataRegion:
                    WaitForData wfdrNode = cNode as WaitForData;
                    WaitForDataRegion(cNode.Data, wfdrNode.Column.Value, wfdrNode.Row.Value, wfdrNode.Width.Value, wfdrNode.Height.Value, wfdrNode.Timeout);
                    break;
                case XmlTag.WaitForCursor:
                    WaitForCursor wfcNode = cNode as WaitForCursor;
                    WaitForCursor(wfcNode.Column, wfcNode.Row);
                    break;

                case XmlTag.GrabLine:
                    {
                        GrabLine glNode = cNode as GrabLine;
                        string line = GrabLine(glNode.Column, glNode.Row, glNode.Width);
                        OnGrabChanged(new GrabChangedEventArgs(cNode.Name, new string[] { line }));
                    }
                    break;
                case XmlTag.GrabLines:
                    {
                        GrabLines glsNode = cNode as GrabLines;
                        string[] lines = GrabLines(glsNode.Column, glsNode.Row, glsNode.Width, glsNode.Height);
                        OnGrabChanged(new GrabChangedEventArgs(cNode.Name, lines));

                    }
                    break;
                case XmlTag.GrabInt32:
                    {
                        GrabLine glNode = cNode as GrabLine;
                        string line = GrabLine(glNode.Column, glNode.Row, glNode.Width);
                        try
                        {
                            Int32 txx = Convert.ToInt32(line);
                        }
                        catch (Exception ex)
                        {
                            throw new TeLInvalidCastException(string.Format("TeXInvalidCast:\nInvalid System.Int32 Cast Attempt for [{0}].\nMessage:{1}.", line, ex.Message)); ;
                        }

                        OnGrabChanged(new GrabChangedEventArgs(cNode.Name, new string[] { line }));
                    }
                    break;
                case XmlTag.GrabDouble:
                    {
                        GrabLine glNode = cNode as GrabLine;
                        string line = GrabLine(glNode.Column, glNode.Row, glNode.Width);
                        try
                        {
                            double txx = Convert.ToDouble(line);
                        }
                        catch (Exception ex)
                        {
                            throw new TeLInvalidCastException(string.Format("TeLInvalidCast:\nInvalid System.Double Cast Attempt for [{0}].\nMessage:{1}.", line, ex.Message));
                        }
                        OnGrabChanged(new GrabChangedEventArgs(cNode.Name, new string[] { line }));
                    }
                    break;
                default:
                    {
                        throw new TeLXMLInvalidTagException(string.Format("TeLXMLInvalidTag:\n XmlTag <{0}> out of range.", cNode.TeLTag));
                    }
            }
            OnNodeProcessed(new TeLSessionNodeProcessEventArg(cNode, level,ModuleLogName));
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets module log name. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="logKey">   The log key.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setModuleLogName(string logKey)
        {
            base.SetModuleLogName(logKey);
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Connects the given node. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node to connect.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void Connect(SessSeq node)
        {
            _debugEnabled = node.DebuggingEnabled ;
            _logEnabled = node.LoggingEnabled ;
            if (_logEnabled)
            {
                StaticLogger.LogTrace(ModuleLogName, string.Format("TelSessionV3 Connect:-{0}", node.ToString()));
            }
            StaticLogger.LogTrace(ModuleLogName, string.Format("HostName Server Host: {0}",  node.ServerName));

            if (getSocketClient().Connected == false)
            {

                getSocketClient().setHostName(node.ServerName);
                getSocketClient().setHostPort( node.ServerPort.Value);
                OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true));
            }
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Resolve condition. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLConditionOutOfRangeException"> Thrown when a Te L Condition Out Of Range
        ///                                                    error condition occurs.</exception>
        ///
        /// <param name="condition">    The condition.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        private bool ResolveCondition(Conditions.ConditionBase condition)
        {
            Conditions.LogicalCondition lcondition = condition as Conditions.LogicalCondition;
            Conditions.FinalCondition fcondition = condition as Conditions.FinalCondition;
            switch (condition.ConditionType)
            {
                case IfConditionType.Not:
                    return !ResolveCondition(lcondition.ChildConditions[0]);

                case IfConditionType.And:
                    foreach (Conditions.ConditionBase child in lcondition.ChildConditions)
                    {
                        if (!ResolveCondition(child))
                            return false;
                    }
                    return true;

                case IfConditionType.Or:
                    foreach (Conditions.ConditionBase child in lcondition.ChildConditions)
                    {
                        if (ResolveCondition(child))
                            return true;
                    }
                    return false;

                case IfConditionType.EmptyIfCondition:
                    return fcondition.ConditionNodes.Count > 0;

                case IfConditionType.DataWithinRegion:
                    Conditions.DataWithinRegionCondition dwrCondition = condition as TelEnvyXmlLib.Conditions.DataWithinRegionCondition;
                    return IsDataWithinRegion(dwrCondition.Pattern, dwrCondition.Column, dwrCondition.Row, dwrCondition.Width, dwrCondition.Height);

                case IfConditionType.DataBeforeCursorPosition:
                    DataBeforeCursorPosition dbfCondition = condition as DataBeforeCursorPosition;
                    //to do 
                    //return IsDataWithinRegion(dbfCondition.Pattern, 0, _helper.Terminal.Screen.CursorTop, _helper.Terminal.Screen.CursorLeft, 1);
                    return false;

                case IfConditionType.CursorAtPosition:
                case IfConditionType.CursorWithinRegion:
                case IfConditionType.DataAtCursorPosition:
                    throw new NotImplementedException();

                default:
                    throw new TeLConditionOutOfRangeException(string.Format("ConditionType [{0}] out of range.", condition.ConditionType));
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Query if this TelSessionSC is data within region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   True if data within region, false if not. </returns>
        ///-------------------------------------------------------------------------------------------------

		private bool IsDataWithinRegion(string pattern, int column, int row, int width, int height)
        {
            bool result = TestRegion(pattern, column, row, width, height);
            Save();
            return result;
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tests region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   True if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        private bool TestRegion(string pattern, int row,int column, int width, int height)
        {
            throw new NotImplementedException();
        }

        #endregion


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Grab lines. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLOutputBufferOverFlowException"> Thrown when a Te L Output Buffer Over
        ///                                                     Flow error condition occurs.</exception>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   A string[]. </returns>
        ///-------------------------------------------------------------------------------------------------

        private string[] GrabLines(int row, int column, int width, int height)
        {
            int buffersize = Columns;
            if ((column + width) > buffersize) throw new TeLOutputBufferOverFlowException(string.Format("GrabLines: \n TeXOutputBufferOverFlow Exception\nScreenSize is [{0}], attemped scrap start offset at [{1}] of [{2}] length.", buffersize, column, width));

            return GetRegionText(row,column, width, height);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Grab line. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLOutputBufferOverFlowException"> Thrown when a Te L Output Buffer Over
        ///                                                     Flow error condition occurs.</exception>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        /// <param name="width">    The width.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        private string GrabLine(int row, int column, int width)
        {
            int buffersize = Columns;
            if ((column + width) > buffersize) throw new TeLOutputBufferOverFlowException(string.Format("GrabLine: \n TeXOutputBufferOverFlow Exception\nScreenSize is [{0}], attemped scrap start offset at [{1}] of [{2}] length.", buffersize, column, width));

            return GetRegionText( row,column, width, 1)[0];
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Grab int 32. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLOutputBufferOverFlowException"> Thrown when a Te L Output Buffer Over
        ///                                                     Flow error condition occurs.</exception>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        /// <param name="width">    The width.</param>
        ///
        /// <returns>   An int? </returns>
        ///-------------------------------------------------------------------------------------------------

        private int? GrabInt32(int row, int column,int width)
        {

            int buffersize = Columns;
            if ((column + width) > buffersize) throw new TeLOutputBufferOverFlowException(string.Format("GrabInt32: \n TeXOutputBufferOverFlow Exception\nScreenSize is [{0}], attemped scrap start offset at [{1}] of [{2}] length.", buffersize, column, width));
            string text = GetRegionText( row,column, width, 1)[0];
            int result;

            if (int.TryParse(text.Trim(), out result))
                return result;
            else
                return null;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Grab double. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLOutputBufferOverFlowException"> Thrown when a Te L Output Buffer Over
        ///                                                     Flow error condition occurs.</exception>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        ///
        /// <returns>   A double? </returns>
        ///-------------------------------------------------------------------------------------------------

		private double? GrabDouble(int column, int row, int width)
        {
            int buffersize = Columns;
            if ((column + width) > buffersize) throw new TeLOutputBufferOverFlowException(string.Format("GrabDouble: \n TeXOutputBufferOverFlow Exception\nScreenSize is [{0}], attempted scrap start offset at [{1}] of [{2}] length.", buffersize, column, width));

            string text = GetRegionText(row, column,width, 1)[0];
            double result;

            if (double.TryParse(text.Trim(), out result))
                return result;
            else
                return null;
        }

        

    

        #region Helper methods

    
    
    
        #endregion
    }
}
