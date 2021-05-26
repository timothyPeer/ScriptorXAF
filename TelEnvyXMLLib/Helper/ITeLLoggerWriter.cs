// ***********************************************************************
// Assembly         : TelEnvyXmlLib
// Author           : Timothy Peer
// Created          : 02-24-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 02-24-2018
// ***********************************************************************
// <copyright file="ITeLLoggerWriter.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;


namespace TelEnvyXmlLib.Helper
{
    #region Documentation
    /// Interface ITeLLoggerWriter
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for te l logger writer. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface ITeLLoggerWriter
    {
        #region Documentation
        /// Gets or sets the stream data.
        ///
        /// \returns    The stream data.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets information describing the stream. </summary>
        ///
        /// <value> Information describing the stream. </value>
        ///-------------------------------------------------------------------------------------------------

        System.IO.StringWriter streamData { get; set; }

   

    }
}
