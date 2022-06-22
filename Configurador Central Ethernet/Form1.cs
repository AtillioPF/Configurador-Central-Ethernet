using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Configurador_Central_Ethernet
{
    public partial class Form1 : Form
    {
        bool permission = false;
        const int PORT_NUMBER = 2126;
        UdpClient Client = new UdpClient(PORT_NUMBER);
        string data = "";
        IAsyncResult ar_ = null;
        IPAddress[] IP_MACHINE_0 = new IPAddress[10];
        String  Rec_msg, Send_Data;
        String[] ip_broadcast = new String[10];
        IPEndPoint ip;
        
        public Form1()
        {
            InitializeComponent();
            this.tabControl1.SelectedTab = connection_page;  
               
        }

        private void LoadForm1(object sender, EventArgs e)
        {
            int j = 0;
            String name = Dns.GetHostName();
            IPHostEntry host = Dns.GetHostEntry(name);
            for(int a = 0; a < host.AddressList.Length; a++)
            {
                if(IsAddressValid(host.AddressList[a].ToString()))
                {
                    IP_MACHINE_0[j] = host.AddressList[a];
                    Byte[] bytes = IP_MACHINE_0[j].GetAddressBytes();
                    ip_broadcast[j] = bytes[0].ToString() + "." + bytes[1].ToString() + "." + bytes[2].ToString() + ".255";
                    richTextBox1.AppendText(ip_broadcast[j]+ "\n");
                    j++;
                }
            }
            
            StartListening();
        }
             

        private void StartListening()
        {
            ar_ = Client.BeginReceive(Receive, new object());
        }

        private void Receive(IAsyncResult ar)
        {            
            ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            byte[] bytes = Client.EndReceive(ar, ref ip);
            Rec_msg = Encoding.ASCII.GetString(bytes);
            richTextBox1.AppendText(Rec_msg);
            StartListening();
        }

        private void change1(object sender, EventArgs e)
        {
            if (Rec_msg == "ok")
            {
                try
                {
                    string ip1 = ip.Address.ToString();
                    IPHostEntry host = Dns.GetHostByAddress(ip.Address);
                    string hostname = host.HostName.ToString();
                    IP_list.Items.Add(ip1 + "\t" + hostname);
                }
                catch
                {
                    string ip1 = ip.Address.ToString();
                    IP_list.Items.Add(ip1 + "\t" + "Unknown Host");
                }
                
            }
            else if (permission)
            {
                string ip1 = ip.Address.ToString();                
                if (ip1==ip_placa.Text)
                {                  
                        CompleteData(Rec_msg);                    
                }
            }
            
        }

        public void Send(string message, String Ip)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip1 = new IPEndPoint(IPAddress.Parse(Ip), PORT_NUMBER);
            try{
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                client.Send(bytes, bytes.Length, ip1);
            }catch (Exception ex) { /* do nothing */}            
            client.Close();
            richTextBox2.AppendText(message + "\n");
        }

        private void search_data_Click(object sender, EventArgs e)
        {
            foreach(String ip in ip_broadcast)
            {
                if (ip != null)
                {
                    Send("broadcast_teste", ip);
                }
            }
            

        }

        private void Clean_dataclick(object sender, EventArgs e)
        {
            this.IP_list.Items.Clear();
            permission = false;

        }

        // confirmação para mudar de pagina
        private void confirm_selection_click(object sender, EventArgs e)
        {
            if(IP_list.SelectedItem != null)
            {
                permission = true;
                this.tabControl1.SelectedTab = config_page;
                String item = IP_list.SelectedItem.ToString();
                int a = item.IndexOf("\t");
                int b = item.Length - a; 
                ip_placa.Text =  item.Substring(0,a);
                hostname.Text = item.Substring(a+1, b-1);
                System.Threading.Thread.Sleep(300);
                Send("GET", ip_placa.Text);
            }
            
        }

        public void clean_config_data()
        {
            ip_concentrador.Text = "";
            porta_concentrador.Text = "";
            bico0a.Text = "";
            bico1a.Text = "";
            bico2a.Text = "";
            bico3a.Text = "";
            bico4a.Text = "";
            bico5a.Text = "";
            bico6a.Text = "";
            bico7a.Text = "";
            bico8a.Text = "";
            bico9a.Text = "";
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Check Credentials Here  

            if ((permission == true) && (tabControl1.SelectedTab == config_page))
            {
                tabControl1.SelectedTab = config_page;
            }
            else if ((permission == false) && (tabControl1.SelectedTab == config_page))
            {
                tabControl1.SelectedTab = connection_page;
                MessageBox.Show("Selecione primeiro uma placa na rede.");

            }
        }


        // Configuration Tab

        private void bico0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }
       
        private void bico1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }
       
        private void bico2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }

        private void bico9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {
                return;
            }

            e.Handled = true;
        }


        private void enviar_dados_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ip_concentrador.Text) && IsAddressValid(ip_concentrador.Text))
            {
                Send_Data = ip_concentrador.Text;
            }
            else
            {
                MessageBox.Show("Endereço IP Inválido!");
                return;
            }

            if (!string.IsNullOrEmpty(porta_concentrador.Text) && ValidateText(porta_concentrador.Text))
            {
                Send_Data = Send_Data + '-' + porta_concentrador.Text;
            }
            else
            {
                MessageBox.Show("Porta Inválida!");
                return;
            }

            if (!string.IsNullOrEmpty(bico0.Text) )
            {
                Send_Data = Send_Data + '-' + bico0.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);
                return;
            }

            if (!string.IsNullOrEmpty(bico1.Text) )
            {
                Send_Data = Send_Data + '-' + bico1.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);                
                return;
            }

            if (!string.IsNullOrEmpty(bico2.Text) )
            {
                Send_Data = Send_Data + '-' + bico2.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);               
                return;
            }

            if (!string.IsNullOrEmpty(bico3.Text) )
            {
                Send_Data = Send_Data + '-' + bico3.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);               
                return;
            }

            if (!string.IsNullOrEmpty(bico4.Text) )
            {
                Send_Data = Send_Data + '-' + bico4.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);                
                return;
            }

            if (!string.IsNullOrEmpty(bico5.Text) )
            {
                Send_Data = Send_Data + '-' + bico5.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);                
                return;
            }

            if (!string.IsNullOrEmpty(bico6.Text) )
            {
                Send_Data = Send_Data + '-' + bico6.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);                
                return;
            }

            if (!string.IsNullOrEmpty(bico7.Text) )
            {
                Send_Data = Send_Data + '-' + bico7.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);               
                return;
            }

            if (!string.IsNullOrEmpty(bico8.Text) )
            {
                Send_Data = Send_Data + '-' + bico8.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);               
                return;
            }

            if (!string.IsNullOrEmpty(bico9.Text) )
            {
                Send_Data = Send_Data + '-' + bico9.Text;
            }
            else
            {
                Send(Send_Data, ip_placa.Text);                
                return;
            }


            Send(Send_Data, ip_placa.Text);            
        }

        public void CompleteData(string message)
        {
           
            int a = message.IndexOf("-");
            ip_concentrador.Text = message.Substring(0, a);
            message = message.Substring(a + 1);
            a = message.IndexOf("-");
            porta_concentrador.Text = message.Substring(0, a);
            message = message.Substring(a + 1);
            a = message.IndexOf("-");
            if (a > 0)
            {
                bico0a.Text = message.Substring(0, a);
                message = message.Substring(a + 1);
                a = message.IndexOf("-");
                if (a > 0)
                {
                    bico1a.Text = message.Substring(0, a);
                    message = message.Substring(a + 1);
                    a = message.IndexOf("-");
                    if (a > 0)
                    {
                        bico2a.Text = message.Substring(0, a);
                        message = message.Substring(a + 1);
                        a = message.IndexOf("-");
                        if (a > 0)
                        {
                            bico3a.Text = message.Substring(0, a);
                            message = message.Substring(a + 1);
                            a = message.IndexOf("-");
                            if (a > 0)
                            {
                                bico4a.Text = message.Substring(0, a);
                                message = message.Substring(a + 1);
                                a = message.IndexOf("-");
                                if (a > 0)
                                {
                                    bico5a.Text = message.Substring(0, a);
                                    message = message.Substring(a + 1);
                                    a = message.IndexOf("-");
                                    if (a > 0)
                                    {
                                        bico6a.Text = message.Substring(0, a);
                                        message = message.Substring(a + 1);
                                        a = message.IndexOf("-");
                                        if (a > 0)
                                        {
                                            bico7a.Text = message.Substring(0, a);
                                            message = message.Substring(a + 1);
                                            a = message.IndexOf("-");
                                            if (a > 0)
                                            {
                                                bico8a.Text = message.Substring(0, a);
                                                message = message.Substring(a + 1);
                                                a = message.IndexOf("-");
                                                if (a > 0)
                                                {
                                                    bico9a.Text = message.Substring(0, a);
                                                    message = message.Substring(a + 1);
                                                    a = message.IndexOf("-");
                                                }
                                                else if (message.Length > 0)
                                                {
                                                    bico9a.Text = message;
                                                }
                                            }
                                            else if (message.Length > 0)
                                            {
                                                bico8a.Text = message;
                                            }
                                        }
                                        else if (message.Length > 0)
                                        {
                                            bico7a.Text = message;
                                        }
                                    }
                                    else if (message.Length > 0)
                                    {
                                        bico6a.Text = message;
                                    }
                                }
                                else if (message.Length > 0)
                                {
                                    bico5a.Text = message;
                                }
                            }
                            else if (message.Length > 0)
                            {
                                bico4a.Text = message;
                            }
                        }
                        else if (message.Length > 0)
                        {
                            bico3a.Text = message;
                        }
                    }
                    else if (message.Length > 0)
                    {
                        bico2a.Text = message;
                    }
                    
                }
                else if(message.Length > 0)
                {
                    bico1a.Text = message;
                }
            }
            else if(message.Length > 0)
            {
                bico0a.Text = message;
            }
                    
        }

        private void receber_dados_Click(object sender, EventArgs e)
        {   
            Send("GET", ip_placa.Text);
        }

    

        public bool IsAddressValid(string addrString)
        {
            IPAddress address;
            return IPAddress.TryParse(addrString, out address);
        }

        public bool ValidateText(string text)
        {
            char[] characters = text.ToCharArray();

            foreach (char c in characters)
            {
                if (!char.IsNumber(c))
                    return false;
            }
            return true;
        }

        

       


    }
}
