[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<br />
<p align="center">
  <a href="https://github.com/mrdaghestani/AsanPardakht.IPG">
    <img src="images/aplogo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">AsanPardakht IPG Sample</h3>

  <p align="center">
    <br />
    <br />
    <a href="https://ap-ipg-sample.itsbeta.ir/">View Demo</a>
    ·
    <a href="https://github.com/mrdaghestani/AsanPardakht.IPG/issues">Report Bug</a>
    ·
    <a href="https://github.com/mrdaghestani/AsanPardakht.IPG/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

The goal of this project is to make it easier to work with the AsanPardakht internet payment gateway (IPG).
This project includes packages that can be installed to make it easier to implement the internet payment gateway.

A list of commonly used resources that I find helpful are listed in the acknowledgements.

### Built With

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet)
* [Bootstrap](https://getbootstrap.com)
* [JQuery](https://jquery.com)



<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Installation

1. Register your merchant & get a MerchantConfiguraion with following [AsanPardakht website](https://asanpardakht.ir/ipg) instruction
2. Install latest version of `AsanPardakht.IPG` package from [nuget](https://www.nuget.org/packages/AsanPardakht.IPG/)

   ```
   Install-Package AsanPardakht.IPG
   ```
3. If you are using asp.net core just install latest version of `AsanPardakht.IPG.AspNetCore` package from [nuget](https://www.nuget.org/packages/AsanPardakht.IPG.AspNetCore/)

   ```
   Install-Package AsanPardakht.IPG.AspNetCore
   ```
4. Enter your MerchantConfiguraion in `appsettings.json`

   ```JSON
   {
      "AsanPardakhtIPGConfig": {
        "MerchantUser": "myUsername",
        "MerchantPassword": "myPassword",
        "MerchantConfigurationId": 1234
      }
   }
   ```
5. Call `AddAsanPardakhtIpg` method with an instance of `IConfiguration` to register default services in `Startup.cs` file

   ```csharp
   public IConfiguration Configuration { get; }

   public void ConfigureServices(IServiceCollection services)
   {
       services.AddAsanPardakhtIpg(Configuration);

       ,...
   }
   ```
6. If you need your own implementation of `AsanPardakht.IPG.Abstractions.ILocalInvoiceIdGenerator` register it as a service.
7. Now you can inject `AsanPardakht.IPG.Abstractions.IServices` in your classes.

<!-- USAGE EXAMPLES -->
## Usage

1. First you need to get a `Payment RefId (Token)` from `GenerateToken` method. You can use one of the following methods:
    * Use `IServices.GenerateBuyToken` method to get a token for a simple buy payment.
    * Use `IServices.GenerateBillToken` method to get a token for paying an existing _Bill_.
    * Use `IServices.GenerateTelecomeChargeToken` method to get a token for buying a _Telecom Sim Charge_.
    * Use `IServices.GenerateTelecomeBoltonToken` method to get a token for buying a _Telecom Package_.
    * Use `IServices.GenerateToken` method to get a token for a custom implementation.
    * For more information about `Token` method refer to the [Online Swagger Doc](https://ipgrest.asanpardakht.ir/index.html).
2. After getting a `RefId` from `Token` method, send user to the _Payment Gateway_ using `POST HTTP METHOD`
    * You can use the following _javascript_ code to send user to the _Payment Gateway_

        ```js
        function postRefId(gatewayUrl, refId, mobileap) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", gatewayUrl);
            form.setAttribute("target", "_self");

            var refIdField = document.createElement("input");
            refIdField.setAttribute("name", "RefId");
            refIdField.setAttribute("value", refId);
            form.appendChild(refIdField);

            if (mobileap) {
                var mobileField = document.createElement("input");
                mobileField.setAttribute("name", "Mobileap");
                mobileField.setAttribute("value", mobileap);
                form.appendChild(mobileField);
            }

            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
        postRefId(tokenResponse.GatewayUrl, tokenResponse.RefId, tokenResponse.Mobileap);
        ```
3. After payment your _callback_ url will called with some parameters, you can ignore those parameters and use `IServices.GetTranResult` method.
4. After receiving successful result from `IServices.GetTranResult` method, you have to _verify_ the payment using `IServices.Verify` method.
5. Now it's time to do whatever you like with your site order, for example check the _payment amount_ with _order amount_ and change its status to _Saccessful Payment_.
6. If your operations were successful call `IServices.Settle` method to complete the payment process else call `IServices.Reverse` method to cancel payment and return money back to user.

_For exploring a working example, please refer to the [HomeController](https://github.com/mrdaghestani/AsanPardakht.IPG/blob/master/ApIpgSample/Controllers/HomeController.cs) file_



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/mrdaghestani/AsanPardakht.IPG/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**. :smirk:

1. Fork the Project
1. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
1. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
1. Push to the Branch (`git push origin feature/AmazingFeature`)
1. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

MohammadReza Daghestani - [@mrdaghestani](https://twitter.com/mrdaghestani) - MRDaghestani@gmail.com

Project Link: [https://github.com/mrdaghestani/AsanPardakht.IPG](https://github.com/mrdaghestani/AsanPardakht.IPG)





[contributors-shield]: https://img.shields.io/github/contributors/mrdaghestani/AsanPardakht.IPG.svg?style=for-the-badge
[forks-shield]: https://img.shields.io/github/forks/mrdaghestani/AsanPardakht.IPG.svg?style=for-the-badge
[stars-shield]: https://img.shields.io/github/stars/mrdaghestani/AsanPardakht.IPG.svg?style=for-the-badge
[issues-shield]: https://img.shields.io/github/issues/mrdaghestani/AsanPardakht.IPG.svg?style=for-the-badge
[license-shield]: https://img.shields.io/github/license/mrdaghestani/AsanPardakht.IPG.svg?style=for-the-badge
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[contributors-url]: https://github.com/mrdaghestani/AsanPardakht.IPG/graphs/contributors
[forks-url]: https://github.com/mrdaghestani/AsanPardakht.IPG/network/members
[stars-url]: https://github.com/mrdaghestani/AsanPardakht.IPG/stargazers
[issues-url]: https://github.com/mrdaghestani/AsanPardakht.IPG/issues
[license-url]: https://github.com/mrdaghestani/AsanPardakht.IPG/blob/master/LICENSE.txt
[linkedin-url]: https://www.linkedin.com/in/mrdaghestani/
