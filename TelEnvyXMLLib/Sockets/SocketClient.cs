
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TelEnvyXmlLib.Common;
using TelEnvyXmlLib.Enums;
using TelEnvyXmlLib.EventArgs;

namespace TelEnvyXmlLib.Sockets
{


    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A socket client. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <seealso cref="T:System.ComponentModel.Component"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class SocketClient : Component
    {
        /// <summary>   Occurs when Socket Disconnected. </summary>
        public event EventHandler<SocketEventArgs> SocketDisconnected;  /* Occurs when Socket Disconnected. */
        
        /// <summary>   Occurs when Socket Connected. </summary>
        public event EventHandler<SocketEventArgs> SocketConnected; /* Occurs when Socket Connected. */

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the total number of bytes read. </summary>
        ///
        /// <value> The total number of bytes read. </value>
        ///-------------------------------------------------------------------------------------------------

        public int totalBytesRead { get; set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Disconnects this SocketClient. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Disconnect()
        {

            client.Disconnect(false);
            totalBytesRead = 0;
            OnSocketDisconnected(this, new SocketEventArgs() { Connected = false});
            if (TransactionDictionary.ContainsKey(TransactionKey))  TransactionDictionary.Remove(TransactionKey);
        }
         /// <summary>  Dictionary of transactions. </summary>
         public static Dictionary<string, string> TransactionDictionary = new Dictionary<string, string>(); /* Dictionary of transactions */

        /// <summary>   The transaction key. </summary>
        string transactionKey;  /* The transaction key */

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the transaction key. </summary>
        ///
        /// <value> The transaction key. </value>
        ///-------------------------------------------------------------------------------------------------

        public string TransactionKey
        {
            get { return transactionKey; }
            set
            {
                transactionKey = value;
            }
        }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets buffer size. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   The buffer size. </returns>
        ///-------------------------------------------------------------------------------------------------

        public int getBufferSize()
        {
            if (client != null) return client.ReceiveBufferSize;
            return -1;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets socket receive buffers. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="bufferSize">   Size of the buffer.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SetSocketReceiveBuffers(int bufferSize)
        {
            ReceiveBuffers = bufferSize;
          
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets socket send buffers. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="bufferSize">   Size of the buffer.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SetSocketSendBuffers(int bufferSize)
        {
            SendBuffers = bufferSize;

        }
        /// <summary>   True to waiting for data. </summary>
        bool waitingForData;    /* True to waiting for data */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the waiting for data. </summary>
        ///
        /// <value> True if waiting for data, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool WaitingForData
        {
            get
            {
                lock (wfdObj)
                {
                    return waitingForData;
                }
            }
            set
            {
                lock (wfdObj)
                {
                    waitingForData = value;
                }
            }
        }
        /// <summary>   The wfd object. </summary>
        readonly object wfdObj = new object();  /* The wfd object */
        /// <summary>   The record object. </summary>
        readonly object RecObj = new object();  /* The record object */
        #region Events

        /// <summary>   Occurs when Data In. </summary>
        public event EventHandler<EventArgs.ClientDataInEventArgs> DataIn;  /* Occurs when Data In. */
        /// <summary>   Occurs when Error. </summary>
        public event EventHandler<TelEnvyXmlLib.EventArgs.ClientErrorEventArgs> Error;  /* Occurs when Error. */
        /// <summary>   Occurs when Ready To Send. </summary>
        public event EventHandler<TelEnvyXmlLib.EventArgs.ClientReadyToSendEventArgs> ReadyToSend;  /* Occurs when Ready To Send. */
        /// <summary>   Occurs when Connection Status. </summary>
        public event EventHandler<TelEnvyXmlLib.EventArgs.ClientConnectionStatusEventArgs> ConnectionStatus;    /* Occurs when Connection Status. */
        #region OnConnectionStatus

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the socket event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnSocketConnected(object sender, SocketEventArgs e)
        {
            EventHandler<SocketEventArgs> handler = SocketConnected;
            if (handler != null)
                handler(sender, e);
        }

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the socket event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="sender">   Source of the event.</param>
        /// <param name="e">        Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnSocketDisconnected(object sender, SocketEventArgs e)
        {

            EventHandler<SocketEventArgs> handler = SocketDisconnected;
            if (handler != null)
                handler(sender, e);
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the client connection status event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ea">   Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        public virtual void OnConnectionStatus(ClientConnectionStatusEventArgs ea)
        {
            EventHandler<TelEnvyXmlLib.EventArgs.ClientConnectionStatusEventArgs> handler = ConnectionStatus;
            if (handler != null)
                handler(null/*this*/, ea);
        }
        #endregion

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the client ready to send event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ea">   Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        public virtual void OnReadyToSend(ClientReadyToSendEventArgs ea)
        {
            EventHandler<TelEnvyXmlLib.EventArgs.ClientReadyToSendEventArgs> handler = ReadyToSend;
            if (handler != null)
                handler(null/*this*/, ea);
        }
        #endregion



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the client error event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ea">   Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        public virtual void OnError(ClientErrorEventArgs ea)
        {
            EventHandler<TelEnvyXmlLib.EventArgs.ClientErrorEventArgs> handler = Error;
            if (handler != null)
                handler(null/*this*/, ea);
        }
        
        #region OnDisconnected
       
        #endregion

        #region OnConnected

  
        #endregion


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Raises the event args. client data in event. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ea">   Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        public virtual void OnDataIn(EventArgs.ClientDataInEventArgs ea)
        {
            EventHandler<EventArgs.ClientDataInEventArgs> handler = DataIn;
            if (handler != null)
                handler(null/*this*/, ea);
        }

       
       
        /// <summary>   The ol. </summary>
        string eOL; /* The ol */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the EOL. </summary>
        ///
        /// <value> The EOL. </value>
        ///-------------------------------------------------------------------------------------------------

        public string EOL
        {
            get { return eOL; }
            set
            {
                eOL = value;
            }
        }

        /// <summary>   ManualResetEvent instances signal completion. </summary>
        private int _BytesSent; /* The bytes sent */
        /// <summary>   True if connected. </summary>
        private bool _Connected;    /* True if connected */
        /// <summary>   The connect done. </summary>
        private static ManualResetEvent connectDone =   /* The connect done */
            new ManualResetEvent(false);
        /// <summary>   The send done. </summary>
        private static ManualResetEvent sendDone =  /* The send done */
            new ManualResetEvent(false);
        /// <summary>   The receive done. </summary>
        private static ManualResetEvent receiveDone =   /* The receive done */
            new ManualResetEvent(false);



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the connected. </summary>
        ///
        /// <value> True if connected, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Connected
        {
            get { return _Connected; }
            set
            {
                socketConnectStatus bSetVale = ConnectClient(value);
                if (bSetVale == socketConnectStatus.Connected)
                    OnSocketConnected(this, new SocketEventArgs() { Connected = true }); 
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the bytes sent. </summary>
        ///
        /// <value> The bytes sent. </value>
        ///-------------------------------------------------------------------------------------------------

        public int BytesSent
        {
            get { return _BytesSent; }
            set
            {
                using (Mutex m = new Mutex())
                {
                    try
                    {
                        m.WaitOne();
                        _BytesSent = value;
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        m.ReleaseMutex();
                    }
                }
            }
        }

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets socket client. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   The socket client. </returns>
        ///-------------------------------------------------------------------------------------------------

        public SocketClient getSocketClient()
        {
            return this;
        }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the client. </summary>
        ///
        /// <value> The client. </value>
        ///-------------------------------------------------------------------------------------------------

        public Socket client { get; private set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the name of the module log. </summary>
        ///
        /// <value> The name of the module log. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ModuleLogName { get; private set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets module log name. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="modLogName">   Name of the modifier log.</param>
        ///-------------------------------------------------------------------------------------------------

        public void SetModuleLogName(string modLogName)
        {
            ModuleLogName = modLogName;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the port. </summary>
        ///
        /// <value> The port. </value>
        ///-------------------------------------------------------------------------------------------------

        public int port { get; private set; }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets host port. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="vPort">    The port.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setHostPort(int vPort)
        {
            port = vPort;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets host name. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ipAddress">    The IP address.</param>
        ///-------------------------------------------------------------------------------------------------

        public void setHostName(string ipAddress)
        {
            hostName = ipAddress;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the name of the host. </summary>
        ///
        /// <value> The name of the host. </value>
        ///-------------------------------------------------------------------------------------------------

        public string hostName { get; private set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the accept data. </summary>
        ///
        /// <value> True if accept data, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool AcceptData { get; internal set; }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this SocketClient is send complete. </summary>
        ///
        /// <value> True if this SocketClient is send complete, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsSendComplete { get; private set; }
        /// <summary>   The receive buffers. </summary>
        int receiveBuffers; /* The receive buffers */

    

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the receive buffers. </summary>
        ///
        /// <value> The receive buffers. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ReceiveBuffers
        {
            get { return receiveBuffers; }
            set
            {
                receiveBuffers = value;
            }
        }
        /// <summary>   The send buffers. </summary>
        int sendBuffers;    /* The send buffers */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the send buffers. </summary>
        ///
        /// <value> The send buffers. </value>
        ///-------------------------------------------------------------------------------------------------

        public int SendBuffers
        {
            get { return sendBuffers; }
            set
            {
                sendBuffers = value;
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Sockets.SocketClient class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public SocketClient()
        {

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process this SocketClient. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        void Process()
        {
            //StringBuilder sbx = new StringBuilder();
            //foreach (string item in SocketQueue)
            //{
            //    sbx.Append(sbx.ToString());
            //}

        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Sockets.SocketClient class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="container">    The container.</param>
        ///-------------------------------------------------------------------------------------------------

        public SocketClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

  

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determines if we can shutdown client. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool ShutdownClient()
        {
            bool bError = false;
            try
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                TransactionDictionary.Clear();
                bError = false;
            }
            catch (Exception ex)
            {
                bError = true;
            }
            finally
            {

            }
            return bError;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Connects the client. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="value">    True to value.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        private socketConnectStatus ConnectClient(bool value)
        {
            switch (value)
            {
                case true:
                    _Connected = ConnectClient();
                    return socketConnectStatus.Connected;

                case false:
                    ShutdownClient();
                    return socketConnectStatus.Disconnected;

            }
            throw new NotImplementedException();
        }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Connects the client. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool ConnectClient()
        {
            if (port == 0) return false;
            if (client != null) ShutdownClient();
            // Connect to a remote device.  
            try
            {
                IPAddress ipAddress = IPAddress.Parse(hostName); //ipHostInfo.AddressList[0].par;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, SendBuffers);
                client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, ReceiveBuffers);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                if (connectDone == null) connectDone = new ManualResetEvent(false);
                connectDone.WaitOne();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return false;
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Async callback, called on completion of receive callback. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ar">   The result of the asynchronous operation.</param>
        ///-------------------------------------------------------------------------------------------------

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  


                lock (RecObj)
                {
                    int bytesRead = client.EndReceive(ar);
                   // Console.WriteLine("Receive Called {0}", bytesRead);
                    StaticLogger.LogDebug(ModuleLogName, string.Format("Receive Called {0} for Transaction {1}", bytesRead, TransactionKey));
                    totalBytesRead += bytesRead;
                    if (bytesRead > 0)
                    {


                        // There might be more data, so store the data received so far.  
                        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                        state.TransactionKey = transactionKey;

                        if (Encoding.ASCII.GetString(state.buffer, 0, bytesRead).Contains(EOL))
                        {
                            string test = state.sb.ToString();
                            TransactionDictionary.Add(TransactionKey, state.sb.ToString());
                            StaticLogger.LogInfo(ModuleLogName, string.Format("Total Bytes Received {0} for Transaction {1}", totalBytesRead, TransactionKey));
                            WaitingForData = false;
                            // Signal that all bytes have been received.  
                            receiveDone.Set();
                        }
                        else
                        {
                            // Get the rest of the data.  
                            client.BeginReceive(state.buffer, 0, state.BufferSize, 0,
                                new AsyncCallback(ReceiveCallback), state);
                        }
                    }
                    else
                    {
                        // All the data has arrived; put it in response.  
                        if (state.sb.Length > 1)
                        {
                            //     TransactionDictionary.Add(TransactionKey, state.sb.ToString());
                        }

                        WaitingForData = false;                 // this release the SendData 
                                                                // Signal that all bytes have been received.  


        
                         receiveDone.Set();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Send this message. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="data"> The data.</param>
        ///-------------------------------------------------------------------------------------------------

        public void Send(String data)
        {
            if (sendDone == null) sendDone = new ManualResetEvent(false);
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);


            WaitingForData = true;              // toggle the wait for data until all data are returned before
                                                // executing the next directive
                                                // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Send this message. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="data"> The data.</param>
        ///-------------------------------------------------------------------------------------------------

        public void Send(byte[] data)
        {
            if (sendDone == null) sendDone = new ManualResetEvent(false);
            // Begin sending the data to the remote device.  
            client.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), client);

        }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Async callback, called on completion of send callback. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ar">   The result of the asynchronous operation.</param>
        ///-------------------------------------------------------------------------------------------------

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                StaticLogger.LogTrace(ModuleLogName, string.Format("Sent {0} bytes to server for Transaction {1}.", bytesSent,TransactionKey));

                // Signal that all bytes have been sent.  
            
                sendDone.Set();
                BytesSent = bytesSent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //public static int Main(String[] args)
        //{
        //    StartClient();
        //    return 0;
        //}

   

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Async callback, called on completion of connect callback. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="ar">   The result of the asynchronous operation.</param>
        ///-------------------------------------------------------------------------------------------------

        public void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                //Console.WriteLine("Socket connected to {0}",
                //    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  

                connectDone.Set();
                OnSocketConnected(this, new SocketEventArgs() { Connected = true });
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //Onsoc(new ClientConnectedEventArgs("Exception", 402)); // 402 represents the ConnectCallBack 
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Receives this SocketClient. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Receive()
        {
            if (receiveDone == null) receiveDone = new ManualResetEvent(false);
            try
            {
                this.WaitingForData = true;
                // Create the state object.  
                StateObject state = new StateObject() { workSocket = client };
                state.SetReceiveBufferSize(getBufferSize());

                // Begin receiving the data from the remote device.  

                client.BeginReceive(state.buffer, 0, state.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }



    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A client state. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ClientState
    {
        /// <summary>   The net stream. </summary>
        private NetworkStream netStream;    /* The net stream */
        /// <summary>   The response. </summary>
        private StringBuilder response; /* The response */
        /// <summary>   The total bytesreceived. </summary>
        private int totalBytesreceived = 0; /* The total bytesreceived */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.Sockets.ClientState class. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="netStream">    .</param>
        /// <param name="byteBuffer">   .</param>
        ///-------------------------------------------------------------------------------------------------

        public ClientState(NetworkStream netStream, byte[] byteBuffer)
        {
            this.netStream = netStream;
            this.ByteBuffer = byteBuffer;
        }

 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Net stream. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   A NetworkStream. </returns>
        ///-------------------------------------------------------------------------------------------------

        public NetworkStream NetStream()
        {
            return netStream;
        }
        /// <summary>   Buffer for byte data. </summary>
        byte[] byteBuffer;  /* Buffer for byte data */

       

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the buffer for byte data. </summary>
        ///
        /// <value> A buffer for byte data. </value>
        ///-------------------------------------------------------------------------------------------------

        public byte[] ByteBuffer
        {
            get { return byteBuffer; }
            set
            {
                byteBuffer = value;
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Appends a response. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="response"> .</param>
        ///-------------------------------------------------------------------------------------------------

        public void AppendResponse(string response)
        {
            this.response.Append(response);
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds to the total bytes. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="count">    Number of.</param>
        ///-------------------------------------------------------------------------------------------------

        public void AddToTotalBytes(int count)
        {
            totalBytesreceived += count;
        }


    }
}
