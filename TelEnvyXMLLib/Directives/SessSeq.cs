// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-25-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TexSessionSessSeqNode.cs" company="eNVy Systems, Inc.">
//     Copyright ? eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml;
using TelEnvyXmlLib.Abstract;
using System.Threading;
using TelEnvyXmlLib.Enums;

namespace TelEnvyXmlLib.Directives
{
   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   The sess sequence. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Abstract.TeLSessionGroupNode"/>
    ///-------------------------------------------------------------------------------------------------

    public class SessSeq : TeLSessionGroupNode
    {
        /// <summary>   The allowed attributes. </summary>
        readonly string[] _allowedAttributes = { "ServerName", "ServerPort", "ServerTimeout", "DebugEnabled", "LoggingEnabled", "RecordingEnabled", "PageWidth", "PageLength", "Mode", "TerminationString" };  /* The allowed attributes */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the allowed attributes. </summary>
        ///
        /// <value> The allowed attributes. </value>
        ///
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionGroupNode.AllowedAttributes"/>
        /// <seealso cref="P:TelEnvyXmlLib.Abstract.TeLSessionNode.AllowedAttributes"/>
        ///-------------------------------------------------------------------------------------------------

        protected override string[] AllowedAttributes { get { return _allowedAttributes; } }

   

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

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the server. </summary>
        ///
        /// <value> The name of the server. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ServerName { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the termination string. </summary>
        ///
        /// <value> The termination string. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TerminationString { get; set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the mode. </summary>
        ///
        /// <value> The mode. </value>
        ///-------------------------------------------------------------------------------------------------

        public TerminalMode Mode { get; set; }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the server port. </summary>
        ///
        /// <value> The server port. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? ServerPort { get; set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the server timeout. </summary>
        ///
        /// <value> The server timeout. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? ServerTimeout { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the debugging is enabled. </summary>
        ///
        /// <value> True if debugging enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool DebuggingEnabled { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the logging is enabled. </summary>
        ///
        /// <value> True if logging enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool LoggingEnabled { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the recording is enabled. </summary>
        ///
        /// <value> True if recording enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool RecordingEnabled { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the width of the page. </summary>
        ///
        /// <value> The width of the page. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? PageWidth { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the page length. </summary>
        ///
        /// <value> The length of the page. </value>
        ///-------------------------------------------------------------------------------------------------

        public int? PageLength { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Directives.SessSeq class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="node"> The node.</param>
        ///-------------------------------------------------------------------------------------------------

        public SessSeq(XmlNode node)
            : base(node, Enums.XmlTag.SessSeq)
        {
            ServerName = Helper.TeLSessionXmlParser.GetAttributeString(node, "ServerName", false);
            ServerTimeout = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "ServerTimeout");
            DebuggingEnabled = Helper.TeLSessionXmlParser.GetAttributeBool(node, "DebugEnabled");
            LoggingEnabled = Helper.TeLSessionXmlParser.GetAttributeBool(node, "LoggingEnabled");
            RecordingEnabled = Helper.TeLSessionXmlParser.GetAttributeBool(node, "RecordingEnabled");
            PageWidth = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "PageWidth").Value;
            PageLength = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "PageLength").Value;
            ServerPort = Helper.TeLSessionXmlParser.GetAttributeNInt32(node, "ServerPort");
            string ModeX = Helper.TeLSessionXmlParser.GetAttributeString(node, "Mode", true);
            if (ModeX.ToUpper().Contains("FORM")) Mode = TerminalMode.Form;
            else
            {
                Mode = TerminalMode.Stream;
            }
            TerminationString = Helper.TeLSessionXmlParser.GetAttributeString(node, "TerminationString", false);
            string vc= node.ToString();
        }
    }
}
