using System.Net;
using System.Net.Http.Json;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Spg.Fachtheorie.Aufgabe3.API.Test;

namespace SPG_Fachtheorie.Aufgabe3.Test
{
    public class PaymentsControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PaymentsControllerTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        public static IEnumerable<object[]> GetPaymentsData =>
            new List<object[]>
            {
                new object[] { 1, null, 2 },
                new object[] { null, "2024-05-13", 1 },
                new object[] { 1, "2024-05-13", 1 }
            };

        [Theory]
        [MemberData(nameof(GetPaymentsData))]
        public async Task GetPayments_Filtered_ReturnsExpectedResults(int? cashDesk, string? dateFrom, int expectedCount)
        {
            var url = "/api/payments?";
            if (cashDesk.HasValue) url += $"cashDesk={cashDesk.Value}&";
            if (!string.IsNullOrWhiteSpace(dateFrom)) url += $"dateFrom={dateFrom}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var payments = await response.Content.ReadFromJsonAsync<List<PaymentDto>>();

            Assert.NotNull(payments);
            Assert.Equal(expectedCount, payments.Count);

            if (cashDesk.HasValue)
                Assert.True(payments.All(p => p.CashDesk.Number == cashDesk));

            if (!string.IsNullOrWhiteSpace(dateFrom))
                Assert.True(payments.All(p => p.Date >= DateTime.Parse(dateFrom)));
        }

        [Fact]
        public async Task GetPaymentById_ReturnsExpectedStatusCodes()
        {
            var validId = 1;
            var notFoundId = 999;

            var validResponse = await _client.GetAsync($"/api/payments/{validId}");
            Assert.Equal(HttpStatusCode.OK, validResponse.StatusCode);

            var notFoundResponse = await _client.GetAsync($"/api/payments/{notFoundId}");
            Assert.Equal(HttpStatusCode.NotFound, notFoundResponse.StatusCode);
        }

        [Fact]
        public async Task PatchPayment_ReturnsExpectedStatusCodes()
        {
            var validId = 1;
            var invalidId = 999;
            var badRequestId = 2; // z.B. bereits bestätigt

            var content = JsonContent.Create(new { });

            var okResponse = await _client.PatchAsync($"/api/payments/{validId}", content);
            Assert.Equal(HttpStatusCode.OK, okResponse.StatusCode);

            var notFound = await _client.PatchAsync($"/api/payments/{invalidId}", content);
            Assert.Equal(HttpStatusCode.NotFound, notFound.StatusCode);

            var badRequest = await _client.PatchAsync($"/api/payments/{badRequestId}", content);
            Assert.Equal(HttpStatusCode.BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public async Task DeletePayment_ReturnsExpectedStatusCodes()
        {
            var validId = 1;
            var notFoundId = 999;

            var ok = await _client.DeleteAsync($"/api/payments/{validId}");
            Assert.Equal(HttpStatusCode.NoContent, ok.StatusCode);

            var notFound = await _client.DeleteAsync($"/api/payments/{notFoundId}");
            Assert.Equal(HttpStatusCode.NotFound, notFound.StatusCode);
        }
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CashDeskDto CashDesk { get; set; } = null!;
    }

    public class CashDeskDto
    {
        public int Number { get; set; }
    }
}
