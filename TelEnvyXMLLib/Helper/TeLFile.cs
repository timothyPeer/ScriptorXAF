// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TeXFile.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TelEnvyXmlLib.Helper
{
   

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   (Serializable) a te l file. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    [Serializable]
    public class TeLFile
    {
        // Fields...



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the filename of the file. </summary>
        ///
        /// <value> The name of the file. </value>
        ///-------------------------------------------------------------------------------------------------

        [XmlElement]
        public string FileName { get; set; }
        // Fields...



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the expect grab line. </summary>
        ///
        /// <value> True if expect grab line, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        [XmlElement]
        public bool ExpectGrabLine { get; set; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the expect grab lines. </summary>
        ///
        /// <value> True if expect grab lines, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        [XmlElement]
        public bool ExpectGrabLines { get; set; }
        // Fields...
        /// <summary>   The sort key. </summary>
        private int _SortKey;   /* The sort key */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the sort key. </summary>
        ///
        /// <value> The sort key. </value>
        ///-------------------------------------------------------------------------------------------------

        public int SortKey
        {
            get { return _SortKey; }
            set
            {
                _SortKey = value;
            }
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether this TeLFile is encrypted. </summary>
        ///
        /// <value> True if this TeLFile is encrypted, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsEncrypted { get; set; }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.TeLFile class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TeLFile(SerializationInfo info, StreamingContext context)
        {
            FileName = info.GetString("FileName");
            ExpectGrabLine = info.GetBoolean("ExpectGrabLine");
            ExpectGrabLines = info.GetBoolean("ExpectGrabLines");
            IsEncrypted = false;
        }

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.TeLFile class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TeLFile()
        {

        }
    }
}
