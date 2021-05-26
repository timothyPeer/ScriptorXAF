
using System;
using System.Linq;

namespace TelEnvyXmlLib
{
 

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for release for processing events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ReleaseForProcessingEventArgs : System.EventArgs
    {
     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the ready to send. </summary>
        ///
        /// <value> True if ready to send, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool ReadyToSend { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.ReleaseForProcessingEventArgs
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public ReleaseForProcessingEventArgs()
        {

        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.ReleaseForProcessingEventArgs
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="v">    True to v.</param>
        ///-------------------------------------------------------------------------------------------------

        public ReleaseForProcessingEventArgs(bool v)
        {
            ReadyToSend = v;
        }
    }
}
