// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : tim
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer, eNVy Systems Inc.
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="TeXWaitCursorTimeOut.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;

namespace TelEnvyXmlLib.Exceptions
{
    #region Documentation
    /// Class TeXWaitCursorTimeOut.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa System.Exception    
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   (Serializable) exception for signalling te l wait cursor time out errors. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.Exception"/>
    ///-------------------------------------------------------------------------------------------------

    [Serializable]
    public class TeLWaitCursorTimeOutException : Exception

    {
        #region Documentation
        /// Initializes a new instance of the <see cref="TeLWaitCursorTimeOutException" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLWaitCursorTimeOutException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TeLWaitCursorTimeOutException()
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLWaitCursorTimeOutException" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message The message that describes the error.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLWaitCursorTimeOutException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLWaitCursorTimeOutException(string message)
            : base(message)
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLWaitCursorTimeOutException" /> class.
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
        ///             TelEnvyXmlLib.Exceptions.TeLWaitCursorTimeOutException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLWaitCursorTimeOutException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLWaitCursorTimeOutException" /> class.
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
        ///             TelEnvyXmlLib.Exceptions.TeLWaitCursorTimeOutException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TeLWaitCursorTimeOutException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
