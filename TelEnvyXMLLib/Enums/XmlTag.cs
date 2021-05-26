// ***********************************************************************
// Assembly         : TeLXmlLib
// Author           : Timothy Peer
// Created          : 01-03-2018
//
// Last Modified By : Timothy Peer
// Last Modified On : 01-31-2018
// ***********************************************************************
// <copyright file="XmlTag.cs" company="eNVy Systems, Inc.">
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
     /// <summary>  Enum XmlTag. </summary>
     ///
     /// <remarks>  Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
     ///-------------------------------------------------------------------------------------------------

     public enum XmlTag
    {
        /// <summary>
        /// Denotes the <Session> </Session> tag.
        /// </summary>
        Session = -2,   /* . */
        /// <summary>
        /// Denotes the start of a transaction sequence.
        /// </summary>
        SessSeq = -1,   /* . */
        /// <summary>
        /// Unused.
        /// </summary>
        None = 0,   /* . */
        /// <summary>
        /// Tag to <SendData>data</SendData> send data to the control.
        /// Data passed in XML is required.
        /// </summary>
        SendData = 1,   /* . */
        /// <summary>
        /// Tag to <SendEnter>data</SendEnter> send data followed by a CR to the control.
        /// Data passed in XML is optional.
        /// </summary>
        SendEnter = 2,  /* . */
        /// <summary>
        /// Tag to <SendTab>data</SendTab> send data followed by a TAB sequence to the control.
        /// Data passed in XML is optional.
        /// </summary>
        SendTab = 3,    /* . */
        /// <summary>
        /// Tag to <SendPFn>data</SendPFn> send a PF1 control sequence to the control followed by the (optional) data.
        /// Data passed in XML is optional.
        /// </summary>
        SendPF1 = 12,   /* . */
        /// <summary>
        /// Tag to <SendPFn>data</SendPFn> send a PF2 control sequence to the control followed by the (optional) data.
        /// Data passed in XML is optional.
        /// </summary>
        SendPF2 = 13,   /* . */
        /// <summary>
        /// Tag to <SendPFn>data</SendPFn> send a PF3 control sequence to the control followed by the (optional) data.
        /// Data passed in XML is optional.
        /// </summary>
        SendPF3 = 14,   /* . */
        /// <summary>
        /// Tag to <SendPFn>data</SendPFn> send a PF4 control sequence to the control followed by the (optional) data.
        /// Data passed in XML is optional.
        /// </summary>
        SendPF4 = 15,   /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F1 control sequence to the control.
        /// </summary>
        SendF1 = 16,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F2 control sequence to the control.
        /// </summary>
        SendF2 = 17,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F3 control sequence to the control.
        /// </summary>
        SendF3 = 18,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F4 control sequence to the control.
        /// </summary>
        SendF4 = 19,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F5 control sequence to the control.
        /// </summary>
        SendF5 = 20,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F6 control sequence to the control.
        /// </summary>
        SendF6 = 21,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F7 control sequence to the control.
        /// </summary>
        SendF7 = 22,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F8 control sequence to the control.
        /// </summary>
        SendF8 = 23,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F9 control sequence to the control.
        /// </summary>
        SendF9 = 24,    /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F10 control sequence to the control.
        /// </summary>
        SendF10 = 25,   /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F11 control sequence to the control.
        /// </summary>
        SendF11 = 26,   /* . */
        /// <summary>
        /// Tag to <SendFn>data</SendFn> send a F12 control sequence to the control.
        /// </summary>
        SendF12 = 27,   /* . */
        /// <summary>
        /// Tag to <Expect>data</Expect> wait for data to arrive.
        /// </summary>
        Expect = 30,    /* . */
        /// <summary>
        /// Tag to <WaitForCursor /> wait for cursor to move on specific position.
        /// </summary>
        WaitForCursor = 40, /* . */
        /// <summary>
        /// Tag to <WaitForData>data</WaitForData> wait for data to appear anywhere on screen.
        /// </summary>
        WaitForDataWholeScreen = 41,    /* . */
        /// <summary>
        /// Tag to <WaitForData>data</WaitForData> wait for data to appear in specified row on screen.
        /// </summary>
        WaitForDataOneRow = 42, /* . */
        /// <summary>
        /// Tag to <WaitForData>data</WaitForData> wait for data to appear in specified region on screen.
        /// </summary>
        WaitForDataRegion = 43, /* . */
        /// <summary>
        /// Tag to GrabLine.
        /// </summary>
        GrabLine = 50,  /* . */
        /// <summary>
        /// Tag to GrabLines.
        /// </summary>
        GrabLines = 51, /* . */
        /// <summary>
        /// Tag to GrabInt32.
        /// </summary>
        GrabInt32 = 52, /* . */
        /// <summary>
        /// Tag to GrabDouble.
        /// </summary>
        GrabDouble = 53,    /* . */
        /// <summary>
        /// Tag to GroupCollection.
        /// </summary>
        GroupCollection = 60,   /* . */
        /// <summary>
        /// Tag to If.
        /// </summary>
        If = 70,    /* An enum constant representing if option */
        /// <summary>
        /// Tag to While.
        /// </summary>
        While = 80, /* An enum constant representing the while option */

        /// <summary>
        /// The send space
        /// </summary>
        /// <autogeneratedoc />
        SendSpace = 90, /* An enum constant representing the send space option */

        /// <summary>
        /// The login
        /// </summary>
        LoginPair = 102,    /* An enum constant representing the login pair option */

        /// <summary>   An enum constant representing the send connect option. </summary>
        SendConnect = 103,  /* An enum constant representing the send connect option */

        /// <summary>   An enum constant representing the send login option. </summary>
        SendLogin = 104,    /* An enum constant representing the send login option */
     }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Values that represent bufferingmodes. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum Bufferingmode
    {
        /// <summary>   An enum constant representing the none option. </summary>
        None = 0,   /* An enum constant representing the none option */
        //Low = 1,
        //Normal = 2,
        //Auto = 3,
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Values that represent requested actions. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum RequestedAction
    {
        /// <summary>
        /// The server requests the client to disconnect.
        /// </summary>
        DisconnectRequest = 0,  /* . */

        /// <summary>
        /// Make a bell sound.
        /// </summary>
        RingBell = 1,   /* . */

        /// <summary>
        /// Resize client screen. 
        /// </summary>
        ResizeScreen = 2,   /* An enum constant representing the resize screen option */

        /// <summary>
        /// Execute a command on the client side. 
        /// </summary>
        ExecuteCommand = 3, /* An enum constant representing the execute command option */

        /// <summary>
        /// Set the icon name. 
        /// </summary>
        IconName = 4,   /* An enum constant representing the icon name option */

        /// <summary>
        /// Set the window title. 
        /// </summary>
        WindowTitle = 5,    /* An enum constant representing the window title option */
    }
}
