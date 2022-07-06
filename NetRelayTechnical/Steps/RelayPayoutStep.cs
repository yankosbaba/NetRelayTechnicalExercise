using FluentAssertions;
using NetRelayTechnical.Base;
using NetRelayTechnical.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace NetRelayTechnical.Steps
{
    [Binding]
    public class RelayPayoutStep
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private AppSettings _appSettings;
        private readonly ApiClient _apiCLient;
        private PayoutResponse _payoutResponse;
        private readonly Payout _payout;
        private string Result;
        private string _statusCode;
        private JToken jToken;

        public RelayPayoutStep(ScenarioContext scenarioContext, FeatureContext featureContext, AppSettings appSettings, ApiClient apiClient)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _appSettings = appSettings;
            _apiCLient = apiClient;
        }
        [Given(@"a GET request is made to payouts EndPoint with status code '(.*)'")]
        public async Task GivenPayoutEndPointIsCalledForEndPointWithStatusCode(string p0)
        {
            _appSettings = _scenarioContext.Get<AppSettings>();
            _statusCode = await _apiCLient.GetContentAsync($"{_appSettings.TestHarness.URI}" + p0);
        }
        [Given(@"a GET request is made to payouts EndPoint One '(.*)'")]
        public async Task GivenAGETRequestIsMadeToPayoutsEndPointOne(string p0)
        {
            _appSettings = _scenarioContext.Get<AppSettings>();
            _payoutResponse = await _apiCLient.GetAsync<PayoutResponse>($"{_appSettings.TestHarness.URI}" + p0);
        }
        [Given(@"a GET request is made to payouts EndPoint Two '(.*)'")]
        public void GivenAGETRequestIsMadeToPayoutsEndPointTwo(string p0)
        {
            _appSettings = _scenarioContext.Get<AppSettings>();
            Result =  _apiCLient.GetSerilizeObjectAsync($"{_appSettings.TestHarness.URI}" + p0).Result;
        }

        [Then(@"verify a status (.*) response code")]
        public void ThenVerifyStatusResponse(string res)
        {
            _statusCode.Should().Be(res);
        }
        [Then(@"Verify charged amount is Integer value for EndPoint One")]
        public void VerifyIntegeValueResponseEndPoint1()
        {
            bool isInt = false;
            var checkType = _payoutResponse.payouts[1].chargedAmount;

            if (checkType.GetType()== typeof(int))
            {
                isInt = true;
            }

            isInt.Should().BeTrue();
        }
        [Then(@"Verify charged amount is Integer value for Endpoint Two")]
        public void VerifyIntegeValueResponseEndPoint2()
        {
            bool isInt = false;
            var obj = JObject.Parse(Result);
            jToken = obj.SelectToken("amount");
            string amount = jToken.ToString();

            Int64.Parse(amount);

            if(amount.GetType() == typeof(int))
            {
                isInt = true;
            }

            isInt.Should().BeTrue();
        }

        [Then(@"Verify payment action is not empty and contains (.*)")]
        public void ThenVerifyPaymentActionIsNotEmptyAndContainsCHARGE(string value)
        {
           
            var obj = JObject.Parse(Result);
            jToken = obj.SelectToken("paymentActions[0].paymentAction");
            string paymentAction = jToken.ToString();
            paymentAction.Should().Be(value);
            
        }

        [Then(@"verify number of payouts is greater than one")]
        public void ThenVerifyNumberOfPayoutsIsGreaterThan()
        {
            bool isCheck = false;
            var numberOfPayout = _payoutResponse.numberOfPayouts;
            if (numberOfPayout > 1)
            {
                isCheck=true;
            }
            isCheck.Should().BeTrue();
        }
        [Then(@"Verify required response data")]
        public void ThenVerifyRequiredResponseData()
        {
            var date =_payoutResponse.payouts[1].date;
            var bankAccountIban = _payoutResponse.payouts[1].bankAccountIban;
            var bankAccountBic = _payoutResponse.payouts[1].bankAccountBic;
            var reference = _payoutResponse.payouts[1].reference;

            date.Should().NotBeNull();
            bankAccountIban.Should().NotBeNull();
            bankAccountBic.Should().NotBeNull();
            reference.Should().NotBeNull();

        }

    }
}
