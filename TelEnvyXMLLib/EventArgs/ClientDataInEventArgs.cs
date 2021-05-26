//-----------------------------------------------------------------------
// <copyright file="C:\AfterMarket\OpenVMS\TelEnvySolutionB\TelEnvySolution\TelEnvyXMLLib\EventArgs\ClientDataInEventArgs.cs" company="">
//     Author: Tim 
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
#region Documentation
#endregion
using System;
using System.Linq;

namespace TelEnvyXmlLib.EventArgs
{
   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for client data in events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ClientDataInEventArgs : System.EventArgs
    {
     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.EventArgs.ClientDataInEventArgs
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="text">     The text.</param>
        /// <param name="textB">    The text b.</param>
        ///-------------------------------------------------------------------------------------------------

        public ClientDataInEventArgs(string text, byte[] textB)
        {
            Text = text;
            TextB = textB;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the text. </summary>
        ///
        /// <value> The text. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Text { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the text b. </summary>
        ///
        /// <value> The text b. </value>
        ///-------------------------------------------------------------------------------------------------

        public byte[] TextB { get; set; }
    }



    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for client connected events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ClientConnectedEventArgs : System.EventArgs
    {
  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.EventArgs.ClientConnectedEventArgs class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="description">  The description.</param>
        /// <param name="statusCode">   The status code.</param>
        ///-------------------------------------------------------------------------------------------------

        public ClientConnectedEventArgs(string description, int statusCode)
        {
            Description = description;
            StatusCode = statusCode;
        }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Description { get; set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the status code. </summary>
        ///
        /// <value> The status code. </value>
        ///-------------------------------------------------------------------------------------------------

        public int StatusCode { get; set; }
    }



    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for client connection status events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ClientConnectionStatusEventArgs : System.EventArgs
    {
       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.EventArgs.ClientConnectionStatusEventArgs class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="connectionEvent">  The connection event.</param>
        /// <param name="description">      The description.</param>
        /// <param name="statusCode">       The status code.</param>
        ///-------------------------------------------------------------------------------------------------

        public ClientConnectionStatusEventArgs(string connectionEvent, string description, int statusCode)
        {
            ConnectionEvent = connectionEvent;
            Description = description;
            StatusCode = statusCode;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the connection event. </summary>
        ///
        /// <value> The connection event. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ConnectionEvent { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Description { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the status code. </summary>
        ///
        /// <value> The status code. </value>
        ///-------------------------------------------------------------------------------------------------

        public int StatusCode { get; set; }
    }



    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for client disconnected events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ClientDisconnectedEventArgs : System.EventArgs
    {
      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.EventArgs.ClientDisconnectedEventArgs class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="description">  The description.</param>
        /// <param name="statusCode">   The status code.</param>
        ///-------------------------------------------------------------------------------------------------

        public ClientDisconnectedEventArgs(string description, int statusCode)
        {
            Description = description;
            StatusCode = statusCode;
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Description { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the status code. </summary>
        ///
        /// <value> The status code. </value>
        ///-------------------------------------------------------------------------------------------------

        public int StatusCode { get; set; }
    }



    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for client error events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ClientErrorEventArgs : System.EventArgs
    {
      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.EventArgs.ClientErrorEventArgs
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="description">  The description.</param>
        /// <param name="errorCode">    The error code.</param>
        ///-------------------------------------------------------------------------------------------------

        public ClientErrorEventArgs(string description, int errorCode)
        {
            Description = description;
            ErrorCode = errorCode;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Description { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the error code. </summary>
        ///
        /// <value> The error code. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ErrorCode { get; set; }
    }

   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for client ready to send events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class ClientReadyToSendEventArgs : System.EventArgs
    {
      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.EventArgs.ClientReadyToSendEventArgs class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public ClientReadyToSendEventArgs()
        {

        }
    }
}