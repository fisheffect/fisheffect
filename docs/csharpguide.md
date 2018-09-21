---
id: csharpguide
title: Smart Contract Development using C# in Windows
sidebar_label: C# Smart Contract Development
---

TODO: Upload Video
# Configuring your machine

## Required Software
The first step is to install the softwares below:
- Visual Studio Community;
  - Cross Platform Development with .NET 2.1. Update your Visual Studio if you face errors.
- Git;

## Downloading and compiling NEO Debugger Tools
We need to install the debugger along with it's compiler.
- Clone neo debugger tools repository: https://github.com/CityOfZion/neo-debugger-tools
- Open the solution in Visual Studio and compile the whole solution;
  - It is ok if errors occur, except if they happen in the compiler or in the debugger.
- Check if neon.exe exist under the compiler/bin folder and that neod.exe exist under win-form-debugger/bin.

## Installing NEO Contract Plugin
This is made through Visual Studio.
- Open Visual Studio;
  - Tools -> Extensions and Updates;
  - Online -> "neo";
  - Download and install it (you need to restart Visual Studio)

## Setting up your Contract Project
This configuration will tell Visual Studio to compile the resulting project dll into an AVM.
Visual Studio must find the compiler in order to complete this task. We solve this using the the compiler from neo-debugger tools \(neon.exe from the previous step\).
- Create a new Project
  - Scroll down and select "NEO Contract Project"
- Tweak the project file in order to use the debugger compiler;
  - Open the project folder, inside the solution folder. Edit the project `.csproj` file. Remove the AfterBuild target in the end of the file:
  ```xml
    <Target Name="AfterBuild">
      <Message Text="Start NeoContract converter, Source File: $(TargetPath)" Importance="high">
      </Message>
      <ConvertTask DataSource="$(TargetPath)" />
    </Target>
  ```
- Open your Smart Contract in Visual Studio;
  - In the project properties, add a new Post Build Event:
    ```bash
    set PATH="C:\source\repos\neo-debugger-tools\NEO-Compiler\bin\Debug";%PATH%
    neon.exe $(TargetPath)
    ```
    NOTE: The folder path must match the output from the neo-debugger-tools compiler output.
- Configure to open the debugger with the resulting `.avm`:
  - In the project properties, in debug, choose `start external program`.
    - Add the `ProjectName.avm` as a parameter;

If all the steps are configured properly, press f5 in Visual Studio to compile and run your project. The debugger will open automatically.

# Configuring Tests
It is a good practice to have your tests saved. Neo-debugger-tools support saving your tests in a separate `.json` file.
- In Visual Studio, create and add a new `.json` file called  `ProjectName.test.json`.
- In the project properties, edit your post build event to copy the test file:
  - ```BASH
  set PATH="C:\source\repos\neo-debugger-tools\NEO-Compiler\bin\Debug";%PATH%
  neon.exe $(TargetPath)
  xcopy /y $(ProjectDir)FishEffect.test.json  $(ProjectDir)$(OutDir)
  ```
- Add a sample test into your `.json` file:
  ```json
  {
    "cases": [
        {
            "name": "NEP-5_symbol",
            "method": "Main",
            "params": [
                "symbol",
                [ 0 ]
            ]
        }
      ]
    }
  ```
If all the steps are configured properly, the debugger will now show your saved test cases.

# Tips
Remember that an address is not the same as the scripthash, so it is very important that you do some proper conversion if you want to deal with address in NEO.
- In the SmartContract, use "```C#"youraddress".ToScriptHash()"```(compile time only);
- When using an address as an input in byte array format, convert it online doing the following:
  - Open https://neocompiler.io/#/ecolab.
  - In conversors, first convert the address into a ScriptHash;
  - Do a reverse-hex (Hex <-> xeH).
  - The output will be something like this:
    - ```ea2b832323fb5e69e7359bb559c1dd902da800a5```.
    - Use in the debugger like this: ```0xea2b832323fb5e69e7359bb559c1dd902da800a5```

# Credits
To Michael Herman who is responsible for a series of tutorials regarding NEO Technology with C#.
