// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-03-2018
//
// Last Modified By : tim
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="Enums.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelEnvyXmlLib.Enums
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Enum RequiredChildNode. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    
	public enum RequiredChildNode
	{
        /// <summary>
        /// The one element
        /// </summary>
        OneElement, /* An enum constant representing the one element option */
        /// <summary>
        /// The elements
        /// </summary>
        Elements,   /* An enum constant representing the elements option */
        /// <summary>
        /// The text
        /// </summary>
        Text,   /* An enum constant representing the text option */
	}
}
