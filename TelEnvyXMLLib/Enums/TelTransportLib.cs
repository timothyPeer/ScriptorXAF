
using System;
using System.Linq;

namespace TelEnvyXmlLib.Enums
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Values that represent tel transport Libraries. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum TelTransportLib
    {
        /// <summary>   An enum constant representing the none option. </summary>
        None,   /* An enum constant representing the none option */

        /// <summary>   An enum constant representing the rebex option. </summary>
        Rebex,  /* An enum constant representing the rebex option */

        /// <summary>   An enum constant representing the IP works option. </summary>
        IpWorks,    /* An enum constant representing the IP works option */

        /// <summary>   An enum constant representing the socket client option. </summary>
        SocketClient    /* An enum constant representing the socket client option */
    }
}
