// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="Session.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using TelEnvyXmlLib.Abstract;

namespace TelEnvyXmlLib.Directives
{
    #region Documentation
    /// Allows: "TcpNoDelay", "EOL", "SocketType","TcpNoDelay", "ConnectionTimeout", "Linker",
    /// "MaxLineLength", "MaxTransferRate","TCPKeepAlive","InBufferSize","OutBufferSize",
    /// "UseBackgroundThread"
    /// 
    /// Requires: at least one SessSeq child
    /// 
    /// 
    /// The Session directive initializes the session for a transaction sequence. This directive is
    /// NOT a repeating section.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa Session 
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A session. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionGroupNode"/>
    ///-------------------------------------------------------------------------------------------------

    public class Session : TeLSessionGroupNode
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   The allowed attributes "SocketType"="IpPort" or "Telnet" "TcpNoDelay"="True" or
        ///             "False" "ConnectionTimeout"="0" if > 0 use TimeOut for Asynchronous Connection
        ///             "SocketLinger"="True" or "False" "MaxLineLength"="0" "EOL"="\n"
        ///             "MaxTransferRate"="0" - disables transfer rate throttles "TCPKeepAlive"="True" or
        ///             "False" "TcpNoDelay"="True" or "False" "InBufferSize"="0" use the default input
        ///             buffer size for the data, value other than 0 - increase the input cache in the
        ///             Socket "OutBufferSize"="0", use the default output buffer size for the data,
        ///             value other than 0 - increase the output cache used by the socket
        ///             "SocketLinger"="true" or "False" "UseBackgroundThread"="True" or "False".
        ///             </summary>
        ///-------------------------------------------------------------------------------------------------

        string[] _allowedAttributes = { "TcpNoDelay","EOL","SocketType","TcpNoDelay", "ConnectionTimeout", "SocketLinger", "MaxLineLength", "MaxTransferRate","TCPKeepAlive","InBufferSize","OutBufferSize","UseBackgroundThread" };    /* The allowed attributes */

        #region Documentation
        /// Gets the allowed attributes.
        ///
        /// \returns    The allowed attributes.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the allowed attributes. </summary>
        ///
        /// <value> The allowed attributes. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionGroupNode.AllowedAttributes"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.AllowedAttributes"/>
        ///-------------------------------------------------------------------------------------------------

        protected override string[] AllowedAttributes { get { return _allowedAttributes; } }

        #region Documentation
        /// Gets a value indicating whether [name required].
        ///
        /// \returns    <c>true</c> if [name required]; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the name required. </summary>
        ///
        /// <value> True if name required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionGroupNode.NameRequired"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.NameRequired"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool NameRequired { get { return false; } }

        #region Documentation
        /// Gets a value indicating whether this instance is child required.
        ///
        /// \returns    <c>true</c> if this instance is child required; otherwise, <c>false</c>.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this TeLSessionGroupNode is child required.
        ///             </summary>
        ///
        /// <value> True if this TeLSessionGroupNode is child required, false if not. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionGroupNode.IsChildRequired"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.IsChildRequired"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool IsChildRequired { get { return true; } }

        #region Documentation
        /// Gets the type of the required child.
        ///
        /// \returns    The type of the required child.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the type of the required child. </summary>
        ///
        /// <value> The type of the required child. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionGroupNode.RequiredChildType"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionPairTag.RequiredChildType"/>
        ///-------------------------------------------------------------------------------------------------

        protected override Enums.RequiredChildNode RequiredChildType { get { return Enums.RequiredChildNode.Elements; } }

        #region Documentation
        /// The allowed attributes "SocketType"="IpPort" or "Telnet" "TcpNoDelay"="True" or "False"
        /// "ConnectionTimeout"="0" if > 0 use TimeOut for Asynchronous Connection "SocketLinger"="True"
        /// or "False" "MaxLineLength"="0" "EOL"="\n" "MaxTransferRate"="0" - disables transfer rate
        /// throttles "TCPKeepAlive"="True" or "False" "TcpNoDelay"="True" or "False" "InBufferSize"="0"
        /// use the default input buffer size for the data, value other than 0 - increase the input cache
        /// in the Socket "OutBufferSize"="0", use the default output buffer size for the data, value
        /// other than 0 - increase the output cache used by the socket "SocketLinger"="true" or "False"
        /// "UseBackgroundThread"="True" or "False" "EOL"="\n"
        ///
        /// \returns    True if use background thread, false if not.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether this Session use background thread.
        ///             </summary>
        ///
        /// <value> True if use background thread, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool UseBackgroundThread { get; set; }

        #region Documentation
        /// Gets or sets the connection timeout
        ///
        /// \returns    The connection timeout.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the connection timeout. </summary>
        ///
        /// <value> The connection timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ConnectionTimeout { get; set; }

        #region Documentation
        /// Gets or sets a value indicating whether the socket linger
        ///
        /// \returns    True if socket linger, false if not.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the socket linger. </summary>
        ///
        /// <value> True if socket linger, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool SocketLinger { get; set; }

