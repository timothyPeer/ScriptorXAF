// ***********************************************************************
// Assembly         : TelEnvyXmlLib
// Author           : Timothy Peer
// Created          : 02-24-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 02-24-2018
// ***********************************************************************
// <copyright file="SimpleLogger.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using Rebex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelEnvyXmlLib.Helper
{
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A simple logger. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:Rebex.ILogWriter"/>
    ///-------------------------------------------------------------------------------------------------

    public class SimpleLogger : ILogWriter
    {
        /// <summary>   The encoding. </summary>
        private Encoding _encoding; /* The encoding */
        /// <summary>   The builder. </summary>
        private StringBuilder _builder; /* The builder */

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether this SimpleLogger is enabled. </summary>
        ///
        /// <value> True if enabled, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Enabled { get; set; }

    
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Get or set the log level - only log messages with log level equal to or greater
        ///             than the specified one.
        ///             </summary>
        ///
        /// <value> Log level. </value>
        ///
        /// <seealso cref="P:Rebex.ILogWriter.Level"/>
        ///-------------------------------------------------------------------------------------------------

        public LogLevel Level { get; set; }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.SimpleLogger class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="encoding"> The encoding.</param>
        ///-------------------------------------------------------------------------------------------------

        public SimpleLogger(Encoding encoding)
        {
            _encoding = encoding;
            _builder = new StringBuilder();
            Enabled = true;
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Write a message into the log. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="level">        Message level.</param>
        /// <param name="objectType">   Logging object type.</param>
        /// <param name="objectId">     Logging object ID.</param>
        /// <param name="area">         Logging area.</param>
        /// <param name="message">      Message.</param>
        ///
        /// <seealso cref="M:Rebex.ILogWriter.Write(LogLevel,Type,int,string,string)"/>
        ///-------------------------------------------------------------------------------------------------

        public void Write(LogLevel level, Type objectType, int objectId, string area, string message)
        {
            Write(level, objectType, objectId, area, message, null, 0, 0);
        }

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Write a message and data block into the log. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="level">        Message level.</param>
        /// <param name="objectType">   Logging object type.</param>
        /// <param name="objectId">     Logging object ID.</param>
        /// <param name="area">         Logging area.</param>
        /// <param name="message">      Message.</param>
        /// <param name="buffer">       Data block.</param>
        /// <param name="offset">       Data offset.</param>
        /// <param name="length">       Date length.</param>
        ///
        /// <seealso cref="M:Rebex.ILogWriter.Write(LogLevel,Type,int,string,string,byte[],int,int)"/>
        ///-------------------------------------------------------------------------------------------------

        public void Write(LogLevel level, Type objectType, int objectId, string area, string message, byte[] buffer, int offset, int length)
        {
            if (!Enabled)
                return;

            if (buffer == null)
                return;

            if (message.StartsWith("Received"))
            {
                Console.WriteLine("<- Received {0} byte(s).", length);
            }
            else if (message.StartsWith("Sent"))
            {
                Console.WriteLine("-> Sent {0} byte(s):", length);
                Console.WriteLine(Transform(_encoding.GetString(buffer, offset, length)));
            }
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Transforms. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="data"> The data.</param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        private string Transform(string data)
        {
            _builder.Length = 0;
            for (int i = 0; i < data.Length; i++)
            {
                switch ((int)data[i])
                {
                    case 0x00: _builder.Append("<NUL>"); break;
                    case 0x01: _builder.Append("<SOH>"); break;
                    case 0x02: _builder.Append("<STX>"); break;
                    case 0x03: _builder.Append("<ETX>"); break;
                    case 0x04: _builder.Append("<EOT>"); break;
                    case 0x05: _builder.Append("<ENQ>"); break;
                    case 0x06: _builder.Append("<ACK>"); break;
                    case 0x07: _builder.Append("<BEL>"); break;
                    case 0x08: _builder.Append("<BS>"); break;
                    case 0x09: _builder.Append("<HT>"); break;
                    case 0x0A: _builder.Append("<LF>"); break;
                    case 0x0B: _builder.Append("<VT>"); break;
                    case 0x0C: _builder.Append("<FF>"); break;
                    case 0x0D: _builder.Append("<CR>"); break;
                    case 0x0E: _builder.Append("<SO>"); break;
                    case 0x0F: _builder.Append("<SI>"); break;
                    case 0x10: _builder.Append("<DLE>"); break;
                    case 0x11: _builder.Append("<DC1>"); break;
                    case 0x12: _builder.Append("<DC2>"); break;
                    case 0x13: _builder.Append("<DC3>"); break;
                    case 0x14: _builder.Append("<DC4>"); break;
                    case 0x15: _builder.Append("<NAK>"); break;
                    case 0x16: _builder.Append("<SYN>"); break;
                    case 0x17: _builder.Append("<ETB>"); break;
                    case 0x18: _builder.Append("<CAN>"); break;
                    case 0x19: _builder.Append("<EM>"); break;
                    case 0x1A: _builder.Append("<SUB>"); break;
                    case 0x1B: _builder.Append("<ESC>"); break;
                    case 0x1C: _builder.Append("<FS>"); break;
                    case 0x1D: _builder.Append("<GS>"); break;
                    case 0x1E: _builder.Append("<RS>"); break;
                    case 0x1F: _builder.Append("<US>"); break;
                    default: _builder.Append(data[i]); break;
                }
            }
            return _builder.ToString();
        }
    }
}
