
using nsoftware.IPWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TelEnvyXmlLib.Directives;
using TelEnvyXmlLib.Enums;
using TelEnvyXmlLib.Exceptions;
using TelEnvyXmlLib.ExtensionMethods;
namespace TelEnvyXmlLib
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A terminal screen. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.ComponentModel.Component"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class TerminalScreen : Component
    {
       
        
        int connectAttempted = 0; // An incrementor - the number of login attempts made for this session.  10 is the hard-coded max.

        object tLock = new object();

        #region Terminal Functions 


        

        /* 
         * Terminal Mode is configured on the sessseq node.
         *    mode="stream" terminator="end of report"
         *    mode="form" pagewidth=80" pagelength="24"
         */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the terminal mode. </summary>
        ///
        /// <value> The terminal mode. </value>
        ///-------------------------------------------------------------------------------------------------

        public TerminalMode terminalMode { get; set; }      // is this session a terminal stream or terminal form


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the termination string. </summary>
        ///
        /// <value> The termination string. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TerminationString { get; set; }



        #endregion



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Finalizes an instance of the TelEnvyXmlLib.TerminalScreen class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        ~TerminalScreen()
        {
            if (telnet1 != null){
                telnet1.Disconnect();
                telnet1.Dispose();
            }
            if (ipport1 != null){
                ipport1.Disconnect();
                ipport1.Dispose();
            }
            // if (telnet1 != null) telnet1.Disconnect();
            _receivedDataBuilder.Clear();
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets module string. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="modString">    The modifier string.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setModuleString(string modString)
        {
            ModuleString = modString;
            StaticLogger.LogTrace(ModuleString, "TerminalScreen.SetModuleString:");
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the module string. </summary>
        ///
        /// <value> The module string. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ModuleString { get; private set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Occurs when [release for processing]. This event is used to synchronize inter-
        ///             systems conversations
        ///              send [wait] receive [wait] send [wait]... etc.
        ///             </summary>
        ///-------------------------------------------------------------------------------------------------

        public event EventHandler<ReleaseForProcessingEventArgs> ReleaseForProcessing;  /* Occurs when Release For Processing. */


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the release for processing event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ea">   Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        public virtual void OnReleaseForProcessing(ReleaseForProcessingEventArgs ea)
        {
            EventHandler<ReleaseForProcessingEventArgs> handler = ReleaseForProcessing;
            if (handler != null)
                handler(null/*this*/, ea);
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TerminalScreen class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TerminalScreen()
        {
            InitializeComponent();
            ipport1.RuntimeLicense = "31504E4241414E58524634574A31303835380000000000000000000000000000000000000000000041364738424650540000564E30473250594D574D344D0000";
            telnet1.RuntimeLicense = "31504E4241414E58524634574A31303835380000000000000000000000000000000000000000000041364738424650540000564E30473250594D574D344D0000";
            if (sizeOfBuffer == 0)
            {
                if (Rows == 0) Rows = 24;
                if (Columns == 0) Columns = 80;
                buffer = new byte[Rows * Columns * 10];
            }
            else
            {
                buffer = new Byte[sizeOfBuffer];
            }
            ipport1.Config("TcpNoDelay = true");
            ipport1.Config("OutBufferSize=32767");
            ipport1.Config("InBufferSize=32767");
            //telnet1.Config("UseBackgroundThread=true");
            //            //  ipport1.Config("TcpNoDelay = true");
            //            ipport1.Config("OutBufferSize=32767");
            //            ipport1.Config("InBufferSize=32767");
            //telnet1.Config("UseBackgroundThread=true");
            //telnet1.EO
            byte[] bytes = new byte[1];
            bytes[0] = 10;
            ipport1.EOLB = bytes;

           
           
            telnet1.Config("OutBufferSize=32767");
            telnet1.Config("InBufferSize=32767");
            //ipport1.Timeout = 1;
            
            bufferReceivedData = new StringBuilder();

        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TerminalScreen class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="container">    The container.</param>
        ///-------------------------------------------------------------------------------------------------

        public TerminalScreen(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            ipport1.RuntimeLicense = "31504E4241414E58524634574A31303835380000000000000000000000000000000000000000000041364738424650540000564E30473250594D574D344D0000";
            if (sizeOfBuffer == 0)
            {
                if (Rows == 0) Rows = 24;
                if (Columns == 0) Columns = 80;
                buffer = new byte[Rows * Columns * 10];
            }
            else
            {
                buffer = new Byte[sizeOfBuffer];
            }
            //telnet1.Timeout = 1000;
            ipport1.Config("TcpNoDelay = true");
            ipport1.Config("OutBufferSize=32767");
            ipport1.Config("InputSize=32767");
            //telnet1.Config("UseBackgroundThread=true");
            //telnet1.EO
            byte[] bytes = new byte[1];
            bytes[0] = 10;
            ipport1.EOLB = bytes;
           
    
            telnet1.Config("OutBufferSize=32767");
            telnet1.Config("InBufferSize=32767");

            //ipport1.Timeout = 1;
            // ipport1.RuntimeLicense = "31504E4241414E58524634574A31303835380000000000000000000000000000000000000000000041364738424650540000564E30473250594D574D344D0000";
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        /// <param name="maxWaitTime">  The maximum time of the wait.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        private bool WaitForData(Regex regexPattern, int maxWaitTime)
        {
            bool bRet = false;
            try
            {
                Monitor.Enter(tLock);
                bRet = WaitForData(regexPattern.ToString(), Columns, Rows, 0, 0);
            }
            catch (System.Exception ex)
            {
            	
            }
            finally
            {
                Monitor.Exit(tLock);
            }
            
            return bRet;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="maxWaitTime">  The maximum time of the wait.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        private bool WaitForData(int maxWaitTime)
        {
            bool bRet = true;
            try
            {
                Monitor.Enter(tLock);
                // not implemented
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return bRet;
            //return true;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool WaitForData(string pattern, int column, int row, int width, int height)
        {
            bool bRet = false;
            try
            {
                Monitor.Enter(tLock);
                Regex expression = new Regex(pattern);

                // test if the pattern is already on the screen
                if (TestRegion(expression, column, row, width, height))
                    return true;

                int timeout = maxWaitTime;
                int start = Environment.TickCount;
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return bRet;
            // _receivedData = ReceivedData;
            // ReceivedData = string.Empty; // is not necessary is the polled data are retrieved from a Queue<string>

          
            //TelLogWriterEventArg lData = null;

            //while (Process(timeout) == TerminalState.DataReceived)
            //{
            //    TelEnvyXmlLib.StaticLogger.LogTrace(6000, "WaitForData-5", bufferReceivedData.ToString());
            //    // process as much data as possible
            //    //while (Process(timeout) == TerminalState.DataReceived)
            //    //{
            //    //    //_receivedData += _terminal.ReceivedData;
            //    //    //lData.LogData = ReceivedData;
            //    //    //OnWritingToLog(this, lData);
            //    //}

            //    if (TestRegion(expression, column, row, width, height))
            //        return true;

            //    timeout = maxWaitTime - (Environment.TickCount - start);
            //    if (timeout < 0)
            //        break;
            //}
            // _receivedBuffer = _receivedData;
            //return false;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for cursor. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TelWaitMissingCoordinateException"> Thrown when a Tel Wait Missing Coordinate
        ///                                                      error condition occurs.</exception>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool WaitForCursor(int column, int row)
        {
            bool bRet = true;
            try
            {
                Monitor.Enter(tLock);
                bool result = WaitForCursor(column, row);
                Save();
                if (!result)
                    throw new TelWaitMissingCoordinateException(string.Format("WaitForCursor Column Row {0} Column {1}, receive buffer:\n{2}", row, column, bufferReceivedData));
                return result;
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return bRet;
         
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool WaitForData(string pattern)
        {
            //_receivedData = ReceivedData;
            //// ReceivedData = string.Empty; // is not necessary is the polled data are retrieved from a Queue<string>

            //Regex expression = new Regex(pattern);

            //// test if the pattern is already on the screen
            //if (TestRegion(expression))
            //    return true;

            //int timeout = maxWaitTime;
            //int start = Environment.TickCount;
            //TelLogWriterEventArg lData = null;

            //while (Process(timeout) == TerminalState.DataReceived)
            //{

            //    lData = new TelLogWriterEventArg(string.Format("WaitForData: {0}", _receivedData));
            //    OnWritingToLog(this, lData);

            //    // process as much data as possible
            //    while (Process(timeout) == TerminalState.DataReceived)
            //    {
            //        //_receivedData += _terminal.ReceivedData;
            //        lData.LogData = ReceivedData;
            //        OnWritingToLog(this, lData);
            //    }

            //    if (TestRegion(expression))
            //        return true;

            //    timeout = maxWaitTime - (Environment.TickCount - start);
            //    if (timeout < 0)
            //        break;
            //}
            //// _receivedBuffer = _receivedData;
            return false;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data on row. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLWaitDataRegionTimeOutException"> Thrown when a Te L Wait Data Region Time
        ///                                                      Out error condition occurs.</exception>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="row">      The row.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool WaitForDataOnRow(string pattern, int row)
        {
            bool bRet = true;
            try
            {
                Monitor.Enter(tLock);
                bool result = WaitForDataOnRow(pattern, row);
                Save();
                if (!result)
                    throw new TeLWaitDataRegionTimeOutException(string.Format("WaitForData {0}, receive buffer:\n{1}", pattern, bufferReceivedData));
                return result;
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return bRet;
          
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data one row. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="data">     The data.</param>
        /// <param name="value">    The value.</param>
        /// <param name="timeout">  The timeout.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void WaitForDataOneRow(string data, int value, int? timeout)
        {
            bool bRet = false;
            try
            {
                Monitor.Enter(tLock);
              //  bool result = WaitForDataOnRow(pattern, row,);
               // Save();
               // if (!result)
               //     throw new TeLWaitDataRegionTimeOutException(string.Format("WaitForData {0}, receive buffer:\n{1}", pattern, bufferReceivedData));
               // return result;
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return ;
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLWaitDataRegionTimeOutException"> Thrown when a Te L Wait Data Region Time
        ///                                                      Out error condition occurs.</exception>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        /// <param name="timeout">  The timeout.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void WaitForDataRegion(string pattern, int column, int row, int width, int height, int? timeout)
        {
            bool bRet = false;
            try
            {
                Monitor.Enter(tLock);
                bRet = WaitForData(pattern, column, row, width, height);
                Save();
                if (!bRet)
                    throw new TeLWaitDataRegionTimeOutException(string.Format("WaitForData {0}, receive buffer:\n{1}", pattern, bufferReceivedData));
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return ;
          
        }

       
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data whole screen. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLWaitDataRegionTimeOutException"> Thrown when a Te L Wait Data Region Time
        ///                                                      Out error condition occurs.</exception>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="timeout">  The timeout.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void WaitForDataWholeScreen(string pattern, int? timeout)
        {
            bool result = WaitForData(pattern);
            Save();
            if (!result)
                throw new TeLWaitDataRegionTimeOutException(string.Format("WaitForData {0}, receive buffer:\n{1}", pattern, bufferReceivedData));
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tests region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="expression">   The expression.</param>
        /// <param name="column">       The column.</param>
        /// <param name="row">          The row.</param>
        /// <param name="width">        The width.</param>
        /// <param name="height">       The height.</param>
        ///
        /// <returns>   True if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        protected internal bool TestRegion(Regex expression, int column, int row, int width, int height)
        {
            string[] lines = GetRegionText(column, row, width, height);
            foreach (var item in lines)
            {
                StaticLogger.LogTrace(ModuleString, string.Format("TestData:TestRegion-1:{0}-ReceivedData:{2}-Expression:{1}", item, bufferReceivedData.ToString(), expression.ToString()));

            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (expression.IsMatch(lines[i]))
                {
                    StaticLogger.LogTrace(ModuleString, string.Format("TestData:TestRegion-2:{0}-ReceivedData:{2}-Expression:{1}", "true", bufferReceivedData.ToString(), expression.ToString()));
                    return true;
                }
            }
            foreach (var item in lines)
            {
                StaticLogger.LogTrace(ModuleString, string.Format("TestData:TestRegion-3:{0}-ReceivedData:{2}-Expression:{1}", item, bufferReceivedData.ToString(), expression.ToString()));

            }
            return false;
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tests data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regex">    The RegEx.</param>
        ///
        /// <returns>   True if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        protected internal bool TestData(Regex regex)
        {
            if (bufferReceivedData == null)
            {
                do
                {
                    StaticLogger.LogTrace(ModuleString, string.Format("TestData[regex]-Do Not Use... Method To Do:"));
                    Thread.Sleep(10);
                } while (1 == 1);

            }

            Match m = regex.Match(bufferReceivedData.ToString());
            if (m.Success)
            {
                StaticLogger.LogTrace(ModuleString, string.Format("TestData:TestRegion-1:{0}-ReceivedData:{2}-Expression:{1}", m.ToString(), bufferReceivedData.ToString(), regex.ToString()));
                string _receivedBuffer = bufferReceivedData.ToString().Substring(m.Index + m.Length);
                _receivedData = _receivedBuffer.Substring(0, m.Index + m.Length);
                return true;
            }

            return false;
        }
        // Use of Width and Height is mutually exclusive

        /*             1         2         3         4         5         6         7         8
         *    12345678901234567890123456789012345678901234567890123456789012345678901234567890
         *  1 
         *  2
         *  3
         *  4
         *  5           this is a test\r
         *  6
         *  7
         *  8
         *  9
         *  0
         *    Calculated offset ((4 * 80) + 10)
         *    Offset start byte 330
         *    Offset end byte 345
         *  
         */

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets region text. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="row">      The row.</param>
        /// <param name="column">   The column.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   (Optional) The height.</param>
        ///
        /// <returns>   An array of string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string[] GetRegionText(int row, int column, int width, int height = 0)
        {
            int blocksize = 0;
            int colOffsetInRow = 0;
            int widthOfColOffsetInRow = 0;
            if (height == 0)
            {
                blocksize = Columns * row;
                colOffsetInRow = blocksize + column;
                widthOfColOffsetInRow = colOffsetInRow + width;
            }
            if (height != 0)
            {
                width = Columns;
                blocksize = Columns * (row * height);
                colOffsetInRow = blocksize + column;
                widthOfColOffsetInRow = colOffsetInRow + width;
            }
            BufferedStream buffStream = new BufferedStream(memStream);
            byte[] tmpBuffer = null;
            int xCnt = buffStream.Read(tmpBuffer, colOffsetInRow, width);
            string[] tmpStr = Encoding.UTF8.GetString(tmpBuffer).Split('\r');
            return tmpStr;
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tests region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="expression">   The expression.</param>
        ///
        /// <returns>   True if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        protected internal bool TestRegion(Regex expression)
        {
            if (_receivedData == null)
                _receivedData = string.Empty;

            StaticLogger.LogTrace(ModuleString, string.Format("TestRegion[regex]:{0}-ReceivedData:{2}-Expression:{1}", ipport1.Connected, bufferReceivedData.ToString(), expression.ToString()));
            Match m = expression.Match(_receivedData);
            if (m.Success)
            {
                selectedDataBuffer = _receivedData.Substring(m.Index + m.Length);
                _receivedData = _receivedData.Substring(0, m.Index + m.Length);
                _receivedDataBuilder.Add(_receivedData);
                return true;
            }
            return false;
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves this TerminalScreen. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        protected internal void Save()
        {

        }

        //private TerminalState Process(int timeout)
        //{
        //    //_receivedData = string.Empty;
        //    //if (ReceivedData.Length > 0)
        //    //{
        //    //    _receivedData += ReceivedData;


        //    //    return TerminalState.DataReceived;
        //    //}
        //    //if (!IsConnected)
        //    //    return TerminalState.Disconnected;
        //    //bool n = WaitForData(maxWaitTime);
        //    ////if (n > 0)
        //    //{
        //    //    //Process(maxWaitTime);
        //    //    //if (ReceivedData.Length > 0)
        //    //    //{
        //    //    //    _receivedData += ReceivedData;

        //    //    //}
        //    //    return TerminalState.DataReceived;
        //    //}

        //    ////if (n < 0)
        //    //{
        //    //    ipport1.Disconnect();
        //    //    return TerminalState.Disconnected;
        //    //}
        //    //Thread.Sleep(10);
        //    //return TerminalState.NoDataAvailable;

        //}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes the stream. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void initializeStream()
        {
            memStream = new MemoryStream();
        }

        #region Send data



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends an enter. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sData">    The data.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SendEnter(string sData)
        {
            StaticLogger.LogTrace(ModuleString, string.Format(ModuleString, "SendEnter: {0}", sData));
            SendData(sData + Environment.NewLine);
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TelHostLoginNotPossibleException"> Thrown when a Tel Host Login Not Possible
        ///                                                     error condition occurs.</exception>
        ///
        /// <param name="cmdText">  The command text.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void SendData(string cmdText)
        {
            bool bRet = false;
            try
            {
                Monitor.Enter(tLock);
                bool successfullySent = false;
                byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(cmdText.ToArray());
                {

                    if (ipport1 != null)
                    {
                        if (!ipport1.Connected) ipport1.Connect(this.ServerName, this.ServerPort);
                        if (!ipport1.Connected) throw new TelHostLoginNotPossibleException("SendData:-The host is not connected.");

                        try
                        {
                            OnReleaseForProcessing(new ReleaseForProcessingEventArgs(false));       // ensure we block the next sequence until unblocked by the datain event
                            {
                                if (ipport1.Connected)
                                {
                                    ipport1.Send(bytes);
                                    StaticLogger.LogTrace(ModuleString, string.Format("Socket (IpPort) Sending Bytes: {0} to {1}:{2}", bytes.Length, ipport1.RemoteHost, ipport1.RemotePort));
                                    if (!ipport1.Connected)
                                    {
                                        StaticLogger.LogTrace(ModuleString, string.Format("Attempted Send - to a disconnected Socket {0}:{1}", ipport1.RemoteHost, ipport1.RemotePort));
                                        StaticLogger.LogTrace(ModuleString, string.Format("Socket (IpPort) Sent Bytes: {0}", bytes.Length));
                                    }
                                    else
                                    {
                                        while (!eolDetected)
                                        {
                                            ipport1.DoEvents();
                                            // Thread.Sleep(500);
                                        }
                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        {

                        }
                        finally
                        {
                            StaticLogger.LogTrace(ModuleString, string.Format(ModuleString, "Socket (IpPort) Connection Status: {0}", ipport1.Connected.ToString()));
                            StaticLogger.LogTrace(ModuleString, string.Format(ModuleString, "*** SendData: Sent to the Server {0}", cmdText));
                        }


                    }

                    if (telnet1 != null)
                    {
                        StaticLogger.LogTrace(ModuleString, string.Format(ModuleString, "Socket (telnet) Connection Status: {0}", telnet1.Connected.ToString()));
                        DateTime startTime = DateTime.Now;
                        DateTime endTime = DateTime.Now;
                        TimeSpan duration = new TimeSpan(endTime.Ticks - startTime.Ticks);
                        if (!telnet1.Connected) throw new TelHostLoginNotPossibleException("telnet-SendData:-The host is not connected.");
                        endTime = DateTime.Now;
                        try
                        {

                            //foreach (byte item in bytes)
                            //{
                            //    Console.WriteLine(item);
                            //}
                            OnReleaseForProcessing(new ReleaseForProcessingEventArgs(false));       // ensure we block the next sequence until unblocked by the datain event
                            {
                                if (ipport1.Connected)
                                {
                                    StaticLogger.LogTrace(ModuleString, string.Format("Socket (telnet) Sending Bytes: {0} to {1}:{2}", bytes.Length, telnet1.RemoteHost, telnet1.RemotePort));
                                    telnet1.DataToSendB = bytes;
              if (!telnet1.Connected)
                                    {
                                        StaticLogger.LogTrace(ModuleString, string.Format("Attempted Send - to a disconnected Socket {0}:{1}", telnet1.RemoteHost, telnet1.RemotePort));
                                        StaticLogger.LogTrace(ModuleString, string.Format("Socket (telnet) Sent Bytes: {0}", bytes.Length));
                                    }
                                    else
                                    {
                                        while (!eolDetected)
                                        {
                                            if (telnet1 != null) telnet1.DoEvents();
                                            else
                                                Console.WriteLine("telnet1 is Null Prematurely");
                                            Thread.Sleep(250);
                                        }
                                    }                      
                                }
                            }

                        }
                        catch (Exception e)
                        {

                        }
                        finally
                        {

                        }
                    }
                    //catch (Exception ex)
                    //{
                    //    StaticLogger.LogError(ModuleString, ex, String.Format("Socket Error Occurred:{0}\n{1}",ex.Message,ex.StackTrace));
                    //}
                    //finally
                    //{

                    //}
                    Save();
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(tLock);
            }

            return ;
         
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a f. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLSpecialKeyNotSupportedException"> Thrown when a Te L Special Key Not
        ///                                                       Supported error condition occurs.</exception>
        ///
        /// <param name="number">   Number of.</param>
        ///-------------------------------------------------------------------------------------------------

		protected internal void SendF(int number)
        {
            string sequence;
            switch (number)
            {
                case 6: sequence = "17~"; break;
                case 7: sequence = "18~"; break;
                case 8: sequence = "19~"; break;
                case 9: sequence = "20~"; break;
                case 10: sequence = "21~"; break;
                case 11: sequence = "22~"; break;
                case 12: sequence = "23~"; break;
                default:
                    {
                        StaticLogger.LogTrace(ModuleString, string.Format("Invalid-SendF Key Sequence: {0}", number));
                        throw new Exceptions.TeLSpecialKeyNotSupportedException(string.Format("F{0} is unsupported.", number));
                    }
            }
            StaticLogger.LogTrace(ModuleString, string.Format("SendF Key Sequence: {0}", sequence));
            SendData(Csi + sequence);
        }

        public void AcceptData(bool acceptData=false)
        {
            if (ipport1 != null) ipport1.AcceptData = acceptData;
            if (telnet1 != null) telnet1.AcceptData = acceptData;
        }

        /*
		 * 
		Key 	Numeric 	ANSI Mode	    Numeric 	VT52 Mode
				Application 		        Application 
		0	    0	        Ss3p 	        0	        Esc?p 
		1	    1	        Ss3q 	        1	        Esc?q 
		2	    2	        Ss3r 	        2	        Esc?r 
		3	    3	        Ss3s 	        3	        Esc?s 
		4	    4	        Ss3t 	        4	        Esc?t 
		5	    5	        Ss3u 	        5	        Esc?u 
		6	    6	        Ss3v 	        6	        Esc?v 
		7	    7	        Ss3w 	        7	        Esc?w 
		8	    8	        Ss3x 	        8	        Esc?x 
		9	    9	        Ss3y 	        9	        Esc?y 
		- 	(minus) 	    Ss3m 	        - 	        Esc?m 
		, 	(comma) 	    Ss3l 	        , 	        Esc?l 
		. 	(period) 	    Ss3n 	        . 	        Esc?n 
		Enter 	Cr or 	    Ss3M 	        Cr or 	    Esc?M 
				CrLf 		                CrLf 	
		PF1 	Ss3P 	    Ss3P 	        EscP 	    EscP 
		PF2 	Ss3Q 	    Ss3Q 	        EscQ 	    EscQ 
		PF3 	Ss3R 	    Ss3R 	        EscR 	    EscR 
		PF4 	Ss3S 	    Ss3S 	        EscS 	    EscS 
		 * 
		 */

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a pf. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLSpecialKeyNotSupportedException"> Thrown when a Te L Special Key Not
        ///                                                       Supported error condition occurs.</exception>
        ///
        /// <param name="number">   Number of.</param>
        /// <param name="cmdText">  The command text.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void SendPF(int number, string cmdText)
        {
            string sequence;
            switch (number)
            {
                case 1: sequence = "P"; break;
                case 2: sequence = "Q"; break;
                case 3: sequence = "R"; break;
                case 4: sequence = "S"; break;
                default:
                    {
                        StaticLogger.LogTrace(ModuleString, string.Format("Invalid-SendF Key Sequence: {0}", number));
                        throw new TeLSpecialKeyNotSupportedException(string.Format("PF{0} is unsupported.", number));
                    }
            }
            StaticLogger.LogTrace(ModuleString, string.Format("SendPF Key Sequence: {0}", sequence));
            SendData(Escape + sequence + cmdText);
        }


        #endregion
        #region Receive data

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Expects. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string Expect(string pattern)
        {
            if (terminalMode == TerminalMode.Stream) // if mode is a stream. Send back all the data in the buffer to the control. 
            {
                
                StaticLogger.LogTrace(ModuleString, string.Format("Expect Terminal Mode = Stream: {0}", pattern));
                if (!ipport1.Connected)
                {
                    ipport1.Connect(ServerName,ServerPort);
                    StaticLogger.LogTrace(ModuleString, string.Format("Expect - Connection Mode = Stream: Server {0} Port {1} Connected is {1} Pattern {2}", ServerName,ServerPort,ipport1.Connected, pattern));
            
                }
                while (!eolDetected)
                {
                    ipport1.DoEvents();
                    Thread.Sleep(500);
                }
                return bufferReceivedData.ToString();
            }
            // the terminal mode must a Form. so we need to map the coordinates to a virtual screen given 
            // predefined screen page and width properties
            lock (qbLock)   // Mode if Form
            {
                StaticLogger.LogTrace(ModuleString, string.Format("Expect Terminal Mode = Form: {0}", pattern));
                telnet1.Connect(ServerName);


                string x = bufferReceivedData.ToString();
                _receivedData = ReceivedData;
            }
            bool result = TestData(new Regex(pattern)); // Expect(pattern, timeout );
            Save();

            return _receivedData;
        }
        public List<string> Expect(string pattern,bool bAsStringList)
        {
            if (terminalMode == TerminalMode.Stream) // if mode is a stream. Send back all the data in the buffer to the control. 
            {
                if (!ipport1.Connected) ipport1.Connect(ServerName, ServerPort);
                StaticLogger.LogTrace(ModuleString, string.Format("Expect Terminal Mode = Stream: {0}", pattern));
                List<string> lst = new List<string>(bufferReceivedData.ToString().Split('\n'));
                return lst;
            }
            // the terminal mode must a Form. so we need to map the coordinates to a virtual screen given 
            // predefined screen page and width properties
            lock (qbLock)
            {
                StaticLogger.LogTrace(ModuleString, string.Format("Expect Terminal Mode = Form: {0}", pattern));
               // _receivedData = ReceivedData;
                _receivedDataBuilder.Add(ReceivedData);
            }
            bool result = TestData(new Regex(pattern)); // Expect(pattern, timeout );
            Save();

            return _receivedDataBuilder;
        }





        #endregion

        #region Protected Methods



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets screen geometry. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="screenRows">       The screen rows.</param>
        /// <param name="screenColumns">    The screen columns.</param>
        ///-------------------------------------------------------------------------------------------------

        protected internal void setScreenGeometry(int screenRows, int screenColumns)
        {
            Columns = screenColumns;
            Rows = screenRows;
            if (Columns >= 0 && Rows >= 0)
            {
                bufferRegion = new TerminalScreenRegion(screenRows, screenRows);
            }
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Disconnects this TerminalScreen. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Disconnect()
        {
            try
            {
                if (ipport1 != null)
                {
                    ipport1.AcceptData = false;
                    ipport1.Disconnect();
                }
                if (telnet1 != null)
                {
                    telnet1.AcceptData = false;
                    telnet1.Disconnect();
                }
            }
            catch (IPWorksException)
            {

            }

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets using forms. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="bIsUsed">  True if is used, false if not.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setUsingForms(bool bIsUsed)
        {
            usingForms = bIsUsed;
            if (bIsUsed) this.ipport1=null;
            else this.telnet1 = null;
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets IP instance. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   The IP instance. </returns>
        ///-------------------------------------------------------------------------------------------------

        public nsoftware.IPWorks.Ipport getIPInstance()
        {
            if (ipport1 != null) return (Ipport) ipport1;

            return null;
        }

        public nsoftware.IPWorks.Telnet GetTEInstance()
        {
            
            if (telnet1 != null) return (nsoftware.IPWorks.Telnet) telnet1;
            return null;
        }

        //public Telnet getTNInstance()
        //{
        //    return telnet1;
        //}


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Connects an and login. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="gLogin">   The login.</param>
        ///-------------------------------------------------------------------------------------------------

        public void ConnectAndLogin(LoginPair gLogin)
        {
            connectAttempted++; // incrementor for the number of attempts a connection is made.
            if (ipport1 != null)
            {
                try
                {
                    if (!ipport1.Connected)
                    {
                        ipport1.Disconnect();

                        ipport1.RemotePort = ServerPort;
                        ipport1.RemoteHost = ServerName;

                        setConnectionToSocket();


                        ipport1.SendLine(gLogin.userName);
                        if (gLogin.UsePassword) ipport1.SendLine(gLogin.password);
                    }
                    else
                    {
                        ipport1.Connected = false;
                        if (connectAttempted <=9)
                        {
                        ConnectAndLogin( gLogin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ipport1.Connected = false;
                    ipport1.AcceptData = false;
                    StaticLogger.LogError(ModuleString, ex, string.Format("IpPort-ConnectAndLogin.ReceivedData: {0}", bufferReceivedData.ToString()));
                }
            }
            if (telnet1 != null)
            {
                try
                {
                    if (!telnet1.Connected)
                    {
                        telnet1.Disconnect();

                        telnet1.RemotePort = ServerPort;
                        telnet1.RemoteHost = ServerName;

                        setConnectionToSocket();

                        telnet1.DataToSend = gLogin.userName;
                        if (gLogin.UsePassword) telnet1.DataToSend = gLogin.password;
                    }
                    else
                    {
                        telnet1.Connected = false;
                        if (connectAttempted <= 9)
                        {
                            ConnectAndLogin(gLogin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    telnet1.Connected = false;
                    telnet1.AcceptData = false;
                    StaticLogger.LogError(ModuleString, ex, string.Format("Telnet-ConnectAndLogin.ReceivedData: {0}", bufferReceivedData.ToString()));
                }
            }
        }

      
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the cells. </summary>
        ///
        /// <value> The cells. </value>
        ///-------------------------------------------------------------------------------------------------

        public TerminalCell[] Cells { get; set; }

        #endregion
        #region Fields

        /// <summary>   Information describing the received. </summary>
        protected internal string _ReceivedData;    /* Information describing the received */
        /// <summary>   True to enable, false to disable the debug. </summary>
        protected internal bool _debugEnabled = false;  /* True to enable, false to disable the debug */

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the columns. </summary>
        ///
        /// <value> The columns. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Columns { get; set; }
        /// <summary>   Fields... </summary>
        protected BlockingQueue<string> _queuedBuffer = new BlockingQueue<string>(100); /* Buffer for queued data */
        /// <summary>   The count of socket in bytes. </summary>
        protected int countOfSocketBytes = 0;   /* The count of socket in bytes */


        /// <summary>   The memory stream. </summary>
        private MemoryStream memStream = new MemoryStream();    /* The memory stream */
        /// <summary>   The buffer. </summary>
        public Byte[] buffer;   /* The buffer */
        /// <summary>   The qb lock. </summary>
        private readonly object qbLock = new object();  /* The qb lock */
        /// <summary>   Information describing the received. </summary>
        protected internal string _receivedData;    /* Information describing the received */
        protected internal List<string> _receivedDataBuilder;    /* Information describing the received as stringBuilder */
        /// <summary>   Size of the buffer. </summary>
        private int sizeOfBuffer = 0;   /* Size of the buffer */
        /// <summary>   The buffer region. </summary>
        TerminalScreenRegion bufferRegion;  /* The buffer region */
        /// <summary>   True to using forms. </summary>
        bool usingForms = false;    /* True to using forms */
        /// <summary>   Information describing the buffer received. </summary>
        private StringBuilder bufferReceivedData;   /* Information describing the buffer received */

        #endregion
        #region Properties



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets information describing the received. </summary>
        ///
        /// <value> Information describing the received. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ReceivedData
        {
            get
            {
                lock (qbLock)
                {
                    return bufferReceivedData.ToString();
                }
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the width. </summary>
        ///
        /// <value> The width. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Width { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the height. </summary>
        ///
        /// <value> The height. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Height { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets connection to socket. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TelHostLoginNotPossibleException"> Thrown when a Tel Host Login Not Possible
        ///                                                     error condition occurs.</exception>
        ///-------------------------------------------------------------------------------------------------

        public void setConnectionToSocket()
        {
            try
            {
                if (ipport1 != null)
                {
                    this.ipport1.Connected = true;
                    StaticLogger.LogTrace(ModuleString, string.Format("IpPort-Connection Status: {0}", ipport1.Connected.ToString()));
                }
                if (telnet1 != null)
                {
                    telnet1.Connected = true;
                    StaticLogger.LogTrace(ModuleString, string.Format("Telnet-Connection Status: {0}", telnet1.Connected.ToString()));
                }
            }
            catch (Exception ex)
            {
                string x = MethodBase.GetCurrentMethod().Name;
                throw new TelHostLoginNotPossibleException("SetConnectionToSocket-Not Connected Exception.", ex);
            }
            finally
            {

            }
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TerminalScreen is connected. </summary>
        ///
        /// <value> True if this TerminalScreen is connected, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsConnected { get; private set; }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the state of the terminal. </summary>
        ///
        /// <value> The terminal state. </value>
        ///-------------------------------------------------------------------------------------------------

        public TerminalState terminalState { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the cursor left. </summary>
        ///
        /// <value> The cursor left. </value>
        ///-------------------------------------------------------------------------------------------------

        public int CursorLeft { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the cursor top. </summary>
        ///
        /// <value> The cursor top. </value>
        ///-------------------------------------------------------------------------------------------------

        public int CursorTop { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the rows. </summary>
        ///
        /// <value> The rows. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Rows { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the maximum time of the wait. </summary>
        ///
        /// <value> The maximum time of the wait. </value>
        ///-------------------------------------------------------------------------------------------------

        public int maxWaitTime { get; private set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the selected data buffer. </summary>
        ///
        /// <value> A buffer for selected data data. </value>
        ///-------------------------------------------------------------------------------------------------

        public string selectedDataBuffer { get; private set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l input directory. </summary>
        ///
        /// <value> The pathname of the te l input directory. </value>
        ///-------------------------------------------------------------------------------------------------

        [DefaultValue(@"C:\Envy\TelEnvy\InputFiles")]
        public string TeLInputDirectory { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TerminalScreen is canceled. </summary>
        ///
        /// <value> True if this TerminalScreen is canceled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsCanceled { get; private set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l output directory. </summary>
        ///
        /// <value> The pathname of the te l output directory. </value>
        ///-------------------------------------------------------------------------------------------------

        [DefaultValue(@"C:\Envy\TelEnvy\OutputFiles")]
        public string TeLOutputDirectory { get; set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l log directory. </summary>
        ///
        /// <value> The pathname of the te l log directory. </value>
        ///-------------------------------------------------------------------------------------------------

        [DefaultValue(@"C:\Envy\TelEnvy\Logs")]
        public string TeLLogDirectory { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the te l debug directory. </summary>
        ///
        /// <value> The pathname of the te l debug directory. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TeLDebugDirectory { get; set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether this TerminalScreen is faulted. </summary>
        ///
        /// <value> True if this TerminalScreen is faulted, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsFaulted { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the log file specification. </summary>
        ///
        /// <value> The log file specification. </value>
        ///-------------------------------------------------------------------------------------------------

        public string LogFileSpecification { get; set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the Date/Time of the transaction timestamp. </summary>
        ///
        /// <value> The transaction timestamp. </value>
        ///-------------------------------------------------------------------------------------------------

        public DateTime TransactionTimestamp { get; set; }

        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a unique identifier of the transaction. </summary>
        ///
        /// <value> Unique identifier of the transaction. </value>
        ///-------------------------------------------------------------------------------------------------

        public Guid transactionGUID { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the name of the server. </summary>
        ///
        /// <value> The name of the server. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ServerName { get; private set; }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the server port. </summary>
        ///
        /// <value> The server port. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ServerPort { get; private set; }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets server name. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="v">    A string to process.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setServerName(string v)
        {
            ServerName = v;
            StaticLogger.LogTrace(ModuleString, string.Format("setServerName", v));
            if (ipport1 != null) ipport1.RemoteHost = v;
            if (telnet1 != null) telnet1.RemoteHost = v;
            

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets server port. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="vPort">    The port.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setServerPort(int vPort)
        {
            ServerPort = vPort;
            StaticLogger.LogTrace(ModuleString, string.Format("setServerPort", vPort));
           if (ipport1 != null) ipport1.RemotePort = vPort;
           if (telnet1 != null) telnet1.RemoteHost = Convert.ToString(vPort);
        }
        #endregion
        #region Telnet Events

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on wont events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet wont event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnWont(object sender, TelnetWontEventArgs e)
        {

        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on will events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet will event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnWill(object sender, TelnetWillEventArgs e)
        {
            try
            {
                if (e.OptionCode == 38)
                    telnet1.DontOption = 38;
            }
            catch (IPWorksException)
            {
            }
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on ready to send events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet ready to send event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnReadyToSend(object sender, TelnetReadyToSendEventArgs e)
        {
            initializeStream();  // initialize the memory stream buffer
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on error events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet error event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnError(object sender, TelnetErrorEventArgs e)
        {

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on dont events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet dont event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnDont(object sender, TelnetDontEventArgs e)
        {

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on do events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet do event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnDo(object sender, TelnetDoEventArgs e)
        {
            //try
            //{
            //    telnet1.WontOption = e.OptionCode;
            //}
            //catch (IPWorksException)
            //{
            //}
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on disconnected events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet disconnected event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnDisconnected(object sender, TelnetDisconnectedEventArgs e)
        {
            StaticLogger.LogDebug(ModuleString, string.Format("telnet1_OnDisconnected: {0}", e.Description));
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the data received. </summary>
        ///
        /// <value> The data received. </value>
        ///-------------------------------------------------------------------------------------------------

        public string dataReceived { get; set; }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on connection status events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet connection status event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnConnectionStatus(object sender, TelnetConnectionStatusEventArgs e)
        {
            StaticLogger.LogTrace(ModuleString, string.Format("OnConnectionStatus -: {0}", e.Description));
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on connected events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet connected event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnConnected(object sender, TelnetConnectedEventArgs e)
        {
            if (telnet1.Connected)
                StaticLogger.LogTrace(ModuleString, string.Format("Telnet-StatusCode:{0} - Description {1}", e.StatusCode, e.Description));


            telnet1.AcceptData = true;
            if ((e.StatusCode == 0) && (e.Description == "OK"))
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;

            }
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on command events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet command event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnCommand(object sender, TelnetCommandEventArgs e)
        {

        }
        #endregion
        #region Enumerations
        /// <summary>   The log option. </summary>
        protected internal EnumLoggingOptions LogOpt;   /* The log option */
        #endregion
        #region Constants
        /// <summary>   Escape Sequence: \x1B. </summary>
        protected internal const string Escape = "\x1B";    /* The escape */
        /// <summary>   Escape Sequence: \x1B[. </summary>
        protected internal const string Csi = "\x1B[";  /* The csi */

        /// <summary>
        /// The default debug path
        /// </summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DefaultDebugPath

        protected internal const string DefaultDebugPath = @"C:\envy\tel\DebugPNG\";    /* . */
        /// <summary>
        /// The default log path
        /// </summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DefaultLogPath
        protected internal const string DefaultLogPath = @"C:\envy\tel\logs\";  /* . */
        /// <summary>
        /// The DefaultRecordPath
        /// </summary>

        protected internal const string DefaultRecordPath = @"C:\envy\tel\DebugREC\";   /* . */

        /// <summary>   Default screen width (columns). The default is 132. </summary>
        protected internal const int DefaultScreenWidth = 132;  /* The default screen width */
        /// <summary>   Default screen height (rows). The default is 24. </summary>
        protected internal const int DefaultScreenHeight = 24;  /* The default screen height */

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The default server timeout. The Default is 3000. May be overridden in the
        ///             LoginInfo.XML file.
        ///             </summary>
        ///-------------------------------------------------------------------------------------------------

        protected internal const int DefaultServerTimeout = 1000;   /* The default server timeout */
        /// <summary>
        /// Gets or sets the log file specification.
        /// </summary>
        /// <value>The log file specification.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LogFileSpecification


        /// <summary>   The session incrementer. </summary>
        protected internal long logIncrementing = 0L;   /* The log incrementing */
        /// <summary>   The debug incrementing. </summary>
        protected internal long debugIncrementing = 0L; /* The debug incrementing */
        /// <summary>   The record incrementing. </summary>
        protected internal long recordIncrementing = 0L;    /* The record incrementing */
        //   protected internal NLog.LogLevel TelEnvyLogLevel;
        #endregion
        #region Private Methods

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Check coordinates. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

        private int CheckCoords(int column, int row)
        {
            int w = Width;
            int h = Height;

            if (column < 0 || column >= w) { }


            if (row < 0 || row >= h) { }

            return w;
        }

        

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Check region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

        private int CheckRegion(int column, int row, int width, int height)
        {
            int w = CheckCoords(column, row);

            if (width < 0) { }
            //throw Rebex.Messages.CreateArgumentOutOfRangeException("width", width, Rebex.Messages.ArgumentOutOfRange);

            if (height < 0) { }
            //throw Rebex.Messages.CreateArgumentOutOfRangeException("height", height, Rebex.Messages.ArgumentOutOfRange);

            if (column + width > w) { }
            //throw Rebex.Messages.CreateArgumentOutOfRangeException("width", width, "Sum of the column and width refers beyond the screen border.");

            if (row + height > Height) { }
            //throw Rebex.Messages.CreateArgumentOutOfRangeException("height", height, "Sum of the row and height refers beyond the screen border.");

            return w;
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   The region. </returns>
        ///-------------------------------------------------------------------------------------------------

		public TerminalScreenRegion GetRegion(int column, int row, int width, int height)
        {
            int w = CheckRegion(column, row, width, height);
            TerminalScreenRegion region = new TerminalScreenRegion(width, height);
            for (int i = 0; i < height; i++)
            {
                Array.Copy(Cells, (row + i) * w + column, region.Cells, i * width, width);
            }
            return region;
        }



        #endregion
        #region IpPort Events

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ipport1 for on connected events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Ipport connected event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ipport1_OnConnected(object sender, IpportConnectedEventArgs e)
        {
            StaticLogger.LogTrace(ModuleString, string.Format("ipport1_HostName: {0} \t HostPort: {1}", ipport1.RemoteHost, ipport1.RemotePort));
            if (ipport1.Connected)
            {
                StaticLogger.LogTrace(ModuleString, string.Format("StatusCode:{0} - Description {1}", e.StatusCode, e.Description));

            }
            else
            {
                StaticLogger.LogTrace(ModuleString, string.Format("StatusCode:{0} - Description {1}", e.StatusCode, e.Description));

            }
            ipport1.AcceptData = true;
            if ((e.StatusCode == 0) && (e.Description == "OK"))
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;

            }
            ipport1.DoEvents();
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ipport1 for on connection status events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Ipport connection status event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ipport1_OnConnectionStatus(object sender, IpportConnectionStatusEventArgs e)
        {
            StaticLogger.LogTrace(ModuleString, string.Format("ipport1_OnConnectionStatus:{0} - Description {1}", e.StatusCode, e.Description));
            StaticLogger.LogTrace(ModuleString, string.Format("ipport1_HostName: {0} \t HostPort: {1} \tStatusCode {2}", ipport1.RemoteHost, ipport1.RemotePort, e.StatusCode));

        }

        /// <summary>   A Mutex to process. </summary>
        Mutex m = new Mutex();  /* A Mutex to process */
        /// <summary>   True if EOL detected. </summary>
        internal  bool eolDetected = false; /* True if EOL detected */
        /// <summary>   True to EOL data in receiving. </summary>
        internal  bool eolDataInReceiving = false;  /* True to EOL data in receiving */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ipport1 for on data in events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Ipport data in event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ipport1_OnDataIn(object sender, IpportDataInEventArgs e)
        {
            StaticLogger.LogTrace(ModuleString, string.Format("ipport_OnDataIn: Entered {0}\t Received Data from Host: {1}:{2}", e.Text, ipport1.RemoteHost, ipport1.RemotePort));
            string tmpString = string.Empty;
            char[] characters = Encoding.ASCII.GetChars(e.TextB);
            tmpString = characters.TrimControls();
            eolDataInReceiving = true;
            countOfSocketBytes = tmpString.Length;
            StaticLogger.LogTrace(ModuleString, string.Format("ipport1_OnDataIn: Bytes Received {0}", countOfSocketBytes));
            if (string.IsNullOrEmpty(e.Text)) Console.WriteLine("");
            StaticLogger.LogTrace(ModuleString, string.Format("OnDataIn", e.Text));
            StaticLogger.LogTrace(ModuleString, string.Format("OnDataIn:dataReceived:", dataReceived));
            StaticLogger.LogTrace(ModuleString, string.Format("OnDataIn", e.TextB));

            if (_queuedBuffer == null) _queuedBuffer = new BlockingQueue<string>();
            if (terminalMode == TerminalMode.Stream)
            {
                //  if (e.Text.Contains(TerminationString)) // since this is a stream mode session... look for the terminating value.
                //lock (qbLock)
                m.WaitOne();
                    try
                    {
                        StaticLogger.LogInfo(ModuleString, string.Format("*** IpPortIn PreQueued Queued DataIn: {0}", e.Text));
                    _queuedBuffer.Enqueue(e.Text);
                        int cntOfQueuedStrings = _queuedBuffer.Count;
                        // we will queue every time the datain event fires. there is a maximum number of buffer length size used by the control 
                        if (e.EOL)                                  // Check for e.EOL - the control will always return false if the EOL terminator bytes is not in the data. True of the EOL terminator byte is in the data.
                        {
                            for (int i = 0; i < cntOfQueuedStrings; i++)
                            {
                                string qValue;
                                try
                                {
                                    if (bufferReceivedData == null) bufferReceivedData = new StringBuilder();  // initialize the buffer as we will refreshen the buffer with new data
                                    bool queuedBufferTryDequeue = _queuedBuffer.TryDequeue(out qValue);
                                    StaticLogger.LogInfo(ModuleString, string.Format("*** IpPortIn.EOL Queued DataIn: {0}",qValue));
                                    if (queuedBufferTryDequeue)
                                    {
                                        bufferReceivedData.Append(qValue);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    StaticLogger.LogError(ModuleString, ex, string.Format("ReceivedData: Bytes Received {0}", bufferReceivedData.ToString()));
                                }
                            StaticLogger.LogTrace(ModuleString, string.Format("ReceivedDataComplete: Bytes Received {0}", e.Text.Length));
                            eolDataInReceiving = false;
                        }
                        
                        OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true)); // Received the echo back from the host
                        }
                    }
                    catch (Exception ex)
                    {
                        string x = string.Format("%1", ex.ToString());
                         Console.WriteLine(x); 
                    }
                    finally
                    {
                        m.ReleaseMutex();
                    }
              
            }
            if (terminalMode == TerminalMode.Form)
            {
                //  if (e.Text.Contains(TerminationString)) // since this is a stream mode session... look for the terminating value.
                //lock (qbLock)
                m.WaitOne();
                try
                {
                    _queuedBuffer.Enqueue(e.Text);
                    System.Diagnostics.Trace.WriteLine(e.Text);

                    if (e.EOL)                                  // Check for e.EOL - the control will aways return false if the EOL terminator bytes is not in the data. True of the EOL terminator byte is in the data.
                    {
                        for (int i = 0; i < _queuedBuffer.Count; i++)
                        {
                            string qValue;
                            try
                            {

                                if (bufferReceivedData == null) bufferReceivedData = new StringBuilder();  // initialize the buffer as we will refreshen the buffer with new data
                                bool queuedBufferTryDequeue = _queuedBuffer.TryDequeue(out qValue);
                                if (queuedBufferTryDequeue)
                                {
                                    bufferReceivedData.Append(qValue);
                                }
                            }
                            catch (Exception ex)
                            {
                                StaticLogger.LogError(ModuleString, ex, string.Format("ReceivedData: Bytes Received {0}", bufferReceivedData.ToString()));
                            }
                            StaticLogger.LogTrace(ModuleString, string.Format("ReceivedDataComplete: Bytes Received {0}", e.Text.Length));
                            eolDataInReceiving = false;
                            OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true)); // Received the echo back from the host
                        }
                    }

                   

                }
                catch (Exception ex)
                {
                    string x = string.Format("%1", ex.ToString());
                    Console.WriteLine(x);
                }
                finally
                {
                    m.ReleaseMutex();
                }

            }
            // We are not using buffered regions in reporting mode. 

            //if (bufferRegion != null) /* We have a scrollable Region of 80|132 / 24 rows */
            //    bufferRegion.LoadBuffer(bufferReceivedData.ToString());        // we will load a buffer at a time up to setGeometry Bytes


            StaticLogger.LogTrace(ModuleString, string.Format("ReceivedDataChunk: Bytes Received {0}", e.Text.Length));
            if (_queuedBuffer.Count == 0) eolDetected = true;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ipport1 for on disconnected events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Ipport disconnected event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ipport1_OnDisconnected(object sender, IpportDisconnectedEventArgs e)
        {
            StaticLogger.LogTrace(ModuleString, string.Format("ipPort_OnDisconnected: {0}", e.Description));
            
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ipport1 for on error events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Ipport error event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ipport1_OnError(object sender, IpportErrorEventArgs e)
        {

        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by ipport1 for on ready to send events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Ipport ready to send event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ipport1_OnReadyToSend(object sender, IpportReadyToSendEventArgs e)
        {
            // initializeStream();  // initialize the memory stream buffer
        }
        #endregion

        private void telnet1_OnDataIn(object sender, TelnetDataInEventArgs e)
        {
          
        }

        private void telnet1_OnSubOption(object sender, TelnetSubOptionEventArgs e)
        {

        }
    }



}
