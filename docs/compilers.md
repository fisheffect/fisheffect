---
id: compilers
title: NEO Compilers
sidebar_label: Compilers
---

# NEO compilers
One of the greatest things in NEO is the possibility of building smart-contracts using regular programming languages, like C# and Python.
This is possible through a NEO compiler, that compiles from these high level programming languages into NEO VM OpCodes.

## Neon - Official Compiler
Neon is the official NEO Compiler. Your smart contract code is compiled into MSIL\(Microsoft Intermediate Language\), and than it goes through the Neon compiler, generating an AVM as output.
If you are receiving compilation errors, it is probably because that the code from your smart contract is valid in C#, but it can't be translated into `NEO VM Opcodes`.
The solution is to In this case, review your current code and try to identify the line that is causing the problem and try to rewrite it using other constructs.

## Neo Debugger Tools - Community based
NEO Debugger tools is a "community" based tool made by Relfos. When you compiler your project using this compiler, it will generate information about your smart contract, allowing you to debug it using the debugger from the same author.

## Neo Python \(Neo Boa\)
NEO Python is possibly the largest community based project for NEO. It includes not only a compiler, but also an SDK, debugger, notification server and also node server. The repository also counts with a "single-click" local private chain deployment.
The compiler is called neo-boa, but it is mainly referred as only 'neo-python'.
