

using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TelEnvyXmlLib.Common
{
    #region Documentation
    /// State object for receiving data from remote device.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A state object. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class StateObject
    {
        /// <summary>   Client socket. </summary>
        public Socket workSocket = null;    /* The work socket */
        /// <summary>   Size of receive buffer. </summary>
        int bufferSize; /* Size of the buffer */

        #region Documentation
        /// Gets or sets the buffer size
        ///
        /// \returns    The size of the buffer.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the buffer size. </summary>
        ///
        /// <value> The size of the buffer. </value>
        ///-------------------------------------------------------------------------------------------------

        public int BufferSize
        {
            get { return bufferSize; }
            private set
            {
                bufferSize = value;
            }
        }
        /// <summary>   The transaction key. </summary>
        string transactionKey;  /* The transaction key */

        #region Documentation
        /// Gets or sets the transaction key
        ///
        /// \returns    The transaction key.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the transaction key. </summary>
        ///
        /// <value> The transaction key. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TransactionKey
        {
            get { return transactionKey; }
            set
            {
                transactionKey = value;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   public const int BufferSize = 10;
        ///              Receive buffer.
        ///             </summary>
        ///-------------------------------------------------------------------------------------------------

        public byte[] buffer = null;    /* The buffer */
        /// <summary>   Received data string. </summary>
        public StringBuilder sb = new StringBuilder();  /* The sb */

        #region Documentation
        /// Sets receive buffer size
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  bufferSize  Size of the buffer.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets receive buffer size. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="bufferSize">   Size of the buffer.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SetReceiveBufferSize(int bufferSize)
        {
            BufferSize = bufferSize;
            buffer = new byte[BufferSize];
        }

        #region Documentation
        /// Constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  bufferSize  Size of the buffer.
        /// \param  buffer      The buffer.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Common.StateObject class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="bufferSize">   Size of the buffer.</param>
        /// <param name="buffer">       The buffer.</param>
        ///-------------------------------------------------------------------------------------------------

        public StateObject(int bufferSize, byte[] buffer)
        {
            this.bufferSize = bufferSize;
            this.buffer = buffer;
        }

        #region Documentation
        /// Default constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Common.StateObject class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public StateObject()
        {
        }
    }
}
