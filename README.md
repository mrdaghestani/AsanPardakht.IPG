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
    <a href="https://ap_ipg_sample.itsbeta.ir/">View Demo</a>
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

[![Product Name Screen Shot][product-screenshot]](https://ap_ipg_sample.itsbeta.ir/)

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
      },...
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

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/othneildrew/Best-README-Template/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Your Name - [@your_twitter](https://twitter.com/your_username) - email@example.com

Project Link: [https://github.com/your_username/repo_name](https://github.com/your_username/repo_name)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
* [Img Shields](https://shields.io)
* [Choose an Open Source License](https://choosealicense.com)
* [GitHub Pages](https://pages.github.com)
* [Animate.css](https://daneden.github.io/animate.css)
* [Loaders.css](https://connoratherton.com/loaders)
* [Slick Carousel](https://kenwheeler.github.io/slick)
* [Smooth Scroll](https://github.com/cferdinandi/smooth-scroll)
* [Sticky Kit](http://leafo.net/sticky-kit)
* [JVectorMap](http://jvectormap.com)
* [Font Awesome](https://fontawesome.com)





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/othneildrew
[product-screenshot]: images/screenshot.png