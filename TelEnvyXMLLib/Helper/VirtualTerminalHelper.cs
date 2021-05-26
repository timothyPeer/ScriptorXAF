// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="VirtualTerminalHelper.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text;
using System.Text.RegularExpressions;
using Rebex;
using Rebex.TerminalEmulation;
using TelEnvyXmlLib.EventArgs;
using TelEnvyXmlLib.Enums;


namespace TelEnvyXmlLib.Helper
{
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A virtual terminal helper. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

	public  class VirtualTerminalHelper
	{
        /// <summary>   The terminal. </summary>
        private VirtualTerminal _terminal;  /* The terminal */
        /// <summary>   The received buffer. </summary>
        private string _receivedBuffer; /* Buffer for received data */
        /// <summary>   The received data. </summary>
        private string _receivedData;   /* Information describing the received */
        /// <summary>   Name of the module. </summary>
        private string _moduleName; /* Name of the module */

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the logging level. </summary>
        ///
        /// <value> The logging level. </value>
        ///-------------------------------------------------------------------------------------------------

		Rebex.LogLevel _LoggingLevel { get; set; }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Callback, called when the set vt. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="terminal"> The terminal.</param>
        ///-------------------------------------------------------------------------------------------------

        delegate void SetVTCallback(VirtualTerminal terminal);

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the writing to log action. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        A TelLogWriterEventArg to process.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnWritingToLog(object sender, TelLogWriterEventArg e)
        {
            EventHandler<TelLogWriterEventArg> handler = WritingToLog;
            if (handler != null)
                handler(sender, e);
        }
        /// <summary>   Occurs when [writing to log]. </summary>
        public event  EventHandler<TelLogWriterEventArg> WritingToLog;  /* Occurs when Writing To Log. */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the terminal. </summary>
        ///
        /// <value> The terminal. </value>
        ///-------------------------------------------------------------------------------------------------

		public VirtualTerminal Terminal { get { return _terminal; } }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the send data wait for data timeout. </summary>
        ///
        /// <value> The send data wait for data timeout. </value>
        ///-------------------------------------------------------------------------------------------------

