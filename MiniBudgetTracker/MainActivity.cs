using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Telephony;
using Android.Support.V7;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Views.InputMethods;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MiniBudgetTracker
{
    [Activity(Label = "MiniBudgetTracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        
        MyBroadcastReceiver mySecond;
        IntentFilter intentfilter;
        private EditText fromValue = null;
        private EditText avblBal = null;
        private TextView one = null;
        private TextView two = null;
        private TextView three = null;
        private TextView four = null;

        private EditText firstAmt = null;
        private EditText secondAmt = null;
        private EditText thirdAmt = null;
        private EditText fourthAmt = null;

        private EditText noDaystxt = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Android.Widget.Button btnSaveConfig = (Android.Widget.Button)FindViewById(Resource.Id.saveButton);
            fromValue = (EditText)FindViewById(Resource.Id.FromText);
            avblBal = (EditText)FindViewById(Resource.Id.AvblBal);

            one = (TextView)FindViewById(Resource.Id.first);
            two = (TextView)FindViewById(Resource.Id.second);
            three = (TextView)FindViewById(Resource.Id.third);
            four = (TextView)FindViewById(Resource.Id.fourth);


            firstAmt = (EditText)FindViewById(Resource.Id.FirstAmt);
            secondAmt = (EditText)FindViewById(Resource.Id.SecondAmt);
            thirdAmt = (EditText)FindViewById(Resource.Id.ThirdAmt);
            fourthAmt = (EditText)FindViewById(Resource.Id.FourthAmt);

            noDaystxt = (EditText) FindViewById(Resource.Id.noDays);

                btnSaveConfig.Click += (sender, e) =>
            {
                int outInt = 0;
                CommonData.Fromtext = fromValue.Text.Trim();
                CommonData.AfterText = avblBal.Text.Trim();

                int.TryParse(firstAmt.Text, out outInt);
                CommonData.FirstAmount = outInt;

                int.TryParse(secondAmt.Text, out outInt);
                CommonData.SecondAmount = outInt;

                int.TryParse(thirdAmt.Text, out outInt);
                CommonData.ThirdAmount = outInt;

                int.TryParse(fourthAmt.Text, out outInt);
                CommonData.FourthAmount = outInt;

                int.TryParse(noDaystxt.Text, out outInt);
                CommonData.NumberOfDays = outInt;

            };


            fromValue.Text = CommonData.Fromtext.Trim();
            avblBal.Text = CommonData.AfterText.Trim();
            firstAmt.Text = CommonData.FirstAmount.ToString();
            secondAmt.Text = CommonData.SecondAmount.ToString();
            thirdAmt.Text = CommonData.ThirdAmount.ToString();
            fourthAmt.Text = CommonData.FourthAmount.ToString();
            noDaystxt.Text = CommonData.NumberOfDays.ToString();

            mySecond = new MyBroadcastReceiver();
          
            intentfilter = new IntentFilter("SMS_RECEIVED");
            CheckPermission();
            

            one.Text = "  " + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 7).ToShortDateString();
            CommonData.checkDates.Add(new DateTime(DateTime.Today.Year,DateTime.Today.Month,7), CommonData.FirstAmount );

            two.Text = "  " + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 14).ToShortDateString();
            CommonData.checkDates.Add(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 14), CommonData.SecondAmount);

            three.Text = "  " + new DateTime(DateTime.Today.Year, DateTime.Today.Month, 21).ToShortDateString();
            CommonData.checkDates.Add(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 21), CommonData.ThirdAmount);

            four.Text = "  " +  new DateTime(DateTime.Today.Year, DateTime.Today.Month, 28).ToShortDateString();
            CommonData.checkDates.Add(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 28), CommonData.FourthAmount);


        }

        protected override void OnResume()
        {
            base.OnResume();
        //    intentfilter.Priority=20;
            //RegisterReceiver(mySecond, intentfilter);
            //RegisterReceiver(myreceiver, intentfilter);
            
        }
        [Android.Runtime.Register("OnPause()", "()V", "GetOnPauseHandler")]
        protected override void OnPause()
        {
            base.OnPause();
            //UnregisterReceiver(mySecond);
            //RegisterReceiver(mySecond, intentfilter);
        }

        private void CheckPermission()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadSms) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReadSms }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReceiveSms) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.ReceiveSms }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessNotificationPolicy) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessNotificationPolicy }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.BindNotificationListenerService) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.BindNotificationListenerService }, 1);
            }
        }

        
    }

   
}

