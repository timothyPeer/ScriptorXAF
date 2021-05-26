
using System;
using System.Linq;

namespace TelEnvyXmlLib.Enums
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Terminal state. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum TerminalState
    {
        /// <summary>Incoming data was just received and processed.</summary>
        DataReceived = 0,   /* An enum constant representing the data received option */

        /// <summary>No incoming data is currently available.</summary>
        NoDataAvailable = 1,    /* An enum constant representing the no data available option */

        /// <summary>The terminal has disconnected.</summary>
        Disconnected = 2,   /* An enum constant representing the disconnected option */
    }
}
