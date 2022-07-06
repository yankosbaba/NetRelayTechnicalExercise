using Microsoft.Extensions.DependencyInjection;
using NetRelayTechnical.Factory;
using NetRelayTechnical.Model;
using System;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace NetRelayTechnical.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private static IHttpClientFactory _clientFactory;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static AppSettings _appSettings;
        private static CookieContainer _cookieContainer;
        private const string JsonDefaultContentType = "application/json";
        private const string JwtDefaultContentType = "application/jwt";

        public ScenarioHooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun(Order = HookOrdering.TestSetting)]
        public static void SetUpBeforeTestRun()
        {
            var appSettingsFactory = new AppSettingsFactory();
            _appSettings = appSettingsFactory.LoadAppSettings();

            SetUpHttpClientFactory();
        }
        [BeforeScenario(Order = HookOrdering.ScenarioHooks)]
        public void SetUpBeforeEachScenarioStarts()
        {
            _scenarioContext.Set(_appSettings);
            _scenarioContext.Set(_clientFactory);
        }

        [AfterTestRun(Order = 15)]
        public static void ResetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("TEST_RUN_ENVIRONMENT", null);
        }

        private static void SetUpHttpClientFactory()
        {
            var service = new ServiceCollection();



            _clientFactory = service.AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();

            _clientFactory = service.AddHttpClient("testharness", c =>
            {
                c.BaseAddress = new Uri(_appSettings.TestHarness.URI);
                c.DefaultRequestHeaders.Add("Accept", JsonDefaultContentType);
                c.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            }).Services.BuildServiceProvider().GetService<IHttpClientFactory>();
        }
    }
}
