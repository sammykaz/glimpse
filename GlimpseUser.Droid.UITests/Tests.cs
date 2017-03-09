using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using Glimpse.Core.Model;
using System.Collections.Generic;

namespace GlimpseUser.Droid.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;
        private readonly string _testEmail = "e5@gmail.com";
        private readonly string _testPassword = "e5";

        [SetUp]
        public void BeforeEachTest()
        {
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
  

            //Arrange scenario condition
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), _testEmail);
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), _testPassword);
            app.Tap(x => x.Id("btnSignIn"));
            app.WaitForElement("cardImage");

            //Act
            var initialCardStack = app.Query(x => x.Id("cardImage"));
            app.SwipeRightToLeft();
            app.Screenshot("Swiping Top Card");
            app.SwipeRightToLeft();
            app.Screenshot("Second Top Card");
            app.SwipeLeftToRight();
            var initialInde = app.Query(x => x.Id("cardImage"));
            var cardStackAfterSwipes = app.Query(x => x.Id("cardImage"));

            //Assert
            Assert.IsTrue((initialCardStack.Length -3 )== cardStackAfterSwipes.Length);

            
        }

        [Test]
        public void TestNavigateThroughAllTheAppPages()
        {

            //Arrange scenario condition
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), _testEmail);
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), _testPassword);
            app.Tap(x => x.Id("btnSignIn"));
            app.WaitForElement("cardImage");
            app.SwipeLeftToRight();


            //Act 
            //retrieving specific cardview element
            var cardViewElement = app.Query(x => x.Id("cardImage")).GetValue(0);            
            app.Tap(x => x.Id("cardImage"));

            //retrieving specific detail view element + screenshot of view
            app.Screenshot("Detail view");
            var detailViewElement = app.Query(x => x.Id("detailViewTitle")).GetValue(0);
            app.Back();

            //retrieving specific card view element + screenshot of view
            app.Screenshot("Card View");
            cardViewElement = app.Query(x => x.Id("cardImage"));
            app.Tap(x => x.Class("AppCompatImageView").Index(1));

            //retrieving specific like promotion view element + screenshot of view
            app.Screenshot("Like Promotion View");
            var likedPromotionViewElement = app.Query(x => x.Id("promotion_picture")).GetValue(0);
            app.Tap(x => x.Id("promotion_picture"));

            //retrieving specific detail view element + screenshot of view
            app.Screenshot("Detail view2");
            detailViewElement = app.Query(x => x.Id("detailViewTitle"));
            app.Back();

            //retrieving specific like promotion view element + screen shot
            app.Screenshot("LikedView2");
            likedPromotionViewElement = app.Query(x => x.Id("promotion_picture")).GetValue(0);

            //retrieving specific like promotion view element + screen shot
            app.Tap(x => x.Class("AppCompatImageView").Index(2));
            app.Screenshot("MapView");
            var mapViewElement = app.Query(x => x.Id("map")).GetValue(0) ;

            //Assert
            //if the elements are not null, then the above views exist
            Assert.IsTrue(cardViewElement != null && detailViewElement != null && likedPromotionViewElement != null && mapViewElement != null);

        }

        [Test]
        public void TestNavigateToTheMapViewAndViewPromotions()
        {
            //Arrange scenario condition(sign in)
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), _testEmail);
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), _testPassword);
            app.Tap(x => x.Id("btnSignIn"));

            //Act
            app.Tap(x => x.Class("AppCompatImageView").Index(2));
            app.ScrollUp();
            app.ScrollUp();
            app.Tap(x => x.Marked("Google Map"));
            app.Tap(x => x.Marked("Zoom out"));
            app.Tap(x => x.Marked("Zoom out"));
            app.Tap(x => x.Marked("Zoom out"));
            app.Tap(x => x.Marked("Zoom out"));
            app.Tap(x => x.Marked("Zoom out"));
            app.Screenshot("Promos with no Cluster");
            app.Tap(x => x.Marked("Zoom out"));
            app.Tap(x => x.Marked("Zoom out"));
            app.Screenshot("Promos with  Cluster");
        }

        [Test]
        public void NewTest()
        {
            //Arrange scenario condition(sign in)
            app.Tap(x => x.Id("btnSignIn"));
            app.Tap(x => x.Id("txtEmail"));
            app.EnterText(x => x.Id("txtEmail"), _testEmail);
            app.Tap(x => x.Id("txtPassword"));
            app.EnterText(x => x.Id("txtPassword"), _testPassword);
            app.Tap(x => x.Id("btnSignIn"));
            app.WaitForElement("cardImage");


            //Act
            //acting on cardview
            app.Tap(x => x.Text("Apparel"));           
            var cardContainsWord = app.Query("cardTitle").First(result => result.Text.ToLower().Contains("apparel"));    
            app.Tap(x => x.Text("All"));
            app.Tap(x => x.Id("search_button"));
            app.EnterText(x => x.Id("search_src_text"), "apparel");
            cardContainsWord = app.Query("cardTitle").First(result => result.Text.ToLower().Contains("apparel"));
            app.SwipeLeftToRight();

            //acting on like view text1
            app.Tap(x => x.Id("search_close_btn"));
            app.ClearText(x => x.Id("search_src_text"));
            app.Tap(x => x.Class("AppCompatImageView").Index(1));
            app.Tap(x => x.Text("Apparel"));
            var elementContainsWord = app.Query("text1").First(result => result.Text.ToLower().Contains("apparel"));
            app.Tap(x => x.Text("All"));
            app.Tap(x => x.Id("search_button"));
            app.EnterText(x => x.Id("search_src_text"), "apparel");
            elementContainsWord = app.Query("text1").First(result => result.Text.ToLower().Contains("apparel"));
            app.Tap(x => x.Id("search_close_btn"));
            app.ClearText(x => x.Id("search_src_text"));

            //acting on map view
            app.Tap(x => x.Class("AppCompatImageView").Index(2));
            app.WaitForElement(x => x.Marked("Google Map"));
            app.Screenshot("All promotions");
            app.Tap(x => x.Text("Apparel"));
            app.Screenshot("apparel promotions");
            app.Tap(x => x.Text("All"));
            app.Screenshot("All promotions2");
            app.Tap(x => x.Id("search_button"));
            app.EnterText(x => x.Id("search_src_text"), "apparel");
            app.Screenshot("apparel promotions2");
            app.Tap(x => x.Id("search_close_btn"));
            app.ClearText(x => x.Id("search_src_text"));

            //assert for cardview and likeview
            Assert.IsTrue(cardContainsWord != null && elementContainsWord != null);

        }

        
     
    





    }
}

