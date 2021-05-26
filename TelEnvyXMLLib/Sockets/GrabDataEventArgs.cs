
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelEnvyXmlLib.Common;

namespace TelEnvyXmlLib.Sockets
{


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for grab data events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class GrabDataEventArgs : System.EventArgs
    {
     

      ///-------------------------------------------------------------------------------------------------
      /// <summary> Gets or sets the state. </summary>
      ///
      /// <value>   The state. </value>
      ///-------------------------------------------------------------------------------------------------

      public   StateObject State { set; get; }
    }
}
