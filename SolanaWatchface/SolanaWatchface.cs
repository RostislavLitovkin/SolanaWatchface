using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;
using Xamarin.Forms;

namespace SolanaWatchface
{
    class Program : FormsWatchface
    {
        ClockViewModel _viewModel;
        TextWatchApplication watchfaceApp;

        private int minute = -1;

        bool animation = false;

        protected override void OnCreate()
        {
            base.OnCreate();
            watchfaceApp = new TextWatchApplication();
            _viewModel = new ClockViewModel();
            watchfaceApp.BindingContext = _viewModel;
            LoadWatchface(watchfaceApp);
        }

        protected override async void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;

                if (animation)
                {
                    if (_viewModel.Time.Second % 10 == 0)
                    {
                        await watchfaceApp.ChangeColor();
                    }
                    if (_viewModel.Time.Minute % 15 == 0 && _viewModel.Time.Minute != minute)
                    {
                        
                        minute = _viewModel.Time.Minute;
                        
                        watchfaceApp.UpdatePrices();
                    }

                }


            }
        }

        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            base.OnAmbientChanged(mode);
            
            base.OnAmbientChanged(mode);
            animation = !mode.Enabled;
            watchfaceApp.SecondsHand.IsVisible = !mode.Enabled;
            watchfaceApp.SolanaBlue.IsVisible = !mode.Enabled;
            watchfaceApp.SolanaYellow.IsVisible = !mode.Enabled;
            watchfaceApp.SolanaGray.IsVisible = mode.Enabled;
        }

        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            FormsCircularUI.Init();

            
            app.Run(args);

            
        }
    }
}
