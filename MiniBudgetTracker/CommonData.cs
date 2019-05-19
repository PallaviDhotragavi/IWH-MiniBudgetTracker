using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MiniBudgetTracker
{
    public static class CommonData
    {
        public static Dictionary<DateTime, int> checkDates = new Dictionary<DateTime, int>();
       // public static string Fromtext;
        //public static string AfterText;


        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

       
        public static string Fromtext
        {
            get => AppSettings.GetValueOrDefault(nameof(Fromtext), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Fromtext), value);
        }

        public static string AfterText
        {
            get => AppSettings.GetValueOrDefault(nameof(AfterText), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(AfterText), value);
        }

        public static int FirstAmount
        {
            get => AppSettings.GetValueOrDefault(nameof(FirstAmount), 0);
            set => AppSettings.AddOrUpdateValue(nameof(FirstAmount), value);
        }

        public static int SecondAmount
        {
            get => AppSettings.GetValueOrDefault(nameof(SecondAmount), 0);
            set => AppSettings.AddOrUpdateValue(nameof(SecondAmount), value);
        }

        public static int ThirdAmount
        {
            get => AppSettings.GetValueOrDefault(nameof(ThirdAmount), 0);
            set => AppSettings.AddOrUpdateValue(nameof(ThirdAmount), value);
        }

        public static int FourthAmount
        {
            get => AppSettings.GetValueOrDefault(nameof(FourthAmount), 0);
            set => AppSettings.AddOrUpdateValue(nameof(FourthAmount), value);
        }

        public static int NumberOfDays
        {
            get => AppSettings.GetValueOrDefault(nameof(NumberOfDays), 0);
            set => AppSettings.AddOrUpdateValue(nameof(NumberOfDays), value);
        }
        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }

    
}