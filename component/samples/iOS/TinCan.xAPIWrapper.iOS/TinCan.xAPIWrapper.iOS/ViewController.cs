using CoreGraphics;
using System;
using TinCan.Standard;
using TinCan.xAPIWrapper.iOS.Helpers;
using UIKit;

namespace TinCan.xAPIWrapper.iOS
{
    public partial class ViewController : UIViewController
    {
        int _count = 1;
        APIWrapper apiWrapper;
        string msg = "\n";

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            apiWrapper = new APIWrapper(string.Empty, string.Empty, string.Empty);

            //View.BackgroundColor = UIColor.FromRGB(232, 75, 57);
            createUI();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void createUI()
        {
            var viewHeight = UIScreen.MainScreen.Bounds.Height;
            var viewWidth = UIScreen.MainScreen.Bounds.Width;
            var viewX = UIScreen.MainScreen.Bounds.X;
            var viewY = UIScreen.MainScreen.Bounds.Y;
            nfloat controlY = viewY;

            nfloat height = YaxisByscreenHeight_Ios(60, viewHeight);
            nfloat space = YaxisByscreenHeight_Ios(25, viewHeight);

            UIImageView logo = new UIImageView(new CGRect(viewX + (space / 2), controlY + space, height - space, height - space));
            logo.Image = UIImage.FromBundle("Images/Logo.png");
            logo.UserInteractionEnabled = true;
            View.Add(logo);

            UILabel lblHeading = new UILabel(new CGRect(viewX, controlY + space, viewWidth, height - space));
            lblHeading.Text = "IWORKTECH";
            lblHeading.TextAlignment = UITextAlignment.Center;
            lblHeading.TextColor = UIColor.FromRGB(232, 75, 57);
            lblHeading.Font = UIFont.FromName("Helvetica-Bold", 25f);
            View.Add(lblHeading);

            controlY = controlY + YaxisByscreenHeight_Ios(150, viewHeight);

            UIButton startButton = new UIButton(new CGRect(30, controlY, viewWidth - 60, viewWidth / 9.3F));
            startButton.ClipsToBounds = true;
            startButton.SetTitle("Start LRS Test", UIControlState.Normal);
            startButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            startButton.SetTitleColor(UIColor.Blue, UIControlState.Highlighted);
            startButton.BackgroundColor = UIColor.FromRGB(232, 75, 57);
            startButton.TouchUpInside += StartButton_TouchUpInside;
            View.Add(startButton);

            controlY = controlY + startButton.Frame.Size.Height + YaxisByscreenHeight_Ios(20, viewHeight);

            UIButton firstButton = new UIButton(new CGRect(30, controlY, viewWidth - 60, viewWidth / 9.3F));
            firstButton.ClipsToBounds = true;
            firstButton.SetTitle("Click Here", UIControlState.Normal);
            firstButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            firstButton.SetTitleColor(UIColor.Blue, UIControlState.Highlighted);
            firstButton.BackgroundColor = UIColor.FromRGB(232, 75, 57);
            firstButton.TouchUpInside += FirstButton_TouchUpInside;
            View.Add(firstButton);

            controlY = controlY + firstButton.Frame.Size.Height + YaxisByscreenHeight_Ios(20, viewHeight);

            UIButton stopButton = new UIButton(new CGRect(30, controlY, viewWidth - 60, viewWidth / 9.3F));
            stopButton.ClipsToBounds = true;
            stopButton.SetTitle("End LRS Test", UIControlState.Normal);
            stopButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            stopButton.SetTitleColor(UIColor.Blue, UIControlState.Highlighted);
            stopButton.BackgroundColor = UIColor.FromRGB(232, 75, 57);
            stopButton.TouchUpInside += StopButton_TouchUpInside;
            View.Add(stopButton);

            controlY = controlY + stopButton.Frame.Size.Height + YaxisByscreenHeight_Ios(50, viewHeight);

            UIButton showButton = new UIButton(new CGRect(30, controlY, viewWidth - 60, viewWidth / 9.3F));
            showButton.ClipsToBounds = true;
            showButton.SetTitle("Show Statements", UIControlState.Normal);
            showButton.Font = UIFont.FromName("Helvetica-Bold", 20f);
            showButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            showButton.SetTitleColor(UIColor.Blue, UIControlState.Highlighted);
            showButton.BackgroundColor = UIColor.FromRGB(232, 75, 57);
            showButton.TouchUpInside += ShowButton_TouchUpInside;
            View.Add(showButton);

        }

        void StartButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                UIButton btn = (UIButton)sender;

                var statement = apiWrapper.PrepareStatement("test@ald.net", "Started", "Activity");
                createStatement(statement, btn.TitleLabel.Text);
            }
            catch (Exception ex)
            {
            }
        }

        void FirstButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                UIButton btn = (UIButton)sender;

                var statement = apiWrapper.PrepareStatement("test@ald.net", "experienced", "Activity");
                createStatement(statement, btn.TitleLabel.Text);
            }
            catch (Exception ex)
            {
            }
        }
        void StopButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                UIButton btn = (UIButton)sender;

                var statement = apiWrapper.PrepareStatement("test@ald.net", "stopped", "Activity");
                createStatement(statement, btn.TitleLabel.Text);
            }
            catch (Exception ex)
            {
            }
        }
        void ShowButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                string title = "Following statements are pushed to IWORKTECH's LRS.";
                UIAlertView alert = new UIAlertView(title, msg, null, "OK");
                alert.Show();
                alert.Clicked += (args, buttonArgs) => { };
            }
            catch (Exception ex)
            {
            }
        }

        async void createStatement(Statement statement, string str)
        {
            bool netStatus = Reachability.IsHostReachable("www.google.com");

            if (!netStatus)
            {
                UIAlertView _error = new UIAlertView("Error", "Please Check Your Network", null, "OK", null);
                _error.Show();
            }
            else
            {
                var task = await apiWrapper.SendStatement(statement);

                var title = $"{_count++}. ";

                if (task.Success)
                {
                    title = title + $"User experienced button \"{ str}\". \n";
                    msg = msg + title;
                }
                else
                {
                    title = title + $"{ str} statement not sent! \n";
                    msg = msg + title;
                }
            }
        }

        public static nfloat YaxisByscreenHeight_Ios(nfloat Yaxis, nfloat screenHeight)
        {
            return Yaxis / 568 * screenHeight;
        }

    }
}