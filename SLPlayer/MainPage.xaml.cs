using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

using System.Xml.Linq;
using Microsoft.Maps.MapControl;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace SLPlayer
{
    public partial class MainPage : UserControl
    {
        BitmapImage place = null;

        DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            InitializeComponent();
            //runString.Text = @"Центр проекционных технологий «Викинг» - крупный системный интегратор и дистрибьютор, специализирующийся на создании аудиовизуальных комплексов – от разработки проекта до сдачи объекта. В настоящее время Викинг является официальным дистрибьютором ведущих производителей видео, аудио и коммутационного оборудования - MITSUBISHI ELECTRIC, CHRISTIE DIGITAL, EXTRON ELECTRONICS, JVC, DIS, SАNAKO, AMX, OR GROUP.";
            runString.Text = @"Пробел-полный экран, ESC-возврат, S-стоп, P-продолжить, Каналы-1..5(клик мыши). Разработка презентаций: Александр Чуканов, achuk2001@mail.ru";
            temperatureMarker.Value = 19;

            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            placeImage.Source = new BitmapImage(new Uri(String.Format("http://188.134.67.240//SLPlayer.Web/Images/MicroTile21.jpg"), UriKind.Absolute));

            //Application.Current.Host.Content.IsFullScreen = true; 
        }

        void OnTimedEvent(object source, EventArgs e)
        {
            TimeSpan leftPeriod = (DateTime.Parse("2/7/2014 12:00:00 PM") - DateTime.Now);
            dayLeft.Text = leftPeriod.Days.ToString();
            timeLeft.Text = (24 - DateTime.Now.Hour).ToString("00") + ":" + (60 - DateTime.Now.Minute).ToString("00") + ":" + (60 - DateTime.Now.Second).ToString("00");
        }

        private void SMFPlayer_TimelineMarkerLeft(object sender, Microsoft.SilverlightMediaFramework.Core.CustomEventArgs<Microsoft.SilverlightMediaFramework.Core.Media.TimelineMediaMarker> e)
        {
            //placeImage.Source = null;
            //tbViking.Visibility = Visibility.Collapsed;
        }

        private void SMFPlayer_TimelineMarkerReached(object sender, Microsoft.SilverlightMediaFramework.Core.TimelineMarkerReachedInfo e)
        {
            place = new BitmapImage(new Uri(String.Format("http://188.134.67.240//SLPlayer.Web/Images/{0}.jpg", e.Marker.Content), UriKind.Absolute));
            placeImage.Source = place;
            //tbViking.Visibility = Visibility.Visible;
        }


        private void SMFPlayer_TimelineMarkerSkipped(object sender, Microsoft.SilverlightMediaFramework.Core.CustomEventArgs<Microsoft.SilverlightMediaFramework.Core.Media.TimelineMediaMarker> e)
        {
        }

        private void main_KeyDown_1(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString().ToLower();
            

            if (key == "space" && Application.Current.Host.Content.IsFullScreen == false)
            {
                Application.Current.Host.Content.IsFullScreen = true;
                return;
            }
           

            SwitchStream(key);
        }

        private void SwitchStream(string key)
        {
            pip1.IsMuted = true;
            pip2.IsMuted = true;
            pip3.IsMuted = true;
            pip4.IsMuted = true;
            pip5.IsMuted = true;

            pip1.Pause();
            pip2.Pause();
            pip3.Pause();
            pip4.Pause();
            pip5.Pause();

            switch (key)
            {
                case "s":
                    pip1.Stop();
                    pip2.Stop();
                    pip3.Stop();
                    pip4.Stop();
                    pip5.Stop();
                    break;

                case "p":
                    pip1.Play();
                    pip2.Play();
                    pip3.Play();
                    pip4.Play();
                    pip5.Play();
                    pip1.IsMuted = false;
                    break;

                case "d1":
                    pip1.IsMuted = false;
                    pip1.Play();
                    pip2.Play();
                    pip3.Play();
                    pip4.Play();
                    pip5.Play();
                    break;

                case "d2":
                    pip2.IsMuted = false;
                    pip2.Play();
                    break;

                case "d3":
                    pip3.IsMuted = false;
                    pip3.Play();
                    break;

                case "d4":
                    pip4.IsMuted = false;
                    pip4.Play();
                    break;

                case "d5":
                    pip5.IsMuted = false;
                    pip5.Play();
                    break;


                default:
                    pip1.Play();
                    pip2.Play();
                    pip3.Play();
                    pip4.Play();
                    pip5.Play();
                    pip1.IsMuted = false;
                    break;
            }
        }



        private void pip2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwitchStream("d2");
        }

        private void pip3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwitchStream("d3");
        }

        private void pip4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwitchStream("d4");
        }

        private void pip5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwitchStream("d5");
        }

        private void pip1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwitchStream("d1");
        }

    }
}
