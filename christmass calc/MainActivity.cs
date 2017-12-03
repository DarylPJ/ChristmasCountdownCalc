using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using CalculatorMethods;
using System.Threading;
using Android.Graphics;
using Android.Media;
using UI_Chanage;
using System.Collections.Generic;

namespace christmascalc
{
    //[Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    [Activity(MainLauncher = true, ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        private TextView FullEquationText; 
        private TextView CurrentNumberText; 
        private HorizontalScrollView hsvBig;
        private HorizontalScrollView hsvTotal;

        private ChristmasSongPlayer SongPlayer = new ChristmasSongPlayer();
        private Calculator Calc = new Calculator();
        private ButtonText AllButtons = new ButtonText(); 

        bool ChristmasClicked = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FullEquationText = FindViewById<TextView>(Resource.Id.Calculator_Full_equation);
            CurrentNumberText = FindViewById<TextView>(Resource.Id.Calculator_Current_Number_text);

            hsvTotal = FindViewById<HorizontalScrollView>(Resource.Id.Scroll1);
            hsvBig = FindViewById<HorizontalScrollView>(Resource.Id.Scroll2);

            AllButtons.Setup(new List<Button>
            {
                FindViewById<Button>(Resource.Id.buttonCE),
                FindViewById<Button>(Resource.Id.buttonClear),
                FindViewById<Button>(Resource.Id.buttonBackArrow),
                FindViewById<Button>(Resource.Id.buttonDivide),
                FindViewById<Button>(Resource.Id.button7),
                FindViewById<Button>(Resource.Id.button8),
                FindViewById<Button>(Resource.Id.button9),
                FindViewById<Button>(Resource.Id.buttonTimes),
                FindViewById<Button>(Resource.Id.button4),
                FindViewById<Button>(Resource.Id.button5),
                FindViewById<Button>(Resource.Id.button6),
                FindViewById<Button>(Resource.Id.buttonMinus),
                FindViewById<Button>(Resource.Id.button1),
                FindViewById<Button>(Resource.Id.button2),
                FindViewById<Button>(Resource.Id.button3),
                FindViewById<Button>(Resource.Id.buttonPluse),
                FindViewById<Button>(Resource.Id.buttonPluseorMinus),
                FindViewById<Button>(Resource.Id.button0),
                FindViewById<Button>(Resource.Id.buttonDot),
                FindViewById<Button>(Resource.Id.buttonEquals),
                FindViewById<Button>(Resource.Id.buttonChristmas),
            });

            FullEquationText.SetTextColor(Color.White);
            CurrentNumberText.SetTextColor(Color.White);

            hsvTotal.SetBackgroundColor(Color.Transparent);
            hsvBig.SetBackgroundColor(Color.Transparent);

            AllButtons.NormalButtons();
            SongPlayer.SetUp(MediaPlayer.Create(this, Resource.Raw.ChristmasCalcMusic),CurrentNumberText,FullEquationText);
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (ChristmasClicked)
            {
                SongPlayer.Pause();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (ChristmasClicked)
            {
                SongPlayer.Play();
            }
        }
        

        [Java.Interop.Export("Number_button_click")]
        public void Number_button_click(View v)
        {
            if (ChristmasClicked)
            {
                return;
            }
            Button button = (Button)v;
            string BigNum = Calc.NumberPress(button.Text);
            CurrentNumberText.Text = BigNum;
            ThreadPool.QueueUserWorkItem(o => Update_the_focus());
        }

        [Java.Interop.Export("Operator_button_click")]
        public void Operator_button_click(View v)
        {
            if (ChristmasClicked)
            {
                return;
            }
            Button button = (Button)v;
            string BigNum = Calc.OperatorPress(button.Text);
            FullEquationText.Text = BigNum;
            CurrentNumberText.Text = "0";
            ThreadPool.QueueUserWorkItem(o => Update_the_focus());
        }

        [Java.Interop.Export("CE_button_click")]
        public void CE_button_click(View v)
        {
            if (ChristmasClicked)
            {
                return;
            }
            Calc.ClearBigNumber();
            CurrentNumberText.Text = "0";
            ThreadPool.QueueUserWorkItem(o => Update_the_focus());
        }

        [Java.Interop.Export("Clear_button_click")]
        public void Clear_button_click(View v)
        {
            if (ChristmasClicked)
            {
                return;
            }
            Calc.Clear_All();
            CurrentNumberText.Text = "0";
            FullEquationText.Text = "";
            ThreadPool.QueueUserWorkItem(o => Update_the_focus());
        }

        [Java.Interop.Export("Back_Arrow_button_click")]
        public void Back_Arrow_button_click(View v)
        {
            if (ChristmasClicked)
            {
                return;
            }
            string BigNum = Calc.Back_Arrow();
            BigNum = BigNum == "" ? "0" : BigNum;
            CurrentNumberText.Text = BigNum;
            ThreadPool.QueueUserWorkItem(o => Update_the_focus());
        }

        [Java.Interop.Export("Equals_button_click")]
        public void Equals_button_click(View v)
        {
            if (ChristmasClicked)
            {
                return;
            }
            string BigNum = Calc.EqualsPress();
            BigNum = BigNum == "" ? "0" : BigNum;
            CurrentNumberText.Text = BigNum;
            FullEquationText.Text = "";
            hsvBig.ScrollBy(int.MinValue, 0);
            hsvTotal.ScrollBy(int.MinValue, 0);
        }

        private void Update_the_focus()
        {
            RunOnUiThread(() => hsvBig.ScrollBy(CurrentNumberText.Width, 0));
            RunOnUiThread(() => hsvTotal.ScrollBy(FullEquationText.Width, 0));
        }

        [Java.Interop.Export("Christmas_button_click")]
        public void Christmas_button_click(View v)
        {
            Calc.Clear_All();
            if (ChristmasClicked)
            {
                ChristmasClicked = false;

                FullEquationText.SetTextColor(Color.White);
                CurrentNumberText.SetTextColor(Color.White);

                hsvTotal.SetBackgroundColor(Color.Transparent);
                hsvBig.SetBackgroundColor(Color.Transparent);

                AllButtons.NormalButtons();
                SongPlayer.Pause();

                ThreadPool.QueueUserWorkItem(o => Reset_Text());
            }
            else
            {
                ChristmasClicked = true;

                FullEquationText.SetTextColor(Color.Red);
                CurrentNumberText.SetTextColor(Color.Red);

                hsvTotal.SetBackgroundColor(Color.Green);
                hsvBig.SetBackgroundColor(Color.Green);

                AllButtons.ChristmasButtons();
                SongPlayer.Replay(MediaPlayer.Create(this, Resource.Raw.ChristmasCalcMusic));
            }
        }

        private void Reset_Text()
        {
            RunOnUiThread(()=>CurrentNumberText.Text = "0");
            RunOnUiThread(() => FullEquationText.Text = "");

        }

    }
}

