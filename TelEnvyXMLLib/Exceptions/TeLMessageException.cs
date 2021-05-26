// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-31-2018
//
// Last Modified By : tim
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TeXMessageException.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelEnvyXmlLib.Exceptions
{
 

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Exception for signalling te l message errors. This class cannot be inherited.
    ///             </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.Exception"/>
    ///-------------------------------------------------------------------------------------------------

    public sealed class TeLMessageException : TelEnvyExceptionBase
    {


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Exceptions.TeLMessageException
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TeLMessageException()
        {

        }

        #region Documentation
        /// Constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message The message that describes the error.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Exceptions.TeLMessageException
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLMessageException(string message)
            : base(message)
        {

        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Exceptions.TeLMessageException
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLMessageException(string message, Exception innerException)
            : base(message, innerException)
        {

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Exceptions.TeLMessageException
        ///             class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLMessageException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }
}
