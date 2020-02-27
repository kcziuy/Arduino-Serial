using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino
{
    public partial class kartoshka : Form
    {
        int port = 3;
        private SerialPort _serialPort;

        public kartoshka()
        {
            InitializeComponent();
        }

        private async Task ReadDataAsync()
        {
            

            try
            {
                if (_serialPort == null)
                    _serialPort = new SerialPort("COM" + port, 9600, Parity.None, 8, StopBits.One);

                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                Console.Write("Open...");
            }
            catch (Exception ex)
            {
                ClosePort();
                MessageBox.Show(ex.ToString());
            }

            while (true)
            {
                byte[] buffer = new byte[4096];
                Task<int> readStringTask = _serialPort.BaseStream.ReadAsync(buffer, 0, 100);

                if (!readStringTask.IsCompleted)
                    Console.WriteLine("Waiting...");

                int bytesRead = await readStringTask;

                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                NUM.Text = data;
            }
        }

        private async Task startasync() {

            await ReadDataAsync();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClosePort();
        }

        private void ClosePort()
        {
            if (_serialPort == null) return;

            if (_serialPort.IsOpen)
                _serialPort.Close();

            _serialPort.Dispose();

            _serialPort = null;

            Console.WriteLine("Close");
        }
        public kartoshka rForm() {
            return this;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            numericUpDown1.Visible = false;
            port = Convert.ToInt32(numericUpDown1.Value);
            try
            {
                Task s = startasync();
            }
            catch (Exception e121) { }
        }

        private void NUM_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
