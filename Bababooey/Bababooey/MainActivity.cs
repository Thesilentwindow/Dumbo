using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Gms.Ads;

namespace Bababooey
{
    [Activity(Theme = "@android:style/Theme.Material.Light", Label = "Bababooey", MainLauncher = true, Icon = "@drawable/ic_launcher")]

    public class MainActivity : Activity
    {
        private int _count = 0;
        protected AdView mAdView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it         
            Button button = FindViewById<Button>(Resource.Id.Bababooey);
            MediaPlayer mp;

            var localNotes = Application.Context.GetSharedPreferences("Notes", FileCreationMode.Private); //create Notes sharedpreferences file in private mode(så er denne app den eneste app der kan bruge den)
            var noteEdit = localNotes.Edit();// redigering af Notes fil
            string countFromPref = localNotes.GetString("Count", null); //Get Count key value pair from sharedpreferences
            _count = Convert.ToInt32(countFromPref);


            var tv = (TextView)FindViewById(Resource.Id.BababooeyCount); //Variable to edit textview, aka label, with.
            tv.Text = _count.ToString();

            //AdMob Ads initialization
            mAdView = FindViewById<AdView>(Resource.Id.adView); //Finder vores AdView control
            AdRequest adRequest = new AdRequest.Builder().Build(); // Sender er ad request og holder på den ad som den modtager
            mAdView.LoadAd(adRequest); //indlæser ad fra adRequest så den kan vises



            if (countFromPref == string.Empty) //if content from sharedpref is empty set count to 0
            {
                tv.Text = "0";
            }
            else //if content from sharedpref contains number set textview to that content
            {
                tv.Text = countFromPref;
            }

            mp = MediaPlayer.Create(this, Resource.Raw.BABABOOEY);


            #region Delegates/eventhndlers
            button.Click += delegate
            {
                mp.Start();

            };
            mp.Completion += delegate
            {
                _count++;
                tv.Text = _count.ToString();
            };
            button.LongClick += delegate
            {
                noteEdit.Clear();
                Toast.MakeText(this, "Thine Bababooey has been cleansed", ToastLength.Short).Show();
                _count = 0;
                tv.Text = _count.ToString();
            };
            #endregion
        }
        protected override void OnDestroy()
        {

            var localNotes = Application.Context.GetSharedPreferences("Notes", FileCreationMode.Private); //create Notes sharedpreferences file in private mode(så er denne app den eneste app der kan bruge den)
            var noteEdit = localNotes.Edit();// redigering af Notes fil
            var tv = (TextView)FindViewById(Resource.Id.BababooeyCount);

            noteEdit.PutString("Count", tv.Text);
            noteEdit.Commit();

            base.OnDestroy();

        }
    }
}

