# Kassasystemet - Mille Elfver
This solution is a .NET 7.0 application that implements a cash register system. It uses the Autofac library for dependency injection and System.Windows.Extensions for additional functionality.
## Project Structure
The solution is composed of several classes, each serving a specific purpose in the system:

•	ProductServices.cs: This file contains the services related to the products in the system.

•	MainMenu.cs: This file handles the main menu of the application.

•	FileManager.cs: This file manages file operations within the application.

•	ReceiptCreation.cs: This file is responsible for creating receipts.

•	ProductCatalog.cs: This file manages the product catalog of the system.

•	ReceiptCounter.cs: This file handles the counting of receipts.

•	AutoFacRegistration.cs: This file is responsible for registering services with Autofac.

•	AdminMenu.cs: This file handles the admin menu of the application.

•	CheckoutProcess.cs: This file manages the checkout process.

•	Program.cs: This is the entry point of the application.

## Dependencies
The project uses the following NuGet packages:
•	Autofac version 7.1.0: An addictive Inversion of Control container for .NET.
•	System.Windows.Extensions version 7.0.0: Provides additional functionality related to Windows development.
## Resources
The project includes a "Kvittoljud" folder which contains a "KACHING.wav" file. This file is always copied to the output directory.
## How to Run
Open the solution in Visual Studio, build the solution, and then run the application. The main menu will guide you through the available options.
