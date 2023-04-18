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
  - **Graduation.BrainwaveSystem.Apps.Device:**
  - **Graduation.BrainwaveSystem.Apps.Web:**
  - **Graduation.BrainwaveSystem.Apps.Mobile:**
