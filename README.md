# redis-stat.net

_redis-stat.net_ is a simple Redis monitoring tool written on .NET 4.5. It allows you to monitor Redis instances with a dashboard page that is served from an IIS instance.

It has been ported from [redis-stat](https://github.com/junegunn/redis-stat) a nice Ruby application (with more features that this one), and it was created with the sole purpose of avoiding the instalation of ruby, java or jruby in your windows servers, allowing you to run it natively from Windows.

This utility is still a work in progress, I will continue updating it until it gets to a point that it works as close as the original application. If you find issues, file them in the issues section so that I can take a look at them.

This version only contains the dashboard page, there is no console application loging.

## Technologies used

- .NET Framework 4.5
- MVC 5
- SignalR 2
- StackExchange.Redis

## Installation

1. Clone it to your desktop.
2. Publish the app to IIS using .NET 4.0 as the apppool's preferred framework.
3. Enable WebSockets protocol in Roles and Features on your Windows Server (Support for SignalR)
4. Modify settings as needed.

## Usage

Modify web.config file manually or from IIS and set following properties.

```
  <appSettings>
    <add key="Verbose" value="false" />
    <add key="RedisPassword" value="your redis password" />
    <add key="Interval" value="2" />
    <add key="Hosts" value="localhost:6379" />
  </appSettings>
```

Or if you feel more adventurous, implement a new class from IOptions.cs and access those 4 values from wherever you want. (don't forget to register it on the IoC container for it to work)

### Screenshot

![Dashboard](https://github.com/junegunn/redis-stat/raw/master/screenshots/redis-stat-web.png)


## Author of this tool

- [Alejandro Mora](https://github.com/amd989) 

## Thanks To 

Original Author of redis-stat. He has made an amazing job at creating that tool from scratch. Please check his version first to get more functionality than this version and showing him some support.
- [Junegunn Choi](https://github.com/junegunn) 

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Added some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request


