using LocationFinder.Views;
using LocationFinderLibrary.Models;
using LocationFinderLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocationFinder.ViewModels
{
    public class MainViewModel : BaseOnPropertyChanged
    {
        private ObservableCollection<GeographicLocation> _Locations;

        public ObservableCollection<GeographicLocation> Locations
        {
            get
            {
                return _Locations;
            }
            set
            {
                _Locations = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            OpenSetLocationWindow();
            //var pickReferenceWindow = new PickReferencePoint();
            //var result = pickReferenceWindow.ShowDialog();

            //var referencePoint = pickReferenceWindow.GeographicLocationResult;

            //Locations = new ObservableCollection<GeographicLocation>(DefaultValues.GetDefaultLocations(referencePoint));
        }

        internal void OpenSetLocationWindow()
        {
            var pickReferenceWindow = new PickReferencePoint();
            var result = pickReferenceWindow.ShowDialog();

            var referencePoint = pickReferenceWindow.GeographicLocationResult;

            Locations = new ObservableCollection<GeographicLocation>(DefaultValues.GetDefaultLocations(referencePoint));
        }
    }
}
