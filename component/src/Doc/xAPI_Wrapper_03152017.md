<left>

# xAPI - Capture Human Performance

</left>

## What's Experience API (xAPI)?

<row>
xAPI lets applications share data about human performance
(broadly defined). More precisely, xAPI lets you capture (big) data
on human performance, along with associated instructional content
or performance context information (i.e., experience). xAPI
applies “activity streams” to tracking data and provides sub-APIs
to access and store information about state and content. This
enables nearly dynamic tracking of activities from any platform
or software system, from traditional Learning Management Systems
to mobile devices, simulations, wearables, physical beacons,
and more. 

#### For more information refer to : [adlnet.gov](https://www.adlnet.gov/xapi)

*****
<row>

![xAPI - Capture Human Experience!](http://48gz441kpuih163ara2ciqn6.wpengine.netdna-cdn.com/wp-content/uploads/2012/05/tin-can-api.jpg "xAPI!")

</row>
<column>

### Q: What we can track with xAPI?
### A: Track micro-behaviors, state, and context, such as…
- Reading an article or interacting with an eBook
- Watching a training video, stopping and starting it
- Training data from a simulation
- Performance in a mobile app
- Chatting with a mentor
- Physiological measures, such as heart-rate data
- Micro-interactions with e-learning content
- Team performance in a multi-player serious game
- Quiz scores and answer history by question
- Real-world performance in an operational context

</column>
</row>

<row>
<column>

## Why do we need xAPI?
- Track any experience (formal, informal, or operational)
- Platform and subject-matter agnostic
- Built upon RESTful and JSON (so it’s lightweight)
- Human (and machine) readable
- 100% free with no licensing fees or vendor lock-in
- Open source and community maintained

</column>

</row>

***

## xAPIWrapper - Xamarin and .NET Standard Component for xAPI

<row>

xAPIWrapper is a Xamarin and .NET Standard component built to simplify interfacing with standards based LRS from any Xamarin iOS, Android or Windows Mobile Application or any .NET Web, Enterprise or Desktop application. The component provides a simple interface, very similar to adding Log statements to the code. It has easy-to-use api's for Android and iOS, helpful classes and methods to track user interaction with any digital assets in a simple Actor, Object, Verb verbatim. The complex interactions can be captured in form of statements, activities or scores using simple log like statements.

## Why use the xAPIWrapper?
1. It's a .NET Standard library, that support majority of the .NET apps
2. An xAPIWrapper.js port implemented in C# that can be leveraged within your Xamarin iOS or Android app to interface with any LRS
3. It abstracts the complexities and semantics of the xAPI specification
4. It eliminates the need for end users to know the internals of the xAPI specification
5. Using the library to generate statements is as easy as adding log statements to your code

## Installation and Usage
Import component in your Android and iOS apps.

### For Android 
Brief: Following code can be used to send a xAPI statement to an LRS
```
button.Click += async delegate
{
    var apiWrapper = new APIWrapper("xAPI Endpoint", "Your Identity", "Your Secret");
    var statement = apiWrapper.PrepareStatement("test@ald.net", "experienced", "Activity");
    var task = await apiWrapper.SendStatement(statement);
    if (task.Success)
    {
        button.Text = $"{_count++} sent!";
    }
};
```

### For iOS 
Brief: Following code can be used to send a xAPI statement to an LRS
```
Button.TouchUpInside += async delegate
{
   var apiWrapper = new APIWrapper("xAPI Endpoint", "Your Identity", "Your Secret");
   var statement = apiWrapper.PrepareStatement("test@ald.net", "experienced", "Activity");
   var task = await apiWrapper.SendStatement(statement);
   var title = $"sending {_count} statement!";
   if (task.Success)
   {
      title = $"{_count++} statements sent!";
   }
   Button.SetTitle(title, UIControlState.Normal);
};
```

#### It's Free and Open Source!

> xAPIWrapper is a open source under a Apache License and is 100% free for anyone to use to enhance. 

## xAPIWrapper is Asynchronous by Design 

 Each interface and API has dual implementation with synchronous and asynchronous calls using async-await pattern to be inherently high performing. The xAPIWrapper is unit tested from within the web and mobile apps to test each interfaces standards compliance. 

![xAPIWrapper - Total Coverage!](http://xapi.iworktech.com/Images/UnitTest1.png "xAPIWrapper - Unit Testing - 1") ![xAPIWrapper - Unit Testing Snapshot!](http://xapi.iworktech.com/Images/UnitTest2.png "xAPIWrapper - Unit Testing - 2") 

***

<left>

Our mission at IWORKTECH is to __inspire ingenuity to transform innovation__. Please [share your feedback][1] via email or replying to this blog post. 

To un-subscribe please click here: [send us an email!][1]

[1]: mailto:info@iworktech.com
</left>
