#region Documentation

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TelEnvyXmlLib.Helper;

namespace TelEnvyXmlLib.Exceptions
{
    #region Documentation
    /// Exception for signaling tel wait missing coordinate errors.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Exception for signalling tel wait missing coordinate errors. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Exceptions.TelEnvyExceptionBase"/>
    ///-------------------------------------------------------------------------------------------------

    public class TelWaitMissingCoordinateException : TelEnvyExceptionBase
    {
        #region Documentation
        /// Initializes a new instance of the <see cref="ObjectToJsonException" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TelWaitMissingCoordinateException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TelWaitMissingCoordinateException()
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TelWaitMissingCoordinateException" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message The message that describes the error.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TelWaitMissingCoordinateException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TelWaitMissingCoordinateException(MessageDetails_c message)
            : base(message)
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TelWaitMissingCoordinateException" /> class.
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
        ///             TelEnvyXmlLib.Exceptions.TelWaitMissingCoordinateException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TelWaitMissingCoordinateException(MessageDetails_c message, Exception innerException)
            : base(message, innerException)
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TelWaitMissingCoordinateException" /> class.
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
        ///             TelEnvyXmlLib.Exceptions.TelWaitMissingCoordinateException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TelWaitMissingCoordinateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TelWaitMissingCoordinateException" /> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message The message that describes the error.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TelWaitMissingCoordinateException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TelWaitMissingCoordinateException(string message)
            : base(message)
        {

        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TelWaitMissingCoordinateException" /> class.
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
        ///             TelEnvyXmlLib.Exceptions.TelWaitMissingCoordinateException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TelWaitMissingCoordinateException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

    }
}
