# Graduation.BrainwaveSystem
University Graduation Project with majoring in information Technology - Computer Engineering at Hanoi University of Science and Technology (Vietnam), that develops a system collects, stores, monitors and analyzes brainwave data.

## Application Architecture:
This is a system with many components, the entire source code is organized into layers (or tiers, base on N-Tier Architecture) from bottom to top:
- **Graduation.BrainwaveSystem.Models _(Model Tier)_:** Almost similar to "Model" in MVC, we define entities and Context for mapping with Database here. Also we define ViewModel/DTO (Data Transfer Object) Classes here.
- **Graduation.BrainwaveSystem.Cores _(Core Technology/Core Tier)_:** Implement methods for intensive data processing such as signal spectrum transformation, normalization, filters,... Build data analysis models, apply AI with Machine Learning, Deep Learning, Neural Networks,... to make corresponding messages, predictions,... from the data.
- **Graduation.BrainwaveSystem.Services _(Service Tier)_:** Use ORM, frameworks, libraries,... necessary to perform queries to the database and related business processes such as calculations, validation,... then encapsulate it to provide Interfaces for the Controllers (APIs) to use simply.
- **Graduation.BrainwaveSystem.APIs _(API/Controller Tier)_:** Encapsulate services to provide features as HTTPS REST/RESTful APIs or Hubs, connections,... so that clients can easily communicate with server to use features and exchange data.
- **Graduation.BrainwaveSystem.Utils _(Utility/Common/Shared Tier)_:** extra tier - define shared components, extensions, utilites,... throughout the Backend (from Model Tier to API Tier) that are not closely related to the business such as custom exceptions, constants, enumerations, methods or functions,...
- **Graduation.BrainwaveSystem.Apps _(Application Tier)_:** develop client-Side Applications, can be known as the edge components of the system and connect to Server-Side to use services:
  - **Graduation.BrainwaveSystem.Apps.Device:** Develop application programing solutions so that embedded devices or corresponding devices can collect brainwave EEG data and send (publish) them in real time to the system.
  - **Graduation.BrainwaveSystem.Apps.Web _(Web App)_:**: Develop Graphical User Interface (GUI) Application for administration, monitoring, receiving information and interaction from users through web browser.
  - **Graduation.BrainwaveSystem.Apps.Mobile _(Mobile App)_:** Similar to Web App but this sub project is developed for SmartPhone with cross-platform mobile application programming uitable for popular native platforms such as Android, IOS.

## Technologies:
-----.NET 6.0, SQL Server,.... từ từ update dần....-----

## How to download, install, run anđ deploy?
-----write something.....----------

## Important Theories:
------....... Update dần.....
- RNN:
- LSTM
- DeepSleepNet:
- Prepareing Data:
.............----------------

## About:
- Version: 1.0.0

Develop By: **Nguyễn Duy Khai** - [GitHub](https://github.com/itKhaiNDdotDev) - <khai12345678t@gmail.com> - <khaind2000@outlook.com>.    
At: [School of Information and Communication Technology _**(SoICT)**_](https://soict.hust.edu.vn/) - [Hanoi University of ScientCe and Technology _**(HUST)**_](https://hust.edu.vn/).

Hanoi, VietNam - April 2023.</center

