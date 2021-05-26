
using System;
using System.Linq;

namespace TelEnvyXmlLib.Enums
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Values that represent socket connect status. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum socketConnectStatus
    {
        /// <summary>   An enum constant representing the connected option. </summary>
        Connected,  /* An enum constant representing the connected option */

        /// <summary>   An enum constant representing the disconnected option. </summary>
        Disconnected,   /* An enum constant representing the disconnected option */

        /// <summary>   An enum constant representing the waiting option. </summary>
        Waiting /* An enum constant representing the waiting option */
    }
}
