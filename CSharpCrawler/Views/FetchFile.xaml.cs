﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSharpCrawler.Model;
using CSharpCrawler.Util;

namespace CSharpCrawler.Views
{
    /// <summary>
    /// FetchFile.xaml 的交互逻辑
    /// </summary>
    public partial class FetchFile : UserControl
    {
        GlobalDataUtil globalData = GlobalDataUtil.GetInstance();

        Paragraph paragraph = new Paragraph();

        public FetchFile()
        {
            InitializeComponent();
        }

        private async void btn_Donwload_Click(object sender, RoutedEventArgs e)
        {
            string url = this.tbox_Url.Text.Trim();
            if(string.IsNullOrEmpty(url))
            {
                MessageBox.Show("请输入下载地址");
                return;
            }

            if (globalData.CrawlerConfig.ImageConfig.IgnoreUrlCheck == false)
            {
                //TODO 
                //文件URL检验
            }

            HttpHeader httpHeader =  WebUtil.GetHeader(url);
            if (httpHeader.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ShowStatusText($"{url} is not available");
                return;
            }
           
            ShowStatusText($"Download file {url}.....\r\n");
            string fileName = await WebUtil.DownloadFileAsync(url);
            ShowStatusText($"Download : {url} Finished\r\n");

            //Temp
            if(System.IO.Path.GetExtension(url) == ".mp4")
            {
                (Application.Current.MainWindow as MainWindow).SetTransparentBackground();
                (Application.Current.MainWindow as MainWindow).SetBackgroundVideo(fileName);
            }
        }

        private void btn_DownLoadFromFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowStatusText(string text)
        {
            this.Dispatcher.Invoke(()=> {
                paragraph.Inlines.Add(new Run(text));
                this.rtbox_Status.Document = new FlowDocument(paragraph);
            });
        }
    }
}
