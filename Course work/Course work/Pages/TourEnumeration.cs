using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

using static Additional.DBController;

namespace Course_work
{
    public partial class MainWindow : System.Windows.Window
    {
        partial void TourEnumerationInit()
        {
            InitTravelWay();
            InitAdditionalType();
            InitMealType();

            TourEnumerationPageReturn.Click += (s, e) => Return();
        }


    }
}
