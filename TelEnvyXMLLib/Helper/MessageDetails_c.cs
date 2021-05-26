// ***********************************************************************
// Assembly         : PZLib
// Author           : Timothy Peer
// Created          : 02-19-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 02-19-2018
// ***********************************************************************
// <copyright file="MessageDetails_c.cs" company="eNVy Systems, Inc.">
//     Copyright ©  2018
// </copyright>

//<remarks> 
// ***********************************************************************
using System.Text;
using Newtonsoft.Json;

namespace TelEnvyXmlLib.Helper
{


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A message details c. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class MessageDetails_c
    {
       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.MessageDetails_c class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public MessageDetails_c()
        {
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Helper.MessageDetails_c class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="messageId">    The identifier of the message.</param>
        /// <param name="messageType">  The type of the message.</param>
        /// <param name="message">      The message.</param>
        ///-------------------------------------------------------------------------------------------------

        public MessageDetails_c(string messageId, string messageType, string message)
        {
            MessageId = messageId;
            MessageType = messageType;
            Message = message;
        }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the identifier of the message. </summary>
        ///
        /// <value> The identifier of the message. </value>
        ///-------------------------------------------------------------------------------------------------

         [JsonProperty(PropertyName = "messageId")]
        public string MessageId { get; set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the type of the message. </summary>
        ///
        /// <value> The type of the message. </value>
        ///-------------------------------------------------------------------------------------------------

           [JsonProperty(PropertyName = "messageType")]
        public string MessageType { get; set; }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the message. </summary>
        ///
        /// <value> The message. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a string that represents the current object. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   A string that represents the current object. </returns>
        ///
        /// <seealso cref="M:System.Object.ToString()"/>
        ///-------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder sbx = new StringBuilder();
            sbx.AppendLine("<MessageDetails>");
            sbx.AppendLine(string.Format("  <Message>{0}</Message>", Message));
            sbx.AppendLine(string.Format("  <MessageId>{0}</MessageId>", MessageId));
            sbx.AppendLine(string.Format("  <MessageType>{0}</MessageType>", MessageType));
            sbx.AppendLine("</MessageDetails>");
            // string jSonStr = new xmlToJson().Convert(sbx.ToString());
            // return jSonStr;

            return sbx.ToString();
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this MessageDetails_c to a message details object. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   This MessageDetails_c as a MessageDetails_c. </returns>
        ///-------------------------------------------------------------------------------------------------

        public MessageDetails_c ToMessageDetailsObject ()
        {
            MessageDetails_c message = new MessageDetails_c { Message = Message, MessageId = MessageId, MessageType = MessageType };

            return message;
        }
    }
}
