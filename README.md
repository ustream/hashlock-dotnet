# Setup IBM Cloud Video Viewer Authentication Demo

## Requirments
* An [IBM Cloud Video Streaming Manager for Enterprise with Authentication API account](https://www.ustream.tv/product/align-secure-streaming-video) *(You can request a Trial account [here](https://www.ustream.tv/enterprise-video/contact-internal-communications))*
* Install [.NET Core SDK](https://www.microsoft.com/net/download)

## Installion
1. Setup channel authentication for your channel(s) on Channels/Authentication dashboard
   * Secret Key: **Some random string**, this secret will used to sing and validate autorization data
   * URL to the entry point of the authentication flow: **http://127.0.0.1:5000/Home/Authorization** The end pont where you can authorize your users to the content and could generate the signed hash data
   ![Set authentication](./doc/set_authentication.png "Set authentication")
2. Set Secret Key in [HomeController.cs](Controllers/HomeController.cs)
```
    ...
        private const string hashSecret = "TOPSECRET";
    ...
```
3. Set a video id in [HomeController.cs](Controllers/HomeController.cs) which will be used in this example. (This video need to be under the channel which is setup above.)
```
    ...
        private const int videoId = 1111;
    ...
```
4. Run demo on localhost
```
dotnet run
```

# Resources
[IBM Cloud Video Viewer Authentication API](http://developers.ustream.tv/channel-api/viewer-authentication-api.html)