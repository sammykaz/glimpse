﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Plugins.Messenger;
using Glimpse.Core.Contracts.Services;
using Glimpse.Core.Services.Data;
using Glimpse.Core.UnitTests.Mocks;
using Glimpse.Core.ViewModel;
using Glimpse.Core.Fake;

namespace Glimpse.Core.UnitTests.Tests.ViewModels
{
    [TestClass]
    public class SearchJourneyViewModelTests
    {
        [TestMethod]
        public async Task Test_LoadFromCitiesCorrectly()
        {
            //arrange
            CityDataService mockCityDataService = ServiceMocks.GetMockCityDataService(3);
            var mockMessenger = new Mock<IMvxMessenger>();
            var mockConnectionService = new Mock<IConnectionService>();
            var mockDialogService = new Mock<IDialogService>();

            //act
            var searchJourneyViewModel = new SearchJourneyViewModel(mockMessenger.Object, mockCityDataService, mockConnectionService.Object, mockDialogService.Object );
            await searchJourneyViewModel.LoadCities();

            //assert
            Assert.AreEqual(searchJourneyViewModel.FromCities.Count, 3);
        }

        [TestMethod]
        public async Task Test_Initialize()
        {
            //arrange
            CityDataService mockCityDataService = ServiceMocks.GetMockCityDataService(3);
            var mockMessenger = new Mock<IMvxMessenger>();
            var mockConnectionService = new Mock<IConnectionService>();

            var a = mockConnectionService.Setup(m => m.CheckOnline()).Returns(true);

            var mockDialogService = new Mock<IDialogService>();
            
            //act
            var searchJourneyViewModel = new FakeSearchJourneyViewModel(mockMessenger.Object, mockCityDataService, mockConnectionService.Object, mockDialogService.Object);
            await searchJourneyViewModel.InitializeAsync();

            //assert
            Assert.IsNotNull(searchJourneyViewModel.FromCities);
            Assert.IsNotNull(searchJourneyViewModel.ToCities);
            Assert.IsNotNull(searchJourneyViewModel.SelectedFromCity);
            Assert.IsNotNull(searchJourneyViewModel.SelectedToCity);
            Assert.IsNotNull(searchJourneyViewModel.SelectedHour);
        }
    }
}
