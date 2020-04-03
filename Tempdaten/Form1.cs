using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace Tempdaten
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Event for Serial Data recive


            /*serialPort1.DataReceived += (sender, e) =>
            {
                string data = serialPort1.ReadLine().Trim();
                lboCelsius.Items.Add(data);
            };*/
           
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e) //Serial Data Receive Event
        {
            
            string data = serialPort1.ReadLine().Trim();
            if(lboCelsius.Items.Count == 50)
            {
                lboCelsius.Items.Clear();
                lboFahrenheit.Items.Clear();
            }
            lboCelsius.Items.Add(data);
            double celsius = Convert.ToDouble(data);
            double fahrenheit = (celsius * (9 / 5)) + 32;
            lboFahrenheit.Items.Add(fahrenheit.ToString());
           

        }

        //search all available Ports on Syttem
        private void ListCom()
        {
            //Write all available ports into String
            string[] ports = SerialPort.GetPortNames();

            //Display available Ports
            foreach (string port in ports)
            {
                cboPorts.Items.Add(port);
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cboPorts.SelectedIndex == -1)
            {
                MessageBox.Show("Please select COM-POrt");
            }
            else
            {
                serialPort1.PortName = cboPorts.SelectedItem.ToString();
                if (!serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1.Open();
                        MessageBox.Show("Port opened");
                        panelStatus.BackColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("A error occurred!");
                        MessageBox.Show(ex.ToString());
                        panelStatus.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void closeApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cboPorts_Click(object sender, EventArgs e)
        {
            ListCom(); //List Ports in Menu
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                MessageBox.Show("Port closed");
                panelStatus.BackColor = Color.Red;
            }
            else
            {
                MessageBox.Show("Port not open");
            }
        }

        private void cmdDeleteSelected_Click(object sender, EventArgs e)
        {
            if(lboCelsius.SelectedIndex != -1)
            {
                lboCelsius.Items.Remove(lboCelsius.SelectedIndex);
                lboFahrenheit.Items.Remove(lboCelsius.SelectedIndex);
            }
            else if(lboFahrenheit.SelectedIndex != -1)
            {
                lboCelsius.Items.Remove(lboFahrenheit.SelectedIndex);
                lboFahrenheit.Items.Remove(lboFahrenheit.SelectedIndex);
            }
        }

        private void cmdCalculate_Click(object sender, EventArgs e)
        {
            double ave = 0;
            double min = Convert.ToDouble(lboCelsius.Items[0]);
            double max = Convert.ToDouble(lboCelsius.Items[0]);
            foreach (string s in lboCelsius.Items)
            {
                ave += Convert.ToDouble(s);
                if(Convert.ToDouble(s)> max)
                {
                    max = Convert.ToDouble(s);
                }
                if (Convert.ToDouble(s) < min)
                {
                    min = Convert.ToDouble(s);
                }
            }
            ave = ave / lboCelsius.Items.Count;

            lblOutput.Text = "Average: " + Convert.ToString(ave) + "\n" +
                "Max Value: " + Convert.ToString(max) + "\n" +
                "Min Value: " + Convert.ToString(min) + "\n";

            lboCelsius.Items.Clear();
            lboFahrenheit.Items.Clear();


        }
    }
}