        #region Documentation
        /// Gets or sets a value indicating whether the TCP keep alive
        ///
        /// \returns    True if TCP keep alive, false if not.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the TCP keep alive. </summary>
        ///
        /// <value> True if TCP keep alive, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool TCPKeepAlive { get; set; }

        #region Documentation
        /// Gets or sets the maximum length of the line
        ///
        /// \returns    The maximum length of the line.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the maximum length of the line. </summary>
        ///
        /// <value> The maximum length of the line. </value>
        ///-------------------------------------------------------------------------------------------------

        public int MaxLineLength { get; set; }

        #region Documentation
        /// Gets or sets the maximum transfer rate
        ///
        /// \returns    The maximum transfer rate.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the maximum transfer rate. </summary>
        ///
        /// <value> The maximum transfer rate. </value>
        ///-------------------------------------------------------------------------------------------------

        public int MaxTransferRate { get; set; }

        #region Documentation
        /// Gets or sets the size of the out buffer
        ///
        /// \returns    The size of the out buffer.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the size of the out buffer. </summary>
        ///
        /// <value> The size of the out buffer. </value>
        ///-------------------------------------------------------------------------------------------------

        public int OutBufferSize { get; set; }

        #region Documentation
        /// Gets or sets the size of the in buffer
        ///
        /// \returns    The size of the in buffer.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the size of the in buffer. </summary>
        ///
        /// <value> The size of the in buffer. </value>
        ///-------------------------------------------------------------------------------------------------

        public int InBufferSize { get; set; }

        #region Documentation
        /// Gets or sets a value indicating whether the TCP no delay
        ///
        /// \returns    True if TCP no delay, false if not.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the TCP no delay. </summary>
        ///
        /// <value> True if TCP no delay, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool TcpNoDelay { get; set; }

        #region Documentation
        /// Gets or sets the type of the socket
        ///
        /// \returns    The type of the socket.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the type of the socket. </summary>
        ///
        /// <value> The type of the socket. </value>
        ///-------------------------------------------------------------------------------------------------

        public string SocketType { get; set; }

        #region Documentation
        /// Gets or sets the EOL
        ///
        /// \returns    The EOL.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the EOL. </summary>
        ///
        /// <value> The EOL. </value>
        ///-------------------------------------------------------------------------------------------------

        public string EOL { get; set; }

        #region Documentation
        /// Initializes a new instance of the <see cref="Session"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  node    The node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.Session class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public Session(XmlNode node) : base(node, Enums.XmlTag.Session) {


            try
            {
                UseBackgroundThread = Helper.TeLSessionXmlParser.GetAttributeBool(node, "UseBackgroundThread");
                ConnectionTimeout = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "ConnectionTimeout").Value;
                SocketLinger = Helper.TeLSessionXmlParser.GetAttributeBool(node, "Linger");
                TCPKeepAlive = Helper.TeLSessionXmlParser.GetAttributeBool(node, "TCPKeepAlive");
                EOL = Helper.TeLSessionXmlParser.GetAttributeString(node, "EOL", false);
                if (string.IsNullOrEmpty(EOL))  // cannot be zero
                {
                    MaxLineLength = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "MaxLineLength").Value;
                    if (MaxLineLength == 0) MaxLineLength = -1;
                }
                if (!(string.IsNullOrEmpty(EOL)))  // cannot be zero
                {
                    MaxLineLength = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "MaxLineLength").Value;
                    if (MaxLineLength == 0) MaxLineLength = -1;
                }
                MaxTransferRate = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "MaxTransferRate").Value;
                OutBufferSize = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "OutBufferSize").Value;
                InBufferSize = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "InBufferSize").Value;
                TcpNoDelay = Helper.TeLSessionXmlParser.GetAttributeBool(node, "TcpNoDelay");
                SocketType = Helper.TeLSessionXmlParser.GetAttributeString(node, "SocketType", true);

                if (EOL != null)
                    if (EOL.ToUpper().Equals("NL")) EOL = "\n";
            }
            catch (Exception ex)
            {
                
            }
        }

        #region Documentation
        /// Parses the child.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \exception  TeLInvalidDataFormatException   Thrown when a Te L Invalid Data Format error
        ///                                             condition occurs.
        ///
        /// \param  child   The child.
        ///
        /// \returns    TexSessionNode.
        ///
        /// ### exception   Exceptions.TeLInvalidDataFormatException    Invalid child node &lt;{0}&gt;
        ///                                                             encountered within &lt;{1}&gt; node.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Parse child. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <exception cref="TeLInvalidDataFormatException"> Thrown when a Te L Invalid Data Format error
        ///                                                  condition occurs.</exception>
        ///
        /// <param name="child">    The child.</param>
        ///
        /// <returns>   A TelEnvyXmlLib.Abstract.TeLSessionNode. </returns>
        ///
        /// <seealso cref="M:TelEnvyXmlLib.Abstract.TeLSessionGroupNode.ParseChild(XmlNode)"/>
        ///-------------------------------------------------------------------------------------------------

        protected override TeLSessionNode ParseChild(XmlNode child)
        {
            if (child.Name != "SessSeq")
                throw new Exceptions.TeLInvalidDataFormatException("Invalid child node <{0}> encountered within <{1}> node.", child.Name, TeLNode.Name);
            return new SessSeq(child);
        }
    }
}
