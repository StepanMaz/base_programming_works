using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace CourseWork.Pages.TourPart
{
    public partial class ImageInput : Window
    {
        public Status status;
        public byte[] image;

        Action<Status, byte[]> onEscbuttonClick;
        public ImageInput(Action<Status, byte[]> onEscbuttonClick)
        {
            InitializeComponent();
            this.onEscbuttonClick = onEscbuttonClick;

            Closed += (s, e) => onEscbuttonClick.Invoke(Status.Canceled, null);
        }

        private void DialogClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "pngs (*.png)|*.png|jpgs (*.jpg or *.jpeg)|*.jpg;*.jpeg";
                if (dialog.ShowDialog() == true)
                {
                    image = File.ReadAllBytes(dialog.FileName);
                    ImageShower.Source = CourseWork.DataConverter.GetImageFromByteArray(image);
                }
                status = Status.Succeed;
                SubmitButton.Width = 80;
            }
            catch
            {
                SubmitButton.Width = 0;
                status = Status.Failed;
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            onEscbuttonClick.Invoke(Status.Canceled, null);
            Close();
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
            onEscbuttonClick.Invoke(status, image);
            Close();
        }

        private void SubmitUrlClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (System.Net.WebClient wbc = new System.Net.WebClient())
                {
                    wbc.Headers.Add("User-Agent", "JustNotCommercialApp/0.0 (https://google.com/; fake@fake.org)");
                    image = wbc.DownloadData(UrlText.Text);
                }
                status = Status.Succeed;
                SubmitButton.Width = 80;
            }
            catch
            {
                SubmitButton.Width = 0;
                status = Status.Failed;
            }
        }

        public enum Status
        {
            Canceled,
            Succeed,
            Failed
        }
    }
}
