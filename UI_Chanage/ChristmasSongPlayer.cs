using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Media;
using Android.Widget;

namespace UI_Chanage
{
    public class ChristmasSongPlayer : Android.App.Activity
    {
        MediaPlayer Player;
        System.Timers.Timer t1 = new System.Timers.Timer();
        private TextView CurrentNumberText;
        private TextView FullEquationText;

        public void SetUp(MediaPlayer AndroidPlayer,TextView CurrentNumber,TextView FullEquation)
        {
            t1.Interval = 100;
            t1.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            Player = AndroidPlayer;
            Player.Looping = true;
            FullEquationText = CurrentNumber;
            CurrentNumberText = FullEquation;
        }

        public void Play()
        {
            t1.Start();
            Player.Start();
        }

        public void Pause()
        {
            t1.Stop();
            Player.Pause();
        }

        public void Replay(MediaPlayer AndroidPlayer)
        {
            t1.Start();
            Player = AndroidPlayer;
            Player.Looping = true;
            Play();
        }



        protected void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime ChristmasDate = new DateTime(DateTime.Today.Year, 12, 25, 0, 0, 0);
            DateTime Today = DateTime.Now;


            if (Today.Month == 12 && Today.Day >= 25)
            {
                ChristmasDate = ChristmasDate.AddYears(1);
            }


            String Days = (ChristmasDate - Today).Days.ToString();
            String Hours = (ChristmasDate - Today).Hours.ToString();
            String Mins = (ChristmasDate - Today).Minutes.ToString();
            String Secs = (ChristmasDate - Today).Seconds.ToString();

           

            RunOnUiThread(() => CurrentNumberText.Text = $"Days: {Days}");
            RunOnUiThread(() => FullEquationText.Text = $"{Hours}:{Mins}:{Secs}");
        }
    }
}