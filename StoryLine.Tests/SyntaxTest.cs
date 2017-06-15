using Xunit;

namespace StoryLine.Tests
{
    public class User
    {
        
    }

    public class SyntaxTest 
    {
        [Fact]
        public void Test()
        {
            //var actor = new Actor();
            //var secondActor = new Actor();

            //Rest.Config.AddServiceEndpont("CRM", "http://localhost:32769/");
            //Wiremock.Config.SetBaseAddress("http://localhost:32769/");

            //Wiremock.Config.Client.ResetAll();
            //Scenario.New()
            //    .Given()
            //        .HasPerformed<StubResponse>(x => x
            //            .Request()
            //                .Method("GET")
            //                .Url(p => p.EqualTo("/dragon"))
            //            .Response()
            //                .Header("Content-Type", "text/plain")
            //                .Body("Dragon123"))
            //        .HasPerformed<HttpRequest>(x => x
            //            .Service("CRM")
            //            .Method("GET")
            //            .Url("/dragon"))
                
            //    .When()
            //        .Performs<HttpRequest>(x => x
            //            .Service("CRM")
            //            .Method("GET")
            //            .Url("/dragon"))
            //    .Then()
            //        .Expects<StubRequest>(x => x
            //            .Request()
            //                .Url(p => p.Matching("/dragon"))
            //            .Received()
            //                .Twice())
            //        .Expects<HttpResponse>(x => x
            //            .Status(200)
            //            .TextBody()
            //                .EqualTo("Dragon123"))
            //    .Run();


            //Scenario.New()
            //    .Given(actor)
            //        .HasPerformed<HttpRequest>()
            //            .Service("dragon")
            //            .Url("dragon")
            //            .Path("xxx")
            //            .QueryParameter("xxx", "ddd")
            //            .TextBody("Somt test content")
            //            .JsonObjectBody()
            //                .FromObject(new User())
            //    .When(actor)
            //        .Performs<HttpRequest>()
            //            .Url("xxx")
            //            .Header("Content-Type", "xxx")
            //            .Header("Content-Type", "xxx")
            //    .Then(secondActor)
            //        .Expects<HttpResponse>()
            //            .Service("crm")
            //            .Url()
            //                .Matching("*.*")
            //            .Header("xxx", "aaa")
            //            .Header("xxx")
            //                .Containing("applicaiton.json")
            //            //.JsonObjectBody()
            //            //    .FromResourceFile()
            //            //.TextBody()
            //            //    .Matching("xxx")
            //        .Expects<HttpResponse>()
            //    .Run();



            //var request = new Request
            //{
            //    Method = HttpMethod.Get,
            //    Service = "Crm",
            //    Headers = Headers.New()
            //        .Accept("application/json", "application/xml")
            //        .ContentType("plain/text")
            //        .ToHeaders(),
            //    Url = Url
            //        .Path("/v1/clients")
            //        .QueryParam("param1", "xxxxx")
            //        .QueryParam("param2", "bbb"),
            //    Body = PlainText
            //        .Value("Some test")
            //        .Encoding(Encoding.UTF8)
            //};

            //var response = new RestClient().Send(request);

            //response.Has().BodyText().EqualTo("xxx");

            //response.BodyText().Should().Be("xxx");
            //response.BodyData<User>().ShouldBeEquivalentTo(new User());
            //response.ContentType().Should().Be("xxx");
            //response.Location().Should().Be("xxxx");
        }
    }
}
