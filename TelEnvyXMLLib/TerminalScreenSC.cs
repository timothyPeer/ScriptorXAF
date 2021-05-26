
using nsoftware.IPWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TelEnvyXmlLib.Directives;
using TelEnvyXmlLib.Enums;
using TelEnvyXmlLib.Exceptions;
using TelEnvyXmlLib.ExtensionMethods;
using System.Reflection;
using TelEnvyXmlLib.Sockets;
using System.Net.Sockets;
using TelEnvyXmlLib.EventArgs;

namespace TelEnvyXmlLib
{
   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A terminal screen screen. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Sockets.SocketClient"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class TerminalScreenSC : SocketClient
    {
       
        /// <copyright file="TerminalScreenSC.cs" company="eNVy Systems, Inc.">
        /// Copyright (c) 2019 eNVy Systems, Inc.. All rights reserved.
        /// </copyright>
        /// <author>Timothy Peer, eNVy Systems Inc.</author>
        /// <date>6/26/2019</date>
        /// <summary>Implements the terminal screen screen class</summary>
        

        /// <summary>   True to released for processing. </summary>
        private bool ReleasedForProcessing = true;  /* True to released for processing */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the transport. </summary>
        ///
        /// <value> The transport. </value>
        ///-------------------------------------------------------------------------------------------------

        public TelTransportLib Transport { get; set; }
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
        /// <summary>   Finalizes an instance of the TelEnvyXmlLib.TerminalScreenSC class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        ~TerminalScreenSC()
        {
            base.ShutdownClient();
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets module log name. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="modString">    The modifier string.</param>
        ///-------------------------------------------------------------------------------------------------

        public new void SetModuleLogName(string modString)
        {
            base.SetModuleLogName(modString);
            //ModuleLogName = modString;
            StaticLogger.LogTrace(base.ModuleLogName, "TerminalScreen.SetModuleLogName: {0}");
        }
     //   public string ModuleLogName { get;  set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Occurs when [release for processing]. This event is used to synchronize inter-
        ///             systems conversations
        ///              send [wait] receive [wait] send [wait]... etc.
        ///             </summary>
        ///-------------------------------------------------------------------------------------------------

        public event EventHandler<ReleaseForProcessingEventArgs> ReleaseForProcessing;  /* Occurs when Release For Processing. */
        #region OnReleaseForProcessing


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
        #endregion

     
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TerminalScreenSC class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TerminalScreenSC()
        {
            InitializeComponent();
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
            this.ReleaseForProcessing += TerminalScreenSC_ReleaseForProcessing; 


        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by TerminalScreenSC for release for processing events.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Release for processing event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void TerminalScreenSC_ReleaseForProcessing(object sender, ReleaseForProcessingEventArgs e)
        {
             ReleasedForProcessing = e.ReadyToSend;
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.TerminalScreenSC class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="container">    The container.</param>
        ///-------------------------------------------------------------------------------------------------

        public TerminalScreenSC(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.ReleaseForProcessing += TerminalScreenSC_ReleaseForProcessing;
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
            return WaitForData(regexPattern.ToString(), Columns, Rows, 0, 0);
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
            return true;
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
           // _receivedData = ReceivedData;
            // ReceivedData = string.Empty; // is not necessary is the polled data are retrieved from a Queue<string>

            Regex expression = new Regex(pattern);

            // test if the pattern is already on the screen
            if (TestRegion(expression, column, row, width, height))
                return true;

            int timeout = maxWaitTime;
            int start = Environment.TickCount;
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
            return false;
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
            bool result = WaitForCursor(column, row);
            Save();
            if (!result)
                throw new TelWaitMissingCoordinateException(string.Format("WaitForCursor Column Row {0} Column {1}, receive buffer:\n{2}", row, column, bufferReceivedData));
            return result;
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
            bool result = WaitForDataOnRow(pattern, row);
            Save();
            if (!result)
                throw new TeLWaitDataRegionTimeOutException(string.Format("WaitForData {0}, receive buffer:\n{1}", pattern, bufferReceivedData));
            return result;
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
            bool result = WaitForData(pattern, column, row, width, height);
            Save();
            if (!result)
                throw new TeLWaitDataRegionTimeOutException(string.Format("WaitForData {0}, receive buffer:\n{1}", pattern, bufferReceivedData));
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
                StaticLogger.LogTrace(ModuleLogName, string.Format("TestData:TestRegion-1:{0}-ReceivedData:{2}-Expression:{1}", item, bufferReceivedData.ToString(), expression.ToString()));
              
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (expression.IsMatch(lines[i]))
                {
                    StaticLogger.LogTrace(ModuleLogName, string.Format("TestData:TestRegion-2:{0}-ReceivedData:{2}-Expression:{1}", "true", bufferReceivedData.ToString(), expression.ToString()));
                    return true;
                }
            }
            foreach (var item in lines)
            {
                StaticLogger.LogTrace(ModuleLogName, string.Format("TestData:TestRegion-3:{0}-ReceivedData:{2}-Expression:{1}", item, bufferReceivedData.ToString(), expression.ToString()));
              
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
                    StaticLogger.LogTrace(ModuleLogName, string.Format("TestData[regex]-Do Not Use... Method To Do:"));
                    Thread.Sleep(10);
                } while (1==1);
            
            }
     
            Match m = regex.Match(bufferReceivedData.ToString());
            if (m.Success)
            {
                StaticLogger.LogTrace(ModuleLogName, string.Format("TestData:TestRegion-1:{0}-ReceivedData:{2}-Expression:{1}", m.ToString(), bufferReceivedData.ToString(), regex.ToString()));
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
            SocketClient bClient = getSocketClient();
            if (_receivedData == null)
                _receivedData = string.Empty;
          
            StaticLogger.LogTrace(ModuleLogName, string.Format("TestRegion[regex]:{0}-ReceivedData:{2}-Expression:{1}", bClient.Connected, bufferReceivedData.ToString(), expression.ToString()));
            Match m = expression.Match(_receivedData);
            if (m.Success)
            {
                selectedDataBuffer = _receivedData.Substring(m.Index + m.Length);
                _receivedData = _receivedData.Substring(0, m.Index + m.Length);
                return true;
            }
            return false;
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves this TerminalScreenSC. </summary>
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
            StaticLogger.LogTrace(ModuleLogName,string.Format(ModuleLogName, "SendEnter: {0}", sData));
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
            StaticLogger.LogTrace(ModuleLogName, string.Format(ModuleLogName, "SendData: {0}", cmdText));
            //   bWaitForData = true;
            {
                SocketClient bClient = getSocketClient();
                if (bClient != null)
                    StaticLogger.LogTrace(ModuleLogName, string.Format(ModuleLogName, "Socket (WinSock) Connection Status: {0}", bClient.Connected));
                
                DateTime startTime = DateTime.Now;
                DateTime endTime = DateTime.Now;
                TimeSpan duration = new TimeSpan(endTime.Ticks - startTime.Ticks);
                if (!IsConnected)
                {
                    while (0 == 0)
                    {
                        if (bClient.Connected) break;     // The connection must be set
                        IsConnected = true;
                    }
                }
                if (!bClient.Connected) throw new TelHostLoginNotPossibleException("SendData:-The host is not connected.");
                endTime = DateTime.Now;
                try
                {
                    byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(cmdText.ToArray());
                    OnReleaseForProcessing(new ReleaseForProcessingEventArgs(false));       // ensure we block the next sequence until unblocked by the datain event
                    if (bClient != null)
                    {
                        if (bClient.Connected)
                        {
                            StaticLogger.LogTrace(ModuleLogName, string.Format("Socket (WinSock) Sending Bytes: {0}", bytes.Length));
                            bClient.Send(bytes);
                            int x = bClient.BytesSent;
                        }
                    }

                   
                    dataReceived = cmdText;
                    StaticLogger.LogTrace(ModuleLogName, string.Format("Socket (WinSock) Sent Bytes: {0}", bytes));
                }
                catch (Exception ex)
                {
                    StaticLogger.LogError(ModuleLogName, ex,string.Format("Socket Error Occurred:{0}\n{1}",ex.Message,ex.InnerException.Message));
                }
                finally
                {

                }
                Save();
            }
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
                        StaticLogger.LogTrace(ModuleLogName, string.Format("Invalid-SendF Key Sequence: {0}", number));
                        throw new Exceptions.TeLSpecialKeyNotSupportedException(string.Format("F{0} is unsupported.", number));
                    }
            }
            StaticLogger.LogTrace(ModuleLogName, string.Format("SendF Key Sequence: {0}",sequence));
            SendData(Csi + sequence);
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
                        StaticLogger.LogTrace(ModuleLogName, string.Format("Invalid-SendF Key Sequence: {0}", number));
                        throw new TeLSpecialKeyNotSupportedException(string.Format("PF{0} is unsupported.", number));
                    }
            }
            StaticLogger.LogTrace(ModuleLogName, string.Format("SendPF Key Sequence: {0}", sequence));
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
                StaticLogger.LogTrace(ModuleLogName, string.Format("Expect Terminal Mode = Stream: {0}", pattern));
                return ReceivedData;
            }
            // the terminal mode must a Form. so we need to map the coordinates to a virtual screen given 
            // predefined screen page and width properties
            lock (qbLock)
            {
                StaticLogger.LogTrace(ModuleLogName, string.Format("Expect Terminal Mode = Form: {0}", pattern));
                _receivedData = ReceivedData;
            }
            bool result = TestData(new Regex(pattern)); // Expect(pattern, timeout );
            Save();

            return _receivedData;
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
        /// <summary>   Sets using forms. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="bIsUsed">  True if is used, false if not.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setUsingForms(bool bIsUsed)
        {
            usingForms = bIsUsed;
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the instance. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   The instance. </returns>
        ///-------------------------------------------------------------------------------------------------

        public SocketClient getInstance()
        {
            return base.getSocketClient();
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Connects an and login. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="gLogin">   The login.</param>
        ///-------------------------------------------------------------------------------------------------

        public void ConnectAndLogin(LoginPair gLogin)
        {
            SocketClient bClient = getSocketClient();
            try
            {
                //telnet1 = new Telnet(this.components);
               
                    if (!bClient.Connected)
                {
                    bClient.Disconnect();

                    bClient.setHostName(ServerName); //  RemotePort = ServerPort;
                    bClient.setHostPort(ServerPort);
                    //bClient.RemoteHost = ServerName;
                    bClient.Connected = true; // (ServerName);
                }
                else
                {
                    bClient.Connected = false;
                }
            }
            catch (Exception ex)
            {
                bClient.Connected = false;
                bClient.AcceptData = false;
                StaticLogger.LogError(ModuleLogName, ex, string.Format("ConnectAndLogin.ReceivedData: {0}", bufferReceivedData.ToString()));
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
                using (Mutex m = new Mutex())
                {
                    try
                    {
                        m.WaitOne();
                        {
                            SocketClient bClient = getSocketClient();
                            if (bClient.Connected)
                            {
                                bClient.Receive();
                                while (bClient.WaitingForData)
                                {
                                    Thread.Sleep(500);
                                }
                                if (TransactionDictionary != null)
                                {
                                    if (TransactionDictionary.ContainsKey(TransactionKey))
                                    {
                                            return TransactionDictionary[TransactionKey];
                                    }
                                }
                                return string.Empty;
                                
                            }
                        }
                    }

                    finally
                    {
                        m.ReleaseMutex();
                    }
                }
                return string.Empty;
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
            SocketClient bClient = getSocketClient();
            try
            {
                
                bClient.Connected = true;
            }
            catch (Exception ex)
            {
                string x = MethodBase.GetCurrentMethod().Name;
                throw new TelHostLoginNotPossibleException("SetConnectionToSocket-Not Connected Exception.",ex);
            }
            finally
            {
                
            }
        }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TerminalScreenSC is connected. </summary>
        ///
        /// <value> True if this TerminalScreenSC is connected, false if not. </value>
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
        /// <summary>   Gets a value indicating whether this TerminalScreenSC is canceled. </summary>
        ///
        /// <value> True if this TerminalScreenSC is canceled, false if not. </value>
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
        /// <summary>   Gets or sets a value indicating whether this TerminalScreenSC is faulted. </summary>
        ///
        /// <value> True if this TerminalScreenSC is faulted, false if not. </value>
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

        public void SetServerName(string v)
        {
            SocketClient bClient = getSocketClient();
            ServerName = v;
            StaticLogger.LogTrace(ModuleLogName, string.Format("setServerName", v));
            bClient.setHostName(v);
            

        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets transaction key. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="transactionKey">   The transaction key.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SetTransactionKey(string transactionKey)
        {
            TransactionKey = transactionKey;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets server port. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="vPort">    The port.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SetServerPort(int vPort)
        {
            SocketClient bClient = getSocketClient();
            ServerPort = vPort;
            StaticLogger.LogTrace(ModuleLogName, string.Format("setServerPort", vPort));
            bClient.setHostPort(vPort);
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
            //try
            //{
            //    if (e.OptionCode == 38)
            //        telnet1.DontOption = 38;
            //}
            //catch (IPWorksException)
            //{
            //}
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
            StaticLogger.LogDebug(ModuleLogName, string.Format("telnet1_OnDisconnected: {0}",e.Description));
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the data received. </summary>
        ///
        /// <value> The data received. </value>
        ///-------------------------------------------------------------------------------------------------

        public string dataReceived { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Event handler. Called by telnet1 for on data in events. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Telnet data in event information.</param>
        ///-------------------------------------------------------------------------------------------------

        private void telnet1_OnDataIn(object sender, TelnetDataInEventArgs e)
        {
            StaticLogger.LogTrace(ModuleLogName, string.Format("telnet1_OnDataIn: Entered {0}\n", e.Text));
            {
                string tmpString = string.Empty;
                //char[] characters = Encoding.ASCII.GetChars(e.TextB);
                tmpString = e.Text;
                    _queuedBuffer.Enqueue(tmpString);
                countOfSocketBytes = tmpString.Length;
                StaticLogger.LogTrace(ModuleLogName, string.Format("telnet1_OnDataIn: Bytes Received {0}\n", countOfSocketBytes));
            }
            if (dataReceived == e.Text)
                OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true)); // Received the echo back from the host
            else
            {
                if (e.Text.Contains(TerminationString))
                    {
                    OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true)); // Received the complete report
                }
            }
            if (!e.Text.Contains("END OF RESPONSE"))
            {
                dataReceived = e.Text;
            }
            
        }



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
           StaticLogger.LogTrace(ModuleLogName, string.Format("OnConnectionStatus -: {0}",e.Description));
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
            SocketClient bClient = getSocketClient();
            if (bClient.Connected)
                StaticLogger.LogTrace(ModuleLogName, string.Format("StatusCode:{0} - Description {1}", e.StatusCode, e.Description));


            bClient.AcceptData = true;
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


        //private void ipport1_OnConnected(object sender, IpportConnectedEventArgs e)
        //{
        //    SocketClient bClient = getSocketClient();
        //    if (bClient.Connected)
        //         StaticLogger.LogTrace(ModuleLogName, string.Format("StatusCode:{0} - Description {1}", e.StatusCode, e.Description));

        //    bClient.AcceptData = true;
        //    if ((e.StatusCode == 0) && (e.Description == "OK"))
        //    {
        //        IsConnected = true;
        //    }
        //    else
        //    {
        //        IsConnected = false;

        //    }
        //}

        //private void ipport1_OnConnectionStatus(object sender, IpportConnectionStatusEventArgs e)
        //{
        //    //Console.WriteLine(e.Description);
        //    StaticLogger.LogTrace(ModuleLogName, string.Format("ipport1_OnConnectionStatus:{0} - Description {1}", e.StatusCode, e.Description));
        //}

        //private void ipport1_OnDataIn(object sender, IpportDataInEventArgs e)
        //{
        //    StaticLogger.LogTrace(ModuleLogName, string.Format("telnet1_OnDataIn: Entered {0}\n", e.Text));
        //   
        //    {
        //        string tmpString = string.Empty;
        //        char[] characters = Encoding.ASCII.GetChars(e.TextB);
        //        tmpString = characters.TrimControls();
        //        _queuedBuffer.Enqueue(e.Text);
        //        countOfSocketBytes = tmpString.Length;
        //        StaticLogger.LogTrace(ModuleLogName, string.Format("telnet1_OnDataIn: Bytes Received {0}\n", countOfSocketBytes));
        //    }
        //    StaticLogger.LogTrace(ModuleLogName, string.Format("OnDataIn", e.Text));
        //    StaticLogger.LogTrace(ModuleLogName, string.Format("OnDataIn:dataReceived:", dataReceived));
        //    StaticLogger.LogTrace(ModuleLogName, string.Format("OnDataIn", e.TextB));
        //    if (terminalMode == TerminalMode.Stream)
        //    {
        //        if (e.Text.Contains(TerminationString)) // since this is a stream mode session... look for the terminating value.
        //        {
        //            lock (qbLock)
        //            {
        //                bufferReceivedData = new StringBuilder();  // initialize the buffer as we will refreshen the buffer with new data
        //                int cntOfQueuedStrings = _queuedBuffer.Count;
        //                for (int i = 0; i < cntOfQueuedStrings; i++)
        //                {
        //                    string qValue;
        //                    try
        //                    {
        //                        bool queuedBufferTryDequeue = _queuedBuffer.TryDequeue(out qValue);
        //                        if (queuedBufferTryDequeue)
        //                        {
        //                            bufferReceivedData.Append(qValue);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        StaticLogger.LogError(ModuleLogName,ex,string.Format("ReceivedData: Bytes Received {0}\n", bufferReceivedData.ToString()));
        //                    }
        //                }
        //                // We are not using buffered regions in reporting mode. 

        //                //if (bufferRegion != null) /* We have a scrollable Region of 80|132 / 24 rows */
        //                //    bufferRegion.LoadBuffer(bufferReceivedData.ToString());        // we will load a buffer at a time up to setGeometry Bytes
        //             
        //                StaticLogger.LogTrace(ModuleLogName,string.Format("ReceivedData: Bytes Received {0}\n", bufferReceivedData.ToString()));
        //                OnReleaseForProcessing(new ReleaseForProcessingEventArgs(true)); // Received the echo back from the host
        //            }

        //        }
        //    }
        //    

        //}

     
        #endregion
    }



}
