// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="GrabChangedEventArgs.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelEnvyXmlLib.EventArgs
{
    #region Documentation
    /// The property is returned with string data when GrabLine and Grablines.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa System.EventArgs    
    ///
    /// ### example
    /// <code>
    /// void comSession1_GrabChanged(object sender, GrabChangedEventArgs e)
    /// {
    /// StringBuilder sb = new StringBuilder(Convert.ToString(textBoxDebug.EditValue));
    /// sb.AppendLine(string.Format("Grabbed data [{0}]:\r\n", e.Name));
    /// sb.AppendLine(string.Join("\r\n", e.Data) + "\r\n");
    /// textBoxDebug.EditValue= sb.ToString();
    /// }
    /// </code>
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Additional information for grab changed events. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.EventArgs"/>
    ///-------------------------------------------------------------------------------------------------

    public class GrabChangedEventArgs : System.EventArgs
    {
        #region Documentation
        /// The name / moniker used with setting the event. This value is configured in the input XML
        /// file for Grabline name="".
        ///
        /// \returns    The name.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the name. </summary>
        ///
        /// <value> The name. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Name { get; private set; }

        #region Documentation
        /// The data scraped by the control. Data are passed in this class as an array of strings.
        /// Grabline sends a single string and Grablines sends an array of strings.
        ///
        /// \returns    The data.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the data. </summary>
        ///
        /// <value> The data. </value>
        ///-------------------------------------------------------------------------------------------------

        public string[] Data { get; private set; }

        #region Documentation
        /// The constructor.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  name    The "name" of the Event.
        /// \param  data    The data scraped from the telnet stream.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.EventArgs.GrabChangedEventArgs
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="name"> The name.</param>
        /// <param name="data"> The data.</param>
        ///-------------------------------------------------------------------------------------------------

        public GrabChangedEventArgs(string name, string[] data)
        {
			Name = name;
            Data = data;
        }
        public GrabChangedEventArgs(string name, List<string> data)
        {
            Name = name;
            Data = data.ToArray();
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="GrabChangedEventArgs"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.EventArgs.GrabChangedEventArgs
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public GrabChangedEventArgs()
        {
            
        }

    }
}
