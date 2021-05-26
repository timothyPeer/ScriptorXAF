using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelEnvyXmlLib.Sockets
{
    partial class SocketClient
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (connectDone != null)
                {
                    connectDone.Dispose();
                    connectDone = null;
                }
                if (sendDone != null)
                {
                    sendDone.Dispose();
                    sendDone = null;
                }
                if (receiveDone != null)
                {
                    receiveDone.Dispose();
                    receiveDone = null;
                }
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();


        }
    }
}
