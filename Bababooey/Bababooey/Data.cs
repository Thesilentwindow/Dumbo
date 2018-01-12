using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Bababooey
{
    class Data
    {
        public int Count { get; set; }

        public Data(int count)
        {
            Count = count;
        }

        public int StartUp()
        {
            var localNotes = Application.Context.GetSharedPreferences("Notes", FileCreationMode.Private); //create Notes sharedpreferences file in private mode(så er denne app den eneste app der kan bruge den)
            var noteEdit = localNotes.Edit();// redigering af Notes fil
            string countFromPref = localNotes.GetString("Count", null); //Get Count key value pair from sharedpreferences
            Count = Convert.ToInt32(countFromPref);
        }

        public void SaveData()
        {

        }

    }
}