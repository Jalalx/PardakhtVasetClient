# PardakhtVasetClient
[![Build status](https://ci.appveyor.com/api/projects/status/h4ow4efs3f6j2buk?svg=true)](https://ci.appveyor.com/project/Jalalx/pardakhtvasetclient)

Install using [Nuget.org](https://www.nuget.org/packages/Septa.PardakhtVaset.Client):

    install-package Septa.PardakhtVaset.Client


# How to use?

First, you need to obtain an API key. Go to https://pardakhtvaset.com and register a new account and get an API key.

In your project, create a public instance of `PardakhtVasetClient` type like this:

     var options = new PardakhtVasetClientOptions();
     options.ConnectionString = "<my connection string>";
     options.ApiKey = "<my api key>";
     options.TablePrefix = "<optional table prefix>";

     _client = new PardakhtVasetClient(options);

Life scope of `_client` must be same as life scope of your application. For windows applications, create instance in main window and in web applications, create instance in `Global.asax` file.

And now you need to initialize it:

    _client.Init("<null or a cluster id>");
    
Cluster id is not required in most cases. Using cluster id, you can separate links for each instance of application in a network. This is useful for desktop applications distributed in a local network.

And now you have to subscribe to `PaymentLinkChanged` event and start notification service:

    _client.PaymentLinkNotificationService.PaymentLinkChanged += PaymentLinkNotificationService_PaymentLinkChanged;
    _client.PaymentLinkNotificationService.Start(TimeSpan.FromMinutes(2));
    
    private void PaymentLinkNotificationService_PaymentLinkChanged(object sender, PaymentLinkChangedEventArgs e)
    {
        if (e.Status == PardakhtVasetServices.RequestStatus.Paid)
        {
            MessageBox.Show(string.Format("Payment link with token '{0}' and follow id '{1}' is paid at '{2}'", e.Token, e.FollowId, e.ResultDate),
                "Paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        e.Handled = true;
    }
    
## Creating a payment link
To create a payment link, you need to call `Create` method of `_client` like this:

    var link = _client.Create(1230000/*amount (in Rials)*/, null/* followId*/, "0123"/* invoice number*/,
            new DateTime(2018,2,2) /* invoice Date*/, 30 /* expire in days*/, "sample link"/* description*/);
        
You can use `link.Url` to get payment link and send it to payer via SMS, Email or any Social Network application.

For more info, checkout the sample windows application in this repository.
