﻿using CSharpCrawler.Util;
using System;
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

namespace CSharpCrawler.Views
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Page
    {
        string defaultImgPath = Environment.CurrentDirectory + "\\User Data\\Theme\\Default.jpg";
        private readonly MainWindow mainWindow;
        private GlobalDataUtil globalData = GlobalDataUtil.GetInstance();
        private int ignoreCount = 0;

        public Setting(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            InitTheme();
            InitCfg();
        }

        private void InitTheme()
        {
            Border border = GlobalSettingPanel.Children[1] as Border;
            if (border == null)
                return;
        
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(defaultImgPath,UriKind.Absolute));
            border.Background = imageBrush;
            border.MouseDown += (a, b) => { mainWindow.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(defaultImgPath,UriKind.Absolute)) }; };
        }

        private void InitCfg()
        {
            ignoreCount = 0;

            if (globalData.CrawlerConfig.UrlConfig.IgnoreUrlCheck == true)
                this.cbox_UrlCheck.IsChecked = true;
            else
                this.cbox_UrlCheck.IsChecked = false;
        }


        #region 事件
        private void cbox_UrlCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (ignoreCount == 0)
            {
                ignoreCount++;
                return;
            }

            bool result = globalData.configUtil.SaveIgnoreUrlCheck(true);
            if (result)
                globalData.CrawlerConfig.UrlConfig.IgnoreUrlCheck = true;
        }

        private void cbox_UrlCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ignoreCount == 0)
            {
                ignoreCount++;
                return;
            }

            bool result = globalData.configUtil.SaveIgnoreUrlCheck(false);
            if (result)
                globalData.CrawlerConfig.UrlConfig.IgnoreUrlCheck = false;
        }

        #endregion
    }
}
