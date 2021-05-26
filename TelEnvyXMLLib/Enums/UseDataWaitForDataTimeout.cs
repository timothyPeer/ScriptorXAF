// ***********************************************************************
// Assembly         : TelEnvyXmlLib
// Author           : Timothy Peer
// Created          : 04-10-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 04-10-2018
// ***********************************************************************
// <copyright file="UsedDataWaitForDataTimeout.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;


namespace TelEnvyXmlLib.Enums
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Enum UseDataWaitForDataTimeout. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum UseDataWaitForDataTimeout
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        Send = 0,   /* An enum constant representing the send option */
        /// <summary>
        /// The long time stamp
        /// </summary>
        Receive = 1 /* An enum constant representing the receive option */
    }
}
