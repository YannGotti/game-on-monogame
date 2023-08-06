using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Sockets;

namespace MyGame;

public class NetworkManager
    {
        private Socket _clientSocket;
        private byte[] _buffer = new byte[1024];

        public void Connect()
        {
            try
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientSocket.BeginConnect("127.0.0.1", 8080, ConnectCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the server: " + ex.Message);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndConnect(ar);
                Console.WriteLine("Connected to the server");

                // Start receiving data
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during connect callback: " + ex.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = _clientSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // Handle received data here
                    string receivedData = System.Text.Encoding.ASCII.GetString(_buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + receivedData);

                    // Continue receiving data
                    _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during receive callback: " + ex.Message);
            }
        }
    }