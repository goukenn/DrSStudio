using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace IGK.ICore.WinCore.Web
{
    /// <summary>
    /// represent internal web server
    /// </summary>
    class WinCoreWebServer
    {
            private TcpListener m_listener;
            private Thread m_runnerThread;
            private int m_Port;
            public string MimeType;
            public string Response;

            public int Port
            {
                get { return m_Port; }
                set
                {
                    if (m_Port != value)
                    {
                        m_Port = value;
                    }
                }
            }
        /// <summary>
        /// .ctr
        /// </summary>
            public WinCoreWebServer()
            {
            }
        /// <summary>
        /// run server
        /// </summary>
            private void _RunServer()
            {

                try
                {
                    while (true)
                    {
                        try
                        {

                            Socket v_socket = this.m_listener.AcceptSocket();
                            //    IPhpResponse rep =  this.CreateNewReponse(v_socket);
                            //  rep.Run();
                            if (v_socket.Connected)
                            {
                                byte[] data = Encoding.Default.GetBytes(this.Response);
                                SendHeader(v_socket, this.MimeType, data.Length, "200");
                                int e = v_socket.Send(data, 0, data.Length, SocketFlags.None);
                                v_socket.Disconnect(false);
                                v_socket.Close();
                            }
                            //SendData(v_socket, this.Response);

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error : " + ex.Message);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error : " + ex.Message);
                }
                finally
                {
                    Stop();
                }
            }

            private static void SendData(Socket v_socket, string datatoSend)
            {
                byte[] data = Encoding.Default.GetBytes(datatoSend);
                v_socket.Send(data, 0, data.Length, SocketFlags.None);
            }

            private static void SendHeader(Socket v_socket, string mimeType, int length, string statusCode, string protocol = "HTTP/1.1")
            {
                mimeType = string.IsNullOrEmpty(mimeType) ? "text/html" : mimeType;

                StringBuilder sb = new StringBuilder();
                sb.Append(protocol);
                sb.Append(" " + statusCode).AppendLine();
                sb.AppendLine("Server: iGKNET-NETPhpServer");
                sb.Append("Content-Type: ");
                sb.Append(mimeType).Append("; charset=utf-8; ").AppendLine();
                sb.AppendLine("Cache-Control:public;");

                //if (this.ParamsContainsKey("Referer"))
                //    sb.AppendLine("Referer:" + this.getParam("Referer"));

                //if (this.ParamsContainsKey("Connection"))
                //    sb.AppendLine("Connection:" + this.getParam("Connection"));
                //else
                //    sb.AppendLine("Connection:Keep-Alive");
                //if (!string.IsNullOrEmpty(this.m_sessionId))
                //{
                //    sb.AppendLine(string.Format("Set-Cookie: {0};", this.m_sessionId));
                //}
                //sb.Append("Accept-Ranges: bytes").AppendLine();
                //string v_redirectLocation = this.getParam("RedirectLocation");
                //if (v_redirectLocation != null)
                //{
                //    sb.AppendLine("Location: " + v_redirectLocation);
                //}
                sb.Append("Content-Length: ");
                //append 2 line to limit header information
                sb.Append(length).AppendLine().AppendLine();
                Byte[] data = Encoding.Default.GetBytes(sb.ToString());
                //send header
                try
                {
                    var e = v_socket.Send(data, 0, data.Length, SocketFlags.None);//, null, this);// .Send(data);

                    //int h =  m_socket.EndSend(e);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception : " + ex.Message);
                }
                sb.Clear();


            }
            public void Start()
            {
                try
                {
                    this.m_listener = new TcpListener(IPAddress.Any, this.Port > 0 ? this.Port : 80);
                    this.m_listener.Start();
                    this.m_runnerThread = new Thread(_RunServer);
                    this.m_runnerThread.IsBackground = true;
                    this.m_runnerThread.Start();
                }
                catch (Exception ex)
                {
                    CoreMessageBox.Show("Erreur : \n" + ex.Message, "Error");
                }
            }
            public void Stop()
            {
                if (this.m_listener != null)
                {
                    this.m_listener.Stop();
                }
            }

            public Uri Url
            {
                get { return new Uri(string.Format("http://localhost:{0}", this.Port > 0 ? this.Port : 80)); }
            }
        
    }
}
