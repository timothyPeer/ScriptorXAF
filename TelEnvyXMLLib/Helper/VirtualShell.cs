// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="VirtualShell.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TelEnvyXmlLib.Helper
{
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A virtual shell. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

	public class VirtualShell
	{
        /// <summary>   The terminal. </summary>
        public Rebex.TerminalEmulation.VirtualTerminal _terminal;   /* The terminal */
        /// <summary>   The received data. </summary>
        private string _receivedData;   /* Information describing the received */
        /// <summary>   The match. </summary>
        protected string _match;    /* Specifies the match */

  
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the terminal. </summary>
        ///
        /// <value> The terminal. </value>
        ///-------------------------------------------------------------------------------------------------

        public Rebex.TerminalEmulation.VirtualTerminal Terminal { get { return _terminal; } }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the timeout. </summary>
        ///
        /// <value> The timeout. </value>
        ///-------------------------------------------------------------------------------------------------

		public int Timeout { get; set; }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets information describing the received. </summary>
        ///
        /// <value> Information describing the received. </value>
        ///-------------------------------------------------------------------------------------------------

		public string ReceivedData { get { return _receivedData; } }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the match. </summary>
        ///
        /// <value> The match. </value>
        ///-------------------------------------------------------------------------------------------------

		public string Match { get { return _match; } }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.VirtualShell class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="terminal"> The terminal.</param>
        ///-------------------------------------------------------------------------------------------------

		public VirtualShell(Rebex.TerminalEmulation.VirtualTerminal terminal)
		{
            _receivedData = String.Empty;
            _terminal = terminal;
			Timeout = 10;
		}

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the given timeout. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

		public string Process()
		{
			return Process(Timeout);
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process the given timeout. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="timeout">  The timeout.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

		public string Process(int timeout)
		{
			_match = null;
			_receivedData = null;
            if (_terminal.Process(timeout) == Rebex.TerminalEmulation.TerminalState.DataReceived)
			{
				_receivedData = _terminal.ReceivedData;
                while (_terminal.Process() == Rebex.TerminalEmulation.TerminalState.DataReceived)
					_receivedData += _terminal.ReceivedData;
			}
			return _receivedData;
		}

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a command. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs.</exception>
        ///
        /// <param name="command">  The command.</param>
        ///-------------------------------------------------------------------------------------------------

		public void SendCommand(string command)
		{
			_match = null;
			_receivedData = null;
			_terminal.SendToServer(command + '\n');
			if (Expect("\n") == null)
				throw new ApplicationException(string.Format("No response of command ('{0}').", command));
		}



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Expects. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="regexPattern"> A pattern specifying the RegEx.</param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

		public string Expect(string regexPattern)
		{
			string output;
			if (Expect(out output, regexPattern) < 0)
				return null;
			else
				return output;
		}

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Expects. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">     Thrown when one or more required arguments are
        ///                                              null.</exception>
        /// <exception cref="ArgumentException">         Thrown when one or more arguments have
        ///                                              unsupported or illegal values.</exception>
        /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid.</exception>
        ///
        /// <param name="output">           [out] The output.</param>
        /// <param name="regexPatterns"> A variable-length parameters list containing RegEx patterns.</param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

		public int Expect(out string output, params string[] regexPatterns)
		{
			if (regexPatterns == null)
				throw new ArgumentNullException("regexPatterns");

			if (regexPatterns.Length == 0)
				throw new ArgumentException("Array of expecting patterns cannot be empty.", "regexPatterns");

			// combine all patterns together and name them
			string pattern = string.Format("(?<p0>{0})", regexPatterns[0]);
			for (int i = 1; i < regexPatterns.Length; i++)
			{
				pattern += string.Format("|(?<p{0}>{1})", i, regexPatterns[i]);
			}

			// wait for any pattern (but no longer than Timeout)
			Match m = _terminal.Expect(new Regex(pattern), Timeout);

			_receivedData = _terminal.ReceivedData;

			// if no pattern found return 0
			if (!m.Success)
			{
				output = _receivedData;
				_match = string.Empty;
				return -1;
			}

			// save the received output and match
			output = _receivedData.Substring(0, m.Index);
			_match = _receivedData.Substring(m.Index);

			// determinate which pattern was found
			for (int i = 0; i < regexPatterns.Length; i++)
			{
				if (m.Groups["p" + i].Success)
					return i;
			}

			// this should never arise
			throw new InvalidOperationException("Unexpected pattern found.");
		}
	}
}