using Moq;
using PardakhtVasetServices;
using Septa.PardakhtVaset.Client;
using System;
using Xunit;

namespace PardakhtVasetClientTests
{
    public class PaymentLinkNotificationServiceTests
    {
        [Fact]
        public void OnTick_WithPaidLinkAndHandlingPaymentLinkEvent_WorksAsExpected()
        {
            var options = new PardakhtVasetClientOptions();
            options.ApiKey = "foo";
            options.Password = "my password";
            options.ConnectionString = "fake connection string";
            options.TablePrefix = "";

            var mockedRepository = new Mock<IPaymentLinkRepository>();
            var mockedPayRequest = new Mock<IPayRequestV2>();

            var nextPaymentLink = new PaymentLink();
            nextPaymentLink.Amount = 26000;
            nextPaymentLink.BankReferenceId =null;
            nextPaymentLink.CreateDate = new DateTime(2017, 12, 3);
            nextPaymentLink.Description = "بابت مانده حساب";
            nextPaymentLink.ExpireDays = 30;
            nextPaymentLink.FollowId = "324";
            nextPaymentLink.LastCheckForUpdateDate = new DateTime(2017, 12, 3);
            nextPaymentLink.PaymentStatus = (int)RequestStatus.Initiated;
            nextPaymentLink.ResultDate = null; 
            nextPaymentLink.Token = "1d372049-17de-4550-aecc-06111fdf2a9b";
            nextPaymentLink.Url = "https://pardakhtvaset.com/pay?token=1d372049-17de-4550-aecc-06111fdf2a9b";

            mockedRepository.Setup(x => x.GetNextLinkForCheck(null)).Returns(nextPaymentLink);

            var fakeEpayResult = new EPayRequestCheckResult();
            fakeEpayResult.BankReferenceId = "57430753503405340";
            fakeEpayResult.RequestStatus = RequestStatus.Paid;
            fakeEpayResult.Success = true;
            fakeEpayResult.VerifyDate = new DateTime(2017, 12, 4);
            mockedPayRequest.Setup(x => x.Check(options.ApiKey, options.Password, nextPaymentLink.Token)).Returns(fakeEpayResult);
            
            var service = new PaymentLinkNotificationService(options, mockedRepository.Object, mockedPayRequest.Object);
            service.PaymentLinkChanged += (o, e) => e.Handled = true;

            service.OnTick();

            mockedRepository.Verify(x => x.GetNextLinkForCheck(It.IsAny<string>()), Times.Once());
            mockedPayRequest.Verify(x => x.Check(options.ApiKey, options.Password, nextPaymentLink.Token), Times.Once());
            mockedRepository.Verify(x => x.Update(nextPaymentLink), Times.Once());
        }

