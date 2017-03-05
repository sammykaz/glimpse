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
                .ApkFile("../../../Glimpse.Droid/bin/Release/Glimpse.Droid.Glimpse.Droid-x86-Signed.apk")
                .EnableLocalScreenshots()
                .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void TestSwipeThroughNearbyPromotions()
        {
            //Arrange scenario condition(sign in)
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), "e5@gmail.com");
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), "e5");
            app.Tap(x => x.Id("btnSignIn"));

            //Act

            app.SwipeLeftToRight();
            app.Screenshot("Swiping Top Card");
            app.SwipeRightToLeft();
            app.Screenshot("Second Top Card");
        }

        [Test]
        public void TestNavigateThroughAllTheAppPages()
        {
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), "e5@gmail.com");
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), "e5");
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("cardImage").Index(3));
            app.Screenshot("Detail view");
            app.Back();
            app.Screenshot("Card View");
            app.Tap(x => x.Class("AppCompatImageView").Index(1));
            app.Screenshot("LikedView");
            app.Tap(x => x.Class("AppCompatImageView").Index(2));
            app.Screenshot("MapView");
            app.Tap(x => x.Marked("Google Map"));
            app.Back();
        }

        



        
        

    }
}

