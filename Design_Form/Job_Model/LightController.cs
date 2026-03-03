using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;

namespace Design_Form.Job_Model
{
	public class LightController 
	{
		private SerialPort _port;
		public LightController(string portName, int baudrate)
		{
			try
			{
                _port = new SerialPort(portName, baudrate, Parity.None, 8, StopBits.One);
                _port.Handshake = Handshake.None;
                _port.Encoding = Encoding.ASCII;
               // _port.Open();
            }
			catch (Exception)
			{
				MessageBox.Show("Cannot open light controller on " + portName);
                throw;
			}
			
		}
		public void SetAllChannels(int[] values)
		{
			if (values.Length != 16)
				throw new ArgumentException("Controller requires 14 channels");

			byte[] frame = BuildFrame(values);
			_port.Write(frame, 0, frame.Length);
		}
		private byte[] BuildFrame(int[] values)
		{
			// STX + "CALLS" + data + ETX
			var buffer = new byte[1 + 5 + 16 * 3 + 1];
			int index = 0;

			buffer[index++] = 0x02; // STX

			// CALLS
			var header = Encoding.ASCII.GetBytes("CALLS");
			Array.Copy(header, 0, buffer, index, header.Length);
			index += header.Length;

			// Channel values (ASCII 3 digits)
			foreach (int v in values)
			{
				int value = v;
				if (value < 0) value = 0;
				if (value > 255) value = 255;
				string s = value.ToString("D3");
				var bytes = Encoding.ASCII.GetBytes(s);
				Array.Copy(bytes, 0, buffer, index, 3);
				index += 3;
			}

			buffer[index] = 0x03; // ETX
			return buffer;
		}
		
	}
}
