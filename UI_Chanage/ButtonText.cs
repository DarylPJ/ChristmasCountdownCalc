using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Widget;
using Android.Graphics;

namespace UI_Chanage
{
    public class ButtonText:Android.App.Activity
    {
        private List<Button> AllButtons;
        private List<string> NormalButtonText = new List<string>
        { "CE","C","←","÷","7","8","9","×","4","5","6","-","1","2","3","+","±","0",".","=","Christmas" };
        private List<string> ChristmasButtonText = new List<string>
        { "M","E","R","R","Y","","C","H","R","I","S","T","M","A","S","!","","YAY",":)","","Calculator"};

        
        public void Setup(List<Button> TheButtonList)
        {
            AllButtons = TheButtonList;
        }

        public void ChristmasButtons()
        {
            for (int i = 0; i < AllButtons.Count()-1; i++)
            {
                AllButtons[i].Text =ChristmasButtonText[i];
                AllButtons[i].SetBackgroundColor(Color.Green);
                AllButtons[i].SetTextColor(Color.Red); 
            }
            AllButtons[AllButtons.Count()-1].Text = ChristmasButtonText[AllButtons.Count()-1];
        }

        public void NormalButtons()
        {
            for (int i = 0; i < AllButtons.Count(); i++)
            {
                AllButtons[i].Text = NormalButtonText[i];
                AllButtons[i].SetBackgroundColor(Color.Transparent);
                AllButtons[i].SetTextColor(Color.White);
            }
        }
    }
}
