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
        int isPressd = 0;
        uint buttonPress1 = 0;
        uint buttonPress2 = 0;
        void Bind()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ip_address);
            IPEndPoint ipep = new IPEndPoint(ip, port);

            try
            {
                socket.Connect(ipep);
            }
            catch { }
            

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
            try
            {
                socket.Send(sendData);
            }
            catch 
            {

                
            }
            
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Bind();
        }
        public void CheckKeyUp(uint keyNumber)
        {
            uint curretntKey = buttonPress2;
            //MessageBox.Show(keyNumber.ToString());
            //MessageBox.Show(curretntKey.ToString());
            if (buttonPress2 != (uint)0) //takdokme gereft bod 
            {
                if (buttonPress2==curretntKey)
                {
                    buttonPress2 = (uint)0;
                }
                else if ((buttonPress1 == curretntKey))
                {
                    buttonPress1 = (uint)buttonPress2;
                    buttonPress2 = (uint)0;
                }
               
                
            
            }
            else if (buttonPress2 == (uint)0)
            {
                buttonPress1 = (uint)0;

            }
            
            lblState.Text = string.Format("you press {0} and {1}", buttonPress1.ToString(), buttonPress2.ToString());
        }
        public void CheckBeforePress(uint keyNumber)
        {
            if (buttonPress1 == 0) //dokmei ghablesh feshar dade ya na
            {
                buttonPress1 = keyNumber;
                isPressd = 1;
            }
            else if(buttonPress1 != 0 && buttonPress1!=keyNumber)
            {
                buttonPress2 = keyNumber;
                isPressd = 1;
            }
            
            lblState.Text = string.Format("you press {0} and {1}", buttonPress1.ToString(), buttonPress2.ToString());
        }
        public void Move()
        {
        // main Dirction
          if(buttonPress1==1 && buttonPress2==0) Send("1");
          if(buttonPress1==2 && buttonPress2==0) Send("2");
          if(buttonPress1==3 && buttonPress2==0) Send("3");
          if(buttonPress1==4 && buttonPress2==0) Send("4");

          if(buttonPress1==1 && buttonPress2==3) Send("5");
          if (buttonPress1==3 && buttonPress2==1) Send("5");

          if(buttonPress1==1 && buttonPress2==4) Send("6");
          if(buttonPress1 == 4 && buttonPress2 == 1) Send("6");

          if (buttonPress1==2 && buttonPress2==3) Send("7");
          if (buttonPress1 == 3 && buttonPress2 == 2) Send("7");

          if (buttonPress1==2 && buttonPress2==4) Send("8");
          if(buttonPress1==4 && buttonPress2==2) Send("8");

          if (buttonPress1 == 0 && buttonPress2 == 0) Send("0");
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up) CheckBeforePress(2);

            //capture down arrow key
            if (e.KeyCode == Keys.Down) CheckBeforePress(1);

            //capture left arrow key
            if (e.KeyCode == Keys.Left) CheckBeforePress(3);

            //capture right arrow key
            if (e.KeyCode == Keys.Right) CheckBeforePress(4);

            if ( e.KeyCode==Keys.Space) Send("0");

            Move();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (isPressd == 1)
            //{
            //    Send("0");
            //    isPressd = 0;
            //}
            if (true)//(buttonPress2 > 0 && buttonPress1 > 0)
            {
                if (e.KeyCode == Keys.Up) CheckKeyUp(2);

                //capture down arrow key
                if (e.KeyCode == Keys.Down) CheckKeyUp(1);

                //capture left arrow key
                if (e.KeyCode == Keys.Left) CheckKeyUp(3);

                //capture right arrow key
                if (e.KeyCode == Keys.Right) CheckKeyUp(4);


                isPressd++;
                Move();
            }

            
           
             
            //lblState.Text = string.Format("you press {0} and {1}", buttonPress1.ToString(), buttonPress2.ToString());



        }
    }
}
