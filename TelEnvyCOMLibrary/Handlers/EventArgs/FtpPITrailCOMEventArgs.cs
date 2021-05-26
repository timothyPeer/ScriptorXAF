// ***********************************************************************
// Assembly         : TelEnvyCOMLibrary
// Author           : Timothy Peer
// Created          : 05-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 05-27-2018
// ***********************************************************************
// <copyright file="FtpPITrailCOMEventArgs.cs" company="eNVy Systems, Inc.">
//     Copyright �  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelEnvyCOMLibrary.EventArgs
{
    /// <summary>
    /// Class FtpPITrailCOMEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for FtpPITrailCOMEventArgs
    public class FtpPITrailCOMEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Direction
        public int Direction { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Message
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpPITrailCOMEventArgs" /> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="message">The message.</param>
        public FtpPITrailCOMEventArgs(int direction, string message)
        {
            Direction = direction;
            Message = message;
        }

    }
}
