
using System;
using System.Linq;

namespace TelEnvyXmlLib.EventArgs
{
    #region Documentation
    /// Additional information for socket events.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for socket events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class SocketEventArgs : System.EventArgs
    {
        /// <summary>   True if connected. </summary>
        bool connected; /* True if connected */

        #region Documentation
        /// Gets or sets a value indicating whether the connected
        ///
        /// \returns    True if connected, false if not.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the connected. </summary>
        ///
        /// <value> True if connected, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Connected
        {
            get { return connected; }
            set
            {
                connected = value;
            }
        }

    }
}
