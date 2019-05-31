using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using System.Net;
using System.Net.Sockets;

namespace ClientForPC
{
    public partial class Form1 : Form
    {
        String ip_address = "192.168.4.1";
        int port = 23;
        Socket socket;

        void Bind()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ip_address);
            IPEndPoint ipep = new IPEndPoint(ip, port);

            socket.Connect(ipep);
        }

        void Exit()
        {
            socket.Close();
            socket = null;
        }

        void Send(String msgData)
        {
            if (socket == null)
                return;

            byte[] sendData = Encoding.ASCII.GetBytes(msgData);
            socket.Send(sendData);
        }

        String Receive()
        {
            byte[] receiveData = new byte[256];
            socket.Receive(receiveData);
            String result = Encoding.UTF8.GetString(receiveData);

            return result;
        }
        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            KeyUp += new KeyEventHandler(Form1_KeyUp);
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //capture up arrow key
        //    if (keyData == Keys.Up)
        //    {
        //        lblState.Text = "You pressed Up arrow key";

        //        return true;
        //    }
        //    //capture down arrow key
        //    if (keyData == Keys.Down)
        //    {
        //        lblState.Text = "You pressed Down arrow key";
        //        return true;
        //    }
        //    //capture left arrow key
        //    if (keyData == Keys.Left)
        //    {
        //        lblState.Text = "You pressed Left arrow key";
        //        return true;
        //    }
        //    //capture right arrow key
        //    if (keyData == Keys.Right)
        //    {
        //        lblState.Text = "You pressed Right arrow key";
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            Bind();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up)
            {
                
                Send("2");
                lblState.Text = "You pressed Up arrow key";
            }
            //capture down arrow key
            if (e.KeyCode == Keys.Down)
            {
                
                Send("1");
                lblState.Text = "You pressed Down arrow key";
            }
            //capture left arrow key
            if (e.KeyCode == Keys.Left)
            {
                
                Send("3");
                lblState.Text = "You pressed Left arrow key";
            }
            //capture right arrow key
            if (e.KeyCode == Keys.Right)
            {
                
                Send("4");
                lblState.Text = "You pressed Right arrow key";
            }
            if ( e.KeyCode==Keys.Space)
            {
                Send("0");
                lblState.Text = "salam";
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
            Send("0");
            lblState.Text = "Normal State";
        }
    }
}
