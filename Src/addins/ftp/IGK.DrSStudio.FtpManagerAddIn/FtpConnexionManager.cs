

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpConnexionManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:FtpConnexionManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    /// <summary>
    /// represent a connexion manager
    /// </summary>
    public class FtpConnexionManager : IDisposable , IFtpConnexionManager 
    {
        private string m_login;
        private string m_pwd;
        private int m_port;
        FtpWebRequest m_request;
        private  string m_uri;
        private string m_currentDir;
        /// <summary>
        /// get the current uri
        /// </summary>
        public string Uri { get { return this.m_uri; } }
        /// <summary>
        /// get the current request
        /// </summary>
        public FtpWebRequest Request{get{return this.m_request ;}}
        private FtpConnexionManager()
        {
            this.m_port = 21;
            this.m_currentDir = string.Empty;
        }
        /// <summary>
        /// create a connexion manager
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <param name="port"></param>
        /// <param name="keepAlive"></param>
        /// <returns></returns>
        public static FtpConnexionManager Create(string uri, string login, string pwd, int port, bool keepAlive)
        {
            FtpWebRequest v_request = (FtpWebRequest ) FtpWebRequest.Create(uri);
            v_request.KeepAlive = keepAlive ;
            v_request.Credentials = new NetworkCredential(login, pwd);
            FtpConnexionManager v_con = null;
            try
            {
                v_request.Method = FtpConnexionOperation.ListDir; //WebRequestMethods.Ftp.ListDirectory;
                //ok if req
                v_request.GetResponse().Close ();
                v_con = new FtpConnexionManager();
                v_con.m_login = login;
                v_con.m_pwd = pwd;
                v_con.m_request = v_request;
                v_con.m_port = port;
                v_con.m_uri = uri;
            }
            catch
            {
                return null;
            }
            finally { 
            }
            return v_con;
        }
        public string GetCurrentDir()
        {
            Connect();
            this.m_request.Method = FtpConnexionOperation.CurrentWorkingDir;
            string v_out = string.Empty ;
            try{
            using (StreamReader sm = new StreamReader(this.m_request.GetResponse().GetResponseStream()))
            {
                v_out = sm.ReadToEnd ();
            }
        }
            catch (Exception ex){
                System.Diagnostics.Debug .WriteLine(ex.Message );
            }
            return v_out ;
        }
        /// <summary>
        /// rename the file
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        /// <returns></returns>
        public bool Rename(string oldname, string newname)
        {
            bool v_result = false;
            string v_cdir = this.m_uri;
            this.m_uri = this.m_uri+ "/" + oldname;
            this.Connect();
            this.m_request.Method = FtpConnexionOperation.Rename;
            this.m_request.RenameTo = newname;
            try
            {
                this.m_request.GetResponse().Close ();
                v_result = true ;
            }
            catch (Exception ex){
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally {
            }
            this.m_uri = v_cdir;
            return v_result ; 
        }
        public void Dispose()
        {
        }
        /// <summary>
        /// cloase this connexion manager
        /// </summary>
        public void Close()
        {
            this.Dispose();
        }
        public string [] ListDir()
        {
            List<string> item = new List<string>();
            try
            {
                //if (this.m_request .Method != FtpConnexionOperation .ListDir )
                this.Connect();
                this.m_request.Method = FtpConnexionOperation.ListDir;
                WebResponse response = this.m_request.GetResponse();
                using (Stream vstream = response.GetResponseStream())
                {
                    if (vstream.CanRead)
                    {
                        using (StreamReader sr = new StreamReader(vstream))
                        {
                            string v_line= sr.ReadLine ();
                            while(v_line !=null)
                            {
                                string[] tab = v_line.Split('/');
                                if (tab.Length > 0)
                                v_line = tab[tab.Length - 1];
                                item.Add(v_line);
                                v_line = sr.ReadLine();
                            }
                        }
                    }
                }
                response.Close();
            }
            catch (Exception Exception){
                Console.WriteLine(Exception.Message);
            }
            return item.ToArray();
        }
        public string[] ListADir(string dir)
        {
            string v_bcuri = this.m_uri;
            List<string> item = new List<string>();
            try
            {
                string v_uri = this.m_uri + "/" + dir;
                //if (this.m_request .Method != FtpConnexionOperation .ListDir )
                FtpWebRequest v_rq = this.Connect(v_uri);
                v_rq.Method = FtpConnexionOperation.ListDirectoryDetails;
                WebResponse response = v_rq.GetResponse();
                using (Stream vstream = response.GetResponseStream())
                {
                    if (vstream.CanRead)
                    {
                        using (StreamReader sr = new StreamReader(vstream))
                        {
                            string v_line = sr.ReadLine();
                            while (v_line != null)
                            {
                                item.Add(v_line);
                                v_line = sr.ReadLine();
                            }
                        }
                    }
                }
                response.Close();
            }
            catch (Exception Exception)
            {
                Console.WriteLine(Exception.Message);
            }
            return item.ToArray();
        }
        public string[] ListADir()
        {
            List<string> item = new List<string>();
            try
            {
                //if (this.m_request .Method != FtpConnexionOperation .ListDir )
                this.Connect();
                this.m_request.Method = FtpConnexionOperation.ListDirectoryDetails;
                WebResponse response = this.m_request.GetResponse();
                using (Stream vstream = response.GetResponseStream())
                {
                    if (vstream.CanRead)
                    {
                        using (StreamReader sr = new StreamReader(vstream))
                        {
                            string v_line = sr.ReadLine();
                            while (v_line != null)
                            {
                                item.Add(v_line);
                                v_line = sr.ReadLine();
                            }
                        }
                    }
                }
                response.Close();
            }
            catch (Exception Exception)
            {
                Console.WriteLine(Exception.Message);
            }
            return item.ToArray();
        }
        public void ListDirAsync(AsyncCallback callback, object state)
        {
            this.Connect();
            this.m_request.Method = FtpConnexionOperation.ListDir;
            IAsyncResult v_result = this.m_request.BeginGetResponse(callback, state);
        }
        public void Mkdir(string dirname)
        {
            string bck_uri = this.m_uri;
            this.m_uri = this.m_uri + "/" + dirname;
            this.Connect();
            this.m_request.Method = FtpConnexionOperation.MkDir;
            this.m_uri = bck_uri;
            try
            {
                this.m_request.GetResponse().Close() ;
            }
            catch (Exception Exception)
            {
                Console.WriteLine(Exception.Message);
            }
        }
        IPAddress remoteAddress;
        IPEndPoint addrEndPoint;
        int Port = 21;
        int StatusCode = 1;
        byte[] Buffer = new Byte[512];
        int Bytes = 0;
        string StatusMessage;
        string Result;
        private void SendCommand(Socket socket, string command)
        {
            Byte[] CommandBytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
            int i = socket.Send(CommandBytes, CommandBytes.Length, 0);
            //read Response
            ReadResponse(socket);
        }
        private void ReadResponse(Socket socket)
        {
            StatusMessage = "";
            Result = SplitResponse(socket);
            StatusCode = int.Parse(Result.Substring(0, 3));
            OnSocketCommandInfoUpdate(EventArgs.Empty);
        }
        private string SplitResponse(Socket socket)
        {
            try
            {
                while (true)
                {
                    Bytes = socket.Receive(Buffer, Buffer.Length, 0); //Number Of Bytes (Count)
                    StatusMessage += Encoding.ASCII.GetString(Buffer, 0, Bytes); //Convert to String
                    if (Bytes < Buffer.Length)  //End Of Response
                        break;
                }
                string[] msg = StatusMessage.Split('\n');
                if (StatusMessage.Length > 2)
                    StatusMessage = msg[msg.Length - 2];  //Remove Last \n
                else
                    StatusMessage = msg[0];
                if (!StatusMessage.Substring(3, 1).Equals(" "))
                    return SplitResponse(socket );
                //for (int i = 0; i < msg.Length - 1; i++)
                //    AppendText(rchLog, "Response : " + msg[i] + "\n", Color.Green);
                return StatusMessage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //AppendText(rchLog, "Status : ERROR. " + ex.Message + "\n", Color.Red);
                socket.Close();
                return "";
            }
        }
        string GetNetworkServer()
        {
            string st = this.m_uri.Substring (6);
            st = st.Split ('/')[0];
            return st;
        }
        public string GetFolderPath()
        {
            string st = this.m_uri.Substring(6);
            StringBuilder sb = new StringBuilder();
            string[] stab = st.Split('/');
            for (int i = 1; i < stab.Length; i++)
            {
                if (i > 1)
                    sb.Append("/");
                sb.Append(stab[i]);
            }
            return sb.ToString() ;
        }
        public bool Chmod(string name, string authorization)
        {
            bool v_result = false;
            Socket v_socket = null;
            try
            {
                v_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                remoteAddress = Dns.GetHostEntry(GetNetworkServer()).AddressList[0];
                //AppendText(rchLog, "Status : IP Address Found ->" + remoteAddress.ToString() + "\n", Color.Red);
                addrEndPoint = new IPEndPoint(remoteAddress, Port);
                //AppendText(rchLog, "Status : EndPoint Found ->" + addrEndPoint.ToString() + "\n", Color.Red);
                v_socket.Connect(addrEndPoint);
                ReadResponse(v_socket);
                SendCommand(v_socket, "USER " + this.m_login);
                if (!(StatusCode == 331 || StatusCode == 230) || StatusCode == 530) //230->Logged in , 331->UserName Okey,Need Password , 530->Login Fail
                {
                    //Something Wrong!
                    //LogOut();
                    //AppendText(rchLog, "Status : " + Result.Substring(4) + "\n", Color.Red);
                    return false ;
                }
                if (StatusCode != 230) //If Not Logged in!
                {
                    SendCommand(v_socket, "PASS " + this.m_pwd);
                    if (!(StatusCode == 230 || StatusCode == 202)) //202 ->Command Not implemented(Password Not Required)
                    {
                        //Something Wrong
                        //LogOut();
                        //AppendText(rchLog, "Status : " + Result.Substring(4) + "\n", Color.Red);
                        return false ;
                    }
                }
                string f = GetFolderPath();
                if (string.IsNullOrEmpty (f))
                SendCommand(v_socket, "SITE CHMOD " + authorization + " " + name);
                else
                    SendCommand(v_socket, "SITE CHMOD " + authorization +" "+f+"/" + name);
                if (this.StatusCode == 200)
                {
                    v_result = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine (ex.Message);
            }
            finally {
                if (v_socket != null)
                    v_socket.Close();
            }
            //string bck_uri = this.m_uri;
            //this.m_uri = this.m_uri + "/" + dirname;
            //this.Connect();
            //this.m_request.Method = FtpConnexionOperation.Chmod;
            //this.m_uri = bck_uri;
            //try
            //{
            //    this.m_request.GetResponse();
            //}
            //catch (Exception Exception)
            //{
            //    Console.WriteLine(Exception.Message);
            //}
            return v_result;
        }
        private void Connect()
        {
            this.Connect(true);            
        }
        public FtpWebRequest Connect(string uri)
        {
           FtpWebRequest rq =  
            _connect(uri, this.m_login, this.m_pwd);
           return rq;
        }
        private void Connect(bool usebinary)
        {
            this.m_request = _connect(this.m_uri, this.m_login, this.m_pwd);
            this.m_request.UseBinary = usebinary;
        }
        private static FtpWebRequest _connect(string uri, string login, string pwd)
        {
            FtpWebRequest v_request = (FtpWebRequest)FtpWebRequest.Create(uri);           
            v_request.Credentials = new NetworkCredential(login, pwd);        
            return v_request;
        }
        public bool MakeDirs(params string[] dirname)
        {
            string bck_uri = this.m_uri;
            bool v_result = false;
            foreach (string item in dirname)
	        {
                this.m_uri = bck_uri + "/" + item;
                this.Connect();
                this.m_request.Method = FtpConnexionOperation.MkDir;
                this.m_request.KeepAlive = false;
                try
                {
                    this.m_request.GetResponse().Close ();
                }
                catch (Exception Exception)
                {
                    Console.WriteLine(Exception.Message);
                    v_result = false;
                }
	        }
            this.m_uri = bck_uri;
            return v_result;
        }
        public bool DeleteDirs(params string[] dirname)
        {
            string bck_uri = this.m_uri;
            bool v_result = false;
            foreach (string item in dirname)
            {
                this.m_uri = bck_uri + "/" + item;
                this.Connect();
                this.m_request.Method = FtpConnexionOperation.RmDir;
                this.m_request.KeepAlive = false;
                try
                {
                    this.m_request.GetResponse().Close();
                }
                catch (Exception Exception)
                {
                    Console.WriteLine(Exception.Message);
                    v_result = false;
                }
            }
            this.m_uri = bck_uri;
            return v_result;
        }
        public bool DeleteFiles(params string[] files)
        {
            string bck_uri = this.m_uri;
            bool v_result = false;
            foreach (string item in files)
            {
                this.m_uri = bck_uri + "/" + item;
                this.Connect();
                this.m_request.Method = FtpConnexionOperation.DeleteFile;
                this.m_request.KeepAlive = false;
                try
                {
                    this.m_request.GetResponse().Close();
                    v_result = true;
                }
                catch (Exception Exception)
                {
                    Console.WriteLine(Exception.Message);
                    v_result = false;
                }
            }
            //restore uri
            this.m_uri = bck_uri;
            return v_result;
        }
        public bool UploadFile(string sourcefile, string destination)
        {
            string bck_uri = this.m_uri;
            bool v_result = false;
            this.m_uri = bck_uri + "/" + destination;
            this.Connect();
            this.m_request.Method = FtpConnexionOperation.UploadFile;
            this.m_request.KeepAlive = false;
            this.m_request.UsePassive = true;
            this.m_request.UseBinary = true;
            using (BinaryReader sreader = new BinaryReader(File.Open(sourcefile , FileMode.Open)))
            {
                this.m_request.ContentLength = sreader.BaseStream.Length;
                try
                {
                    Byte[] v_data = new byte[2048];
                    int length = 0;
                    using (BinaryWriter swriter = new BinaryWriter(this.m_request.GetRequestStream()))
                    {
                        while ((length = sreader.Read(v_data, 0, v_data.Length)) > 0)
                        {
                            swriter.Write(v_data, 0, length);
                        }
                    }
                }
                catch (Exception Exception)
                {
                    Console.WriteLine(Exception.Message);
                    v_result = false;
                }
            }
            this.m_uri = bck_uri;
            return v_result;
        }
        /// <summary>
        /// upload file to current directory
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool UploadFile(string file)
        {
            return this.UploadFile (file , Path.GetFileName (file));
        }
        public bool DownloadFile(string file, string destination)
        {
            bool v_result = false;
            using (BinaryWriter sw = new BinaryWriter(File.Create(destination)))
            {
                FtpWebRequest v_req =  Connect(this.m_uri + "/" + file);
                v_req.Method = FtpConnexionOperation.DownloadFile;
                v_req.KeepAlive = true;
                v_req.UseBinary = true ;
                v_req.UsePassive = true ;
                Byte[] v_data = new byte[512];
                int v_count = 0;
                try
                {
                    using (BinaryReader binR = new BinaryReader(v_req.GetRequestStream()))
                    {
                        while ((v_count = binR.Read(v_data, 0, v_data.Length)) > 0)
                        {
                            sw.Write(v_data, 0, v_count);
                        }
                    }
                    v_result = true;
                }
                catch (Exception Exception){
                    System.Diagnostics.Debug.WriteLine(Exception.Message);
                }
            }
            return v_result ;
        }
        private Socket _SocketConnect()
        {
            Socket v_FTPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            remoteAddress = Dns.GetHostEntry(GetNetworkServer()).AddressList[0];
            //AppendText(rchLog, "Status : IP Address Found ->" + remoteAddress.ToString() + "\n", Color.Red);
            addrEndPoint = new IPEndPoint(remoteAddress, Port);
            //AppendText(rchLog, "Status : EndPoint Found ->" + addrEndPoint.ToString() + "\n", Color.Red);
            v_FTPSocket.Connect(addrEndPoint);
            ReadResponse(v_FTPSocket);
            SendCommand(v_FTPSocket, "USER " + this.m_login);
            if (!(StatusCode == 331 || StatusCode == 230) || StatusCode == 530) //230->Logged in , 331->UserName Okey,Need Password , 530->Login Fail
            {
                //Something Wrong!
                //LogOut();
                //AppendText(rchLog, "Status : " + Result.Substring(4) + "\n", Color.Red);
                return null;
            }
            if (StatusCode != 230) //If Not Logged in!
            {
                SendCommand(v_FTPSocket, "PASS " + this.m_pwd);
                if (!(StatusCode == 230 || StatusCode == 202)) //202 ->Command Not implemented(Password Not Required)
                {
                    //Something Wrong
                    //LogOut();
                    //AppendText(rchLog, "Status : " + Result.Substring(4) + "\n", Color.Red);
                    return null;
                }
            }
            return v_FTPSocket;
        }
        /// <summary>
        /// change current directory . this command uses socket
        /// </summary>
        /// <param name="newDir"></param>
        /// <returns></returns>
        public Socket  ChangeDir(string newDir)
        {
            Socket socket = _SocketConnect();
            SendCommand(socket, "PWD "+newDir);
            Console.WriteLine (PrintWD(socket));
            return socket;
        }
        public string PrintWD(Socket socket)
        {
            SendCommand(socket, "PWD");
             return SocketStatusMessage;//.Split('\t')[1];
        }
        public bool ChangeDir(Socket socket, string newDir)
        {
            if (socket != null)
            {
                SendCommand(socket, "CWD " + newDir);
                return (this.StatusCode == 250);
            }
            return false;
        }
        /// <summary>
        /// create a new socket for operation
        /// </summary>
        /// <returns></returns>
        public Socket CreateNewSocket()
        {
            return this._SocketConnect();
        }
        public event EventHandler SocketCommandInfoUpdate;
        public int SocketStatusCode
        {
            get { return this.StatusCode; }
        }
        public string SocketStatusMessage
        {
            get { return this.StatusMessage; }
        }
        void OnSocketCommandInfoUpdate(EventArgs e)
        {
            if (this.SocketCommandInfoUpdate != null)
                this.SocketCommandInfoUpdate(this, e);
        }
    }
}

