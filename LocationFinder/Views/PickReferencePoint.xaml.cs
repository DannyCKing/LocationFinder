using LocationFinderLibrary.Models;
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
using System.Windows.Shapes;

namespace LocationFinder.Views
{
    /// <summary>
    /// Interaction logic for PickReferencePoint.xaml
    /// </summary>
    public partial class PickReferencePoint : Window
    {
        public GeographicLocation GeographicLocationResult { get; private set; }
        public PickReferencePoint()
        {
            InitializeComponent();

            this.Loaded += PickReferencePoint_Loaded;
        }

        private void PickReferencePoint_Loaded(object sender, RoutedEventArgs e)
        {
            this.LocationNameTextBox.Text = "Zero Zero ";
            this.LatitudeTextBox.Text = "0";
            this.LongitudeTextBox.Text = "0";
            this.ElevationTextBox.Text = "0";



            this.LocationNameTextBox.Text = "Disneyland";
            this.LatitudeTextBox.Text = "33.85";
            this.LongitudeTextBox.Text = "-117.91";
            this.ElevationTextBox.Text = "0";
            //new GeographicLocation("Disneyland", 33.812092, -117.918987, 0, referencePoint),

            //new GeographicLocation("South Temple and State Street", 40.7694, -111.891, 0, referencePoint),
            this.LocationNameTextBox.Text = "Downton SLC ";
            this.LatitudeTextBox.Text = "40.7694";
            this.LongitudeTextBox.Text = "-111.891";
            this.ElevationTextBox.Text = "0";

            this.LocationNameTextBox.Text = "Downton SLC ";
            this.LatitudeTextBox.Text = "40.770978";
            this.LongitudeTextBox.Text = "-111.889232";
            this.ElevationTextBox.Text = "200";

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var latdegrees = LatitudeTextBox.Text;
            var longdegress = LongitudeTextBox.Text;
            var elevationString = ElevationTextBox.Text;

            if (!Latitude.IsValidLatitude(latdegrees))
            {
                MessageBox.Show("Invalid latitude - please enter in the decimal format.  Possible values include -90.0 to 90.0.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Longitude.IsValidLongitude(longdegress))
            {
                MessageBox.Show("Invalid longitude - please enter in the decimal format. Possible values include -180.0 to 180.0.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double elevationValue = 0;
            if(!double.TryParse(elevationString, out elevationValue))
            {
                MessageBox.Show("Invalid elevation - please enter a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            double lat = double.Parse(latdegrees);
            double longitude = double.Parse(longdegress);

            DialogResult = true;
            GeographicLocationResult = new GeographicLocation("Reference Point", lat, longitude, elevationValue, null);

        }
    }
}
