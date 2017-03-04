using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;

namespace GlimpseUser.Droid.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                .ApkFile ("../../../Glimpse.Droid/bin/Release/Glimpse.Droid.Glimpse.Droid-x86-Signed.apk")
                .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void NewTest()
        {
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), "e9@gmail.com");
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), "e9");
            app.Tap(x => x.Id("btnSignIn"));
            app.SwipeRightToLeft();
            app.SwipeLeftToRight();
            app.Tap(x => x.Text("Footwear"));
            app.Tap(x => x.Text("Electronics"));
            app.Tap(x => x.Class("AppCompatImageView").Index(1));
            app.Tap(x => x.Class("AppCompatImageView").Index(2));
            app.ScrollUp();
            app.SwipeLeftToRight();
            app.SwipeRightToLeft();
            app.Tap(x => x.Marked("Google Map"));
            app.SwipeLeftToRight();
            app.SwipeLeftToRight();
        }

    }
}

