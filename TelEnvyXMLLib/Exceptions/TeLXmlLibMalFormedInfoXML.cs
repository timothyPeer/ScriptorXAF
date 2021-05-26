﻿// ***********************************************************************
// Assembly         : TelEnvyXmlLib
// Author           : Timothy Peer
// Created          : 02-27-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 02-27-2018
// ***********************************************************************
// <copyright file="TeLXmlLibMalFormedInfoXML.cs" company="eNVy Systems, Inc.">
//     Copyright © eNVy Systems, Inc. 2011
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TelEnvyXmlLib.Helper;

namespace TelEnvyXmlLib.Exceptions
{
    #region Documentation
    /// Class TeLXmlLibMalFormedInfoXML.
    ///
    /// \author Timothy Peer, eNVy Systems Inc.
    /// \date   6/26/2019
    ///
    /// \sa System.Exception
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TeLXmlLibMalFormedInfoXML
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A te l XML library mal formed information xml. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:TelEnvyXmlLib.Exceptions.TelEnvyExceptionBase"/>
    ///-------------------------------------------------------------------------------------------------

    public class TeLXmlLibMalFormedInfoXML : TelEnvyExceptionBase
    {
        #region Documentation
        /// Initializes a new instance of the <see cref="TeLXmlLibMalFormedInfoXML"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLXmlLibMalFormedInfoXML class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public TeLXmlLibMalFormedInfoXML()
        {
            
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLXmlLibMalFormedInfoXML"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message The message that describes the error.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLXmlLibMalFormedInfoXML class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLXmlLibMalFormedInfoXML(MessageDetails_c message)
            : base(message)
        {
            
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLXmlLibMalFormedInfoXML"/> class.
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
        ///             TelEnvyXmlLib.Exceptions.TeLXmlLibMalFormedInfoXML class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLXmlLibMalFormedInfoXML(MessageDetails_c message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLXmlLibMalFormedInfoXML"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  info    The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds
        ///                 the serialized object data about the exception being thrown.
        /// \param  context The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that
        ///                 contains contextual information about the source or destination.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLXmlLibMalFormedInfoXML class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="info">     The information.</param>
        /// <param name="context">  The context.</param>
        ///-------------------------------------------------------------------------------------------------

        protected TeLXmlLibMalFormedInfoXML(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLXmlLibMalFormedInfoXML"/> class.
        ///
        /// \author Timothy Peer, eNVy Systems Inc.
        /// \date   6/26/2019
        ///
        /// \param  message The message that describes the error.
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the
        ///             TelEnvyXmlLib.Exceptions.TeLXmlLibMalFormedInfoXML class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">  The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLXmlLibMalFormedInfoXML(string message)
            : base(message)
        {
            
        }

        #region Documentation
        /// Initializes a new instance of the <see cref="TeLXmlLibMalFormedInfoXML"/> class.
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
        ///             TelEnvyXmlLib.Exceptions.TeLXmlLibMalFormedInfoXML class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="message">          The message.</param>
        /// <param name="innerException">   The inner exception.</param>
        ///-------------------------------------------------------------------------------------------------

        public TeLXmlLibMalFormedInfoXML(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

    }
}