		public int sendDataWaitForDataTimeout { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the receive data wait for data timeout. </summary>
        ///
        /// <value> The receive data wait for data timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int receiveDataWaitForDataTimeout { get; set; }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets information describing the received. </summary>
        ///
        /// <value> Information describing the received. </value>
        ///-------------------------------------------------------------------------------------------------

		public string ReceivedData { get { return _receivedData; } }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.VirtualTerminalHelper
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="terminal"> The terminal.</param>
        ///-------------------------------------------------------------------------------------------------

        public VirtualTerminalHelper(VirtualTerminal terminal)
           
		{
			_terminal = terminal;
            //if (sendDataWaitForDataTimeout == 0)// Permit the application default to persist if necessary
            //    sendDataWaitForDataTimeout = (Settings1.Default.SendDataWaitForDataTimeOut != 0) ? Settings1.Default.SendDataWaitForDataTimeOut : 1000;
            //if (receiveDataWaitForDataTimeout == 0) // Permit the application default to persist if necessary
            //    receiveDataWaitForDataTimeout = (Settings1.Default.ReceiveDataWaitForDataTimeOut != 0) ? Settings1.Default.ReceiveDataWaitForDataTimeOut : 1000;

            _LoggingLevel = Rebex.LogLevel.Off;
            _terminal.Options.AutoWrapMode = AutoWrapMode.Off;
		}

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging verbose. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void SetLoggingVerbose()
		{
			_LoggingLevel = Rebex.LogLevel.Verbose;
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging debug. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void SetLoggingDebug()
		{
			_LoggingLevel = Rebex.LogLevel.Debug;
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging information. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

		public void SetLoggingInfo()
		{
			_LoggingLevel = Rebex.LogLevel.Info;
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging error. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

		public void SetLoggingError()
		{
			_LoggingLevel = Rebex.LogLevel.Error;
		}

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets logging off. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

		public void SetLoggingOff()
		{
			_LoggingLevel = Rebex.LogLevel.Off;
		}

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the given timeOut. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

		public void Process()
		{
			Process(sendDataWaitForDataTimeout);
		}

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the given timeOut. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="MaximumWaitTime">  The maximum time of the wait.</param>
        ///-------------------------------------------------------------------------------------------------

		public void Process(int MaximumWaitTime)
		{
            while (_terminal.Process(MaximumWaitTime) == Rebex.TerminalEmulation.TerminalState.DataReceived)
            {
                //_receivedBuffer += _terminal.ReceivedData;
                var logData = new TelLogWriterEventArg(string.Format("Process: {0}", _terminal.ReceivedData));
                OnWritingToLog(this, logData);
            }
		}

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the given timeOut. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="timeOut">  The time out.</param>
        ///-------------------------------------------------------------------------------------------------

        public void Process(UseDataWaitForDataTimeout timeOut)
        {
            switch (timeOut)
            {
                case UseDataWaitForDataTimeout.Send:
                    {
                        while (_terminal.Process(sendDataWaitForDataTimeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
                        {
                            _receivedBuffer += _terminal.ReceivedData;
                            var logData = new TelLogWriterEventArg(string.Format("Process: {0}", _terminal.ReceivedData));
                            OnWritingToLog(this, logData);
                        }
                    }
                    break;
                case UseDataWaitForDataTimeout.Receive:
                    {
                        while (_terminal.Process(receiveDataWaitForDataTimeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
                        {
                            _receivedBuffer += _terminal.ReceivedData;
                            var logData = new TelLogWriterEventArg(string.Format("Process: {0}", _terminal.ReceivedData));
                            OnWritingToLog(this, logData);
                        }
                    }
                    break;

            } 
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends to server. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="text"> The text.</param>
        ///-------------------------------------------------------------------------------------------------

		public void SendToServer(string text)
		{
            StaticLogger.LogTrace(_moduleName, string.Format("SendToServer : {0} with SendDataWaitForDataTimeOut: {1}", text,sendDataWaitForDataTimeout));
            _receivedBuffer = _receivedData = null;

            //if (this._terminal.be)
			_terminal.SendToServer(text);

            // process as much data as possible
            while (_terminal.Process(sendDataWaitForDataTimeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
            {
                
                _receivedBuffer += _terminal.ReceivedData;
                StaticLogger.LogTrace(_moduleName, string.Format("SendToServer : Received -: {0}", _terminal.ReceivedData));
            }
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Expects. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool Expect(string regexPattern)
		{
			return Expect(regexPattern, receiveDataWaitForDataTimeout);
		}

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets log module name. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="modName">  Name of the modifier.</param>
        ///-------------------------------------------------------------------------------------------------

        public void  SetLogModuleName(string modName)
        {
            _moduleName = modName;
        }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Expects. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        /// <param name="maxWaitTime">  The maximum time of the wait.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool Expect(string regexPattern, int maxWaitTime)
		{
			_receivedData = _receivedBuffer;
			_receivedBuffer = null;

			Regex expression = new Regex(regexPattern);

			if (TestData(expression))
				return true;

			int timeout = maxWaitTime;
			int start = Environment.TickCount;
			while (_terminal.Process(timeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
			{
				_receivedData += _terminal.ReceivedData;
                StaticLogger.LogTrace(_moduleName, string.Format("Expect : {0}", _terminal.ReceivedData));

                // process as much data as possible
                while (_terminal.Process(receiveDataWaitForDataTimeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
                {
                    _receivedData += _terminal.ReceivedData;
                    StaticLogger.LogTrace(_moduleName, string.Format("l-Expect : {0}", _terminal.ReceivedData));
                }

				if (TestData(expression))
                {
                    StaticLogger.LogTrace(_moduleName, string.Format("TestData : Expression Found {0}", expression));
                    return true;
                }
					

				timeout = maxWaitTime - (Environment.TickCount - start);
				if (timeout < 0)
                {
                    StaticLogger.LogTrace(_moduleName, string.Format("TestData : TimeOut of {0} Exceeded MaxWaitTime of {1}", timeout, maxWaitTime));
                    break;
                }
					
			}
			_receivedBuffer = _receivedData;
			return false;
		}

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tests data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="expression">   The expression.</param>
        ///
        /// <returns>   True if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		private bool TestData(Regex expression)
		{
            if (_receivedData == null)
            {
                _receivedData = string.Empty;
                StaticLogger.LogTrace(_moduleName, string.Format("TestData : Regex - _receivedData is null or empty." ));
            }

            Match m = expression.Match(_receivedData);
			if (m.Success)
			{
				_receivedBuffer = _receivedData.Substring(m.Index + m.Length);
				_receivedData = _receivedData.Substring(0, m.Index + m.Length);
                StaticLogger.LogTrace(_moduleName, string.Format("TestData : Regex Match Success New Buffer - {0}",_receivedData));
                return true;
			}
			return false;
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data on row. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        /// <param name="row">          The row.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForDataOnRow(string regexPattern, int row)
		{
			return WaitForDataOnRow(regexPattern, row, receiveDataWaitForDataTimeout);
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data on row. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        /// <param name="row">          The row.</param>
        /// <param name="maxWaitTime">  The maximum time of the wait.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForDataOnRow(string regexPattern, int row, int maxWaitTime)
		{
			return WaitForData(regexPattern, 0, row, _terminal.Screen.Columns, 1, maxWaitTime);
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForData(string regexPattern)
		{
			return WaitForData(regexPattern, receiveDataWaitForDataTimeout);
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

		public bool WaitForData(string regexPattern, int maxWaitTime)
        {
            return WaitForData(regexPattern, 0, 0, _terminal.Screen.Columns, _terminal.Screen.Rows, maxWaitTime);
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        /// <param name="column">       The column.</param>
        /// <param name="row">          The row.</param>
        /// <param name="width">        The width.</param>
        /// <param name="height">       The height.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForData(string regexPattern, int column, int row, int width, int height)
		{
			return WaitForData(regexPattern, column, row, width, height, receiveDataWaitForDataTimeout);
		}

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for data. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        /// <param name="column">       The column.</param>
        /// <param name="row">          The row.</param>
        /// <param name="width">        The width.</param>
        /// <param name="height">       The height.</param>
        /// <param name="maxWaitTime">  The maximum time of the wait.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForData(string regexPattern, int column, int row, int width, int height, int maxWaitTime)
		{
			_receivedData = _receivedBuffer;
			_receivedBuffer = null;

			Regex expression = new Regex(regexPattern);

			// test if the pattern is already on the screen
			if (TestRegion(expression, column, row, width, height))
				return true;

			int timeout = maxWaitTime;
			int start = Environment.TickCount;
            TelLogWriterEventArg lData = null;

            while (_terminal.Process(timeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
			{
				_receivedData += _terminal.ReceivedData;
                lData  = new TelLogWriterEventArg(string.Format("WaitForData: {0}",_receivedData));
                OnWritingToLog(this, lData);

                // process as much data as possible
                while (_terminal.Process(1) == Rebex.TerminalEmulation.TerminalState.DataReceived)
                {
                    _receivedData += _terminal.ReceivedData;
                    lData.LogData = _receivedData;
                    OnWritingToLog(this, lData);
                }

                if (TestRegion(expression, column, row, width, height))
					return true;

				timeout = maxWaitTime - (Environment.TickCount - start);
				if (timeout < 0)
					break;
			}
			_receivedBuffer = _receivedData;
			return false;
		}

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tests region. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="pattern">  Specifies the pattern.</param>
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        /// <param name="width">    The width.</param>
        /// <param name="height">   The height.</param>
        ///
        /// <returns>   True if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool TestRegion(string pattern, int column, int row, int width, int height)
		{
			return TestRegion(new Regex(pattern), column, row, width, height);
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

		private bool TestRegion(Regex expression, int column, int row, int width, int height)
		{
			string[] lines = _terminal.Screen.GetRegionText(column, row, width, height);
            foreach (var item in lines)
            {
                TelLogWriterEventArg lData = new TelLogWriterEventArg(String.Format("TestRegion {0}",item));
                OnWritingToLog(this, lData);
            }
            
            for (int i = 0; i < lines.Length; i++)
			{
                if (expression.IsMatch(lines[i]))
                {
                    TelLogWriterEventArg lData = new TelLogWriterEventArg(String.Format("TestRegion Matched : {0}", lines[i]));
                    OnWritingToLog(this, lData);
                    return true;
                }
			}
            foreach (var item in lines)
            {
                TelLogWriterEventArg lData = new TelLogWriterEventArg(String.Format("TestRegion No Match : {0}", item));
                OnWritingToLog(this, lData);
            }
            return false;
		}

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for cursor. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="column">   The column.</param>
        /// <param name="row">      The row.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForCursor(int column, int row)
		{
			return WaitForCursor(column, row, receiveDataWaitForDataTimeout);
		}

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Wait for cursor. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="column">       The column.</param>
        /// <param name="row">          The row.</param>
        /// <param name="maxWaitTime">  The maximum time of the wait.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

		public bool WaitForCursor(int column, int row, int maxWaitTime)
		{
			_receivedData = _receivedBuffer;
			_receivedBuffer = null;

			if (_terminal.Screen.CursorLeft == column && _terminal.Screen.CursorTop == row)
				return true;

			int timeout = maxWaitTime;
			int start = Environment.TickCount;
			while (_terminal.Process(timeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
			{
				_receivedData += _terminal.ReceivedData;

                // process as much data as possible
                while (_terminal.Process(1) == Rebex.TerminalEmulation.TerminalState.DataReceived)
                {
                    
                    _receivedData += _terminal.ReceivedData;
                    TelLogWriterEventArg lData = new TelLogWriterEventArg(string.Format("WaitForCursor : {0}", _terminal.ReceivedData));
                    OnWritingToLog(this, lData);
                }

				if (_terminal.Screen.CursorLeft == column && _terminal.Screen.CursorTop == row)
					return true;

				timeout = maxWaitTime - (Environment.TickCount - start);
				if (timeout < 0)
					break;
			}
			_receivedBuffer = _receivedData;
			return false;
		}
	}
}
