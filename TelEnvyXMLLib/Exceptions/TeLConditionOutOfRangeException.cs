// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="TeXConditionOutOfRange.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelEnvyXmlLib.Exceptions
{
    #region Documentation
    /// Exception Class: The TelEnvyXmlLib processor encountered an invalid condition If tag in the
    /// XML File.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa System.Exception
    /// <IfCondition Type="CursorAtPosition" Row="1" Column="3" />
    /// <IfCondition Type="CursorWithinRegion" Row="1" Column="3" Width="5" Height="3" />
    /// <IfCondition Type="DataAtCursorPosition" Data="Delete?" />
    /// <IfCondition Type="DataBeforeCursorPosition" Data="Delete?" />
    /// <IfCondition Type="DataWithinRegion" Row="1" Column="3" Width="5" Height="3" Data="Delete?" />
    /// <IfCondition Type="DataWithinRegion" Row="0" Column="-10" Width="10" Height="1" Data="Delete?" RelativeToCursor="true" />
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   (Serializable) exception for signalling te l condition out of range errors.
    ///             </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.Exception"/>
    ///-------------------------------------------------------------------------------------------------

    [Serializable]
    public class TeLConditionOutOfRangeException : System.Exception
    {
        #region Documentation
        /// Constructor
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLConditionOutOfRangeException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TeLConditionOutOfRangeException()
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
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLConditionOutOfRangeException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLConditionOutOfRangeException(string message)
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
        ///             TelEnvyXmlLib.Exceptions.TeLConditionOutOfRangeException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLConditionOutOfRangeException(string message, Exception innerException)
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
        ///             TelEnvyXmlLib.Exceptions.TeLConditionOutOfRangeException class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TeLConditionOutOfRangeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }
   
}
