using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("bignoodletitling.ttf")]

namespace SolanaWatchface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextWatchApplication : Application
    {
        private CurrentPriceModel currentPriceModel = new CurrentPriceModel();
        private bool yellow = false;
        public TextWatchApplication()
        {
            InitializeComponent();
            CurrentPriceLabel = currentPriceModel.FindCost("SOL").ToString().Substring(0, 4);
        }
    public Image SecondsHand => secondsHand;
        public Image SolanaBlue => blueLogo;
        public Image SolanaYellow => yellowLogo;
        public Image SolanaGray => grayLogo;

        public void UpdatePrices()
        {
            try
            {
                currentPriceModel.makeAPICall();
                CurrentPriceLabel = currentPriceModel.FindCost("SOL").ToString();
            }
            catch
            {
                CurrentPriceLabel = "..";
            }
        }
        public string CurrentPriceLabel { set { currentPriceLabel.Text = value + " CZK"; } }

        public async Task ChangeColor()
        {
            if (yellow)
            {
                yellow = false;
                await yellowLogo.FadeTo(0, 2500);

            }
            else
            {
                yellow = true;

                await yellowLogo.FadeTo(1, 2500);
            }
        }
    }
}