        [Fact]
        public void OnTick_WithPaidLinkAndNotHandlingPaymentLinkEvent_NotCallingUpdate()
        {
            var options = new PardakhtVasetClientOptions();
            options.ApiKey = "foo";
            options.Password = "my password";
            options.ConnectionString = "fake connection string";
            options.TablePrefix = "";

            var mockedRepository = new Mock<IPaymentLinkRepository>();
            var mockedPayRequest = new Mock<IPayRequestV2>();

            var nextPaymentLink = new PaymentLink();
            nextPaymentLink.Amount = 26000;
            nextPaymentLink.BankReferenceId = null;
            nextPaymentLink.CreateDate = new DateTime(2017, 12, 3);
            nextPaymentLink.Description = "بابت مانده حساب";
            nextPaymentLink.ExpireDays = 30;
            nextPaymentLink.FollowId = "324";
            nextPaymentLink.LastCheckForUpdateDate = new DateTime(2017, 12, 3);
            nextPaymentLink.PaymentStatus = (int)RequestStatus.Initiated;
            nextPaymentLink.ResultDate = null;
            nextPaymentLink.Token = "1d372049-17de-4550-aecc-06111fdf2a9b";
            nextPaymentLink.Url = "https://pardakhtvaset.com/pay?token=1d372049-17de-4550-aecc-06111fdf2a9b";

            mockedRepository.Setup(x => x.GetNextLinkForCheck(It.IsAny<string>())).Returns(nextPaymentLink);

            var fakeEpayResult = new EPayRequestCheckResult();
            fakeEpayResult.BankReferenceId = "57430753503405340";
            fakeEpayResult.RequestStatus = RequestStatus.Paid;
            fakeEpayResult.Success = true;
            fakeEpayResult.VerifyDate = new DateTime(2017, 12, 4);
            mockedPayRequest.Setup(x => x.Check(options.ApiKey, options.Password, nextPaymentLink.Token)).Returns(fakeEpayResult);

            var service = new PaymentLinkNotificationService(options, mockedRepository.Object, mockedPayRequest.Object);
            service.PaymentLinkChanged += (o, e) => e.Handled = false;

            service.OnTick();

            mockedRepository.Verify(x => x.GetNextLinkForCheck(null), Times.Once());
            mockedPayRequest.Verify(x => x.Check(options.ApiKey, options.Password, nextPaymentLink.Token), Times.Once());
            mockedRepository.Verify(x => x.Update(nextPaymentLink), Times.Never());
        }

        [Fact]
        public void OnTick_CheckResultFailsForSomeReason_NotCallingEventNorUpdate()
        {
            var paymentLinkEventCalled = false;
            var options = new PardakhtVasetClientOptions();
            options.ApiKey = "foo";
            options.Password = "my password";
            options.ConnectionString = "fake connection string";
            options.TablePrefix = "";

            var mockedRepository = new Mock<IPaymentLinkRepository>();
            var mockedPayRequest = new Mock<IPayRequestV2>();

            var nextPaymentLink = new PaymentLink();
            nextPaymentLink.Amount = 26000;
            nextPaymentLink.BankReferenceId = null;
            nextPaymentLink.CreateDate = new DateTime(2017, 12, 3);
            nextPaymentLink.Description = "بابت مانده حساب";
            nextPaymentLink.ExpireDays = 30;
            nextPaymentLink.FollowId = "324";
            nextPaymentLink.LastCheckForUpdateDate = new DateTime(2017, 12, 3);
            nextPaymentLink.PaymentStatus = (int)RequestStatus.Initiated;
            nextPaymentLink.ResultDate = null;
            nextPaymentLink.Token = "1d372049-17de-4550-aecc-06111fdf2a9b";
            nextPaymentLink.Url = "https://pardakhtvaset.com/pay?token=1d372049-17de-4550-aecc-06111fdf2a9b";

            mockedRepository.Setup(x => x.GetNextLinkForCheck(It.IsAny<string>())).Returns(nextPaymentLink);

            var fakeEpayResult = new EPayRequestCheckResult();
            fakeEpayResult.BankReferenceId = null;
            fakeEpayResult.RequestStatus = RequestStatus.Initiated;
            fakeEpayResult.ExceptionType = ExceptionType.ArgumentException;
            fakeEpayResult.Message = "some random error message";
            fakeEpayResult.Success = false;
            fakeEpayResult.VerifyDate = null;
            mockedPayRequest.Setup(x => x.Check(options.ApiKey, options.Password, nextPaymentLink.Token)).Returns(fakeEpayResult);

            var service = new PaymentLinkNotificationService(options, mockedRepository.Object, mockedPayRequest.Object);
            service.PaymentLinkChanged += (o, e) => paymentLinkEventCalled = true;

            service.OnTick();

            mockedRepository.Verify(x => x.GetNextLinkForCheck(It.IsAny<string>()), Times.Once());
            mockedPayRequest.Verify(x => x.Check(options.ApiKey, options.Password, nextPaymentLink.Token), Times.Once());
            mockedRepository.Verify(x => x.Update(nextPaymentLink), Times.Never());
            Assert.True(paymentLinkEventCalled);
        }

    }
}
