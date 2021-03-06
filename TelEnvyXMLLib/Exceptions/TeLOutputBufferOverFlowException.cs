// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TeXOutputBufferOverFlow.cs" company="eNVy Systems, Inc.">
//     Copyright ? eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TelEnvyXmlLib.Exceptions
{
    #region Documentation
    /// Exception Class: An Output Buffer OverFlow exception occurs in the Grab Methods if the sum of
    /// start scrap offset and field length exceeds the bounds of the VT screen "Screensize"
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa System.Exception    
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   (Serializable) exception for signalling te l output buffer over flow errors.
    ///             </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.Exception"/>
    ///-------------------------------------------------------------------------------------------------

    [Serializable]
    public  class TeLOutputBufferOverFlowException   : Exception
    {
           #region Documentation
           /// Constructor
           ///
           /// \author  Timothy Peer, eNVy Systems Inc.
           /// \date    6/26/2019
           ///
           /// \param   message The message that describes the error.
           #endregion

           ///-------------------------------------------------------------------------------------------------
           /// <summary>    Initializes a new instance of the
           ///              TelEnvyXmlLib.Exceptions.TeLOutputBufferOverFlowException class.
           ///              </summary>
           ///
           /// <remarks>    Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
           ///
           /// <param name="message">   The message.</param>
           ///-------------------------------------------------------------------------------------------------

           public TeLOutputBufferOverFlowException(string message)
            : base(message)
        {

        }

        #region Documentation
        /// Constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message         The error message that explains the reason for the exception.
        /// \param  innerException  The exception that is the cause of the current exception, or a null
        ///                         reference (Nothing in Visual Basic) if no inner exception is
        ///                         specified.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLOutputBufferOverFlowException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLOutputBufferOverFlowException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        #region Documentation
        /// Constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  info    The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that
        ///                 holds the serialized object data about the exception being thrown.
        /// \param  context The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that
        ///                 contains contextual information about the source or destination.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLOutputBufferOverFlowException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TeLOutputBufferOverFlowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
