using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace MiniBudgetTracker
{
    [BroadcastReceiver(Enabled = true , Exported = true)]
    public class MyBroadcastReceiver : BroadcastReceiver
    {


        private int balance = 0;
        private string balanceText = string.Empty;
        public override void OnReceive(Context context, Intent intent)
        {
            //Toast.MakeText(context, "Date :" + DateTime.Today, ToastLength.Long)
            //    .Show();

            foreach (DateTime dt in CommonData.checkDates.Keys.Reverse())
            {
                //Toast.MakeText(context, "checkdate :" +dt.ToShortDateString(), ToastLength.Long)
                //    .Show();

                if (DateTime.Today >= dt && DateTime.Today <= dt.AddDays(CommonData.NumberOfDays))
                {
                    //Toast.MakeText(context, "Days :" + CommonData.NumberOfDays, ToastLength.Long)
                    //    .Show();

                    Bundle bundle = intent.Extras;

                    if (bundle != null)
                    {
                        Java.Lang.Object[] pdus = (Java.Lang.Object[]) bundle.Get("pdus");

                        if (pdus.Length == 0)
                        {
                            return;
                        }

                        SmsMessage[] msgs;
                        msgs = new SmsMessage[pdus.Length];

                        for (int i = 0; i < msgs.Length; i++)
                        {
                            msgs[i] = SmsMessage.CreateFromPdu((byte[]) pdus[i], "3gpp");

                            //Toast.MakeText(context, "Received broadcast in MySecondBroadcastReceiver; " +
                            //                        " SMS Received from: " + msgs[i].OriginatingAddress + "SMS Data: "
                            //                        + msgs[i].MessageBody.ToString(), ToastLength.Long)
                                //.Show();
                            if (msgs[i].OriginatingAddress.Contains(CommonData.Fromtext))
                            {
                                //Toast.MakeText(context, "verified from adress" + CommonData.Fromtext,ToastLength.Long).Show();

                                if (msgs[i].MessageBody.Split(CommonData.AfterText.Trim()).Length > 1)                                
                                    balanceText = msgs[i].MessageBody.Split(CommonData.AfterText.Trim())[1].Trim();
                                else                              
                                    balanceText = msgs[i].MessageBody.Split(CommonData.AfterText.Trim())[0].Trim();
                                if (balanceText.Trim().Contains(' '))
                                
                                balanceText = balanceText.Trim().Split(' ')[0].Trim();

                                //Toast.MakeText(context, "balance amount" + balanceText, ToastLength.Long).Show();
                                balance = Convert.ToInt32(balanceText);
                               
                                  //  Toast.MakeText(context, "before amt check" + balance, ToastLength.Long).Show();
                                    if (balance < CommonData.checkDates[dt])
                                    {
                                        //Toast.MakeText(context, "before notification" + balance, ToastLength.Long)
                                        //    .Show();
                                        CreateNotification(context, intent, balance.ToString(),
                                            "You have exceeded the Budget");
                                    }
                                    else
                                    {
                                     //   Toast.MakeText(context, "after notification" + balance, ToastLength.Long).Show();
                                        CreateNotification(context, intent, balance.ToString(),
                                            "Hurrey! You are on track");
                                    }
                                
                            }

                            break;
                        }

                    }
                    break;

                }
            }
        }

        public void CreateNotification(Context context,Intent intent, string balance, string notification)
        {
           

            try
            {
                PendingIntent contentIntent = PendingIntent.GetActivity(context, 0,
                    new Intent(context, typeof(second_activity)), PendingIntentFlags.OneShot);
                NotificationChannel c = new NotificationChannel("test", "test", NotificationImportance.High);
                NotificationCompat.Builder mBuilder =
                    new NotificationCompat.Builder(context, "test")
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetContentTitle(notification)
                        .SetColor(Color.RosyBrown)
                        .SetColorized(true)
                        .SetContentText("and available balance is " + balance);


                mBuilder.SetContentIntent(contentIntent);
                mBuilder.SetAutoCancel(true);


                NotificationManager mNotificationManager =
                    (NotificationManager) context.GetSystemService(Context.NotificationService);

                mNotificationManager.CreateNotificationChannel(c);

                mNotificationManager.Notify(1, mBuilder.Build());
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, notification + "and available balance is " + balance, ToastLength.Long).Show();
            }
        }
    }
}