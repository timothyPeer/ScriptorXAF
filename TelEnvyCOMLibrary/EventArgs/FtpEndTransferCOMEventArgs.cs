﻿// ***********************************************************************
// Assembly         : TelEnvyCOMLibrary
// Author           : Timothy Peer
// Created          : 05-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 05-27-2018
// ***********************************************************************
// <copyright file="FtpEndTransferCOMEventArgs.cs" company="eNVy Systems, Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelEnvyCOMLibrary.EventArgs
{
    /// <summary>
    /// Class FtpEndTransferCOMEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for FtpEndTransferCOMEventArgs
    public class FtpEndTransferCOMEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Direction
        public int Direction { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpEndTransferCOMEventArgs" /> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public FtpEndTransferCOMEventArgs(int direction)
        {
            Direction = direction;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpEndTransferCOMEventArgs"/> class.
        /// </summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public FtpEndTransferCOMEventArgs()
        {
            
        }

    }
}