using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace UDP_Messenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Data Data;
        private readonly UDPWorker udpWorker;
        private bool running;

        public MainWindow()
        {
            InitializeComponent();
            Data = new Data();
            DataContext = Data;
            udpWorker = new UDPWorker();
            running = true;
        }

        private void Received_TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(StartListener));
            thread.Start();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            udpWorker.Send(Data.IPAddress, Data.Message);
            Data.Message = "";
        }

        private void OwnIP_TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            //string ip = Dns.GetHostAddresses(Dns.GetHostName())[^1].ToString();
            //if (!ip.Contains("192"))
            //    ip = "Please connect to a network first!";
            IPAddress[] ipAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            OwnIP_TextBlock.Text = ipAddresses[ipAddresses.Length - 1].ToString();
        }

        private void StartListener()
        {
            while (running)
            {
                string[] data = udpWorker.Receive();
                if (data != null)
                {
                    Data.ReceivedMessages += "[" + data[0] + "@" + DateTime.Now.ToString("hh:mm:ss") + "]  " + data[1] + "\n";
                    if (Data.IsChecked)
                        Data.IPAddress = data[0];
                }
            }
        }

        private void CommandBinding_Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
            e.Handled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            running = false;
            udpWorker.EndReceive();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && Data.IsEnabled)
            {
                udpWorker.Send(Data.IPAddress, Data.Message);
                Data.Message = "";
            }
        }

        private void Received_TextBlock_ScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            Received_TextBlock_ScrollViewer.ScrollToBottom();
        }
    }
}
